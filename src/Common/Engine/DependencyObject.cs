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
            _valueStorage.CurrentValueChanged += ValueStorage_CurrentValueChanged;
        }

        private void LoadState()
        {
            _components = new Dictionary<string, IComponentIntern>();
            _indexes = new Dictionary<IComponentIntern, int>();
            _messageHandlers = new MultiValueDictionary<Type, IComponentIntern>();
        }

#if ECS_SERVER
        public override async Task OnActivateAsync()
        {
            LoadState();
            await InitializeComponents();
        }

        protected virtual Task InitializeComponents()
        {
            return Task.CompletedTask;
        }
#else
        /// <inheritdoc/>
        protected override void Awake()
        {
            base.Awake();
            LoadState();
            InitializeComponents();
        }

        /// <summary>
        /// 初始化组件
        /// </summary>
        protected virtual void InitializeComponents()
        {
        }
#endif

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
        public
#if ECS_SERVER
        async Task
#else
        void
#endif
            SetComponent(Component component)
        {
            var name = component.Name;
            if (_components.TryGetValue(name, out var old))
            {
                if (old == component) return;
                Unsubscribe(old);
#if ECS_SERVER
                await
#endif
                old.Detach();
                _indexes.Remove(old);
                _components.Remove(name);
            }

            _components.Add(name, component);
            _indexes.Add(component, _index++);
#if ECS_SERVER
            await
#endif
            ((IComponentIntern)component).Attach(this, ServiceProvider);
            Subscribe(component);
        }

        /// <summary>
        /// 清除组件
        /// </summary>
        /// <typeparam name="T">组件类型</typeparam>
        public
#if ECS_SERVER
        async Task
#else
        void
