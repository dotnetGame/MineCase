using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using MineCase.Engine.Data;
using Orleans;

namespace MineCase.Engine
{
    public abstract class DependencyObject : Grain, IDependencyObject
    {
        private Dictionary<string, IComponentIntern> _components;
        private Dictionary<IComponentIntern, int> _indexes;
        private int _index = 0;

        public DependencyObject()
        {
            _realType = this.GetType();
            _valueStorage.CurrentValueChanged += ValueStorage_CurrentValueChanged;
        }

        public override async Task OnActivateAsync()
        {
            _components = new Dictionary<string, IComponentIntern>();
            _indexes = new Dictionary<IComponentIntern, int>();
            _messageHandlers = new MultiValueDictionary<Type, IComponentIntern>();
            await InitializeComponents();
        }

        protected virtual Task InitializeComponents()
        {
            return Task.CompletedTask;
        }

        public T GetComponent<T>()
            where T : Component
        {
            foreach (var component in _components)
            {
                if (component.Value is T result)
                    return result;
            }

            return null;
        }

        public async Task SetComponent(Component component)
        {
            var name = component.Name;
            if (_components.TryGetValue(name, out var old))
            {
                if (old == component) return;
                Unsubscribe(old);
                await old.Detach();
                _indexes.Remove(old);
                _components.Remove(name);
            }

            _components.Add(name, component);
            _indexes.Add(component, _index++);
            await ((IComponentIntern)component).Attach(this, ServiceProvider);
            Subscribe(component);
        }

        public async Task ClearComponent<T>()
            where T : Component
        {
            var components = _components.Where(o => o.Value is T);
            foreach (var component in components)
            {
                Unsubscribe(component.Value);
                await component.Value.Detach();
                _indexes.Remove(component.Value);
                _components.Remove(component.Key);
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
                await ((AsyncEventHandler<PropertyChangedEventArgs<T>>)d).InvokeSerial(this, e);

            if (_propertyChangedHandlersGen.TryGetValue(e.Property, out d))
                await ((AsyncEventHandler<PropertyChangedEventArgs>)d).InvokeSerial(this, e);
            await ((AsyncEventHandler<PropertyChangedEventArgs>)_anyPropertyChangedHandler).InvokeSerial(this, e);
        }

        private T GetDefaultValue<T>(DependencyProperty<T> property)
        {
            T value;
            if (property.TryGetDefaultValue(this, _realType, out value))
                return value;
            return default(T);
        }

        private MultiValueDictionary<Type, IComponentIntern> _messageHandlers;
        private static readonly ConcurrentDictionary<Type, Delegate> _messageCaller = new ConcurrentDictionary<Type, Delegate>();

        private static Delegate GetOrAddMessageCaller(Type messageType)
        {
            return _messageCaller.GetOrAdd(messageType, k =>
            {
                var iface = (from i in messageType.GetInterfaces()
                             where i == typeof(IEntityMessage) ||
                             (i.IsConstructedGenericType && i.GetGenericTypeDefinition() == typeof(IEntityMessage<>))
                             select i).Single();
                var paramExp = Expression.Parameter(typeof(IComponentIntern), "c");
                if (iface.IsConstructedGenericType)
                {
                    var responseType = iface.GetGenericArguments()[0];
                    var messageParamExp = Expression.Parameter(typeof(IEntityMessage<>).MakeGenericType(responseType), "m");
                    var handlerType = typeof(IHandle<,>).MakeGenericType(messageType, responseType);
                    var handleMethod = handlerType.GetMethod("Handle");
                    return Expression.Lambda(
                        Expression.Call(
                            Expression.Convert(paramExp, handlerType),
                            handleMethod,
                            Expression.Convert(messageParamExp, messageType)),
                        paramExp,
                        messageParamExp).Compile();
                }
                else
                {
                    var messageParamExp = Expression.Parameter(typeof(IEntityMessage), "m");
                    var handlerType = typeof(IHandle<>).MakeGenericType(messageType);
                    var handleMethod = handlerType.GetMethod("Handle");
                    return Expression.Lambda(
                        Expression.Call(
                            Expression.Convert(paramExp, handlerType),
                            handleMethod,
                            Expression.Convert(messageParamExp, messageType)),
                        paramExp,
                        messageParamExp).Compile();
                }
            });
        }

        private IEnumerable<Type> GetComponentHandledMessageTypes(IComponentIntern component)
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

        private void Subscribe(IComponentIntern component)
        {
            foreach (var type in GetComponentHandledMessageTypes(component))
                _messageHandlers.Add(type, component);
        }

        private void Unsubscribe(IComponentIntern component)
        {
            foreach (var type in GetComponentHandledMessageTypes(component))
                _messageHandlers.Remove(type, component);
        }

        public Task Tell(IEntityMessage message)
        {
            return Tell(message, message.GetType());
        }

        public Task Tell<T>(T message)
            where T : IEntityMessage
        {
            return Tell(message, typeof(T));
        }

        private async Task Tell(IEntityMessage message, Type messageType)
        {
            var invoker = (Func<IComponentIntern, IEntityMessage, Task>)GetOrAddMessageCaller(messageType);
            if (_messageHandlers.TryGetValue(messageType, out var handlers))
            {
                foreach (var handler in from h in handlers
                                        orderby h.GetMessageOrder(message), _indexes[h]
                                        select h)
                    await invoker(handler, message);
            }
        }

        public async Task<TResponse> Ask<TResponse>(IEntityMessage<TResponse> message)
        {
            var response = await TryAsk(message);
            if (!response.Succeeded)
                throw new ReceiverNotFoundException();
            return response.Response;
        }

        public async Task<AskResult<TResponse>> TryAsk<TResponse>(IEntityMessage<TResponse> message)
        {
            var messageType = message.GetType();
            var invoker = (Func<IComponentIntern, IEntityMessage<TResponse>, Task<TResponse>>)GetOrAddMessageCaller(messageType);
            if (_messageHandlers.TryGetValue(messageType, out var handlers))
            {
                foreach (var handler in from h in handlers
                                        orderby h.GetMessageOrder(message), _indexes[h]
                                        select h)
                    return new AskResult<TResponse> { Succeeded = true, Response = await invoker(handler, message) };
            }

            return AskResult<TResponse>.Failed;
        }

        public virtual void Destroy()
        {
            DeactivateOnIdle();
        }
    }
}
