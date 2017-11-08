using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineCase.Engine
{
    /// <summary>
    /// 事件聚合器接口
    /// </summary>
    public interface IEventAggregator
    {
        /// <summary>
        /// 订阅事件
        /// </summary>
        /// <param name="subscriber">订阅主体</param>
        void Subscribe(object subscriber);

        /// <summary>
        /// 注销订阅事件
        /// </summary>
        /// <param name="subscriber">订阅主体</param>
        void Unsubscribe(object subscriber);

        /// <summary>
        /// 发布消息
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="marshal">封送器</param>
        void Publish(object message, Action<Action> marshal);
    }
}
