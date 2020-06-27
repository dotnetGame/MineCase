using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MineCase.Serialization;

namespace MineCase.Protocol.Play
{
    public enum ActionId : uint
    {
        StartSneaking = 0,
        StopSneaking = 1,
        LeaveBed = 2,
        StartSprinting = 3,
        StopSprinting = 4,
        StartJumpWithHorse = 5,
        StopJumpWithHorse = 6,
        OpenHorseInventory = 7,
        StartFlyingWithElytra = 8
    }

    [Packet(0x1B)]
    [GenerateSerializer]
    public sealed partial class EntityAction : IPacket
    {
        [SerializeAs(DataType.VarInt)]
        public uint EntityId;

        [SerializeAs(DataType.VarInt)]
        public ActionId ActionId;

        [SerializeAs(DataType.VarInt)]
        public uint JumpBoost;
    }
}
