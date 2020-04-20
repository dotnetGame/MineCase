using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Engine;
using MineCase.Graphics;
using MineCase.Server.Components;
using MineCase.Server.Network.Play;
using MineCase.Server.User;
using MineCase.World;
using Orleans;

namespace MineCase.Server.Game.Entities.Components
{
    internal class SyncPlayerStateComponent : Component<PlayerGrain>, IHandle<PlayerLoggedIn>, IHandle<BindToUser>
    {
        private IUser _user;

        private bool _isHandlersInstalled;

        public SyncPlayerStateComponent(string name = "syncPlayerState")
            : base(name)
        {
            _isHandlersInstalled = false;
        }

        async Task IHandle<PlayerLoggedIn>.Handle(PlayerLoggedIn message)
        {
            var generator = AttachedObject.GetComponent<ClientboundPacketComponent>().GetGenerator();

            // PositionAndLook
            var position = AttachedObject.GetEntityWorldPosition();
            var lookComponent = AttachedObject.GetComponent<EntityLookComponent>();
            await generator.PositionAndLook(position.X, position.Y, position.Z, lookComponent.Yaw, lookComponent.Pitch, 0, AttachedObject.GetComponent<TeleportComponent>().StartNew());

            // Update view position
            var chunkPos = position.ToChunkWorldPos();
            await generator.UpdateViewPosition(chunkPos.X, chunkPos.Z);

            // Health
            var healthComponent = AttachedObject.GetComponent<HealthComponent>();
            var foodComponent = AttachedObject.GetComponent<FoodComponent>();
            await generator.UpdateHealth(healthComponent.Health, healthComponent.MaxHealth, foodComponent.Food, foodComponent.MaxFood, foodComponent.FoodSaturation);

            // Experience
            var expComponent = AttachedObject.GetComponent<ExperienceComponent>();
            await generator.SetExperience(expComponent.ExperienceBar, expComponent.Level, expComponent.TotalExperience);

            // Inventory
            var slots = await AttachedObject.GetComponent<InventoryComponent>().GetInventoryWindow().GetSlots(AttachedObject);
            await generator.WindowItems(0, slots);

            if (!_isHandlersInstalled)
            {
                InstallPropertyChangedHandlers();
                _isHandlersInstalled = true;
            }
        }

        private void InstallPropertyChangedHandlers()
        {
            AttachedObject.RegisterPropertyChangedHandler(DraggedSlotComponent.DraggedSlotProperty, OnDraggedSlotChanged);
            AttachedObject.RegisterPropertyChangedHandler(EntityWorldPositionComponent.EntityWorldPositionProperty, OnEntityWorldPositionChanged);
            AttachedObject.RegisterPropertyChangedHandler(EntityLookComponent.HeadYawProperty, OnEntityHeadYawChanged);
            AttachedObject.RegisterPropertyChangedHandler(EntityLookComponent.PitchProperty, OnEntityPitchChanged);
            AttachedObject.RegisterPropertyChangedHandler(EntityLookComponent.YawProperty, OnEntityYawChanged);
            AttachedObject.RegisterPropertyChangedHandler(HealthComponent.HealthProperty, OnEntityHealthChanged);
        }

        private ChunkEventBroadcastComponent _broadcastComponent;

        private void OnEntityHeadYawChanged(object sender, PropertyChangedEventArgs<float> e)
        {
            _broadcastComponent = _broadcastComponent ?? AttachedObject.GetComponent<ChunkEventBroadcastComponent>();
            _broadcastComponent.GetGenerator(AttachedObject)
                .EntityHeadLook(
                AttachedObject.EntityId,
                GetAngle(e.NewValue));
        }

        private void OnEntityYawChanged(object sender, PropertyChangedEventArgs<float> e)
        {
            var yaw = AttachedObject.GetValue(EntityLookComponent.YawProperty);
            var pitch = AttachedObject.GetValue(EntityLookComponent.PitchProperty);
            _broadcastComponent = _broadcastComponent ?? AttachedObject.GetComponent<ChunkEventBroadcastComponent>();
            _broadcastComponent.GetGenerator(AttachedObject)
                .EntityLook(
                AttachedObject.EntityId,
                GetAngle(yaw),
                GetAngle(pitch),
                AttachedObject.GetValue(EntityOnGroundComponent.IsOnGroundProperty));
        }

