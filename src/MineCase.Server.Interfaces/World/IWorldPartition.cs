using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Core.World;
using MineCase.World;
using Orleans;

namespace MineCase.Server.Interfaces.World
{
    public interface IWorldPartition : IGrainWithStringKey
    {
        Task OnTick();

        Task<ChunkColumn> GetState(ChunkWorldPos pos);
    }
}
