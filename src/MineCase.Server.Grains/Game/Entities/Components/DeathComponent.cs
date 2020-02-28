using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Engine;
using MineCase.Server.Components;
using MineCase.Server.Network.Play;
using MineCase.World;

namespace MineCase.Server.Game.Entities.Components
{
    /// <summary>
    /// When player health is less than 0, respawn him.
    /// </summary>
    internal class DeathComponent : Component<PlayerGrain>
    {
        public static readonly DependencyProperty<bool> IsDeathProperty =
            DependencyProperty.Register<bool>("IsDeath", typeof(DeathComponent), new PropertyMetadata<bool>(false, OnDeath));

        public bool IsDeath => AttachedObject.GetValue(IsDeathProperty);

        public DeathComponent(string name = "death")
            : base(name)
        {
        }

        protected override void OnAttached()
        {
        }

        protected override void OnDetached()
        {
        }

        private async Task Respawn()
        {
            var generator = AttachedObject.GetComponent<ClientboundPacketComponent>().GetGenerator();

            var teleportComponent = AttachedObject.GetComponent<TeleportComponent>();
            var world = AttachedObject.GetWorld();
            var spawnPos = await world.GetSpawnPosition();
            await teleportComponent.Teleport(spawnPos, 0, 0);
            await generator.Respawn(Dimension.Overworld, await world.GetSeed(), new GameMode { ModeClass = GameMode.Class.Survival, IsHardcore = false }, LevelTypes.Default);
            AttachedObject.SetLocalValue(HealthComponent.HealthProperty, AttachedObject.GetValue(HealthComponent.MaxHealthProperty));
            AttachedObject.SetLocalValue(FoodComponent.FoodProperty, AttachedObject.GetValue(FoodComponent.MaxFoodProperty));
            AttachedObject.SetLocalValue(DeathComponent.IsDeathProperty, false);
        }

        private void OnDeath(PropertyChangedEventArgs<bool> e)
        {
            Task.Run(Respawn);
        }

        private static void OnDeath(object sender, PropertyChangedEventArgs<bool> e)
        {
            var component = ((DependencyObject)sender).GetComponent<DeathComponent>();
            component.OnDeath(e);
        }
    }
}
