using Orleans;
using System;
using System.Collections.Generic;
using System.Text;
using MineCase.Server.Game;
using MineCase.Server.World;
using System.Threading.Tasks;

namespace MineCase.Server.Player
{
    class PlayerGrain : Grain, IPlayer
    {
        private string _worldId;

        public async Task<IWorld> GetWorld()
        {
            if(string.IsNullOrEmpty(_worldId))
            {
                var world = await GrainFactory.GetGrain<IWorldAccessor>(0).GetDefaultWorld();
                _worldId = world.GetPrimaryKeyString();
                return world;
            }
            return await GrainFactory.GetGrain<IWorldAccessor>(0).GetWorld(_worldId);
        }
    }
}
