using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Orleans;

namespace MineCase.Server.Engine
{
    public class BigWorldCellApp : Grain, IBigWorldCellApp
    {
        public Task Init()
        {
            return Task.CompletedTask;
        }

        public Task Destroy()
        {
            return Task.CompletedTask;
        }
    }
}
