using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Server.Game;
using Orleans;

namespace MineCase.Server.Game
{
    public class CellGrain : Grain, ICell
    {
        private Cell _cell;

        public CellGrain()
        {
            _cell = new Cell();
            _cell.SetCell(this);
        }

        public Task<ICell> GetCell(String key)
        {
            return Task.FromResult(GrainFactory.GetGrain<ICell>(key));
        }

        public Task<object> Tell(Guid entityId, object message)
        {
            return _cell.Tell(entityId, message);
        }
    }
}
