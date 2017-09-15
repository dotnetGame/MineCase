using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using MineCase.Engine.Data;
using Orleans;

namespace MineCase.Engine
{
    public abstract class DependencyObject : Grain
    {
        private Dictionary<Type, Component> _components;

        public DependencyObject()
        {
            _realType = this.GetType();
            _valueStorage.CurrentValueChanged += ValueStorage_CurrentValueChanged;
        }

        public override Task OnActivateAsync()
        {
            _components = new Dictionary<Type, Component>();
            _messageHandlers = new MultiValueDictionary<Type, Component>();
            return base.OnActivateAsync();
        }

        public T GetComponent<T>()
            where T : Component
        {
            if (_components.TryGetValue(typeof(T), out var component))
                return (T)component;
            return null;
        }

        public async Task SetComponent(Component component)
        {
            var type = component.GetType();
            if (_components.TryGetValue(type, out var old))
            {
                if (old == component) return;
                await old.Detach();
                _components.Remove(type);
            }

            _components.Add(type, component);
            await component.Attach(this);
        }

        public async Task ClearComponent<T>()
            where T : Component
        {
            var type = typeof(T);
            if (_components.TryGetValue(type, out var old))
            {
                await old.Detach();
                _components.Remove(type);
            }
        }

        internal readonly Type _realType;

        private readonly DependencyValueStorage _valueStorage = new DependencyValueStorage();

        internal IDependencyValueStorage ValueStorage => _valueStorage;

        private readonly ConcurrentDictionary<DependencyProperty, Delegate> _propertyChangedHandlers = new ConcurrentDictionary<DependencyProperty, Delegate>();
        private readonly ConcurrentDictionary<DependencyProperty, Delegate> _propertyChangedHandlersGen = new ConcurrentDictionary<DependencyProperty, Delegate>();
        private Delegate _anyPropertyChangedHandler;

        public T GetValue<T>(DependencyProperty<T> property)
        {
            T value;
            if (!(_valueStorage.TryGetCurrentValue(property, out value) ||
                property.TryGetNonDefaultValue(this, _realType, out value)))
                return GetDefaultValue(property);
            return value;
        }

        public Task SetCurrentValue<T>(DependencyProperty<T> property, T value)
        {
            IEffectiveValue<T> eValue;
            if (_valueStorage.TryGetCurrentEffectiveValue(property, out eValue) && eValue.CanSetValue)
                return eValue.SetValue(value);
            else
                return this.SetLocalValue(property, value);
        }

        private static readonly MethodInfo _raisePropertyChangedHelper = typeof(DependencyObject).GetRuntimeMethods().Single(o => o.Name == nameof(RaisePropertyChangedHelper));

        private Task ValueStorage_CurrentValueChanged(object sender, CurrentValueChangedEventArgs e)
        {
            return (Task)_raisePropertyChangedHelper.MakeGenericMethod(e.Property.PropertyType).Invoke(this, new object[] { e.Property, e });
        }

        public void RegisterPropertyChangedHandler<T>(DependencyProperty<T> property, AsyncEventHandler<PropertyChangedEventArgs<T>> handler)
        {
            _propertyChangedHandlers.AddOrUpdate(property, handler, (k, old) => Delegate.Combine(old, handler));
        }

        public void RemovePropertyChangedHandler<T>(DependencyProperty<T> property, AsyncEventHandler<PropertyChangedEventArgs<T>> handler)
        {
            Delegate d = null;
            _propertyChangedHandlers.TryRemove(property, out d);
            if (d != (Delegate)handler)
                _propertyChangedHandlers.AddOrUpdate(property, k => Delegate.Remove(d, handler), (k, old) => Delegate.Combine(old, Delegate.Remove(d, handler)));
        }

        public void RegisterPropertyChangedHandler(DependencyProperty property, AsyncEventHandler<PropertyChangedEventArgs> handler)
        {
            _propertyChangedHandlersGen.AddOrUpdate(property, handler, (k, old) => Delegate.Combine(old, handler));
        }

