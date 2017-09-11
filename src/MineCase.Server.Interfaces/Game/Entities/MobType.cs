using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase.Server.Game.Entities
{
    public enum MobType : byte
    {
        Item = 1,
        XPOrb = 2,
        AreaEffectCloud = 3,
        ElderGuardian = 4,
        WitherSkeleton = 5,
        Stray = 6,
        Husk = 23,
        ZombieVillager = 27,
        SkeletonHorse = 28,
        ZombieHorse = 29,
        ArmorStand = 30,
        Donkey = 31,
        Mule = 32,
        EvocationIllager = 34,
        Vex = 35,
        VindicationIllager = 36,
        IllusionIllager = 37,
        Creeper = 50
    }
}
