using MineCase.Engine;
using MineCase.Server.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase.Server.Game.Entities.Components
{
    public sealed class PlayerLoggedIn : IEntityMessage
    {
    }

    public sealed class BindToUser : IEntityMessage
    {
        public IUser User { get; set; }
    }
}
