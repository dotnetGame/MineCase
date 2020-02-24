using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Util.Math;
using MineCase.World;

namespace MineCase.Server.Entity
{
    public interface IEntity
    {
        Task<Guid> GetID();

        Task<EntityPos> GetPosition();

        Task OnGameTick(object sender, GameTickArgs tickArgs);
    }
}
