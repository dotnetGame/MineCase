using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MineCase.Server.Game
{
    public class Cell
    {
        String SpaceKey { get; set; }

        String CellKey { get; set; }

        public int X { get; set; }

        public int Y { get; set; }

        public int Z { get; set; }

        private Dictionary<Guid, CellEntity> _entityValues;

        private ICell _cell;

        public Task<object> Tell(CellEntityRef entity, object message)
        {
            if (entity.SpaceKey == SpaceKey && entity.CellKey == CellKey)
            {
                if (_entityValues.ContainsKey(entity.Id))
                {
                    return _entityValues[entity.Id].OnMessage(message);
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return _cell.Tell(entity.Id, message);
            }
        }

        public Task<object> Tell(Guid entityId, object message)
        {
            if (_entityValues.ContainsKey(entityId))
            {
                return _entityValues[entityId].OnMessage(message);
            }
            else
            {
                return null;
            }
        }

        public void SetCell(ICell cell)
        {
            _cell = cell;
        }
    }
}
