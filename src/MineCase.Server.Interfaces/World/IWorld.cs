using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Formats;
using MineCase.Server.Game;
using MineCase.Server.World.Generation;
using Orleans;

namespace MineCase.Server.World
{
    public interface IWorld : IGrainWithStringKey
    {
        Task<uint> NewEntityId();

        Task<byte> NewWindowId();

        Task AttachEntity(IEntity entity);

        Task<IEntity> FindEntity(uint eid);

        Task<(long age, long timeOfDay)> GetTime();

        Task<long> GetAge();

        Task OnGameTick(TimeSpan deltaTime);

        Task<IBlockAccessor> GetBlockAccessor();

        Task<int> GetSeed();

        Task<GeneratorSettings> GetGeneratorSettings();
    }
}
