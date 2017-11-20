using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase.Engine
{
    /// <summary>
    /// 是实体消息
    /// </summary>
    public interface IEntityMessage
    {
    }

    /// <summary>
    /// 具有回复的实体消息
    /// </summary>
    /// <typeparam name="TResponse">回复类型</typeparam>
    public interface IEntityMessage<TResponse>
    {
    }

    /// <summary>
    /// 找不到接收者异常
    /// </summary>
    public class ReceiverNotFoundException : Exception
    {
    }
}
