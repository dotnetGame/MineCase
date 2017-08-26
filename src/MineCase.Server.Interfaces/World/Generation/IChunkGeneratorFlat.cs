using System;
using System.Threading.Tasks;
using Orleans;

namespace MineCase.Server.World.Generation
{
    public interface IChunkGeneratorFlat : IGrainWithIntegerKey, IChunkGenerator
    {
    }
}
