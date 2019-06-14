using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.World;
using Orleans;

namespace MineCase.Server.World.Decoration.Plants
{
    public interface IPlantsGenerator : IGrainWithStringKey
    {
        Task Generate(IWorld world, ChunkWorldPos pos, int countPerChunk);
    }
}
