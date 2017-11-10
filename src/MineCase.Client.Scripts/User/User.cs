using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineCase.Client.User
{
    public interface IUser
    {
        Guid UUID { get; }

        string UserName { get; }
    }

    internal class User : IUser
    {
        public Guid UUID { get; }

        public string UserName { get; }

        public User(Guid uuid, string userName)
        {
            UUID = uuid;
            UserName = userName;
        }
    }
}
