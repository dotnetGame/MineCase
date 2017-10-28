using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Algorithm;
using MineCase.Algorithm.Game.Entity.Ai.MobAi;
using MineCase.Engine;
using MineCase.Graphics;
using MineCase.Server.Components;
using MineCase.Server.Game.BlockEntities;
using MineCase.Server.World;
using MineCase.Server.World.EntitySpawner;
using MineCase.Server.World.EntitySpawner.Ai;
using MineCase.Server.World.EntitySpawner.Ai.Action;
using MineCase.Server.World.EntitySpawner.Ai.MobAi;
using MineCase.World;
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

        private Random random;

        public EntityAiComponent(string name = "entityAi")
            : base(name)
        {
            random = new Random();
        }

        protected override Task OnAttached()
        {
            Register();
            AttachedObject.SetLocalValue(EntityAiComponent.CreatureStateProperty, CreatureState.Stop);
            AttachedObject.SetLocalValue(EntityAiComponent.CreatureEventProperty, CreatureEvent.Nothing);
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
                case MobType.Squid:
                    // TODO new ai for squid
                    await AttachedObject.SetLocalValue(EntityAiComponent.AiTypeProperty, new AiChicken());
                    break;
                case MobType.Zombie:
                    await AttachedObject.SetLocalValue(EntityAiComponent.AiTypeProperty, new AiZombie());
                    break;
                default:
                    // TODO add more ai
                    // throw new NotImplementedException("AI of this mob has not been implemented.");
                    break;
            }
        }

        private async Task ActionWalk()
        {
            float step = 0.3f;
            float theta = (float)(random.NextDouble() * 2 * Math.PI);
            float yaw = AttachedObject.GetValue(EntityLookComponent.YawProperty);
            float head;
            EntityWorldPos pos = AttachedObject.GetValue(EntityWorldPositionComponent.EntityWorldPositionProperty);
            if (random.Next(50) == 0)
            {
                head = theta;

                await AttachedObject.SetLocalValue(EntityLookComponent.YawProperty, (float)(head / Math.PI * 180.0f));
                await AttachedObject.SetLocalValue(EntityLookComponent.HeadYawProperty, (float)(head / Math.PI * 180.0f));
            }
            else
            {
                head = (float)(yaw / 180.0f * Math.PI);
            }

            // 新的位置
            EntityWorldPos entityPos = new EntityWorldPos(pos.X - step * (float)Math.Sin(head), pos.Y, pos.Z + step * (float)Math.Cos(head));
            BlockWorldPos blockPos = entityPos.ToBlockWorldPos();

            // 检测行进方向的方块是否满足要求
            Cuboid entityBoundbox = new Cuboid(new Point3d(entityPos.X, entityPos.Y, entityPos.Z), new Size(1, 1, 2)); // TODO data from Boundbox component
            var chunkAccessor = AttachedObject.GetComponent<ChunkAccessorComponent>();
            bool isCollided = false;
            int yJumpHeight = 0;

            // 检测此位置会不会与方块碰撞
            for (int i = 1; blockPos.Y + i < 256 && i <= 3; ++i)
            {
                BlockWorldPos upblock = BlockWorldPos.Add(blockPos, 0, i, 0);
                BlockState upstate = await chunkAccessor.GetBlockState(upblock);
                if (!upstate.IsAir())
                {
                    Cuboid blockBoundbox = new Cuboid(new Point3d(upblock.X, upblock.Y, upblock.Z), new Size(1, 1, 1));
                    if (Collision.IsCollided(entityBoundbox, blockBoundbox))
                    {
                        isCollided = true;
                        break;
                    }
                }
            }

            // 获得高度变化
            for (int i = -2; blockPos.Y + i < 256 && i <= 0; ++i)
            {
                BlockState state = await chunkAccessor.GetBlockState(BlockWorldPos.Add(blockPos, 0, i, 0));
                if (!state.IsAir())
                {
                    yJumpHeight = i + 1;
                }
            }

            if (!isCollided)
            {
                await AttachedObject.SetLocalValue(
                    EntityWorldPositionComponent.EntityWorldPositionProperty,
                    EntityWorldPos.Add(entityPos, 0, yJumpHeight, 0));
            }
        }

        private Task ActionFollow()
        {
            return Task.CompletedTask;
        }

        private Task ActionEscape()
        {
            float yaw = AttachedObject.GetValue(EntityLookComponent.YawProperty);

            return Task.CompletedTask;
        }

        private async Task OnGameTick(object sender, (TimeSpan deltaTime, long worldAge) e)
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

            // get state
            ICreatureAi ai = AttachedObject.GetValue(EntityAiComponent.AiTypeProperty);
            CreatureState state = AttachedObject.GetValue(EntityAiComponent.CreatureStateProperty);

            // random walk
            if (state == CreatureState.Stop && random.Next(10) == 0)
            {
                await AttachedObject.SetLocalValue(EntityAiComponent.CreatureEventProperty, CreatureEvent.RandomWalk);
            }
            else if (state == CreatureState.Walk && random.Next(30) == 0)
            {
                await AttachedObject.SetLocalValue(EntityAiComponent.CreatureEventProperty, CreatureEvent.Stop);
            }

            CreatureEvent evnt = AttachedObject.GetValue(EntityAiComponent.CreatureEventProperty);
            CreatureState newState = ai.GetState(state, evnt);
            await AttachedObject.SetLocalValue(EntityAiComponent.CreatureStateProperty, newState);
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
                case CreatureState.Walk:
                    await ActionWalk();
                    break;
                case CreatureState.Stop:
                    break;
                default:
                    throw new NotSupportedException("Unsupported state.");
            }
        }
    }
}