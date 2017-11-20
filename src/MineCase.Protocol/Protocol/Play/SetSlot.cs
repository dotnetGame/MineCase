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
    [Packet(0x16)]
    public sealed class SetSlot : ISerializablePacket
    {
        [SerializeAs(DataType.Byte)]
        public byte WindowId;

        [SerializeAs(DataType.Short)]
        public short Slot;

        [SerializeAs(DataType.Slot)]
        public Slot SlotData;

        public void Serialize(BinaryWriter bw)
        {
            bw.WriteAsByte(WindowId);
            bw.WriteAsShort(Slot);
            bw.WriteAsSlot(SlotData);
        }
    }
}
