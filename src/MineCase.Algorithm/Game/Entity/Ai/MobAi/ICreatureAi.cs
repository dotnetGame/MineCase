using System;
using System.Collections.Generic;
using System.Text;
using MineCase.Server.World.EntitySpawner;
using MineCase.Server.World.EntitySpawner.Ai;
using MineCase.Server.World.EntitySpawner.Ai.Action;

namespace MineCase.Algorithm.Game.Entity.Ai.MobAi
{
    public interface ICreatureAi
    {
        ICreatureAiAction GetAction(CreatureState creatureState, CreatureEvent creatureEvent);
    }
}
