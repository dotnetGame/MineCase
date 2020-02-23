using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Server.Server.MultiPlayer;
using MineCase.Server.World.Chunk;
using MineCase.Util.Math;
using Orleans;

namespace MineCase.Server.Server
{
    public class GameSession : Grain, IGameSession
    {
        private ChunkManager _chunkManager;

        private List<ChunkPos> _loadedRegion;

        private List<IUser> _users;

        public override Task OnActivateAsync()
        {
            _chunkManager = new ChunkManager();
            _loadedRegion = new List<ChunkPos>();
            _users = new List<IUser>();
            return base.OnActivateAsync();
        }

        public Task UserEnter(IUser user)
        {
            return Task.CompletedTask;
        }

        public Task UserLeave(IUser user)
        {
            return Task.CompletedTask;
        }
    }
}
