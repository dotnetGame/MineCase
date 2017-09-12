using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Server.Game;
using MineCase.Server.Network;
using MineCase.Server.User;
using Orleans;

namespace MineCase.Server.World
{
    public interface IChunkTrackingHub : IGrainWithStringKey, ITickable, IPacketSink
    {
        Task Subscribe(IUser user);

        Task Unsubscribe(IUser user);

        Task Subscribe(ITickable tickable);

        Task Unsubscribe(ITickable tickable);
    }

    public static class ChunkTrackingHubExtensions
    {
        public static string MakeChunkTrackingHubKey(this IWorld world, int x, int z)
        {
            return $"{world.GetPrimaryKeyString()},{x},{z}";
        }

        public static (int x, int z) GetChunkPosition(this IChunkTrackingHub chunkTrackingHub)
        {
            var key = chunkTrackingHub.GetPrimaryKeyString().Split(',');
            return (int.Parse(key[1]), int.Parse(key[2]));
        }

        public static (string worldKey, int x, int z) GetWorldAndChunkPosition(this IChunkTrackingHub chunkTrackingHub)
        {
            var key = chunkTrackingHub.GetPrimaryKeyString().Split(',');
            return (key[0], int.Parse(key[1]), int.Parse(key[2]));
        }
    }
}
