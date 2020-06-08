using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using MineCase.Serialization;

namespace MineCase.Protocol.Play
{
    [Packet(0x2C)]
    public sealed class PlayerBlockPlacement
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

        public static PlayerBlockPlacement Deserialize(ref SpanReader br)
        {
            return new PlayerBlockPlacement
            {
                Location = br.ReadAsPosition(),
                Face = (PlayerDiggingFace)br.ReadAsVarInt(out _),
                Hand = (Hand)br.ReadAsVarInt(out _),
                CursorPositionX = br.ReadAsFloat(),
                CursorPositionY = br.ReadAsFloat(),
                CursorPositionZ = br.ReadAsFloat()
            };
        }
    }
}
