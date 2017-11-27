using System;
using System.Collections.Generic;
using System.Text;
using Orleans;
using Orleans.Runtime;

namespace MineCase.Server.User
{
    internal class UserLifecycle : LifecycleObservable, IUserLifecycle
    {
        public UserLifecycle(Logger logger)
            : base(logger)
        {
        }
    }
}
