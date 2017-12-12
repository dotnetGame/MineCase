using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Protocol;
using MineCase.Server.Game.Entities;
using Orleans.Concurrency;

namespace MineCase.Server.Network
{
    public interface IPacketSink
    {
        Task SendPacket(ISerializablePacket packet);

        Task SendPacket(uint packetId, Immutable<byte[]> data);
    }

    public interface IBroadcastPacketSink
    {
        Task SendPacket(ISerializablePacket packet, IPlayer except);

        Task SendPacket(uint packetId, Immutable<byte[]> data, IPlayer except);
    }
}
