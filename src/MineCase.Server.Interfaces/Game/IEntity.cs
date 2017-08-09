using Orleans;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MineCase.Server.Game
{
    public interface IEntity : IGrain
    {
        Task SetEntityId(uint eid);
    }
}
