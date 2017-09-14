using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using MineCase.Server.Game;
using MineCase.Server.Game.Entities;
using MineCase.World;
using Orleans;

namespace MineCase.Server.World.EntitySpawner
{
    public class MonsterSpawner
    {
        private int _groupMaxNum;

        private MobType _mobType;

        public MonsterSpawner(MobType mobType, int groupMaxNum)
        {
            _mobType = mobType;
            _groupMaxNum = groupMaxNum;
        }

        public async void Spawn(IWorld world, IGrainFactory grainFactory, ChunkColumnStorage chunk, Random random, BlockWorldPos pos)
        {
            int num = random.Next(_groupMaxNum);
            for (int n = 0; n < num; ++n)
            {
                int x = random.Next(16);
                int z = random.Next(16);

                int height;
                for (height = 255; height >= 0; height--)
                {
                    if (chunk[x, height, z] != BlockStates.Air())
                    {
                        break;
                    }
                }

                BlockWorldPos standPos = new BlockWorldPos(pos.X + x, height + 1, pos.Z + z);
                if (CanMobStand(world, grainFactory, chunk, random, standPos.ToBlockChunkPos()))
                {
                    // 添加一个生物
                    var eid = await world.NewEntityId();
                    var entity = grainFactory.GetGrain<IPassiveMob>(world.MakeEntityKey(eid));
                    await world.AttachEntity(entity);

                    await entity.Spawn(Guid.NewGuid(), new Vector3(pos.X + x + 0.5F, height + 1, pos.Z + z + 0.5F), _mobType);
                    await entity.OnCreated();
                }
            }
        }

        public bool CanMobStand(IWorld world, IGrainFactory grainFactory, ChunkColumnStorage chunk, Random random, BlockChunkPos pos)
        {
            // TODO 以后结合boundbox判断
            BlockChunkPos downPos = new BlockChunkPos(pos.X, pos.Y - 1, pos.Z);
            if (chunk[pos.X, pos.Y - 1, pos.Z].IsLightOpacity() == 0)
            {
                if (chunk[pos.X, pos.Y, pos.Z] == BlockStates.Air() &&
                    chunk[pos.X, pos.Y + 1, pos.Z] == BlockStates.Air())
                {
                    return true;
                }
            }

            return false;
        }
    }
}
