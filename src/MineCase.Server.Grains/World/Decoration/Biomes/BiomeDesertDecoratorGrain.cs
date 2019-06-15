using System;
using System.Collections.Generic;
using System.Text;
using Orleans.Concurrency;

namespace MineCase.Server.World.Decoration.Biomes
{
    [StatelessWorker]
    public class BiomeDesertDecoratorGrain : BiomeDecoratorGrain, IBiomeDesertDecorator
    {
    }
}