#endif
            ClearComponent<T>()
            where T : Component
        {
            var components = _components.Where(o => o.Value is T);
            foreach (var component in components)
            {
                Unsubscribe(component.Value);
#if ECS_SERVER
                await
#endif
                component.Value.Detach();
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
        public
#if ECS_SERVER
        Task
#else
        void
#endif
            SetCurrentValue<T>(DependencyProperty<T> property, T value)
        {
            IEffectiveValue<T> eValue;
            if (_valueStorage.TryGetCurrentEffectiveValue(property, out eValue) && eValue.CanSetValue)
            {
#if ECS_SERVER
                return
#endif
                eValue.SetValue(value);
            }
            else
            {
#if ECS_SERVER
                return
#endif
                this.SetLocalValue(property, value);
            }
        }

        private static readonly MethodInfo _raisePropertyChangedHelper = typeof(DependencyObject).GetRuntimeMethods().Single(o => o.Name == nameof(RaisePropertyChangedHelper));

        private
#if ECS_SERVER
        Task
#else
        void
#endif
            ValueStorage_CurrentValueChanged(object sender, CurrentValueChangedEventArgs e)
        {
#if ECS_SERVER
            return (Task)_raisePropertyChangedHelper.MakeGenericMethod(e.Property.PropertyType).Invoke(this, new object[] { e.Property, e });
#else
            _raisePropertyChangedHelper.MakeGenericMethod(e.Property.PropertyType).Invoke(this, new object[] { e.Property, e });
#endif
        }

        /// <summary>
        /// 注册属性变更处理器
        /// </summary>
        /// <typeparam name="T">值类型</typeparam>
        /// <param name="property">依赖属性</param>
        /// <param name="handler">处理器</param>
        public void RegisterPropertyChangedHandler<T>(
            DependencyProperty<T> property,
#if ECS_SERVER
            AsyncEventHandler<PropertyChangedEventArgs<T>>
#else
            EventHandler<PropertyChangedEventArgs<T>>
#endif
            handler)
        {
            _propertyChangedHandlers.AddOrUpdate(property, handler, (k, old) => Delegate.Combine(old, handler));
        }

        /// <summary>
        /// 删除属性变更处理器
        /// </summary>
        /// <typeparam name="T">值类型</typeparam>
        /// <param name="property">依赖属性</param>
        /// <param name="handler">处理器</param>
        public void RemovePropertyChangedHandler<T>(
            DependencyProperty<T> property,
#if ECS_SERVER
            AsyncEventHandler<PropertyChangedEventArgs<T>>
#else
            EventHandler<PropertyChangedEventArgs<T>>
#endif
            handler)
        {
            Delegate d = null;
            _propertyChangedHandlers.TryRemove(property, out d);
            if (d != (Delegate)handler)
                _propertyChangedHandlers.AddOrUpdate(property, k => Delegate.Remove(d, handler), (k, old) => Delegate.Combine(old, Delegate.Remove(d, handler)));
        }

        /// <summary>
        /// 注册属性变更处理器
        /// </summary>
        /// <param name="property">依赖属性</param>
        /// <param name="handler">处理器</param>
        public void RegisterPropertyChangedHandler(
            DependencyProperty property,
#if ECS_SERVER
            AsyncEventHandler<PropertyChangedEventArgs>
#else
            EventHandler<PropertyChangedEventArgs>
#endif
            handler)
        {
            _propertyChangedHandlersGen.AddOrUpdate(property, handler, (k, old) => Delegate.Combine(old, handler));
        }

        /// <summary>
        /// 删除属性变更处理器
        /// </summary>
        /// <param name="property">依赖属性</param>
        /// <param name="handler">处理器</param>
        public void RemovePropertyChangedHandler(
            DependencyProperty property,
#if ECS_SERVER
            AsyncEventHandler<PropertyChangedEventArgs>
#else
            EventHandler<PropertyChangedEventArgs>
#endif
            handler)
        {
            Delegate d = null;
            _propertyChangedHandlersGen.TryRemove(property, out d);
            if (d != (Delegate)handler)
                _propertyChangedHandlersGen.AddOrUpdate(property, k => Delegate.Remove(d, handler), (k, old) => Delegate.Combine(old, Delegate.Remove(d, handler)));
        }

        /// <summary>
        /// 注册任意属性变更处理器
        /// </summary>
        /// <param name="handler">处理器</param>
        public void RegisterAnyPropertyChangedHandler(
#if ECS_SERVER
            AsyncEventHandler<PropertyChangedEventArgs>
#else
            EventHandler<PropertyChangedEventArgs>
#endif
            handler)
        {
            _anyPropertyChangedHandler = Delegate.Combine(_anyPropertyChangedHandler, handler);
        }

        /// <summary>
        /// 删除任意属性变更处理器
        /// </summary>
        /// <param name="handler">处理器</param>
        public void RemoveAnyPropertyChangedHandler(
#if ECS_SERVER
            AsyncEventHandler<PropertyChangedEventArgs>
#else
            EventHandler<PropertyChangedEventArgs>
#endif
            handler)
        {
            _anyPropertyChangedHandler = Delegate.Remove(_anyPropertyChangedHandler, handler);
        }

        internal
#if ECS_SERVER
        async Task
#else
        void
#endif
            RaisePropertyChangedHelper<T>(DependencyProperty<T> property, CurrentValueChangedEventArgs e)
        {
            var oldValue = e.HasOldValue ? (T)e.OldValue : GetDefaultValue(property);
            var newValue = e.HasNewValue ? (T)e.NewValue : GetDefaultValue(property);

            if (e.HasOldValue && e.HasNewValue && EqualityComparer<T>.Default.Equals((T)e.OldValue, (T)e.NewValue))
                return;
            var args = new PropertyChangedEventArgs<T>(property, oldValue, newValue);

#if ECS_SERVER
            await
#endif
            property.RaisePropertyChanged(_realType, this, args);
#if ECS_SERVER
            await
#endif
            InvokeLocalPropertyChangedHandlers(args);
#if ECS_SERVER
            await
#endif
            OnDependencyPropertyChanged(args);
        }

        /// <summary>
        /// 依赖属性发生变更时
        /// </summary>
        /// <typeparam name="T">值类型</typeparam>
        /// <param name="args">参数</param>
#if ECS_SERVER
        public virtual Task OnDependencyPropertyChanged<T>(PropertyChangedEventArgs<T> args)
        {
            return Task.CompletedTask;
        }
#else
        public virtual void OnDependencyPropertyChanged<T>(PropertyChangedEventArgs<T> args)
        {
        }
#endif

#if ECS_SERVER
        private async Task InvokeLocalPropertyChangedHandlers<T>(PropertyChangedEventArgs<T> e)
        {
            Delegate d;
            if (_propertyChangedHandlers.TryGetValue(e.Property, out d))
                await ((AsyncEventHandler<PropertyChangedEventArgs<T>>)d).InvokeSerial(this, e);

            if (_propertyChangedHandlersGen.TryGetValue(e.Property, out d))
                await ((AsyncEventHandler<PropertyChangedEventArgs>)d).InvokeSerial(this, e);
            await ((AsyncEventHandler<PropertyChangedEventArgs>)_anyPropertyChangedHandler).InvokeSerial(this, e);
        }
#else
        private void InvokeLocalPropertyChangedHandlers<T>(PropertyChangedEventArgs<T> e)
        {
            Delegate d;
            if (_propertyChangedHandlers.TryGetValue(e.Property, out d))
                ((EventHandler<PropertyChangedEventArgs<T>>)d).InvokeSerial(this, e);

            if (_propertyChangedHandlersGen.TryGetValue(e.Property, out d))
                ((EventHandler<PropertyChangedEventArgs>)d).InvokeSerial(this, e);
            ((EventHandler<PropertyChangedEventArgs>)_anyPropertyChangedHandler).InvokeSerial(this, e);
        }
#endif

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
                    return new AskResult<TResponse> { Succeeded = true, Response = response };
                }
            }

            return AskResult<TResponse>.Failed;
        }

#if ECS_SERVER
        public virtual void Destroy()
        {
            DeactivateOnIdle();
        }
#endif
    }
}
