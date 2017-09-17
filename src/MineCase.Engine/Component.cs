﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MineCase.Engine
{
    public abstract class Component
    {
        public string Name { get; }

        protected DependencyObject AttachedObject { get; private set; }

        protected IServiceProvider ServiceProvider { get; private set; }

        public Component(string name)
        {
            Name = name;
        }

        internal Task Attach(DependencyObject dependencyObject, IServiceProvider serviceProvider)
        {
            AttachedObject = dependencyObject;
            ServiceProvider = serviceProvider;
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