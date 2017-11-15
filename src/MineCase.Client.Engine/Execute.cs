using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MineCase.Engine.Builder;

namespace MineCase.Engine
{
    /// <summary>
    /// 执行代码的一些扩展
    /// </summary>
    public static class Execute
    {
        /// <summary>
        /// 获取是否运行在 Unity Editor
        /// </summary>
        public static bool InEditor => UnityEngine.Application.isEditor;

        /// <summary>
        /// 在主线程同步执行
        /// </summary>
        /// <param name="action">操作</param>
        public static void OnMainThread(this Action action)
        {
            BootstrapperBase.Current.OnMainThread(action);
        }

        /// <summary>
        /// 在主线程异步执行
        /// </summary>
        /// <param name="action">操作</param>
        public static void OnMainThreadAsync(this Action action)
        {
            BootstrapperBase.Current.OnMainThreadAsync(action);
        }
    }
}
