using Autofac;
using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase.Server.Network.Login
{
    class LoginModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<LoginFlowGrain>();
        }
    }
}
