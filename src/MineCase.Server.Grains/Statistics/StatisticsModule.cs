using Autofac;
using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase.Server.Statistics
{
    class StatisticsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ServerStatisticsGrain>();
        }
    }
}
