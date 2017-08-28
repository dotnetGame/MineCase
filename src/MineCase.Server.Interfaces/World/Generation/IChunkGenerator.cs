using System;
using System.Threading.Tasks;
using Orleans;

namespace MineCase.Server.World.Generation
{
    public interface IChunkGenerator
    {
        Task<ChunkColumn> Generate(int x, int z, GeneratorSettings settings);

        Task GenerateChunk(ChunkColumn chunk, int x, int z, GeneratorSettings settings);

        Task PopulateChunk(ChunkColumn chunk, int x, int z, GeneratorSettings settings);
    }
}
