using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Block;
using MineCase.World;
using Orleans;

namespace MineCase.Server.World.Decoration.Mine
{
    public interface IMinableGenerator : IGrainWithIntegerKey
    {
        Task Generate(IWorld world, ChunkWorldPos chunkWorldPos, BlockState blockState, int count, int size, int minHeight, int maxHeight);
    }
}
