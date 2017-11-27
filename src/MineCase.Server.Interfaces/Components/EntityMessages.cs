using System;
using System.Collections.Generic;
using System.Text;
using MineCase.Engine;
using MineCase.Server.Game.Entities;
using MineCase.World;
using Orleans.Concurrency;

namespace MineCase.Server.Components
{
    [Immutable]
    public sealed class GameTick : IEntityMessage
    {
        public GameTickArgs Args { get; set; }
    }

    [Immutable]
    public sealed class Disable : IEntityMessage
    {
        public static readonly Disable Default = new Disable();
    }

    [Immutable]
    public sealed class Enable : IEntityMessage
    {
        public static readonly Enable Default = new Enable();
    }

    [Immutable]
    public sealed class CollisionWith : IEntityMessage
    {
        public IReadOnlyCollection<IDependencyObject> Entities { get; set; }
    }
}
