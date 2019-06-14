using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MineCase.Algorithm;
using MineCase.Algorithm.Game.Entity.Ai.MobAi;
using MineCase.Block;
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
    internal class EntityAiComponent : Component<MobGrain>
    {
        public static readonly DependencyProperty<CreatureState> CreatureStateProperty =
            DependencyProperty.Register<CreatureState>(nameof(CreatureState), typeof(EntityAiComponent));

        public MobType MobType => AttachedObject.GetValue(MobTypeComponent.MobTypeProperty);

        public CreatureState CreatureState => AttachedObject.GetValue(CreatureStateProperty);

        private Random random;
        private CreatureAi _ai;

        public EntityAiComponent(string name = "entityAi")
            : base(name)
        {
            random = new Random();
        }

        protected override void OnAttached()
        {
            // Register();
            AttachedObject.SetLocalValue(EntityAiComponent.CreatureStateProperty, CreatureState.Stop);
            CreateAi(MobType);
            AttachedObject.RegisterPropertyChangedHandler(MobTypeComponent.MobTypeProperty, OnMobTypeChanged);
        }

        private void OnMobTypeChanged(object sender, PropertyChangedEventArgs<MobType> e)
        {
            CreateAi(e.NewValue);
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

        private void CreateAi(MobType mobType)
        {
            Func<CreatureState> getter = () => AttachedObject.GetValue(CreatureStateProperty);
            Action<CreatureState> setter = v => AttachedObject.SetLocalValue(CreatureStateProperty, v);
            CreatureAi ai;

            switch (mobType)
            {
                case Entities.MobType.Chicken:
                    ai = new AiChicken(getter, setter);
                    break;
                case Entities.MobType.Cow:
                    ai = new AiCow(getter, setter);
                    break;
                case Entities.MobType.Creeper:
                    ai = new AiCreeper(getter, setter);
                    break;
                case Entities.MobType.Pig:
                    ai = new AiPig(getter, setter);
                    break;
                case Entities.MobType.Sheep:
                    ai = new AiSheep(getter, setter);
                    break;
                case Entities.MobType.Skeleton:
                    ai = new AiSkeleton(getter, setter);
                    break;
                case Entities.MobType.Spider:
                    ai = new AiSpider(getter, setter);
                    break;
                case Entities.MobType.Squid:
                    // TODO new ai for squid
                    ai = new AiChicken(getter, setter);
                    break;
                case Entities.MobType.Zombie:
                    ai = new AiZombie(getter, setter);
                    break;
                default:
                    // TODO add more ai
                    Logger.LogWarning("AI of this mob has not been implemented: {0}.", mobType);
                    ai = null;
                    break;
            }

            _ai = ai;
        }

        private Task ActionStop()
        {
            float theta = (float)(random.NextDouble() * 360);
            float yaw = AttachedObject.GetValue(EntityLookComponent.YawProperty);
            if (random.Next(20) == 0)
            {
                AttachedObject.SetLocalValue(EntityLookComponent.YawProperty, theta);
                AttachedObject.SetLocalValue(EntityLookComponent.HeadYawProperty, theta);
            }
            else
            {
                // AttachedObject.SetLocalValue(EntityLookComponent.YawProperty, yaw);
                AttachedObject.SetLocalValue(EntityLookComponent.HeadYawProperty, yaw);
            }

            return Task.CompletedTask;
        }

        private async Task ActionWalk()
        {
            float step = 0.2f;
            float theta = (float)(random.NextDouble() * 2 * Math.PI);
            float yaw = AttachedObject.GetValue(EntityLookComponent.YawProperty);
            float head;
            EntityWorldPos pos = AttachedObject.GetValue(EntityWorldPositionComponent.EntityWorldPositionProperty);
            if (random.Next(50) == 0)
            {
                head = theta;
            }
            else
            {
                head = (float)(yaw / 180.0f * Math.PI);
            }

            AttachedObject.SetLocalValue(EntityLookComponent.YawProperty, (float)(head / Math.PI * 180.0f));
            AttachedObject.SetLocalValue(EntityLookComponent.HeadYawProperty, (float)(head / Math.PI * 180.0f));

            // 新的位置
            EntityWorldPos entityPos = new EntityWorldPos(pos.X - step * (float)Math.Sin(head), pos.Y, pos.Z + step * (float)Math.Cos(head));
            BlockWorldPos blockPos = entityPos.ToBlockWorldPos();

            // 检测行进方向的方块是否满足要求
            Cuboid entityBoundbox = new Cuboid(new Point3d(entityPos.X, entityPos.Y, entityPos.Z), new Size(1, 1, 2)); // TODO data from Boundbox component
            var chunkAccessor = AttachedObject.GetComponent<ChunkAccessorComponent>();
            bool isCollided = false;

            // 检测此位置会不会与方块碰撞
            for (int i = 1; blockPos.Y + i < 256 && i <= 3; ++i)
            {
                BlockWorldPos upblock = BlockWorldPos.Add(blockPos, 0, i, 0);
                BlockState upstate = await chunkAccessor.GetBlockState(upblock);
                if (upstate.IsMobCollided())
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
            int yJumpHeight = 0;
            bool canWalk = false;
            for (int i = 0; blockPos.Y + i >= 0 && i >= -2; --i)
            {
                BlockState upstate = await chunkAccessor.GetBlockState(BlockWorldPos.Add(blockPos, 0, i + 1, 0));
                BlockState state = await chunkAccessor.GetBlockState(BlockWorldPos.Add(blockPos, 0, i, 0));
                if (!upstate.IsMobCollided() && state.IsMobCollided() && state.CanMobStand())
                {
                    yJumpHeight = i + 1;
                    canWalk = true;
                    break;
                }
            }

            if (!isCollided && canWalk)
            {
                AttachedObject.SetLocalValue(
                    EntityWorldPositionComponent.EntityWorldPositionProperty,
                    EntityWorldPos.Add(entityPos, 0, yJumpHeight, 0));
            }
        }

        private async Task ActionLook()
        {
            // 通知周围creature entity看着玩家
            EntityWorldPos entityPos = AttachedObject.GetValue(EntityWorldPositionComponent.EntityWorldPositionProperty);
            ChunkWorldPos chunkPos = entityPos.ToChunkWorldPos();
            IChunkTrackingHub tracker = GrainFactory.GetGrain<IChunkTrackingHub>(AttachedObject.GetAddressByPartitionKey());
            var list = await tracker.GetTrackedPlayers();

            // TODO 多位玩家的话只看一位
            foreach (IPlayer each in list)
            {
                EntityWorldPos playerPosition = await each.GetPosition();

                // 三格内玩家
                if (EntityWorldPos.Distance(playerPosition, entityPos) < 3)
                {
                    (var yaw, var pitch) = VectorToYawAndPitch(entityPos, playerPosition);

                    AttachedObject.SetLocalValue(EntityLookComponent.YawProperty, yaw);
                    AttachedObject.SetLocalValue(EntityLookComponent.HeadYawProperty, yaw);
                    AttachedObject.SetLocalValue(EntityLookComponent.PitchProperty, pitch);
                    break;
                }
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

        private async Task GenerateEvent()
        {
            // get state
            var state = _ai.State;
            var nextEvent = CreatureEvent.Nothing;

            // player approaching event
            if (state == CreatureState.Stop)
            {
                IChunkTrackingHub tracker = GrainFactory.GetGrain<IChunkTrackingHub>(AttachedObject.GetAddressByPartitionKey());
                var list = await tracker.GetTrackedPlayers();
                if (list.Count != 0)
                {
                    nextEvent = CreatureEvent.PlayerApproaching;
                }
            }

            // random walk
            if (state == CreatureState.Stop && random.Next(10) == 0)
            {
                nextEvent = CreatureEvent.RandomWalk;
            }

            // stop
            if (state == CreatureState.Walk && random.Next(30) == 0)
            {
                nextEvent = CreatureEvent.Stop;
            }
            else if (state == CreatureState.Look && random.Next(10) == 0)
            {
                nextEvent = CreatureEvent.Stop;
            }

            _ai.Fire(nextEvent);
        }

        private async Task OnGameTick(object sender, GameTickArgs e)
        {
            if (_ai == null) return;
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
            await GenerateEvent();

            // get state
            var newState = _ai.State;
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
                    await ActionFollow();
                    break;
                case CreatureState.Walk:
                    await ActionWalk();
                    break;
                case CreatureState.Stop:
                    await ActionStop();
                    break;
                case CreatureState.Look:
                    await ActionLook();
                    break;
                default:
                    System.Console.WriteLine(newState);
                    throw new NotSupportedException("Unsupported state.");
            }
        }

        public static (float, float) VectorToYawAndPitch(Vector3 from, Vector3 to)
        {
            Vector3 v = to - from;
            v = Vector3.Normalize(v);

            double tmpYaw = -Math.Atan2(v.X, v.Z) / Math.PI * 180;
            if (tmpYaw < 0)
                tmpYaw = 360 + tmpYaw;
            double tmpPitch = -Math.Asin(v.Y) / Math.PI * 180;

            // byte yaw = (byte)(tmpYaw * 255 / 360);
            // byte pitch = (byte)(tmppitch * 255 / 360);
            return ((float)tmpYaw, (float)tmpPitch);
        }
    }
}