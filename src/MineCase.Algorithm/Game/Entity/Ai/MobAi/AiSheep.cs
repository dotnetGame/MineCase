using System;
using System.Collections.Generic;
using System.Text;
using MineCase.Server.World.EntitySpawner.Ai.Action;

namespace MineCase.Server.World.EntitySpawner.Ai.MobAi
{
    internal class AiSheep
    {
        public static ICreatureAiAction[,] Automation { get; set; }

        static AiSheep()
        {
            Automation = new ICreatureAiAction[3, 3];
            Automation[(int)CreatureState.Stop, (int)CreatureEvent.Attacked] = new CreatureAiActionEscape();
            Automation[(int)CreatureState.Stop, (int)CreatureEvent.PlayerApproaching] = new CreatureAiActionLookAtPlayer();
            Automation[(int)CreatureState.Stop, (int)CreatureEvent.RandomWalk] = new CreatureAiActionWalk();
            Automation[(int)CreatureState.Walk, (int)CreatureEvent.Attacked] = new CreatureAiActionEscape();
            Automation[(int)CreatureState.Escape, (int)CreatureEvent.Attacked] = new CreatureAiActionEscape();
        }

        public ICreatureAiAction GetAction(CreatureState creatureState, CreatureEvent creatureEvent)
        {
            return Automation[(int)creatureState, (int)creatureEvent];
        }
    }
}
