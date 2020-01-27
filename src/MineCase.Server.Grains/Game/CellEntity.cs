using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MineCase.Server.Game
{
    public abstract class CellEntity
    {
        String Space { get; set; }

        String Cell { get; set; }

        String EntityType { get; set; }

        float X { get; set; }

        float Y { get; set; }

        float Z { get; set; }

        bool Global { get; set; }

        Guid Id { get; set; }

        public abstract Task<object> OnMessage(object message);
    }
}
