using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using MineCase.Protocol;
using MineCase.Protocol.Play;
using MineCase.Serialization;
using MineCase.World;
using Orleans.Concurrency;

namespace MineCase.Server.Network.Play
{
    internal struct ClientPlayPacketGenerator
    {
        public IPacketSink Sink { get; }

        // public IBroadcastPacketSink BroadcastSink { get; }

        // private IPlayer _except;
        public ClientPlayPacketGenerator(IPacketSink sink)
        {
            Sink = sink;

            // BroadcastSink = null;
            // _except = null;
        }

        // public Task SendPacket(uint packetId, byte[] data)
        // {
        //    if (Sink != null)
        //        return Sink.SendPacket(packetId, data.AsImmutable());
        //    else
        //        return BroadcastSink.SendPacket(packetId, data.AsImmutable(), _except);
        // }

        public Task SendPacket(ISerializablePacket packet)
        {
            if (Sink != null)
                return Sink.SendPacket(packet);

            return Task.CompletedTask;
        }

        /*
        public Task ChunkData(Dimension dimension, int chunkX, int chunkZ, ChunkColumnCompactStorage chunkColumn)
        {
            return SendPacket(PacketFactory.ChunkDataPacket());
        }
        */
    }

    [Flags]
    public enum RelativeFlags : byte
    {
        X = 0x1,
        Y = 0x2,
        Z = 0x4,
        Yaw = 0x8,
        Pitch = 0x10
    }
}
