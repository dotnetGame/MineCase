using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MineCase.Serialization;

namespace MineCase.Protocol.Play
{
#if !NET46
    [Orleans.Concurrency.Immutable]
#endif
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
