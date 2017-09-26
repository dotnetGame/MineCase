using System;
using System.Collections.Generic;
using System.Text;
using MineCase.Server.Game.Entities;
using Orleans;

namespace MineCase.Server.World.EntitySpawner.Ai.Action
{
    public abstract class CreatureAiAction
    {
        public CreatureState State { get; set; }

        public CreatureAiAction(CreatureState state)
        {
            State = state;
        }

        public abstract void Action(IGrainFactory grainFactory, ICreature creature, IWorld world);
    }
}
