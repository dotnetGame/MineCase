using MineCase.Engine;
using MineCase.Server.Network.Play;
using MineCase.Server.World;
using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase.Server.Components
{
    internal class ChunkEventBroadcastComponent : Component
    {
        public ChunkEventBroadcastComponent(string name = "chunkEventBroadcast")
            : base(name)
        {
        }

        public ClientPlayPacketGenerator GetGenerator()
        {
            return new ClientPlayPacketGenerator(GrainFactory.GetGrain<IChunkTrackingHub>(AttachedObject.GetAddressByPartitionKey()));
        }
    }

    internal static class ChunkEventBroadcastComponentExtensions
    {
        public static ClientPlayPacketGenerator GetBroadcaster(this DependencyObject d) =>
            d.GetComponent<ChunkEventBroadcastComponent>().GetGenerator();
    }
}
