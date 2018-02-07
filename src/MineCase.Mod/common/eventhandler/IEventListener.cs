using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase.Mod.common.eventhandler
{
    public interface IEventListener
    {
        void Invoke(Event evnt);
    }
}
