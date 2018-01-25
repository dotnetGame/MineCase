using MineCase.Mod.common.eventhandler;
using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase.Mod.common.eventhandler
{
    public class EventBus
    {
        private static int maxID = 0;

        private Dictionary<Object, List<IEventListener>> _listeners = new Dictionary<Object, List<IEventListener>>();
        private Dictionary<Object, ModContainer> _listenerOwners;
        private readonly int _busID = maxID++;
        private IEventExceptionHandler _exceptionHandler;

        public EventBus()
        {
            ListenerList.Resize(_busID + 1);
            _exceptionHandler = null;
        }

        public EventBus(IEventExceptionHandler handler)
        {
            if (handler == null)
                throw new ArgumentNullException();
            _exceptionHandler = handler;
        }

        /*
        public void Register(Object target)
        {
            if (_listeners.ContainsKey(target))
            {
                return;
            }

            ModContainer activeModContainer = Loader.instance().activeModContainer();
            if (activeModContainer == null)
            {
                FMLLog.log.error("Unable to determine registrant mod for {}. This is a critical error and should be impossible", target, new Throwable());
                activeModContainer = Loader.instance().getMinecraftModContainer();
            }
            listenerOwners.put(target, activeModContainer);
            bool isStatic = target.getClass() == Class.class;
            @SuppressWarnings("unchecked")
            Set<? extends Class<?>> supers = isStatic ? Sets.newHashSet((Class <?>)target) : TypeToken.of(target.getClass()).getTypes().rawTypes();
            for (Method method : (isStatic? (Class<?>) target : target.getClass()).getMethods())
            {
                if (isStatic && !Modifier.isStatic(method.getModifiers()))
                    continue;
                else if (!isStatic && Modifier.isStatic(method.getModifiers()))
                    continue;

                for (Class<?> cls : supers)
                {
                    try
                    {
                        Method real = cls.getDeclaredMethod(method.getName(), method.getParameterTypes());
                        if (real.isAnnotationPresent(SubscribeEvent.class))
                        {
                            Class<?>[] parameterTypes = method.getParameterTypes();
                            if (parameterTypes.length != 1)
                            {
                                throw new IllegalArgumentException(
                                    "Method " + method + " has @SubscribeEvent annotation, but requires " + parameterTypes.length +
                                    " arguments.  Event handler methods must require a single argument."
                                );
    }

    Class<?> eventType = parameterTypes[0];

                            if (!Event.class.isAssignableFrom(eventType))
                            {
                                throw new IllegalArgumentException("Method " + method + " has @SubscribeEvent annotation, but takes a argument that is not an Event " + eventType);
                            }

                            register(eventType, target, real, activeModContainer);
                            break;
                        }
                    }
                    catch (NoSuchMethodException e)
                    {
                        ; // Eat the error, this is not unexpected
                    }
                }
            }
        }

        private void Register(Class<?> eventType, Object target, Method method, final ModContainer owner)
        {
            try
            {
                Constructor <?> ctr = eventType.getConstructor();
                ctr.setAccessible(true);
                Event event = (Event)ctr.newInstance();
                final ASMEventHandler asm = new ASMEventHandler(target, method, owner, IGenericEvent.class.isAssignableFrom(eventType));

                    IEventListener listener = asm;
                    if (IContextSetter.class.isAssignableFrom(eventType))
                    {
                        listener = new IEventListener()
        {
            @Override
                            public void invoke(Event event)
            {
                ModContainer old = Loader.instance().activeModContainer();
                Loader.instance().setActiveModContainer(owner);
                ((IContextSetter)event).setModContainer(owner);
            asm.invoke(event);
            Loader.instance().setActiveModContainer(old);
        }
                        };
                    }

                    event.getListenerList().register(busID, asm.getPriority(), listener);

        ArrayList<IEventListener> others = listeners.computeIfAbsent(target, k -> new ArrayList<>());
                    others.add(listener);
                }
                catch (Exception e)
                {
                    FMLLog.log.error("Error registering event handler: {} {} {}", owner, eventType, method, e);
                }
            }
    */

        public void Unregister(Object obj)
        {
            _listeners.Remove(obj);
            var list = _listeners.Values;
            if (list == null)
                return;
            foreach (IEventListener listener in list)
            {
                ListenerList.UnregisterAll(_busID, listener);
            }
        }

        public bool Post(Event evnt)
        {
            IEventListener[] listeners = evnt.GetListenerList().GetListeners(_busID);
            int index = 0;
            try
            {
                for (; index < listeners.Length; index++)
                {
                    listeners[index].Invoke(evnt);
                }
            }
            catch (Exception e)
            {
                _exceptionHandler.handleException(this, evnt, listeners, index, e);
            }
            return (evnt.IsCancelable() ? evnt.IsCanceled() : false);
        }

        public void HandleException(EventBus bus, Event evnt, IEventListener[] listeners, int index, Exception throwable)
        {
            // FMLLog.log.error("Exception caught during firing event {}:", event, throwable);
            // FMLLog.log.error("Index: {} Listeners:", index);
            for (int x = 0; x < listeners.Length; x++)
            {
                // FMLLog.log.error("{}: {}", x, listeners[x]);
            }
        }
    }
}
