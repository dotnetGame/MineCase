using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase
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

        public byte ToByte()
        {
            return (byte)(((uint)this.ModeClass) | (this.IsHardcore ? 0b100u : 0u));
        }
    }
}
