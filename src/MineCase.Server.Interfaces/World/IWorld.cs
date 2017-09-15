using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Server.Game;
using MineCase.World.Generation;
using Orleans;

namespace MineCase.Server.World
{
    public interface IWorld : IGrainWithStringKey
    {
        Task<uint> NewEntityId();

        Task AttachEntity(IEntity entity);

        Task<IEntity> FindEntity(uint eid);

        Task<IEnumerable<IEntity>> GetEntities();

        Task<(long age, long timeOfDay)> GetTime();

        Task<long> GetAge();

        Task OnGameTick(TimeSpan deltaTime);

        Task<int> GetSeed();

        Task<GeneratorSettings> GetGeneratorSettings();
    }
}
