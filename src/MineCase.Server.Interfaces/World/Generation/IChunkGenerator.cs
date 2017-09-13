using System;
using System.Threading.Tasks;
using MineCase.World;
using Orleans;

namespace MineCase.Server.World.Generation
{
    public interface IChunkGenerator
    {
        Task<ChunkColumnCompactStorage> Generate(IWorld world, int x, int z, GeneratorSettings settings);
    }
}
