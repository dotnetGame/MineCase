using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase.Server.World.EntitySpawner.Ai
{
    public enum CreatureEvent : int
    {
        RandomWalk,
        Attacked,
        PlayerApproaching,
        FollowFood,
        EatingGrass,
        BurnedBySunshine,
        Burned
    }
}
