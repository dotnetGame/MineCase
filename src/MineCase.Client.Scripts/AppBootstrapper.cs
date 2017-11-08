using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using MineCase.Engine.Builder;

[assembly: BootstrapperType(typeof(MineCase.Client.AppBootstrapper))]

namespace MineCase.Client
{
    internal class AppBootstrapper : BootstrapperBase
    {
        public AppBootstrapper()
        {
            Initialize();
        }

        protected override void ConfigureApplicationParts(ICollection<Assembly> assemblies)
        {
            assemblies.Add(typeof(AppBootstrapper).Assembly);
        }
    }
}
