using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase.Server.Engine
{
    /// <summary>
    /// 是实体消息.
    /// </summary>
    public interface IEntityMessage
    {
    }

    /// <summary>
    /// 具有回复的实体消息.
    /// </summary>
    /// <typeparam name="TResponse">回复类型.</typeparam>
    public interface IEntityMessage<TResponse>
    {
    }

    public sealed class AskResult<TResponse>
    {
        /// <summary>
        /// 询问失败.
        /// </summary>
        public static readonly AskResult<TResponse> Failed = new AskResult<TResponse> { Succeeded = false };

        /// <summary>
        /// 是否成功.
        /// </summary>
        public bool Succeeded;

        /// <summary>
        /// 回复.
        /// </summary>
        public TResponse Response;
    }

    /// <summary>
    /// 找不到接收者异常.
    /// </summary>
    public class ReceiverNotFoundException : Exception
    {
    }
}
