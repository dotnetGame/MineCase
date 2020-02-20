using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MineCase.Util.Event
{
    public delegate Task AsyncEventHandler<TEventArgs>(object sender, TEventArgs e);

    public static class AsyncEventHandlerExtensions
    {
        public static async Task InvokeSerial<TEventArgs>(this AsyncEventHandler<TEventArgs> handler, object sender, TEventArgs e)
        {
            if (handler != null)
            {
                foreach (AsyncEventHandler<TEventArgs> invoke in handler.GetInvocationList())
                    await invoke(sender, e);
            }
        }
    }
}
