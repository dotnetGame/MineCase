using System;
using System.Collections.Generic;
using System.Text;
using MineCase.Engine;
using MineCase.Server.Game.Windows;
using MineCase.Server.User;
using MineCase.Server.World;
using MineCase.World;
using Orleans.Concurrency;

namespace MineCase.Server.Game.Entities.Components
{
    [Immutable]
    public sealed class PlayerLoggedIn : IEntityMessage
    {
        public static readonly PlayerLoggedIn Default = new PlayerLoggedIn();
    }

    [Immutable]
    public sealed class KickPlayer : IEntityMessage
    {
        public Chat Reason { get; set; }
    }

    [Immutable]
    public sealed class BindToUser : IEntityMessage
    {
        public IUser User { get; set; }
    }

    [Immutable]
    public sealed class SetHeldItemIndex : IEntityMessage
    {
        public int Index { get; set; }
    }

    [Immutable]
    public sealed class AskHeldItem : IEntityMessage<(int index, Slot slot)>
    {
        public static readonly AskHeldItem Default = new AskHeldItem();
    }

    [Immutable]
    public sealed class SetDraggedSlot : IEntityMessage
    {
        public Slot Slot { get; set; }
    }

    [Immutable]
    public sealed class AskDraggedSlot : IEntityMessage<Slot>
    {
        public static readonly AskDraggedSlot Default = new AskDraggedSlot();
    }

    [Immutable]
    public sealed class AskPlayerDescription : IEntityMessage<PlayerDescription>
    {
        public static readonly AskPlayerDescription Default = new AskPlayerDescription();
    }

    [Immutable]
    public sealed class SpawnEntity : IEntityMessage
    {
        public IWorld World { get; set; }

        public uint EntityId { get; set; }

        public EntityWorldPos Position { get; set; }

        public float Pitch { get; set; }

        public float Yaw { get; set; }
    }

    [Immutable]
    public sealed class SetSlot : IEntityMessage
    {
        public int Index { get; set; }

        public Slot Slot { get; set; }
    }

    [Immutable]
    public sealed class AskSlot : IEntityMessage<Slot>
    {
        public int Index { get; set; }
    }

    [Immutable]
    public sealed class AskWindowId : IEntityMessage<byte>
    {
        public IWindow Window { get; set; }
    }

    [Immutable]
    public sealed class OpenWindow : IEntityMessage
    {
        public IWindow Window { get; set; }
    }

    [Immutable]
    public sealed class AskInventoryWindow : IEntityMessage<IInventoryWindow>
    {
        public static readonly AskInventoryWindow Default = new AskInventoryWindow();
    }

    [Immutable]
    public sealed class TossPickup : IEntityMessage
    {
        public Slot[] Slots { get; set; }
    }

    [Immutable]
    public sealed class DiscoveredByPlayer : IEntityMessage
    {
        public IPlayer Player { get; set; }
    }

    [Immutable]
    public sealed class BroadcasstDiscovered : IEntityMessage
    {
        public static readonly BroadcasstDiscovered Default = new BroadcasstDiscovered();
    }
}
