using Orleans;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MineCase.Server.World
{
    public interface IWorldAccessor : IGrainWithIntegerKey
    {
        Task<IWorld> GetDefaultWorld();
        Task<IWorld> GetWorld(string name);
    }
}
