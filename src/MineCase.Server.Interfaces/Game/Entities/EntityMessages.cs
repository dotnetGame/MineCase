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
    public sealed class PlayerListAdd : IEntityMessage
    {
        public IReadOnlyList<IPlayer> Players { get; set; }
    }

    [Immutable]
    public sealed class PlayerListRemove : IEntityMessage
    {
        public IReadOnlyList<IPlayer> Players { get; set; }
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
    public sealed class SetHeldItem : IEntityMessage
    {
        public Slot Slot { get; set; }
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
    public sealed class BeginLogin : IEntityMessage
    {
        public static readonly BeginLogin Default = new BeginLogin();
    }

    [Immutable]
    public class SpawnEntity : IEntityMessage
    {
        public IWorld World { get; set; }

        public uint EntityId { get; set; }

        public EntityWorldPos Position { get; set; }

        public float Pitch { get; set; }

        public float Yaw { get; set; }
    }

    [Immutable]
    public class SpawnMob : SpawnEntity
    {
        public MobType MobType { get; set; }
    }

    [Immutable]
    public class SpawnPlayer : SpawnEntity
    {
    }

    [Immutable]
    public class EntityLook : IEntityMessage
    {
        public float Yaw { get; set; }

        public float Pitch { get; set; }
    }

    [Immutable]
    public class EntityMove : IEntityMessage
    {
        // 实际的相对位移，并非mc协议中位移/(32*128)
        public float DeltaX { get; set; }

        public float DeltaY { get; set; }

        public float DeltaZ { get; set; }

        public bool OnGround { get; set; }
    }

    [Immutable]
    public sealed class DestroyEntity : IEntityMessage
    {
        public static readonly DestroyEntity Default = new DestroyEntity();
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
    public sealed class BroadcastDiscovered : IEntityMessage
    {
        public static readonly BroadcastDiscovered Default = new BroadcastDiscovered();
    }

    [Immutable]
    public sealed class CollectBy : IEntityMessage
    {
        public IEntity Entity { get; set; }
    }

    [Immutable]
    public sealed class AskCollectionResult : IEntityMessage<Slot>
    {
        public IEntity Source { get; set; }

        public Slot Slot { get; set; }
    }
}
