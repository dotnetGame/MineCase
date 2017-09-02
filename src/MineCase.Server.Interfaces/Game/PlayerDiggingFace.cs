using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase.Server.Game
{
    public enum PlayerDiggingFace : byte
    {
        Bottom = 0,
        Top = 1,
        North = 2,
        South = 3,
        West = 4,
        East = 5,
        Special = 255
    }
}
