using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;

namespace MineCase.Client.Network
{
    public sealed class SessionScope
    {
        public string Name { get; set; }

        public string ServerAddress { get; set; }

        public int ServerPort { get; set; }

        public IComponentContext ServiceProvider { get; set; }
    }
}
