using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Server.Network;
using MineCase.Util.Math;
using MineCase.World;
using Orleans;

namespace MineCase.Server.Entity.Player
{
    public class PlayerEntity : LivingEntity
    {
        public SectionPos LastSectionPos { get; set; } = new SectionPos(0, 0, 0);

        public int ViewDistance { get; set; }

        public PlayerEntity(IGrainFactory grainFactory)
            : base(grainFactory)
        {
        }

        public new Task OnGameTick(object sender, GameTickArgs tickArgs)
        {
            return Task.CompletedTask;
        }
    }
}
