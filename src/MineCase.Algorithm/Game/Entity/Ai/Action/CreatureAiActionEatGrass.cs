using System;
using System.Collections.Generic;
using System.Text;
using MineCase.Server.Game.Entities;
using Orleans;

namespace MineCase.Server.World.EntitySpawner.Ai.Action
{
    internal class CreatureAiActionEatGrass : CreatureAiAction
    {
        public CreatureAiActionEatGrass()
            : base(CreatureState.EatingGrass)
        {
        }

        public override void Action(IEntity creature)
        {
            throw new NotImplementedException();
        }
    }
}
