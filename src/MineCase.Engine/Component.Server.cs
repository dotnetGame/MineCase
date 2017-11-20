using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Orleans;

namespace MineCase.Engine
{
    public partial class Component
    {
        protected IGrainFactory GrainFactory { get; private set; }

        protected ILogger Logger { get; private set; }

        partial void AttatchPartial(DependencyObject dependencyObject, IServiceProvider serviceProvider)
        {
            GrainFactory = serviceProvider.GetService<IGrainFactory>();
            Logger = serviceProvider.GetService<ILoggerFactory>()?.CreateLogger(GetType());
        }
    }
}
