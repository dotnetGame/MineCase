using System;
using System.Collections.Generic;
using System.Text;
using Orleans.Concurrency;

namespace MineCase.Server.Game.Light
{
    [StatelessWorker]
    public class BlockLightCollectorGrain : LightCollectorGrain, IBlockLightCollector
    {
    }
}
