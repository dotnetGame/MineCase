using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Orleans;

namespace MineCase.Server.Engine
{
    public class BigWorldClient : Grain, IBigWorldClient
    {
        private BigWorldEntityArray _clientEntities;

        public override async Task OnActivateAsync()
        {
            await base.OnActivateAsync();
            _clientEntities = new BigWorldEntityArray(GrainFactory);
        }

        public Task OnInit()
        {
            return Task.CompletedTask;
        }
    }
}
