using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Server.Interfaces.World;
using Orleans;

namespace MineCase.Server.World
{
    public class WorldGrain : Grain, IWorld
    {
        private string _name = "";
        private long _worldAge = 0;
        private HashSet<string> _activePartition = new HashSet<string>();

        public override async Task OnActivateAsync()
        {
            var worldName = this.GetPrimaryKeyString();
            _name = worldName;
            var tickEmitter = GrainFactory.GetGrain<ITickEmitter>(worldName);
            await tickEmitter.Start();
            await base.OnActivateAsync();
        }

        public override async Task OnDeactivateAsync()
        {
            var tickEmitter = GrainFactory.GetGrain<ITickEmitter>(_name);
            await tickEmitter.Stop();
            await base.OnDeactivateAsync();
        }

        public async Task OnTick()
        {
            foreach (var eachPartition in _activePartition)
            {
                var partition = GrainFactory.GetGrain<IWorldPartition>(eachPartition);
                await partition.OnTick();
            }
        }

        public Task ActivatePartition(string key)
        {
            return Task.CompletedTask;
        }

        public Task DeactivatePartition(string key)
        {
            return Task.CompletedTask;
        }
    }
}
