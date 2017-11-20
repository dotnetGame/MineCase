using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MineCase.Engine
{
    /// <summary>
    /// 处理消息接口
    /// </summary>
    /// <typeparam name="TMessage">消息类型</typeparam>
    public interface IHandle<TMessage>
    {
        /// <summary>
        /// 处理消息
        /// </summary>
        /// <param name="message">消息</param>
        void Handle(TMessage message);
    }
}
