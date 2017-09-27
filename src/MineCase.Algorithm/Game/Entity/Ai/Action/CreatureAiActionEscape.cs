using System;
using System.Collections.Generic;
using System.Text;
using MineCase.Server.Game.Entities;
using Orleans;

namespace MineCase.Server.World.EntitySpawner.Ai.Action
{
    internal class CreatureAiActionEscape : CreatureAiAction
    {
        public CreatureAiActionEscape()
            : base(CreatureState.Escaping)
        {
        }

        public override void Action(IEntity creature)
        {
            throw new NotImplementedException();
        }
    }
}
