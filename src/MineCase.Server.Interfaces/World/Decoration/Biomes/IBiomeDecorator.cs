using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.World;
using Orleans;

namespace MineCase.Server.World.Decoration.Biomes
{
    public interface IBiomeDecorator : IGrainWithIntegerKey
    {
        Task Decorate(IWorld world, ChunkWorldPos chunkWorldPos, BlockWorldPos pos);

        Task SpawnMob(IWorld world, ChunkWorldPos chunkWorldPos, BlockWorldPos pos);

        Task SpawnMonster(IWorld world, ChunkWorldPos chunkWorldPos, BlockWorldPos pos);
    }
}
