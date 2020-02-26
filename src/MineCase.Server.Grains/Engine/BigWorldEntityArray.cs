using System;
using System.Collections.Generic;
using System.Text;
using Orleans;

namespace MineCase.Server.Engine
{
    public class BigWorldEntityArray
    {
        private IGrainFactory _grainFactory;

        public List<BigWorldEntity> Entities { get; set; }

        public BigWorldEntityArray(IGrainFactory grainFactory)
        {
            _grainFactory = grainFactory;
        }
    }
}
