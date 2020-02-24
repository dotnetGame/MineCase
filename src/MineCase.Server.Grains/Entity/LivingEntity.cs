using System;
using System.Collections.Generic;
using System.Text;
using Orleans;

namespace MineCase.Server.Entity
{
    public abstract class LivingEntity : Entity
    {
        public LivingEntity(IGrainFactory grainFactory)
            : base(grainFactory)
        {
        }
    }
}
