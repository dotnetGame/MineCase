using System;
using System.Collections.Generic;
using System.Text;

using MineCase.Serialization;

namespace MineCase.Protocol.Play
{
    public enum PlayerDiggingStatus : uint
    {
        StartedDigging = 0,
        CancelledDigging = 1,
        FinishedDigging = 2,
        DropItemStack = 3,
        DropItem = 4,
        ShootArrowOrFinishEating = 5,
        SwapItemInHand = 6
    }

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

    [Packet(0x1A)]
    [GenerateSerializer]
    public sealed partial class PlayerDigging : IPacket
    {
        [SerializeAs(DataType.VarInt)]
        public PlayerDiggingStatus Status;

        [SerializeAs(DataType.Position)]
        public Position Location;

        [SerializeAs(DataType.Byte)]
        public PlayerDiggingFace Face;
    }
}
