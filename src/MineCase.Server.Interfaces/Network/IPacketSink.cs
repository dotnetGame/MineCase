using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Protocol;
using Orleans.Concurrency;

namespace MineCase.Server.Network
{
    public interface IPacketSink
    {
        [OneWay]
        Task SendPacket(ISerializablePacket packet);

        [OneWay]
        Task SendPacket(uint packetId, Immutable<byte[]> data);
    }
}
