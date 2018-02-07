using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase.Mod.common.eventhandler
{
    public class EventPriority : IEventListener
    {
        /*Priority of event listeners, listeners will be sorted with respect to this priority level.
         *
         * Note:
         *   Due to using a ArrayList in the ListenerList,
         *   these need to stay in a contiguous index starting at 0. {Default ordinal}
         */
        public enum Priority : uint
        {
            HIGHEST,
            HIGH,
            NORMAL,
            LOW,
            LOWEST
        }

        public Priority Values { get; set; }

        public void Invoke(Event evnt)
        {
            evnt.SetPhase(this);
        }
    }
}
