using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Engine;
using MineCase.Server.Components;
using MineCase.World;

namespace MineCase.Server.Game.Entities.Components
{
    internal class OutOfWorldComponent : Component<PlayerGrain>
    {
        public OutOfWorldComponent(string name = "outOfWorld")
            : base(name)
        {
        }

        protected override void OnAttached()
        {
            Register();
        }

        protected override void OnDetached()
        {
            Unregister();
        }

        private void Register()
        {
            AttachedObject.GetComponent<GameTickComponent>()
                .Tick += OnGameTick;
        }

        private void Unregister()
        {
            AttachedObject.GetComponent<GameTickComponent>()
                .Tick -= OnGameTick;
        }

        private Task OnGameTick(object sender, GameTickArgs e)
        {
            // Every 40 tick, check whether entities are out of world
            if (e.WorldAge % 40 == 0)
            {
                if (AttachedObject.Position.Y < -64)
                {
                    var health = AttachedObject.GetComponent<HealthComponent>();
                    health.SetHealth(health.Health - 4);
                }
            }

            return Task.CompletedTask;
        }
    }
}
