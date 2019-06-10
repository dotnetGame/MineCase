using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Runtime;

namespace MineCase.Server.User
{
    internal class UserLifecycle : LifecycleObservable, IUserLifecycle
    {
        public UserLifecycle(ILogger logger)
            : base(logger)
        {
        }
    }
}
