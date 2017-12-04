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

        public SyncPlayerStateComponent(string name = "syncPlayerState")
            : base(name)
        {
        }

        async Task IHandle<PlayerLoggedIn>.Handle(PlayerLoggedIn message)
        {
            var generator = AttachedObject.GetComponent<ClientboundPacketComponent>().GetGenerator();

            // PositionAndLook
            var position = AttachedObject.GetEntityWorldPosition();
            var lookComponent = AttachedObject.GetComponent<EntityLookComponent>();
            await generator.PositionAndLook(position.X, position.Y, position.Z, lookComponent.Yaw, lookComponent.Pitch, 0, AttachedObject.GetComponent<TeleportComponent>().StartNew());

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

            InstallPropertyChangedHandlers();
        }

        private void InstallPropertyChangedHandlers()
        {
            AttachedObject.RegisterPropertyChangedHandler(DraggedSlotComponent.DraggedSlotProperty, OnDraggedSlotChanged);
            AttachedObject.RegisterPropertyChangedHandler(EntityWorldPositionComponent.EntityWorldPositionProperty, OnEntityWorldPositionChanged);
        }

        private void OnEntityWorldPositionChanged(object sender, PropertyChangedEventArgs<EntityWorldPos> e)
        {
            var pos = e.NewValue;
            var box = new Cuboid(new Point3d(pos.X, pos.Z, pos.Y), new Size(0.6f, 0.6f, 1.75f));
            AttachedObject.SetLocalValue(ColliderComponent.ColliderShapeProperty, box);
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
