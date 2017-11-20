using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineCase.Engine
{
    /// <summary>
    /// 事件聚合器扩展
    /// </summary>
    public static class EventAggregatorExtensions
    {
        /// <summary>
        /// 在当前线程发布消息
        /// </summary>
        /// <param name="eventAggregator">事件聚合器</param>
        /// <param name="message">消息</param>
        public static void PublishOnCurrentThread(this IEventAggregator eventAggregator, object message)
        {
            eventAggregator.Publish(message, a => a());
        }

        /// <summary>
        /// 在主线程发布同步消息
        /// </summary>
        /// <param name="eventAggregator">事件聚合器</param>
        /// <param name="message">消息</param>
        public static void PublishOnMainThread(this IEventAggregator eventAggregator, object message)
        {
            eventAggregator.Publish(message, a => Execute.OnMainThread(a));
        }

        /// <summary>
        /// 在主线程发布异步消息
        /// </summary>
        /// <param name="eventAggregator">事件聚合器</param>
        /// <param name="message">消息</param>
        public static void PublishOnMainThreadAsync(this IEventAggregator eventAggregator, object message)
        {
            eventAggregator.Publish(message, a => Execute.OnMainThreadAsync(a));
        }
    }
}
