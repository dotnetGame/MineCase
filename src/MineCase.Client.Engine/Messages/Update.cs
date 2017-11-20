using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineCase.Engine.Messages
{
    /// <summary>
    /// 每帧调用
    /// </summary>
    public sealed class Update : IEntityMessage
    {
        /// <summary>
        /// 默认
        /// </summary>
        public static readonly Update Default = new Update();
    }
}
