using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Algorithm.Game.Entity.Ai.MobAi;
using MineCase.Engine;
using MineCase.Server.Components;
using MineCase.Server.Game.BlockEntities;
using MineCase.Server.World;
using MineCase.Server.World.EntitySpawner;
using MineCase.Server.World.EntitySpawner.Ai;
using MineCase.Server.World.EntitySpawner.Ai.Action;
using Orleans;

namespace MineCase.Server.Game.Entities.Components
{
    internal class EntityAiComponent : Component<MobGrain>
    {
        public static readonly DependencyProperty<ICreatureAi> AiTypeProperty =
            DependencyProperty.Register<ICreatureAi>("AiType", typeof(EntityAiComponent));

        public static readonly DependencyProperty<CreatureState> CreatureStateProperty =
            DependencyProperty.Register<CreatureState>("CreatureState", typeof(EntityAiComponent));

        public static readonly DependencyProperty<CreatureEvent> CreatureEventProperty =
            DependencyProperty.Register<CreatureEvent>("CreatureEvent", typeof(EntityAiComponent));

        public ICreatureAi AiType => AttachedObject.GetValue(AiTypeProperty);

        public CreatureState CreatureState => AttachedObject.GetValue(CreatureStateProperty);

        public CreatureEvent CreatureEvent => AttachedObject.GetValue(CreatureEventProperty);

        public EntityAiComponent(string name = "entityAi")
            : base(name)
        {
        }

        protected override Task OnAttached()
        {
            Register();
            return base.OnAttached();
        }

        protected override Task OnDetached()
        {
            Unregister();
            return base.OnDetached();
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
            /*
            if (e.worldAge % 16 == 0)
            {
                float pitch = AttachedObject.GetValue(EntityLookComponent.PitchProperty);
                pitch += 30 * 360.0f / 255;
                if (pitch > 360)
                {
                    pitch = 0;
                }

                AttachedObject.SetLocalValue(EntityLookComponent.PitchProperty, pitch);
            }
            */

            /*
            ICreatureAi ai = AttachedObject.GetValue(EntityAiComponent.AiTypeProperty);
            IWorld world = AttachedObject.GetWorld();
            var chunkAccessor = AttachedObject.GetComponent<ChunkAccessorComponent>();
            */

            // CreatureAiAction action = AttachedObject.GetValue(EntityAiComponent.CreatureAiActionProperty);
            // action.Action(AttachedObject);
            ICreatureAi ai = AttachedObject.GetValue(EntityAiComponent.AiTypeProperty);
            CreatureState state = AttachedObject.GetValue(EntityAiComponent.CreatureStateProperty);
            CreatureEvent evnt = AttachedObject.GetValue(EntityAiComponent.CreatureEventProperty);
            CreatureState newState = ai.GetState(state, evnt);
            AttachedObject.SetLocalValue(EntityAiComponent.CreatureStateProperty, newState);
            switch (newState)
            {
                case CreatureState.Attacking:
                    break;
                case CreatureState.Burned:
                    break;
                case CreatureState.BurnedBySunshine:
                    break;
                case CreatureState.EatingGrass:
                    break;
                case CreatureState.Escaping:
                    break;
                case CreatureState.Explosion:
                    break;
                case CreatureState.Follow:
                    break;
                default:
                    throw new NotSupportedException("Unsupported state.");
            }

            return Task.CompletedTask;
        }
    }
}