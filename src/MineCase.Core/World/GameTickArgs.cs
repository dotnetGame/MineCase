using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase.World
{
    public sealed class GameTickArgs
    {
        public TimeSpan DeltaTime { get; set; }

        public long WorldAge { get; set; }

        public long TimeOfDay { get; set; }
    }
}
