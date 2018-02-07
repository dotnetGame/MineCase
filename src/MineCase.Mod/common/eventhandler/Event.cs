﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase.Mod.common.eventhandler
{
    /**
 * Base Event class that all other events are derived from
 */
    public class Event
    {
        public enum Result
        {
            DENY,
            DEFAULT,
            ALLOW
        }

        private bool _isCanceled = false;
        private Result _result = Result.DEFAULT;
        private static ListenerList _listeners = new ListenerList();
        private EventPriority _phase = null;

        public Event()
        {
            Setup();
        }

        /**
         * Determine if this function is cancelable at all.
         * @return If access to setCanceled should be allowed
         *
         * Note:
         * Events with the Cancelable annotation will have this method automatically added to return true.
         */
        public bool IsCancelable()
        {
            return false;
        }

        /**
         * Determine if this event is canceled and should stop executing.
         * @return The current canceled state
         */
        public bool IsCanceled()
        {
            return _isCanceled;
        }

        /**
         * Sets the cancel state of this event. Note, not all events are cancelable, and any attempt to
         * invoke this method on an event that is not cancelable (as determined by {@link #isCancelable}
         * will result in an {@link UnsupportedOperationException}.
         *
         * The functionality of setting the canceled state is defined on a per-event bases.
         *
         * @param cancel The new canceled value
         */
        public void SetCanceled(bool cancel)
        {
            if (!IsCancelable())
            {
                throw new InvalidOperationException(
                    "Attempted to call Event#setCanceled() on a non-cancelable event of type: "
                    + GetType().Name
                );
            }
            _isCanceled = cancel;
        }

        /**
         * Determines if this event expects a significant result value.
         *
         * Note:
         * Events with the HasResult annotation will have this method automatically added to return true.
         */
        public bool HasResult()
        {
            return false;
        }

        /**
         * Returns the value set as the result of this event
         */
        public Result GetResult()
        {
            return _result;
        }

        /**
         * Sets the result value for this event, not all events can have a result set, and any attempt to
         * set a result for a event that isn't expecting it will result in a IllegalArgumentException.
         *
         * The functionality of setting the result is defined on a per-event bases.
         *
         * @param value The new result
         */
        public void SetResult(Result value)
        {
            _result = value;
        }

        /**
         * Called by the base constructor, this is used by ASM generated
         * event classes to setup various functionality such as the listener list.
         */
        protected void Setup()
        {
        }

        /**
         * Returns a ListenerList object that contains all listeners
         * that are registered to this event.
         *
         * @return Listener List
         */
        public ListenerList GetListenerList()
        {
            return _listeners;
        }

        public EventPriority GetPhase()
        {
            return _phase;
        }

        public void SetPhase(EventPriority value)
        {
            if (value == null)
                throw new ArgumentNullException();
            int prev = _phase == null ? -1 : (int)_phase.Values;

            if (prev < (uint)value.Values)
                throw new ArgumentException(
                    string.Format("Attempted to set event phase to %s when already %s",
                    value.ToString(),
                    System.Enum.GetName(typeof(EventPriority.Priority), _phase.Values)));
            _phase = value;
        }
    }
}
