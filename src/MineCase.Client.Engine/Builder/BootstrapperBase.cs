using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using UnityEngine;

namespace MineCase.Engine.Builder
{
    /// <summary>
    /// 应用初始化器基类
    /// </summary>
    public abstract class BootstrapperBase
    {
        private static BootstrapperBase _current;

        /// <summary>
        /// 获取当前应用
        /// </summary>
        public static BootstrapperBase Current => OnGetCurrent();

        private bool _initialized;
        private IContainer _container;

        /// <summary>
        /// Initializes a new instance of the <see cref="BootstrapperBase"/> class.
        /// </summary>
        public BootstrapperBase()
        {
        }

        /// <summary>
        /// 配置应用程序组件
        /// </summary>
        /// <param name="assemblies">程序集集合</param>
        protected abstract void ConfigureApplicationParts(ICollection<Assembly> assemblies);

        /// <summary>
        /// 初始化
        /// </summary>
        protected void Initialize()
        {
            if (!_initialized)
            {
                var assemblies = new List<Assembly>
                {
                    typeof(BootstrapperBase).Assembly
                };
                ConfigureApplicationParts(assemblies);

                var containerBuilder = new ContainerBuilder();
                containerBuilder.RegisterAssemblyModules(assemblies.ToArray());
                containerBuilder.Register<IComponentContext>(c => _container);
                _container = containerBuilder.Build();

                _initialized = true;
            }
        }

        /// <summary>
        /// 进行属性注入依赖
        /// </summary>
        /// <param name="instance">实例</param>
        public void BuildUp(object instance)
        {
            _container.InjectProperties(instance);
        }

        private void CheckInitialized()
        {
            if (!_initialized)
                throw new InvalidOperationException($"Bootstrapper 为初始化，确认调用了 {nameof(Initialize)}。");
        }

        private static BootstrapperBase OnGetCurrent()
        {
            var type = (from a in AppDomain.CurrentDomain.GetAssemblies()
                        let attr = a.GetCustomAttribute<BootstrapperTypeAttribute>()
                        where attr != null
                        select attr.Type).FirstOrDefault();

            if (type == null)
                throw new InvalidOperationException("未找到 BootstrapperTypeAttribute 标记的初始化器类型。");
            var boot = (BootstrapperBase)Activator.CreateInstance(type);
            _current = boot;
            return boot;
        }
    }
}
