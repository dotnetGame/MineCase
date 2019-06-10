using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Runtime;

namespace MineCase.Server.User
{
    internal class UserLifecycle : IUserLifecycle
    {
        public UserLifecycle(ILogger logger)
        {
        }

        public IDisposable Subscribe(string observerName, int stage, ILifecycleObserver observer)
        {
            throw new NotImplementedException();
        }
    }
}
