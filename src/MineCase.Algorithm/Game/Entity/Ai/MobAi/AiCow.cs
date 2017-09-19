using System;
using System.Collections.Generic;
using System.Text;
using MineCase.Server.World.EntitySpawner.Ai.Action;

namespace MineCase.Server.World.EntitySpawner.Ai.MobAi
{
    internal class AiCow
    {
        public static ICreatureAiAction[,] Automation { get; set; }

        static AiCow()
        {
            int creatureStateMax = Enum.GetValues(typeof(CreatureState)).Length;
            int creatureEventMax = Enum.GetValues(typeof(CreatureEvent)).Length;
            Automation = new ICreatureAiAction[creatureStateMax, creatureEventMax];
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
