using System;
using System.Threading.Tasks;
using Orleans;

namespace MineCase.Server.World.Generation
{
    public interface IChunkGenerator
    {
        Task<ChunkColumnStorage> Generate(IWorld world, int x, int z, GeneratorSettings settings);
    }
}
