using System;
using System.Collections.Generic;
using System.Text;
using MineCase.Engine;
using MineCase.Server.Game.Entities;
using Orleans.Concurrency;

namespace MineCase.Server.Components
{
    [Immutable]
    public sealed class GameTick : IEntityMessage
    {
        public TimeSpan DeltaTime { get; set; }

        public long WorldAge { get; set; }
    }

    [Immutable]
    public sealed class Disable : IEntityMessage
    {
        public static readonly Disable Default = new Disable();
    }
}
