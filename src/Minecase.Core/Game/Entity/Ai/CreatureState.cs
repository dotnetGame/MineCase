using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase.Server.World.EntitySpawner
{
    public enum CreatureState : int
    {
        Stop,
        Walk,
        Look,
        Follow,
        EatingGrass,
        BurnedBySunshine,
        Burned,
        Escaping,
        Attacking,
        Explosion
    }
}
