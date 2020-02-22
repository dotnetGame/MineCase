using System;
using System.Collections.Generic;
using System.Text;
using Orleans;

namespace MineCase.Server.World
{
    public class WorldAccessor
    {
        private IGrainFactory _grainFactory;
        private WorldPartitionData _partition;

        public WorldAccessor(IGrainFactory grainFactory, WorldPartitionData partition)
        {
            _grainFactory = grainFactory;
            _partition = partition;
        }
    }
}
