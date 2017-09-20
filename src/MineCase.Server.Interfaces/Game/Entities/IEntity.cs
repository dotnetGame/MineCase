using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using MineCase.Engine;
using MineCase.Server.World;
using MineCase.World;
using Orleans;

namespace MineCase.Server.Game.Entities
{
    public interface IEntity : IDependencyObject, IGrainWithGuidKey
    {
        Task<uint> GetEntityId();

        Task<IWorld> GetWorld();

        Task<EntityWorldPos> GetPosition();

        Task<float> GetYaw();
    }
}
