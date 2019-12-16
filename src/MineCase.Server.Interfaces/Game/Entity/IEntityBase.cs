using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Server.World;
using MineCase.World;

namespace MineCase.Server.Game.Entity
{
    public interface IEntityBase
    {
        Task<Guid> GetUUID();

        Task<uint> GetEntityId();

        Task<EntityWorldPos> GetPosition();

        Task<IWorld> GetWorld();

        Task<float> GetPitch();

        Task<float> GetHeadYaw();

        Task<float> GetYaw();
    }
}
