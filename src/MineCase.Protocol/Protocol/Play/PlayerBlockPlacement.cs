using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using MineCase.Serialization;

namespace MineCase.Protocol.Play
{
    [Packet(0x2C)]
    [GenerateSerializer]
    public sealed partial class PlayerBlockPlacement : IPacket
    {
        [SerializeAs(DataType.Position)]
        public Position Location;

        [SerializeAs(DataType.VarInt)]
        public PlayerDiggingFace Face;

        [SerializeAs(DataType.VarInt)]
        public Hand Hand;

        [SerializeAs(DataType.Float)]
        public float CursorPositionX;

        [SerializeAs(DataType.Float)]
        public float CursorPositionY;

        [SerializeAs(DataType.Float)]
        public float CursorPositionZ;
    }
}
