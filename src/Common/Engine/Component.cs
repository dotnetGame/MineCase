using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
#if ECS_SERVER
using ServiceProviderType = System.IServiceProvider;
#else
using ServiceProviderType = Autofac.ILifetimeScope;
#endif

namespace MineCase.Engine
{
    internal interface IComponentIntern
    {
#if ECS_SERVER
        Task
#else
        void
#endif
            Attach(DependencyObject dependencyObject, ServiceProviderType serviceProvider);

#if ECS_SERVER
        Task
#else
        void
#endif
            Detach();

        int GetMessageOrder(object message);
    }

    /// <summary>
    /// 组件基类
    /// </summary>
    public abstract partial class Component : IComponentIntern
    {
        /// <summary>
        /// 获取名称
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// 获取附加到的实体
        /// </summary>
        protected DependencyObject AttachedObject { get; private set; }

        /// <summary>
        /// 获取服务提供程序
        /// </summary>
        protected ServiceProviderType ServiceProvider { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Component"/> class.
        /// </summary>
        /// <param name="name">名称</param>
        public Component(string name)
        {
            Name = name;
        }

#if ECS_SERVER
        Task
#else
        void
#endif
            IComponentIntern.Attach(DependencyObject dependencyObject, ServiceProviderType serviceProvider)
        {
            AttachedObject = dependencyObject;
            ServiceProvider = serviceProvider;
            AttatchPartial(dependencyObject, serviceProvider);
#if ECS_SERVER
            return
#endif
            OnAttached();
        }

        partial void AttatchPartial(DependencyObject dependencyObject, ServiceProviderType serviceProvider);

#if ECS_SERVER
        Task
#else
        void
#endif
            IComponentIntern.Detach()
        {
            AttachedObject = null;
#if ECS_SERVER
            return
#endif
            OnDetached();
        }

        /// <summary>
        /// 组件被附加到实体时
        /// </summary>
        protected virtual

#if ECS_SERVER
        Task
#else
        void
#endif
            OnAttached()
        {
#if ECS_SERVER
            return Task.CompletedTask;
#endif
        }

        /// <summary>
        /// 组件从实体卸载时
        /// </summary>
        protected virtual Task OnDetached()
        {
            return Task.CompletedTask;
        }

        /// <summary>
        /// 获取消息处理顺序
        /// </summary>
        /// <param name="message">消息</param>
        /// <returns>处理顺序（数字越小越靠前）</returns>
        public virtual int GetMessageOrder(object message)
        {
            return 0;
        }
    }

    /// <summary>
    /// 组件基类
    /// </summary>
    /// <typeparam name="T">实体类型</typeparam>
    public abstract class Component<T> : Component
        where T : DependencyObject
    {
        /// <summary>
        /// 获取附加到的实体
        /// </summary>
        public new T AttachedObject => (T)base.AttachedObject;

        /// <summary>
        /// Initializes a new instance of the <see cref="Component{T}"/> class.
        /// </summary>
        /// <param name="name">名称</param>
        public Component(string name)
            : base(name)
        {
        }
    }
}
