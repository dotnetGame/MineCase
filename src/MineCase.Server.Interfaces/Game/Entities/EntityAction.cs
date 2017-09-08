using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase.Server.Game.Entities
{
    public enum ActionId : int
    {
        StartSneaking = 0,
        StopSneaking = 1,
        LeaveBed = 2,
        StartSprinting = 3,
        StopSprinting = 4,
        StartJumpWithHorse = 5,
        StopJumpWithHorse = 6,
        OpenHorseInventory = 7,
        StartFlyingWithElytra = 8
    }
}
