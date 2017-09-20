using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Engine;
using MineCase.Server.Components;
using MineCase.Server.Network.Play;

namespace MineCase.Server.Game.Entities.Components
{
    internal class SyncPlayerStateComponent : Component<PlayerGrain>, IHandle<PlayerLoggedIn>, IHandle<BindToUser>
    {
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
        }

        private Task OnDraggedSlotChanged(object sender, PropertyChangedEventArgs<Slot> e)
        {
            return AttachedObject.GetComponent<ClientboundPacketComponent>().GetGenerator()
                .SetSlot(0xFF, 0, e.NewValue);
        }

        async Task IHandle<BindToUser>.Handle(BindToUser message)
        {
            await AttachedObject.GetComponent<NameComponent>().SetName(await message.User.GetName());
            await AttachedObject.GetComponent<SlotContainerComponent>().SetSlots(await message.User.GetInventorySlots());
        }
    }
}
