using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineCase.Client.Messages
{
    internal class CursorMoveMessage
    {
        public float DeltaX { get; set; }

        public float DeltaY { get; set; }
    }
}
