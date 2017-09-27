using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using MineCase.Server.Game.Entities;
using MineCase.World;
using Orleans;

namespace MineCase.Server.World.EntitySpawner.Ai.Action
{
    internal class CreatureAiActionLookAtPlayer : CreatureAiAction
    {
        public CreatureAiActionLookAtPlayer()
            : base(CreatureState.Look)
        {
        }

        public override void Action(IEntity creature)
        {
            /*
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
                    (var yaw, var pitch) = VectorToYawAndPitch(entityPos, playerPosition);
                    CreatureTask task = await creature.GetCreatureTask();
                    task.Pitch = pitch;
                    task.Yaw = yaw;
                    break;
                }
            }
            */
        }

        public static (byte, byte) VectorToYawAndPitch(Vector3 from, Vector3 to)
        {
            Vector3 v = to - from;
            v = Vector3.Normalize(v);

            double tmpYaw = -Math.Atan2(v.X, v.Z) / Math.PI * 180;
            if (tmpYaw < 0)
                tmpYaw = 360 + tmpYaw;
            double tmppitch = -Math.Asin(v.Y) / Math.PI * 180;

            byte yaw = (byte)(tmpYaw * 255 / 360);
            byte pitch = (byte)(tmppitch * 255 / 360);
            return (yaw, pitch);
        }
    }
}