        public void RemovePropertyChangedHandler(DependencyProperty property, AsyncEventHandler<PropertyChangedEventArgs> handler)
        {
            Delegate d = null;
            _propertyChangedHandlersGen.TryRemove(property, out d);
            if (d != (Delegate)handler)
                _propertyChangedHandlersGen.AddOrUpdate(property, k => Delegate.Remove(d, handler), (k, old) => Delegate.Combine(old, Delegate.Remove(d, handler)));
        }

        public void RegisterAnyPropertyChangedHandler(AsyncEventHandler<PropertyChangedEventArgs> handler)
        {
            _anyPropertyChangedHandler = Delegate.Combine(_anyPropertyChangedHandler, handler);
        }

        public void RemoveAnyPropertyChangedHandler(AsyncEventHandler<PropertyChangedEventArgs> handler)
        {
            _anyPropertyChangedHandler = Delegate.Remove(_anyPropertyChangedHandler, handler);
        }

        internal async Task RaisePropertyChangedHelper<T>(DependencyProperty<T> property, CurrentValueChangedEventArgs e)
        {
            var oldValue = e.HasOldValue ? (T)e.OldValue : GetDefaultValue(property);
            var newValue = e.HasNewValue ? (T)e.NewValue : GetDefaultValue(property);

            if (e.HasOldValue && e.HasNewValue && EqualityComparer<T>.Default.Equals((T)e.OldValue, (T)e.NewValue))
                return;
            var args = new PropertyChangedEventArgs<T>(property, oldValue, newValue);
            await property.RaisePropertyChanged(_realType, this, args);
            await InvokeLocalPropertyChangedHandlers(args);
            await OnDependencyPropertyChanged(args);
        }

        public virtual Task OnDependencyPropertyChanged<T>(PropertyChangedEventArgs<T> args)
        {
            return Task.CompletedTask;
        }

        private async Task InvokeLocalPropertyChangedHandlers<T>(PropertyChangedEventArgs<T> e)
        {
            Delegate d;
            if (_propertyChangedHandlers.TryGetValue(e.Property, out d))
                await ((AsyncEventHandler<PropertyChangedEventArgs<T>>)d)?.InvokeSerial(this, e);

            if (_propertyChangedHandlersGen.TryGetValue(e.Property, out d))
                await ((AsyncEventHandler<PropertyChangedEventArgs>)d)?.InvokeSerial(this, e);
            await ((AsyncEventHandler<PropertyChangedEventArgs>)_anyPropertyChangedHandler)?.InvokeSerial(this, e);
        }

        private T GetDefaultValue<T>(DependencyProperty<T> property)
        {
            T value;
            if (property.TryGetDefaultValue(this, _realType, out value))
                return value;
            return default(T);
        }

        private MultiValueDictionary<Type, Component> _messageHandlers;

        private IEnumerable<Type> GetComponentHandledMessageTypes(Component component)
        {
            foreach (var iface in component.GetType().GetInterfaces())
            {
                if (iface.IsConstructedGenericType)
                {
                    var genface = iface.GetGenericTypeDefinition();
                    if (genface == typeof(IHandle<>) || genface == typeof(IHandle<,>))
                    {
                        yield return iface.GetGenericArguments()[0];
                    }
                }
            }
        }

        public void Subscribe(Component component)
        {
            foreach (var type in GetComponentHandledMessageTypes(component))
                _messageHandlers.Add(type, component);
        }

        public void Unsubscribe(Component component)
        {
            foreach (var type in GetComponentHandledMessageTypes(component))
                _messageHandlers.Remove(type, component);
        }

        public Task Tell(IEntityMessage message)
        {
            throw new NotImplementedException();
        }

        public Task<TResponse> Ask<TResponse>(IEntityMessage<TResponse> message)
        {
            throw new NotImplementedException();
        }
    }
}
