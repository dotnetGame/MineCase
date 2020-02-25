using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Server.Network;
using MineCase.Server.World;
using MineCase.Util.Math;
using MineCase.World.Chunk;
using Orleans;

namespace MineCase.Game.Server.MultiPlayer
{
    public interface IUser : IGrainWithGuidKey
    {
        Task SetServer(IMinecraftServer server);

        Task<IMinecraftServer> GetServer();

        Task SetWorld(IWorld world);

        Task<IWorld> GetWorld();

        Task SetSession(IGameSession session);

        Task<IGameSession> GetSession();

        Task SetName(string name);

        Task<string> GetGame();

        Task<Guid> GetEntityId();

        Task BindPacketSink(IPacketSink sink);

        Task SetPlayerPosition(EntityPos pos);

        Task SendChunkData(int chunkX, int chunkZ, ChunkColumn chunk);
    }
}
