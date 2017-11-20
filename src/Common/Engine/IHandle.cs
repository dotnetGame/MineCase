using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MineCase.Engine
{
    /// <summary>
    /// 实体消息处理接口
    /// </summary>
    /// <typeparam name="TMessage">消息类型</typeparam>
    public interface IHandle<TMessage>
        where TMessage : IEntityMessage
    {
        /// <summary>
        /// 处理消息
        /// </summary>
        /// <param name="message">消息</param>
#if ECS_SERVER
        Task
#else
        void
#endif
            Handle(TMessage message);
    }

    /// <summary>
    /// 实体消息处理接口
    /// </summary>
    /// <typeparam name="TMessage">消息类型</typeparam>
    /// <typeparam name="TResponse">返回类型</typeparam>
    public interface IHandle<TMessage, TResponse>
        where TMessage : IEntityMessage<TResponse>
    {
        /// <summary>
        /// 处理消息
        /// </summary>
        /// <param name="message">消息</param>
        /// <returns>回复</returns>
#if ECS_SERVER
        Task<TResponse>
#else
        TResponse
#endif
            Handle(TMessage message);
    }
}
