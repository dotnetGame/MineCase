using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using MineCase.Engine.Builder;
using UnityEngine;

namespace MineCase.Engine
{
    /// <summary>
    /// 聪明的 MonoBehaviour
    /// </summary>
    public abstract class SmartBehaviour : MonoBehaviour
    {
        /// <summary>
        /// 获取服务提供程序
        /// </summary>
        public ILifetimeScope ServiceProvider { get; set; }

        /// <summary>
        /// 加载时执行
        /// </summary>
        protected virtual void Awake()
        {
            BootstrapperBase.Current.BuildUp(this);
        }
    }
}
