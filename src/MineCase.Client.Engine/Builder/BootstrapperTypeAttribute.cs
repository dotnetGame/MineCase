using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineCase.Engine.Builder
{
    /// <summary>
    /// 指明初始化器类型特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false, Inherited = false)]
    public sealed class BootstrapperTypeAttribute : Attribute
    {
        /// <summary>
        /// 获取初始化器类型
        /// </summary>
        public Type Type { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BootstrapperTypeAttribute"/> class.
        /// </summary>
        /// <param name="type">初始化器类型</param>
        public BootstrapperTypeAttribute(Type type)
        {
            Type = type;
        }
    }
}
