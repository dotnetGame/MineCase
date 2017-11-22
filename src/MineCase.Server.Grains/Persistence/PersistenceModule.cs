using System;
using System.Collections.Generic;
using System.Text;
using Autofac;

namespace MineCase.Server.Persistence
{
    internal class PersistenceModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AppDbContext>();
        }
    }
}
