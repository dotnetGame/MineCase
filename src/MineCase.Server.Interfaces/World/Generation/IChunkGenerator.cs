using System;
using System.Threading.Tasks;
using Orleans;

namespace MineCase.Server.World.Generation
{
    public interface IChunkGenerator
    {
        Task<ChunkColumnStorage> Generate(int x, int z, GeneratorSettings settings);
    }
}
