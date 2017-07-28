using Orleans;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MineCase.Server.Player
{
    public interface INonAuthenticatedPlayer : IGrainWithStringKey
    {
        Task<Guid> GetUUID();
    }
}
