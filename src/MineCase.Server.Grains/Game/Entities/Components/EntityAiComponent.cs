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
using MineCase.Server.World.EntitySpawner.Ai.MobAi;
using Orleans;

namespace MineCase.Server.Game.Entities.Components
{
    internal class EntityAiComponent : Component<MobGrain>, IHandle<SpawnMob>
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

        async Task IHandle<SpawnMob>.Handle(SpawnMob message)
        {
            switch (message.MobType)
            {
                case MobType.Chicken:
                    await AttachedObject.SetLocalValue(EntityAiComponent.AiTypeProperty, new AiChicken());
                    break;
                case MobType.Cow:
                    await AttachedObject.SetLocalValue(EntityAiComponent.AiTypeProperty, new AiCow());
                    break;
                case MobType.Creeper:
                    await AttachedObject.SetLocalValue(EntityAiComponent.AiTypeProperty, new AiCreeper());
                    break;
                case MobType.Pig:
                    await AttachedObject.SetLocalValue(EntityAiComponent.AiTypeProperty, new AiPig());
                    break;
                case MobType.Sheep:
                    await AttachedObject.SetLocalValue(EntityAiComponent.AiTypeProperty, new AiSheep());
                    break;
                case MobType.Skeleton:
                    await AttachedObject.SetLocalValue(EntityAiComponent.AiTypeProperty, new AiSkeleton());
                    break;
                case MobType.Zombie:
                    await AttachedObject.SetLocalValue(EntityAiComponent.AiTypeProperty, new AiZombie());
                    break;
                default:
                    throw new NotImplementedException("AI of this mob has not been implemented.");
            }
        }

        private Task ActionWalk()
        {
            return Task.CompletedTask;
        }

        private Task ActionFollow()
        {
            return Task.CompletedTask;
        }

        private Task ActionEscape()
        {
            return Task.CompletedTask;
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