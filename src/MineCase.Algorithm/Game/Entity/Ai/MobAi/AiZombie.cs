using System;
using System.Collections.Generic;
using System.Text;
using MineCase.Algorithm.Game.Entity.Ai.MobAi;
using MineCase.Server.World.EntitySpawner.Ai.Action;

namespace MineCase.Server.World.EntitySpawner.Ai.MobAi
{
    public class AiZombie : AiMonster
    {
        public AiZombie(Func<CreatureState> getter, Action<CreatureState> setter)
            : base(getter, setter)
        {
        }
    }
}
