using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using MineCase.Server.Game.Entities;
using MineCase.World;
using Orleans;

namespace MineCase.Server.World.EntitySpawner.Ai.Action
{
    internal class CreatureAiActionLookAtPlayer : ICreatureAiAction
    {
        public async void Action(IGrainFactory grainFactory, ICreature creature, IWorld world)
        {
            // 通知周围creature entity看着玩家
            Vector3 entityPos = await creature.GetPosition();
            ChunkWorldPos pos = new EntityWorldPos(entityPos.X, entityPos.Y, entityPos.Z).ToChunkWorldPos();
            var finder = grainFactory.GetGrain<IEntityFinder>(world.MakeEntityFinderKey(pos.X, pos.Z));
            var list = await finder.CollisionPlayer(creature);

            // TODO 多位玩家的话只看一位
            foreach (IPlayer each in list)
            {
                Vector3 playerPosition = await each.GetPosition();

                // 三格内玩家
                if (Vector3.Distance(playerPosition, entityPos) < 3)
                {
                    await creature.Look(playerPosition);
                    break;
                }
            }
        }
    }
}
