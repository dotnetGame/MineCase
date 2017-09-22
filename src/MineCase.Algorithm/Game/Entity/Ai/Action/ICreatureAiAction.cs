using System;
using System.Collections.Generic;
using System.Text;
using MineCase.Server.Game.Entities;
using Orleans;

namespace MineCase.Server.World.EntitySpawner.Ai.Action
{
    public interface ICreatureAiAction
    {
        void Action(IGrainFactory grainFactory, ICreature creature, IWorld world);
    }
}
