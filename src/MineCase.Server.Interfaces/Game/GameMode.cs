using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase.Server.Game
{
    public struct GameMode
    {
        public enum Class : byte
        {
            Survival = 0,
            Creative = 1,
            Adventure = 2,
            Spectator = 3
        }

        public Class ModeClass { get; set; }

        public bool IsHardcore { get; set; }
    }
}
