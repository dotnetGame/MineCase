using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using MineCase.Server.Game.Entities;
using MineCase.World;
using Orleans;

namespace MineCase.Server.World.EntitySpawner.Ai.Action
{
    internal class CreatureAiActionWalk : CreatureAiAction
    {
        public CreatureAiActionWalk()
            : base(CreatureState.Walk)
        {
        }

        public override void Action(IEntity creature)
        {
            /*
            // 随机选择一个步长
            Random random = new Random();
            int stepNum = random.Next(16);
            for (int i = 0; i < 16; ++i)
            {
                // 获取生物所在坐标
                Vector3 pos = await creature.GetPosition();
                BlockWorldPos curBLockPos = new EntityWorldPos(pos.X, pos.Y, pos.Z).ToBlockWorldPos();

                // 检测前后左右哪边可以走
                for (int x = -1; x <= 1; ++x)
                {
                    for (int z = -1; z <= 1; ++z)
                    {
                        for (int y = -2; y <= 1; ++y)
                        {
                            // TODO need boundbox
                            await world.GetBlockState(grainFactory, BlockWorldPos.Add(curBLockPos, x, y, z));
                        }
                    }
                }
            }
            */
        }
    }
}
