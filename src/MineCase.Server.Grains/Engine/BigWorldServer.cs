using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Orleans;

namespace MineCase.Server.Engine
{
    public class BigWorldServer : Grain, IBigWorldServer
    {
        private bool isRunning;

        public override async Task OnActivateAsync()
        {
            await base.OnActivateAsync();
            isRunning = false;
        }

        public Task StartServer()
        {
            isRunning = true;
            return Task.CompletedTask;
        }

        public Task StopServer()
        {
            isRunning = false;
            return Task.CompletedTask;
        }

        public async Task<BigWorldEntityRef> CreateCellEntity(string typeName, Guid spaceId, BigWorldPosition position)
        {
            Guid entityId = Guid.NewGuid();
            return new BigWorldEntityRef { Id = entityId };
        }

        public async Task<BigWorldEntityRef> CreateBaseEntity(string typeName, Guid spaceId, BigWorldPosition position)
        {
            Guid entityId = Guid.NewGuid();
            return new BigWorldEntityRef { Id = entityId };
        }
    }
}
