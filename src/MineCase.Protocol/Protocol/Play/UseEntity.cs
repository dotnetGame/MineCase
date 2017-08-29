using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MineCase.Serialization;
using Orleans.Concurrency;

namespace MineCase.Protocol.Play
{
    [Immutable]
    [Packet(0x0A)]
    public sealed class UseEntity
    {
        [SerializeAs(DataType.VarInt)]
        public int Target;

        [SerializeAs(DataType.VarInt)]
        public int Type;

        [SerializeAs(DataType.Float)]
        public float? TargetX;

        [SerializeAs(DataType.Float)]
        public float? TargetY;

        [SerializeAs(DataType.Float)]
        public float? TargetZ;

        [SerializeAs(DataType.VarInt)]
        public int? Hand;

        public static UseEntity Deserialize(ref SpanReader br)
        {
            var packet = new UseEntity
            {
                Target = (int)br.ReadAsVarInt(out _),
                Type = (int)br.ReadAsVarInt(out _)
            };

            // Only if Type is interact at
            if (packet.Type == 2)
            {
                packet.TargetX = br.ReadAsFloat();
                packet.TargetY = br.ReadAsFloat();
                packet.TargetZ = br.ReadAsFloat();
            }

            // Only if Type is interact or interact at
            if (packet.Type == 0 || packet.Type == 2)
                packet.Hand = (int)br.ReadAsVarInt(out _);
            return packet;
        }
    }
}
