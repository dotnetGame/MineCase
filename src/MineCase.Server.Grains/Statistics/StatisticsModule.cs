using System;
using System.Collections.Generic;
using System.Text;
using Autofac;

namespace MineCase.Server.Statistics
{
    internal class StatisticsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ServerStatisticsGrain>();
        }
    }
}
