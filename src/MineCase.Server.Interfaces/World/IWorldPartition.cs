using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.World;
using MineCase.World.Chunk;
using Orleans;

namespace MineCase.Server.World
{
    public interface IWorldPartition : IAddressByPartition
    {
        Task OnTick();

        Task<ChunkColumn> GetState(ChunkWorldPos pos);
    }
}
