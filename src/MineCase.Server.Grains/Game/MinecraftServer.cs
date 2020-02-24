using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Core.World.Dimension;
using MineCase.Game.Server.MultiPlayer;
using MineCase.Server.World;
using Orleans;
using Orleans.Providers;

namespace MineCase.Game.Server
{
    public class MinecraftServerState
    {
        public Dictionary<DimensionType, IWorld> Worlds { get; set; } = new Dictionary<DimensionType, IWorld>();
    }

    [StorageProvider(ProviderName = "MongoDBStore")]
    public class MinecraftServer : Grain<MinecraftServerState>, IMinecraftServer
    {
        private readonly List<IUser> _users = new List<IUser>();

        public Task<int> GetNetworkCompressionThreshold()
        {
            return Task.FromResult(-1);
        }

        public Task<bool> GetOnlineMode()
        {
            return Task.FromResult(false);
        }

        public Task UserJoin(IUser user)
        {
            _users.Add(user);
            user.SetServer(this);

            // Get main session and join in
            var mainSession = GrainFactory.GetGrain<IGameSession>(Guid.Empty);
            mainSession.UserEnter(user);
            return Task.CompletedTask;
        }

        public Task UserLeave()
        {
            return Task.CompletedTask;
        }

        public Task<int> UserNumber()
        {
            return Task.FromResult(_users.Count);
        }
    }
}
