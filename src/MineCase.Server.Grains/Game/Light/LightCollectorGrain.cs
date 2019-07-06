using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Orleans;

namespace MineCase.Server.Game.Light
{
    public abstract class LightCollectorGrain : Grain, ILightCollector
    {
    }
}
