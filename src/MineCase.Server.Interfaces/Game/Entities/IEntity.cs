using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using MineCase.Server.World;
using MineCase.World;
using Orleans;
using MineCase.Engine;

namespace MineCase.Server.Game.Entities
{
    public interface IEntity : IDependencyObject, IGrainWithGuidKey
    {
        Task<IWorld> GetWorld();

        Task<EntityWorldPos> GetPosition();
    }
}
