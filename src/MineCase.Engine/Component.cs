using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MineCase.Engine
{
    public abstract class Component
    {
        protected DependencyObject AttachedObject { get; private set; }

        internal Task Attach(DependencyObject dependencyObject)
        {
            AttachedObject = dependencyObject;
            return OnAttached();
        }

        internal Task Detach()
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
}
