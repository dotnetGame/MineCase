using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Server.Game;
using MineCase.World;
using MineCase.World.Generation;
using Orleans;

namespace MineCase.Server.World
{
    public interface IWorld : IGrainWithStringKey
    {
        Task<uint> NewEntityId();

        Task<WorldTime> GetTime();

        Task<long> GetAge();

        Task OnGameTick(GameTickArgs e);

        Task<int> GetSeed();

        Task<GeneratorSettings> GetGeneratorSettings();

        Task<EntityWorldPos> GetSpawnPosition();
    }
}
