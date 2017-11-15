using System;
using System.Collections.Generic;
using System.Text;
using MineCase.Server.World.EntitySpawner;
using MineCase.Server.World.EntitySpawner.Ai;

namespace MineCase.Algorithm.Game.Entity.Ai.MobAi
{
    public abstract class AiPassive : CreatureAi
    {
        public static CreatureState[,] Automation { get; set; }

        static AiPassive()
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

        public override CreatureState GetState(CreatureState creatureState, CreatureEvent creatureEvent)
        {
            return Automation[(int)creatureState, (int)creatureEvent];
        }
    }
}
