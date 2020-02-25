using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Core.World.Dimension;
using MineCase.Game.Server.MultiPlayer;
using MineCase.Server.Entity;
using MineCase.Server.Entity.Player;
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

        public override async Task OnActivateAsync()
        {
            await base.OnActivateAsync();

            if (!State.Worlds.ContainsKey(DimensionType.Overworld))
            {
                var world = GrainFactory.GetGrain<IWorld>(DimensionType.Overworld.ToString());
                State.Worlds[DimensionType.Overworld] = world;
            }
        }

        public Task<int> GetNetworkCompressionThreshold()
        {
            return Task.FromResult(-1);
        }

        public Task<bool> GetOnlineMode()
        {
            return Task.FromResult(false);
        }

        public async Task UserJoin(IUser user)
        {
            _users.Add(user);
            await user.SetServer(this);

            // Spawn Player Entity
            var playerHolder = GrainFactory.GetGrain<IEntityHolder>(user.GetPrimaryKey());
            if (!await playerHolder.IsSpawned())
            {
                PlayerEntity player = new PlayerEntity(GrainFactory, State.Worlds[DimensionType.Overworld]);
                await playerHolder.Save(player);
            }

            // Get main session and join in
            var mainSession = GrainFactory.GetGrain<IGameSession>(Guid.Empty);
            await mainSession.UserEnter(user);
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
