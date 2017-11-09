using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MineCase.Protocol;

namespace MineCase.Client.Network
{
    public interface IPacketSink
    {
        Task SendPacket(ISerializablePacket packet);

        Task SendPacket(uint packetId, byte[] data);
    }
}
