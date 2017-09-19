using System;
using System.Collections.Generic;
using System.Text;
using MineCase.Engine;
using MineCase.Protocol;
using Orleans.Concurrency;

namespace MineCase.Server.Network.Play
{
    [Immutable]
    public sealed class PacketForwardToPlayer : IEntityMessage
    {
        public uint PacketId { get; set; }

        public byte[] Data { get; set; }
    }

    [Immutable]
    public sealed class PacketBroadcastToChunk : IEntityMessage
    {
        public uint PacketId { get; set; }

        public byte[] Data { get; set; }
    }
}
