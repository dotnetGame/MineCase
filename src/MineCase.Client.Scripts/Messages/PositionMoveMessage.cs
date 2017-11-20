using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineCase.Client.Messages
{
    internal class PositionMoveMessage
    {
        public float Horizontal { get; set; }

        public float Vertical { get; set; }
    }
}
