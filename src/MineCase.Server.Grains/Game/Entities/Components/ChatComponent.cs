using System;
using System.Collections.Generic;
using System.Text;
using MineCase.Engine;

namespace MineCase.Server.Game.Entities.Components
{
    internal class ChatComponent : Component<PlayerGrain>
    {
        public ChatComponent(string name = "chat")
            : base(name)
        {
        }
    }
}
