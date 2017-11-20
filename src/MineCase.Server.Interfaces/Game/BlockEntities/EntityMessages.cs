using System;
using System.Collections.Generic;
using System.Text;
using MineCase.Engine;
using MineCase.Server.Game.Entities;
using MineCase.Server.World;
using MineCase.World;
using Orleans.Concurrency;

namespace MineCase.Server.Game.BlockEntities
{
    [Immutable]
    public sealed class SpawnBlockEntity : IEntityMessage
    {
        public IWorld World { get; set; }

        public BlockWorldPos Position { get; set; }
    }

    [Immutable]
    public sealed class DestroyBlockEntity : IEntityMessage
    {
        public static readonly DestroyBlockEntity Default = new DestroyBlockEntity();
    }

    [Immutable]
    public sealed class NeighborEntityChanged : IEntityMessage
    {
        public static readonly NeighborEntityChanged Empty = new NeighborEntityChanged();

        public IBlockEntity Entity { get; set; }
    }

    [Immutable]
    public sealed class UseBy : IEntityMessage
    {
        public IEntity Entity { get; set; }
    }
}
