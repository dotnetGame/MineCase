using System;
using System.Collections.Generic;
using System.Text;
using Autofac;

namespace MineCase.Server.User
{
    internal class UserModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<NonAuthenticatedUserGrain>();
        }
    }
}
