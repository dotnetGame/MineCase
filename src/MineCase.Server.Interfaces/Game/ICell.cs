using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Orleans;

namespace MineCase.Server.Game
{
    public interface ICell : IGrainWithStringKey
    {
        Task<ICell> GetCell(String key);

        Task<object> Tell(Guid entityId, object message);
    }
}
