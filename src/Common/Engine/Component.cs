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
        void Attach(DependencyObject dependencyObject, ServiceProviderType serviceProvider);

        void Detach();

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

        void IComponentIntern.Attach(DependencyObject dependencyObject, ServiceProviderType serviceProvider)
        {
            AttachedObject = dependencyObject;
            ServiceProvider = serviceProvider;
            AttatchPartial(dependencyObject, serviceProvider);
            OnAttached();
        }

        partial void AttatchPartial(DependencyObject dependencyObject, ServiceProviderType serviceProvider);

        void IComponentIntern.Detach()
        {
            OnDetached();
            AttachedObject = null;
        }

        /// <summary>
        /// 组件被附加到实体时
        /// </summary>
        protected virtual void OnAttached()
        {
        }

        /// <summary>
        /// 组件从实体卸载时
        /// </summary>
        protected virtual void OnDetached()
        {
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
