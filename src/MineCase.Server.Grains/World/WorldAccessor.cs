using System;
using System.Collections.Generic;
using System.Text;
using Orleans;

namespace MineCase.Server.World
{
    public class WorldAccessor
    {
        private IGrainFactory _grainFactory;

        public WorldAccessor(IGrainFactory grainFactory)
        {
            _grainFactory = grainFactory;
        }
    }
}
