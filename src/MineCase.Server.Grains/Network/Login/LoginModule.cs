using System;
using System.Collections.Generic;
using System.Text;
using Autofac;

namespace MineCase.Server.Network.Login
{
    internal class LoginModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<LoginFlowGrain>();
        }
    }
}
