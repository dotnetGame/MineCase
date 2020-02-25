using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.World.Chunk;

namespace MineCase.Server.World.Chunk
{
    public interface IChunk
    {
        Task<ChunkColumn> GetChunkColumn();
    }
}
