using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using MineCase.Engine.Data;
#if ECS_SERVER
using Orleans;
#endif

namespace MineCase.Engine
{
    /// <summary>
    /// 依赖对象
    /// </summary>
    public abstract partial class DependencyObject
        :
#if ECS_SERVER
        Grain,
#else
        SmartBehaviour,
#endif
        IDependencyObject
    {
        private Dictionary<string, IComponentIntern> _components;
        private Dictionary<IComponentIntern, int> _indexes;
        private int _index = 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="DependencyObject"/> class.
        /// </summary>
        public DependencyObject()
        {
            _realType = this.GetType();
            _components = new Dictionary<string, IComponentIntern>();
            _indexes = new Dictionary<IComponentIntern, int>();
            _messageHandlers = new MultiValueDictionary<Type, IComponentIntern>();
        }

        /// <summary>
        /// 获取组件
        /// </summary>
        /// <typeparam name="T">组件类型</typeparam>
        /// <returns>组件</returns>
#if ECS_SERVER
        public
#else
        public new
#endif
            T GetComponent<T>()
            where T : Component
        {
            foreach (var component in _components)
            {
                if (component.Value is T result)
                    return result;
            }

            return null;
        }

#if !ECS_SERVER
        /// <summary>
        /// 获取 Unity 组件
        /// </summary>
        /// <typeparam name="T">组件类型</typeparam>
        /// <returns>组件</returns>
        public T GetUnityComponent<T>()
            where T : UnityEngine.Component =>
            base.GetComponent<T>();
#endif

        /// <summary>
        /// 设置组件
        /// </summary>
        /// <param name="component">组件</param>
        public void SetComponent(Component component)
        {
            var name = component.Name;
            if (_components.TryGetValue(name, out var old))
            {
                if (old == component) return;
                Unsubscribe(old);
                old.Detach();
                _indexes.Remove(old);
                _components.Remove(name);
            }

            _components.Add(name, component);
            _indexes.Add(component, _index++);
            ((IComponentIntern)component).Attach(this, ServiceProvider);
            Subscribe(component);
        }

        /// <summary>
        /// 清除组件
        /// </summary>
        /// <typeparam name="T">组件类型</typeparam>
        public void ClearComponent<T>()
            where T : Component
        {
            var components = _components.Where(o => o.Value is T);
            foreach (var component in components)
            {
                Unsubscribe(component.Value);
                component.Value.Detach();
                _indexes.Remove(component.Value);
                _components.Remove(component.Key);
            }
        }

        internal readonly Type _realType;

        private DependencyValueStorage _valueStorage;

        /// <summary>
        /// 获取值存储
        /// </summary>
        public IDependencyValueStorage ValueStorage => _valueStorage;

        private readonly Dictionary<DependencyProperty, Delegate> _propertyChangedHandlers = new Dictionary<DependencyProperty, Delegate>();
        private readonly Dictionary<DependencyProperty, Delegate> _propertyChangedHandlersGen = new Dictionary<DependencyProperty, Delegate>();
        private Delegate _anyPropertyChangedHandler;

        /// <summary>
        /// 获取值
        /// </summary>
        /// <typeparam name="T">值类型</typeparam>
        /// <param name="property">依赖属性</param>
        /// <returns>值</returns>
        public T GetValue<T>(DependencyProperty<T> property)
        {
            T value;
            if (!(_valueStorage.TryGetCurrentValue(property, out value) ||
                property.TryGetNonDefaultValue(this, _realType, out value)))
                return GetDefaultValue(property);
            return value;
        }

        /// <summary>
        /// 设置当前值
        /// </summary>
        /// <typeparam name="T">值类型</typeparam>
        /// <param name="property">依赖属性</param>
        /// <param name="value">值</param>
        public void SetCurrentValue<T>(DependencyProperty<T> property, T value)
        {
            IEffectiveValue<T> eValue;
            if (_valueStorage.TryGetCurrentEffectiveValue(property, out eValue) && eValue.CanSetValue)
            {
                eValue.SetValue(value);
            }
            else
            {
                this.SetLocalValue(property, value);
            }
        }

        private static readonly MethodInfo _raisePropertyChangedHelper = typeof(DependencyObject).GetRuntimeMethods().Single(o => o.Name == nameof(RaisePropertyChangedHelper));

        private void ValueStorage_CurrentValueChanged(object sender, CurrentValueChangedEventArgs e)
        {
            _raisePropertyChangedHelper.MakeGenericMethod(e.Property.PropertyType).Invoke(this, new object[] { e.Property, e });
        }

        /// <summary>
        /// 注册属性变更处理器
        /// </summary>
        /// <typeparam name="T">值类型</typeparam>
        /// <param name="property">依赖属性</param>
        /// <param name="handler">处理器</param>
        public void RegisterPropertyChangedHandler<T>(DependencyProperty<T> property, EventHandler<PropertyChangedEventArgs<T>> handler)
        {
            if (_propertyChangedHandlers.TryGetValue(property, out var newHandler))
                newHandler = Delegate.Combine(newHandler, handler);
            else
                newHandler = handler;
            _propertyChangedHandlers[property] = newHandler;
        }

        /// <summary>
        /// 删除属性变更处理器
        /// </summary>
        /// <typeparam name="T">值类型</typeparam>
        /// <param name="property">依赖属性</param>
        /// <param name="handler">处理器</param>
        public void RemovePropertyChangedHandler<T>(DependencyProperty<T> property, EventHandler<PropertyChangedEventArgs<T>> handler)
        {
            if (_propertyChangedHandlers.TryGetValue(property, out var newHandler))
                newHandler = Delegate.Remove(newHandler, handler);

            if (newHandler == null)
                _propertyChangedHandlers.Remove(property);
            else
                _propertyChangedHandlers[property] = newHandler;
        }

        /// <summary>
        /// 注册属性变更处理器
        /// </summary>
        /// <param name="property">依赖属性</param>
        /// <param name="handler">处理器</param>
        public void RegisterPropertyChangedHandler(DependencyProperty property, EventHandler<PropertyChangedEventArgs> handler)
        {
            if (_propertyChangedHandlersGen.TryGetValue(property, out var newHandler))
                newHandler = Delegate.Combine(newHandler, handler);
            else
                newHandler = handler;
            _propertyChangedHandlersGen[property] = newHandler;
        }

        /// <summary>
        /// 删除属性变更处理器
        /// </summary>
        /// <param name="property">依赖属性</param>
        /// <param name="handler">处理器</param>
        public void RemovePropertyChangedHandler(DependencyProperty property, EventHandler<PropertyChangedEventArgs> handler)
        {
            if (_propertyChangedHandlersGen.TryGetValue(property, out var newHandler))
                newHandler = Delegate.Remove(newHandler, handler);

            if (newHandler == null)
                _propertyChangedHandlersGen.Remove(property);
            else
                _propertyChangedHandlersGen[property] = newHandler;
        }

        /// <summary>
        /// 注册任意属性变更处理器
        /// </summary>
        /// <param name="handler">处理器</param>
        public void RegisterAnyPropertyChangedHandler(EventHandler<PropertyChangedEventArgs> handler)
        {
            _anyPropertyChangedHandler = Delegate.Combine(_anyPropertyChangedHandler, handler);
        }

        /// <summary>
        /// 删除任意属性变更处理器
        /// </summary>
        /// <param name="handler">处理器</param>
        public void RemoveAnyPropertyChangedHandler(EventHandler<PropertyChangedEventArgs> handler)
        {
            _anyPropertyChangedHandler = Delegate.Remove(_anyPropertyChangedHandler, handler);
        }

        internal void RaisePropertyChangedHelper<T>(DependencyProperty<T> property, CurrentValueChangedEventArgs e)
        {
            var oldValue = e.HasOldValue ? (T)e.OldValue : GetDefaultValue(property);
            var newValue = e.HasNewValue ? (T)e.NewValue : GetDefaultValue(property);

            if (e.HasOldValue && e.HasNewValue && EqualityComparer<T>.Default.Equals((T)e.OldValue, (T)e.NewValue))
                return;
            var args = new PropertyChangedEventArgs<T>(property, oldValue, newValue);

            property.RaisePropertyChanged(_realType, this, args);
            InvokeLocalPropertyChangedHandlers(args);
            OnDependencyPropertyChanged(args);
        }

        /// <summary>
        /// 依赖属性发生变更时
        /// </summary>
        /// <typeparam name="T">值类型</typeparam>
        /// <param name="args">参数</param>
        public virtual void OnDependencyPropertyChanged<T>(PropertyChangedEventArgs<T> args)
        {
        }

        private void InvokeLocalPropertyChangedHandlers<T>(PropertyChangedEventArgs<T> e)
        {
            Delegate d;
            if (_propertyChangedHandlers.TryGetValue(e.Property, out d))
                ((EventHandler<PropertyChangedEventArgs<T>>)d).InvokeSerial(this, e);

            if (_propertyChangedHandlersGen.TryGetValue(e.Property, out d))
                ((EventHandler<PropertyChangedEventArgs>)d).InvokeSerial(this, e);
            ((EventHandler<PropertyChangedEventArgs>)_anyPropertyChangedHandler).InvokeSerial(this, e);
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

        /// <inheritdoc />
        public
#if ECS_SERVER
        Task
#else
        void
#endif
            Tell(IEntityMessage message)
        {
#if ECS_SERVER
            return
#endif
            Tell(message, message.GetType());
        }

        /// <summary>
        /// 告知
        /// </summary>
        /// <typeparam name="T">消息类型</typeparam>
        /// <param name="message">消息</param>
        public
#if ECS_SERVER
        Task
#else
        void
#endif
            Tell<T>(T message)
            where T : IEntityMessage
        {
#if ECS_SERVER
            return
#endif
            Tell(message, typeof(T));
        }

        private
#if ECS_SERVER
        async Task
#else
        void
#endif
            Tell(IEntityMessage message, Type messageType)
        {
            var invoker =
#if ECS_SERVER
                (Func<IComponentIntern, IEntityMessage, Task>
#else
                (Action<IComponentIntern, IEntityMessage>
#endif
)GetOrAddMessageCaller(messageType);
            if (_messageHandlers.TryGetValue(messageType, out var handlers))
            {
                foreach (var handler in from h in handlers
                                        orderby h.GetMessageOrder(message), _indexes[h]
                                        select h)
                {
#if ECS_SERVER
                await
#endif
                    invoker(handler, message);
                }
            }

#if ECS_SERVER
            await ClearOperationQueue();
#endif
        }

        /// <inheritdoc />
        public
#if ECS_SERVER
        async Task<TResponse>
#else
        TResponse
#endif
            Ask<TResponse>(IEntityMessage<TResponse> message)
        {
            var response =
#if ECS_SERVER
            await
#endif
                TryAsk(message);
            if (!response.Succeeded)
                throw new ReceiverNotFoundException();
            return response.Response;
        }

        /// <inheritdoc />
        public
#if ECS_SERVER
        async Task<AskResult<TResponse>>
#else
        AskResult<TResponse>
#endif
            TryAsk<TResponse>(IEntityMessage<TResponse> message)
        {
            var messageType = message.GetType();
            var invoker =
#if ECS_SERVER
                (Func<IComponentIntern, IEntityMessage<TResponse>, Task<TResponse>>
#else
                (Func<IComponentIntern, IEntityMessage<TResponse>, TResponse>
#endif
)GetOrAddMessageCaller(messageType);
            if (_messageHandlers.TryGetValue(messageType, out var handlers))
            {
                foreach (var handler in from h in handlers
                                        orderby h.GetMessageOrder(message), _indexes[h]
                                        select h)
                {
                    var response =
#if ECS_SERVER
            await
#endif
                    invoker(handler, message);
#if ECS_SERVER
                    await ClearOperationQueue();
#endif
                    return new AskResult<TResponse> { Succeeded = true, Response = response };
                }
            }

            return AskResult<TResponse>.Failed;
        }
    }
}
