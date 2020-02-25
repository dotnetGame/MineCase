using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Game.Server;
using Orleans;

namespace MineCase.Server.Entity
{
    public interface IEntityHolder : IGrainWithGuidKey
    {
        Task<bool> IsSpawned();

        Task<bool> Lock(IGameSession gameSession);

        Task<bool> UnLock(IGameSession gameSession);

        Task<IEntity> Load();

        Task Save(IEntity entity);
    }
}
