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
    [Packet(0x27)]
    public sealed class EntityLookAndRelativeMove : ISerializablePacket
    {
        [SerializeAs(DataType.VarInt)]
        public uint EID;

        [SerializeAs(DataType.Short)]
        public short DeltaX;

        [SerializeAs(DataType.Short)]
        public short DeltaY;

        [SerializeAs(DataType.Short)]
        public short DeltaZ;

        [SerializeAs(DataType.Angle)]
        public byte Yaw;

        [SerializeAs(DataType.Angle)]
        public byte Pitch;

        [SerializeAs(DataType.Boolean)]
        public bool OnGround;

        public static EntityLookAndRelativeMove Deserialize(ref SpanReader br)
        {
            return new EntityLookAndRelativeMove
            {
                EID = br.ReadAsVarInt(out _),
                DeltaX = br.ReadAsShort(),
                DeltaY = br.ReadAsShort(),
                DeltaZ = br.ReadAsShort(),
                Yaw = br.ReadAsByte(),
                Pitch = br.ReadAsByte(),
                OnGround = br.ReadAsBoolean()
            };
        }

        public void Serialize(BinaryWriter bw)
        {
            bw.WriteAsVarInt(EID, out _);
            bw.WriteAsShort(DeltaX);
            bw.WriteAsShort(DeltaY);
            bw.WriteAsShort(DeltaZ);
            bw.WriteAsByte(Yaw);
            bw.WriteAsByte(Pitch);
            bw.WriteAsBoolean(OnGround);
        }
    }
}
