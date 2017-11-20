using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
#if ECS_SERVER
using Orleans;
using Orleans.Concurrency;
#endif

namespace MineCase.Engine
{
    /// <summary>
    /// 询问结果
    /// </summary>
    /// <typeparam name="TResponse">回复类型</typeparam>
#if ECS_SERVER
    [Immutable]
#endif
    public sealed class AskResult<TResponse>
    {
        /// <summary>
        /// 询问失败
        /// </summary>
        public static readonly AskResult<TResponse> Failed = new AskResult<TResponse> { Succeeded = false };

        /// <summary>
        /// 是否成功
        /// </summary>
        public bool Succeeded;

        /// <summary>
        /// 回复
        /// </summary>
        public TResponse Response;
    }

    /// <summary>
    /// 依赖对象接口
    /// </summary>
    public interface IDependencyObject
#if ECS_SERVER
        : IGrain
#endif
    {
        /// <summary>
        /// 通知
        /// </summary>
        /// <param name="message">消息</param>
#if ECS_SERVER
        Task
#else
        void
#endif
            Tell(IEntityMessage message);

        /// <summary>
        /// 询问
        /// </summary>
        /// <param name="message">消息</param>
        /// <returns>回复</returns>
#if ECS_SERVER
        Task<TResponse>
#else
        TResponse
#endif
            Ask<TResponse>(IEntityMessage<TResponse> message);

        /// <summary>
        /// 尝试询问
        /// </summary>
        /// <param name="message">消息</param>
        /// <returns>询问结果</returns>
#if ECS_SERVER
        Task<AskResult<TResponse>>
#else
        AskResult<TResponse>
#endif
            TryAsk<TResponse>(IEntityMessage<TResponse> message);
    }
}
