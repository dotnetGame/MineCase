using System;
using System.Collections.Generic;
using System.Text;
using MineCase.Server.Game.Entities;
using Orleans;

namespace MineCase.Server.World.EntitySpawner.Ai.Action
{
    internal class CreatureAiActionAttackPlayer : ICreatureAiAction
    {
        public void Action(IGrainFactory grainFactory, ICreature creature, IWorld world)
        {
            throw new NotImplementedException();
        }
    }
}
