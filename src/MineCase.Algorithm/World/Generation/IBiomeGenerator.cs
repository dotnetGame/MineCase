using System;
using System.Threading.Tasks;
using Orleans;

namespace MineCase.Server.World.Generation
{
    public interface IBiomeGenerator : IGrainWithIntegerKey
    {
    }
}
