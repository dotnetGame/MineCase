using System;
using System.Threading.Tasks;
using Orleans;

namespace MineCase.Server.Game
{
    public interface ITickable : IGrainWithStringKey
    {
        Task OnGameTick(TimeSpan deltaTime);
    }
}
