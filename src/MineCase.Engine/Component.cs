using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Orleans;
using Microsoft.Extensions.Logging;

namespace MineCase.Engine
{
    internal interface IComponentIntern
    {
        Task Attach(DependencyObject dependencyObject, IServiceProvider serviceProvider);

        Task Detach();
    }

    public abstract class Component<T> : IComponentIntern
        where T : DependencyObject
    {
        public string Name { get; }

        protected T AttachedObject { get; private set; }

        protected IServiceProvider ServiceProvider { get; private set; }

        protected IGrainFactory GrainFactory { get; private set; }

        protected ILogger Logger { get; private set; }

        public Component(string name)
        {
            Name = name;
        }

        Task IComponentIntern.Attach(DependencyObject dependencyObject, IServiceProvider serviceProvider)
        {
            AttachedObject = (T)dependencyObject;
            ServiceProvider = serviceProvider;
            GrainFactory = serviceProvider.GetService<IGrainFactory>();
            Logger = serviceProvider.GetService<ILoggerFactory>()?.CreateLogger(GetType());
            return OnAttached();
        }

        Task IComponentIntern.Detach()
        {
            AttachedObject = null;
            return OnDetached();
        }

        protected virtual Task OnAttached()
        {
            return Task.CompletedTask;
        }

        protected virtual Task OnDetached()
        {
            return Task.CompletedTask;
        }
    }

    public abstract class Component : Component<DependencyObject>
    {
        public Component(string name)
            : base(name)
        {
        }
    }
}