        private void OnEntityPitchChanged(object sender, PropertyChangedEventArgs<float> e)
        {
            var yaw = AttachedObject.GetValue(EntityLookComponent.YawProperty);
            var pitch = AttachedObject.GetValue(EntityLookComponent.PitchProperty);
            _broadcastComponent = _broadcastComponent ?? AttachedObject.GetComponent<ChunkEventBroadcastComponent>();
            _broadcastComponent.GetGenerator(AttachedObject)
                .EntityLook(
                AttachedObject.EntityId,
                GetAngle(yaw),
                GetAngle(pitch),
                AttachedObject.GetValue(EntityOnGroundComponent.IsOnGroundProperty));
        }

        private void OnEntityWorldPositionChanged(object sender, PropertyChangedEventArgs<EntityWorldPos> e)
        {
            var generator = AttachedObject.GetComponent<ClientboundPacketComponent>().GetGenerator();

            // Update Collider
            var pos = e.NewValue;
            var box = new Cuboid(new Point3d(pos.X, pos.Z, pos.Y), new Size(0.6f, 0.6f, 1.75f));
            AttachedObject.SetLocalValue(ColliderComponent.ColliderShapeProperty, box);

            // Check if we need to send UpdateViewPosition packet. If the player walk cross chunk borders, send it.
            var oldChunkPos = e.OldValue.ToChunkWorldPos();
            var newChunkPos = e.NewValue.ToChunkWorldPos();
            if (oldChunkPos != newChunkPos)
            {
                generator.UpdateViewPosition(newChunkPos.X, newChunkPos.Z);
            }

            // Broadcast to trackers
            _broadcastComponent = _broadcastComponent ?? AttachedObject.GetComponent<ChunkEventBroadcastComponent>();
            _broadcastComponent.GetGenerator(AttachedObject)
                .EntityRelativeMove(
                AttachedObject.EntityId,
                GetDelta(e.OldValue.X, e.NewValue.X),
                GetDelta(e.OldValue.Y, e.NewValue.Y),
                GetDelta(e.OldValue.Z, e.NewValue.Z),
                AttachedObject.GetValue(EntityOnGroundComponent.IsOnGroundProperty));
        }

        private void OnEntityHealthChanged(object sender, PropertyChangedEventArgs<int> e)
        {
            var generator = AttachedObject.GetComponent<ClientboundPacketComponent>().GetGenerator();
            var healthComponent = AttachedObject.GetComponent<HealthComponent>();
            var foodComponent = AttachedObject.GetComponent<FoodComponent>();
            generator.UpdateHealth(healthComponent.Health, healthComponent.MaxHealth, foodComponent.Food, foodComponent.MaxFood, foodComponent.FoodSaturation);
            if (healthComponent.Health < 0)
            {
                AttachedObject.SetLocalValue(DeathComponent.IsDeathProperty, true);
            }
        }

        private static short GetDelta(float before, float after)
        {
            return (short)((after * 32 - before * 32) * 128);
        }

        private static byte GetAngle(float degree)
        {
            return (byte)(degree * 256 / 360);
        }

        private void OnDraggedSlotChanged(object sender, PropertyChangedEventArgs<Slot> e)
        {
            AttachedObject.QueueOperation(() => AttachedObject.GetComponent<ClientboundPacketComponent>().GetGenerator()
                .SetSlot(0xFF, 0, e.NewValue));
        }

        async Task IHandle<BindToUser>.Handle(BindToUser message)
        {
            AttachedObject.GetComponent<SlotContainerComponent>().SlotChanged -= InventorySlotChanged;

            _user = message.User;
            AttachedObject.GetComponent<NameComponent>().SetName(await message.User.GetName());
            AttachedObject.GetComponent<GameModeComponent>().SetGameMode(await message.User.GetGameMode());
            AttachedObject.GetComponent<SlotContainerComponent>().SetSlots(await message.User.GetInventorySlots());

            AttachedObject.GetComponent<SlotContainerComponent>().SlotChanged += InventorySlotChanged;
        }

        private void InventorySlotChanged(object sender, (int index, Slot slot) e)
        {
            AttachedObject.QueueOperation(() => _user.SetInventorySlot(e.index, e.slot));
        }
    }
}
