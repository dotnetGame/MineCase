using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MineCase.Protocol;

namespace MineCase.Client.Network
{
    public interface IPacketPackager
    {
        (uint packetId, byte[] data) PreparePacket(ISerializablePacket packet);
    }
}
