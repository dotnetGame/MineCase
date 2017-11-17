using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MineCase.Engine
{
    /// <summary>
    /// 事件聚合器
    /// </summary>
    public class EventAggregator : IEventAggregator
    {
        /// <inheritdoc/>
        public void Publish(object message, Action<Action> marshal)
        {
            if (message == null) throw new ArgumentNullException(nameof(message));
            if (marshal == null) throw new ArgumentNullException(nameof(marshal));

            marshal(() => Publish(message, message.GetType()));
        }

        private void Publish(object message, Type messageType)
        {
            var invoker = (Action<object, object>)GetOrAddMessageCaller(messageType);
            lock (_messageHandlers)
            {
                if (_messageHandlers.TryGetValue(messageType, out var handlers))
                {
                    foreach (var handlerCode in from h in handlers
                                                select h)
                    {
                        var handlerWeak = _subscribers[handlerCode];
                        if (TryGetAliveHandler(handlerWeak.weak, out var handler))
                        {
                            invoker(handler, message);
                        }
                        else
                        {
                            _deadSubscribers.Add(handlerCode);
                        }
                    }
                }

                ClearDeadSubscribers();
            }
        }

        private void ClearDeadSubscribers()
        {
            foreach (var dead in _deadSubscribers)
            {
                if (_subscribers.TryRemove(dead, out var weak))
                {
                    foreach (var type in GetComponentHandledMessageTypes(weak.type))
                        _messageHandlers.Remove(type, dead);
                }
            }

            _deadSubscribers.Clear();
        }

        private static bool TryGetAliveHandler(WeakReference weak, out object handler)
        {
            if (weak.IsAlive)
            {
                var obj = weak.Target;
                var uo = obj as UnityEngine.Object;
                if (obj != null && (uo == null || uo))
                {
                    handler = obj;
                    return true;
                }
            }

            handler = null;
            return false;
        }

        /// <inheritdoc/>
        public void Subscribe(object subscriber)
        {
            if (subscriber == null) throw new ArgumentNullException(nameof(subscriber));

            var code = subscriber.GetHashCode();
            var weak = new WeakReference(subscriber);
            var subscriberType = subscriber.GetType();
            if (_subscribers.TryAdd(code, (weak, subscriberType)))
            {
                lock (_messageHandlers)
                {
                    foreach (var type in GetComponentHandledMessageTypes(subscriberType))
                        _messageHandlers.Add(type, code);
                }
            }
        }

        /// <inheritdoc/>
        public void Unsubscribe(object subscriber)
        {
            if (subscriber == null) throw new ArgumentNullException(nameof(subscriber));

            var code = subscriber.GetHashCode();
            if (_subscribers.TryRemove(code, out var weak))
            {
                lock (_messageHandlers)
                {
                    foreach (var type in GetComponentHandledMessageTypes(weak.type))
                        _messageHandlers.Remove(type, code);
                }
            }
        }

        private readonly List<int> _deadSubscribers = new List<int>();
        private readonly ConcurrentDictionary<int, (WeakReference weak, Type type)> _subscribers = new ConcurrentDictionary<int, (WeakReference weak, Type type)>();
        private readonly MultiValueDictionary<Type, int> _messageHandlers = new MultiValueDictionary<Type, int>();
        private static readonly ConcurrentDictionary<Type, Delegate> _messageCaller = new ConcurrentDictionary<Type, Delegate>();

        private static Delegate GetOrAddMessageCaller(Type messageType)
        {
            return _messageCaller.GetOrAdd(messageType, k =>
            {
                var paramExp = Expression.Parameter(typeof(object), "c");
                var messageParamExp = Expression.Parameter(typeof(object), "m");
                var handlerType = typeof(IHandleEvent<>).MakeGenericType(messageType);
                var handleMethod = handlerType.GetMethod(nameof(IHandleEvent<object>.Handle));
                return Expression.Lambda(
                    Expression.Call(
                        Expression.Convert(paramExp, handlerType),
                        handleMethod,
                        Expression.Convert(messageParamExp, messageType)),
                    paramExp,
                    messageParamExp).Compile();
            });
        }

        private IEnumerable<Type> GetComponentHandledMessageTypes(Type componentType)
        {
            foreach (var iface in componentType.GetInterfaces())
            {
                if (iface.IsConstructedGenericType)
                {
                    var genface = iface.GetGenericTypeDefinition();
                    if (genface == typeof(IHandleEvent<>))
                    {
                        yield return iface.GetGenericArguments()[0];
                    }
                }
            }
        }
    }
}
