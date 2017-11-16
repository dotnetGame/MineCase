using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase
{
    public enum EntityUsage : int
    {
        Interact = 0,
        Attack = 1,
        InteractAt = 2
    }

    public enum EntityInteractHand : int
    {
        MainHand = 0,
        OffHand = 1
    }
}
