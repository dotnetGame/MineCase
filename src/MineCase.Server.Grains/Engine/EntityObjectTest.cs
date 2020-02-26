using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Util.Math;
using Orleans;

namespace MineCase.Server.Engine
{
    public class EntityObjectTest : BigWorldEntity
    {
        public EntityObjectTest(IGrainFactory grainFactory)
            : base(grainFactory)
        {
        }
    }
}
