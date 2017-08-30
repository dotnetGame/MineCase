using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase.Formats
{
    // http://wiki.vg/index.php?title=Protocol#Position
    public class Position
    {
        public int X;
        public int Y;
        public int Z;
    }

    public enum DiggingStatus : uint
    {
        StartedDigging = 0,
        CancelledDigging = 1,
        FinishedDigging = 2,
        DropItemStack = 3,
        DropItem = 4,
        ShootArrowOrFinishEating = 5,
        SwapItemInHand = 6
    }

    public enum DiggingFace : byte
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
