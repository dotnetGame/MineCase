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
        }
    }
}
