using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase.Mod.common.eventhandler
{
    public class ListenerList
    {
        private static List<ListenerList> allLists = new List<ListenerList>();
        private static int maxSize = 0;

        private ListenerList _parent;
        private ListenerListInst[] _lists;

        public ListenerList()
        {
            _lists = new ListenerListInst[0];
        }

        public ListenerList(ListenerList parent):this()
        {
            _parent = parent;
            ResizeLists(maxSize);
        }

        public static void Resize(int max)
        {
            if (max <= maxSize)
            {
                return;
            }
            foreach (ListenerList list in allLists)
            {
                list.ResizeLists(max);
            }
            maxSize = max;
        }

        public void ResizeLists(int max)
        {
            if (_parent != null)
            {
                _parent.ResizeLists(max);
            }

            if (_lists.Length >= max)
            {
                return;
            }

            ListenerListInst[] newList = new ListenerListInst[max];
            int x = 0;
            for (; x < _lists.Length; x++)
            {
                newList[x] = _lists[x];
            }
            for (; x < max; x++)
            {
                if (_parent != null)
                {
                    newList[x] = new ListenerListInst(_parent.GetInstance(x));
                }
                else
                {
                    newList[x] = new ListenerListInst();
                }
            }
            _lists = newList;
        }

        public static void ClearBusID(int id)
        {
            foreach (ListenerList list in allLists)
            {
                list._lists[id].Dispose();
            }
        }

        protected ListenerListInst GetInstance(int id)
        {
            return _lists[id];
        }

        public IEventListener[] GetListeners(int id)
        {
            return _lists[id].GetListeners();
        }

        public void Register(int id, EventPriority priority, IEventListener listener)
        {
            _lists[id].Register(priority, listener);
        }

        public void Unregister(int id, IEventListener listener)
        {
            _lists[id].Unregister(listener);
        }

        public static void UnregisterAll(int id, IEventListener listener)
        {
            foreach (ListenerList list in allLists)
            {
                list.Unregister(id, listener);
            }
        }

        public class ListenerListInst
        {
            private bool _rebuild = true;
            private IEventListener[] _listeners;
            private List<List<IEventListener>> _priorities;
            private ListenerListInst _parent;
            private List<ListenerListInst> _children;


            public ListenerListInst()
            {
                int count = System.Enum.GetValues(typeof(EventPriority.Values)).Length;
                _priorities = new List<List<IEventListener>>(count);

                for (int x = 0; x < count; x++)
                {
                    _priorities.Add(new List<IEventListener>());
                }
            }

            public void Dispose()
            {
                foreach (List<IEventListener> listeners in _priorities)
                {
                    listeners.Clear();
                }
                _priorities.Clear();
                _parent = null;
                _listeners = null;
                if (_children != null)
                    _children.Clear();
            }

            public ListenerListInst(ListenerListInst parent) : this()
            {
                _parent = parent;
                _parent.AddChild(this);
            }

            /**
             * Returns a ArrayList containing all listeners for this event,
             * and all parent events for the specified priority.
             *
             * The list is returned with the listeners for the children events first.
             *
             * @param priority The Priority to get
             * @return ArrayList containing listeners
             */
            public List<IEventListener> GetListeners(EventPriority priority)
            {
                List<IEventListener> ret = new List<IEventListener>(_priorities.Get(priority.ordinal()));
                if (_parent != null)
                {
                    ret.AddAll(_parent.GetListeners(priority));
                }
                return ret;
            }

            /**
             * Returns a full list of all listeners for all priority levels.
             * Including all parent listeners.
             *
             * List is returned in proper priority order.
             *
             * Automatically rebuilds the internal Array cache if its information is out of date.
             *
             * @return Array containing listeners
             */
            public IEventListener[] GetListeners()
            {
                if (ShouldRebuild()) BuildCache();
                return _listeners;
            }

            protected bool ShouldRebuild()
            {
                return _rebuild;// || (parent != null && parent.shouldRebuild());
            }

            protected void ForceRebuild()
            {
                _rebuild = true;
                if (_children != null)
                {
                    foreach (ListenerListInst child in _children)
                        child.ForceRebuild();
                }
            }

            private void AddChild(ListenerListInst child)
            {
                if (_children == null)
                    _children = new List<ListenerListInst>();
                _children.Add(child);
            }

            /**
             * Rebuild the local Array of listeners, returns early if there is no work to do.
             */
            private void BuildCache()
            {
                if (_parent != null && _parent.ShouldRebuild())
                {
                    _parent.BuildCache();
                }

                List<IEventListener> ret = new List<IEventListener>();
                foreach (EventPriority value in System.Enum.GetValues(typeof(EventPriority.Values)))
                {
                    List<IEventListener> listeners = GetListeners(value);
                    if (listeners.Count > 0)
                    {
                        ret.Add(value); //Add the priority to notify the event of it's current phase.
                        ret.AddAll(listeners);
                    }
                }
                _listeners = ret.toArray(new IEventListener[ret.size()]);
                _rebuild = false;
            }

            public void Register(EventPriority priority, IEventListener listener)
            {
                _priorities.Get(priority.ordinal()).add(listener);
                ForceRebuild();
            }

            public void Unregister(IEventListener listener)
            {
                foreach (List<IEventListener> list in _priorities)
                {
                    if (list.Remove(listener))
                    {
                        this.ForceRebuild();
                    }
                }
            }
        }
    }
}
