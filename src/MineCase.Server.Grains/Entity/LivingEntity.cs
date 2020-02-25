using System;
using System.Collections.Generic;
using System.Text;
using MineCase.Server.World;
using Orleans;

namespace MineCase.Server.Entity
{
    public abstract class LivingEntity : Entity
    {
        public LivingEntity(IGrainFactory grainFactory, IWorld world)
            : base(grainFactory, world)
        {
        }
    }
}
