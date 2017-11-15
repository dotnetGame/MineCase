using System;
using System.Collections.Generic;
using System.Text;
using MineCase.Server.World.EntitySpawner;
using MineCase.Server.World.EntitySpawner.Ai;
using Stateless;

namespace MineCase.Algorithm.Game.Entity.Ai.MobAi
{
    public abstract class AiMonster : CreatureAi
    {
        public AiMonster(Func<CreatureState> getter, Action<CreatureState> setter)
            : base(getter, setter)
        {
        }

        protected override void Configure(StateMachine<CreatureState, CreatureEvent> stateMachine)
        {
            stateMachine.Configure(CreatureState.Stop)
                .PermitReentry(CreatureEvent.Nothing)
                .Permit(CreatureEvent.RandomWalk, CreatureState.Walk)
                .Permit(CreatureEvent.Attacked, CreatureState.Escaping)
                .Permit(CreatureEvent.PlayerApproaching, CreatureState.Look);
            stateMachine.Configure(CreatureState.Walk)
                .PermitReentry(CreatureEvent.Nothing)
                .Permit(CreatureEvent.Stop, CreatureState.Stop)
                .Permit(CreatureEvent.Attacked, CreatureState.Escaping)
                .Permit(CreatureEvent.PlayerApproaching, CreatureState.Look);
            stateMachine.Configure(CreatureState.Look)
                .PermitReentry(CreatureEvent.Nothing)
                .Permit(CreatureEvent.Stop, CreatureState.Stop)
                .Permit(CreatureEvent.Attacked, CreatureState.Escaping)
                .PermitReentry(CreatureEvent.PlayerApproaching);
            stateMachine.Configure(CreatureState.Escaping)
                .PermitReentry(CreatureEvent.Attacked);
        }
    }
}
