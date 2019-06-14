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
        Task Decorate(IWorld world, ChunkWorldPos chunkWorldPos);

        Task SpawnMob(IWorld world, ChunkWorldPos chunkWorldPos);

        Task SpawnMonster(IWorld world, ChunkWorldPos chunkWorldPos);
    }
}
