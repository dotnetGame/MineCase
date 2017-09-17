using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Engine;
using Orleans.Concurrency;

namespace MineCase.Server.Components
{
    internal class GameTickNodeComponent : Component, IHandle<GameTickMessage>
    {
        public event AsyncEventHandler<(TimeSpan deltaTime, long worldAge)> Tick;

        public GameTickNodeComponent(string name = "gameTick")
            : base(name)
        {
        }

        public Task Handle(GameTickMessage message)
        {
            return Tick.InvokeSerial(this, (message.DeltaTime, message.WorldAge));
        }
    }

    [Immutable]
    public sealed class GameTickMessage : IEntityMessage
    {
        public TimeSpan DeltaTime { get; set; }

        public long WorldAge { get; set; }
    }
}
