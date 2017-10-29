using System;
using System.Collections.Generic;
using System.Text;
using MineCase.Algorithm.Game.Entity.Ai.MobAi;
using MineCase.Server.World.EntitySpawner.Ai.Action;

namespace MineCase.Server.World.EntitySpawner.Ai.MobAi
{
    public class AiCreeper : ICreatureAi
    {
        public static CreatureState[,] Automation { get; set; }

        static AiCreeper()
        {
            int creatureStateMax = Enum.GetValues(typeof(CreatureState)).Length;
            int creatureEventMax = Enum.GetValues(typeof(CreatureEvent)).Length;
            Automation = new CreatureState[creatureStateMax, creatureEventMax];
            Automation[(int)CreatureState.Stop, (int)CreatureEvent.Nothing] = CreatureState.Stop;
            Automation[(int)CreatureState.Stop, (int)CreatureEvent.RandomWalk] = CreatureState.Walk;
            Automation[(int)CreatureState.Stop, (int)CreatureEvent.Attacked] = CreatureState.Escaping;
            Automation[(int)CreatureState.Stop, (int)CreatureEvent.PlayerApproaching] = CreatureState.Look;

            Automation[(int)CreatureState.Walk, (int)CreatureEvent.Nothing] = CreatureState.Walk;
            Automation[(int)CreatureState.Walk, (int)CreatureEvent.Stop] = CreatureState.Stop;
            Automation[(int)CreatureState.Walk, (int)CreatureEvent.Attacked] = CreatureState.Escaping;
            Automation[(int)CreatureState.Walk, (int)CreatureEvent.PlayerApproaching] = CreatureState.Look;

            Automation[(int)CreatureState.Look, (int)CreatureEvent.Nothing] = CreatureState.Look;
            Automation[(int)CreatureState.Look, (int)CreatureEvent.Stop] = CreatureState.Stop;
            Automation[(int)CreatureState.Look, (int)CreatureEvent.Attacked] = CreatureState.Escaping;
            Automation[(int)CreatureState.Look, (int)CreatureEvent.PlayerApproaching] = CreatureState.Look;

            Automation[(int)CreatureState.Escaping, (int)CreatureEvent.Attacked] = CreatureState.Escaping;
        }

        public CreatureState GetState(CreatureState creatureState, CreatureEvent creatureEvent)
        {
            return Automation[(int)creatureState, (int)creatureEvent];
        }
    }
}
