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
        Task SendPacket(ISerializablePacket packet);
    }
}
