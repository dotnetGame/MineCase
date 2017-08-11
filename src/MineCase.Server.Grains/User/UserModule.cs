using Autofac;
using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase.Server.User
{
    class UserModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<NonAuthenticatedUserGrain>();
        }
    }
}
