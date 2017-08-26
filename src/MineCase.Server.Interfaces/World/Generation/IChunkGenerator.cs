using System;
using System.Threading.Tasks;
using Orleans;

namespace MineCase.Server.World.Generation
{
    public interface IChunkGenerator
    {
        Task<ChunkColumn> generateChunk(int x, int z, long seed);
        Task populateChunk(ChunkColumn chunk, int x, int z, long seed);
    }
}
