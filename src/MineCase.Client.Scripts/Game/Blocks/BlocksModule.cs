using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;

namespace MineCase.Client.Game.Blocks
{
    internal class BlocksModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<BlockTextureLoader>().As<IBlockTextureLoader>().SingleInstance();
        }
    }
}
