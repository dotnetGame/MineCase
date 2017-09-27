using System;
using System.Collections.Generic;
using System.Text;
using MineCase.Algorithm.Game.Entity.Ai.MobAi;
using MineCase.Server.World.EntitySpawner.Ai.Action;

namespace MineCase.Server.World.EntitySpawner.Ai.MobAi
{
    public class AiChicken : ICreatureAi
    {
        public static CreatureAiAction[,] Automation { get; set; }

        static AiChicken()
        {
            int creatureStateMax = Enum.GetValues(typeof(CreatureState)).Length;
            int creatureEventMax = Enum.GetValues(typeof(CreatureEvent)).Length;
            Automation = new CreatureAiAction[creatureStateMax, creatureEventMax];
            Automation[(int)CreatureState.Stop, (int)CreatureEvent.Attacked] = new CreatureAiActionEscape();
            Automation[(int)CreatureState.Stop, (int)CreatureEvent.PlayerApproaching] = new CreatureAiActionLookAtPlayer();
            Automation[(int)CreatureState.Stop, (int)CreatureEvent.RandomWalk] = new CreatureAiActionWalk();
            Automation[(int)CreatureState.Walk, (int)CreatureEvent.Attacked] = new CreatureAiActionEscape();
            Automation[(int)CreatureState.Escaping, (int)CreatureEvent.Attacked] = new CreatureAiActionEscape();
        }

        public CreatureAiAction GetAction(CreatureState creatureState, CreatureEvent creatureEvent)
        {
            return Automation[(int)creatureState, (int)creatureEvent];
        }
    }
}
