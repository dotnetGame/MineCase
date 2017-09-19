using System;
using System.Collections.Generic;
using System.Text;
using MineCase.Server.Game.Entities;
using MineCase.Server.World;
using MineCase.Server.World.EntitySpawner.Ai.Action;
using Orleans;

namespace MineCase.Algorithm.Game.Entity.Ai.Action
{
    internal class CreatureAiActionFollow : ICreatureAiAction
    {
        private uint _itemId;

        public CreatureAiActionFollow(uint itemId)
        {
            _itemId = itemId;
        }

        public void Action(IGrainFactory grainFactory, ICreature creature, IWorld world)
        {
            throw new NotImplementedException();
        }
    }
}
