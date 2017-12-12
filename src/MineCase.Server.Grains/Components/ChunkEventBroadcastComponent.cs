using System;
using System.Collections.Generic;
using System.Text;
using MineCase.Engine;
using MineCase.Server.Game.Entities;
using MineCase.Server.Network.Play;
using MineCase.Server.World;

namespace MineCase.Server.Components
{
    internal class ChunkEventBroadcastComponent : Component
    {
        public ChunkEventBroadcastComponent(string name = "chunkEventBroadcast")
            : base(name)
        {
        }

        public ClientPlayPacketGenerator GetGenerator(IPlayer except = null)
        {
            return new ClientPlayPacketGenerator(GrainFactory.GetGrain<IChunkTrackingHub>(AttachedObject.GetAddressByPartitionKey()), except);
        }
    }

    internal static class ChunkEventBroadcastComponentExtensions
    {
        public static ClientPlayPacketGenerator GetBroadcaster(this DependencyObject d) =>
            d.GetComponent<ChunkEventBroadcastComponent>().GetGenerator();
    }
}
