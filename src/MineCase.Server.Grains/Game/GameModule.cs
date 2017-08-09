using Autofac;
using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase.Server.Game
{
    class GameModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<GameSession>();
        }
    }
}
