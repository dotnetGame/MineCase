using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MineCase.Client.Network;
using MineCase.Client.User;

namespace MineCase.Client.Messages
{
    public class UserLoggedInMessage
    {
        public SessionScope SessionScope { get; set; }

        public IUser User { get; set; }
    }
}
