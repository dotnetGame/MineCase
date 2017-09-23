using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Algorithm.Game.Entity.Ai.MobAi;
using MineCase.Engine;
using MineCase.Server.Components;
using MineCase.Server.Game.BlockEntities;
using MineCase.Server.World.EntitySpawner;

namespace MineCase.Server.Game.Entities.Components
{
    internal class EntityAiComponent : Component<MobGrain>
    {
        public static readonly DependencyProperty<ICreatureAi> AiTypeProperty =
            DependencyProperty.Register<ICreatureAi>("AiType", typeof(EntityAiComponent));

        public static readonly DependencyProperty<CreatureState> CreatureStateProperty =
            DependencyProperty.Register<CreatureState>("CreatureState", typeof(EntityAiComponent));

        public ICreatureAi AiType => AttachedObject.GetValue(AiTypeProperty);

        public CreatureState State => AttachedObject.GetValue(CreatureStateProperty);

        public EntityAiComponent(string name = "entityAi")
            : base(name)
        {
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

        private Task OnGameTick(object sender, (TimeSpan deltaTime, long worldAge) e)
        {
            float pitch = AttachedObject.GetComponent<EntityLookComponent>().Pitch;
            AttachedObject.GetComponent<EntityLookComponent>().SetYaw(pitch + 360.0f / 255);
            if (pitch > 360)
            {
                pitch = 0;
            }

            return Task.CompletedTask;
        }
    }
}