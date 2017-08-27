using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MineCase.Serialization;
using Orleans.Concurrency;

namespace MineCase.Protocol.Play
{
    [Immutable]
    [Packet(0x40)]
    public sealed class UpdateHealth : ISerializablePacket
    {
        [SerializeAs(DataType.Float)]
        public float Health;

        [SerializeAs(DataType.VarInt)]
        public uint Food;

        [SerializeAs(DataType.Float)]
        public float FoodSaturation;

        public void Serialize(BinaryWriter bw)
        {
            bw.WriteAsFloat(Health);
            bw.WriteAsVarInt(Food, out _);
            bw.WriteAsFloat(FoodSaturation);
        }
    }
}
