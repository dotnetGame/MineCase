using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Server.World;
using MineCase.Util.Math;
using MineCase.World;
using Orleans;

namespace MineCase.Server.Entity
{
    public class Entity : IEntity
    {
        public Guid PrimaryKey { get; set; }

        public int EID { get; set; }

        public IWorld World { get; set; }

        public EntityPos Position { get; set; }

        protected IGrainFactory _grainFactory;

        public Entity(IGrainFactory grainFactory)
        {
            _grainFactory = grainFactory;
        }

        public Task<Guid> GetID()
        {
            return Task.FromResult(PrimaryKey);
        }

        public Task<EntityPos> GetPosition()
        {
            return Task.FromResult(Position);
        }

        public Task OnGameTick(object sender, GameTickArgs tickArgs)
        {
            return Task.CompletedTask;
        }
    }
}
