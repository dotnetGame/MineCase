using System;
using System.Collections.Generic;
using System.Text;
using MineCase.Server.Game.Entities;
using MineCase.Server.World;
using MineCase.Server.World.EntitySpawner;
using MineCase.Server.World.EntitySpawner.Ai.Action;
using Orleans;

namespace MineCase.Algorithm.Game.Entity.Ai.Action
{
    internal class CreatureAiActionFollow : CreatureAiAction
    {
        private uint _itemId;

        public CreatureAiActionFollow(uint itemId)
            : base(CreatureState.Follow)
        {
            _itemId = itemId;
        }

        public override void Action(IGrainFactory grainFactory, ICreature creature, IWorld world)
        {
            throw new NotImplementedException();
        }
    }
}
