using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.World.Chunk;

namespace MineCase.Server.World.Chunk
{
    public class Chunk : IChunk
    {
        private ChunkColumn _chunkColumn;

        public Task<ChunkColumn> GetChunkColumn()
        {
            return Task.FromResult(_chunkColumn);
        }
    }
}
