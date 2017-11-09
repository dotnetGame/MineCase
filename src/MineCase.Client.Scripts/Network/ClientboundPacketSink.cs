using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MineCase.Protocol;

namespace MineCase.Client.Network
{
    internal class ClientboundPacketSink : IPacketSink
    {
        public Task SendPacket(ISerializablePacket packet)
        {
            throw new NotImplementedException();
        }

        public Task SendPacket(uint packetId, byte[] data)
        {
            throw new NotImplementedException();
        }
    }
}
