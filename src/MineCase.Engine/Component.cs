using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Orleans;

namespace MineCase.Engine
{
    internal interface IComponentIntern
    {
        Task Attach(DependencyObject dependencyObject, IServiceProvider serviceProvider);

        Task Detach();
    }

    public abstract class Component : IComponentIntern
    {
        public string Name { get; }

        protected DependencyObject AttachedObject { get; private set; }

        protected IServiceProvider ServiceProvider { get; private set; }

        protected IGrainFactory GrainFactory { get; private set; }

        protected ILogger Logger { get; private set; }

        public Component(string name)
        {
            Name = name;
        }

        Task IComponentIntern.Attach(DependencyObject dependencyObject, IServiceProvider serviceProvider)
        {
            AttachedObject = dependencyObject;
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

    public abstract class Component<T> : Component
        where T : DependencyObject
    {
        public new T AttachedObject => (T)base.AttachedObject;

        public Component(string name)
            : base(name)
        {
        }
    }
}
