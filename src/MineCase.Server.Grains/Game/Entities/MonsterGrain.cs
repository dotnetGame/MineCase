using System;
using System.Collections.Generic;
using System.Numerics;
using System.Threading.Tasks;
using MineCase.Engine;
using MineCase.Server.Components;
using MineCase.Server.Game.Entities.Components;
using MineCase.Server.World;
using MineCase.Server.World.EntitySpawner;
using MineCase.World;

namespace MineCase.Server.Game.Entities
{
    internal class MonsterGrain : EntityGrain, IMonster
    {
        /*
        private string _name;

        private uint _health;
        private const uint MaxHealth = 20;

        private Vector3 _prevPosition;
        private byte _pitch;
        private byte _yaw;
        private bool _onGround;

        private MobType _mobType;

        private Queue<CreatureTask> _tasks;
        */

        protected override async Task InitializeComponents()
        {
            await base.InitializeComponents();
            await SetComponent(new BlockPlacementComponent());  // 末影人
            await SetComponent(new DiggingComponent()); // 末影人
            await SetComponent(new EntityLifeTimeComponent());
            await SetComponent(new EntityOnGroundComponent());
            await SetComponent(new HealthComponent());
            await SetComponent(new StandaloneHeldItemComponent());
            await SetComponent(new NameComponent());
        }

        public async override Task OnActivateAsync()
        {
            await base.OnActivateAsync();
            await this.SetLocalValue(HealthComponent.MaxHealthProperty, 20u);
            await this.SetLocalValue(FoodComponent.MaxFoodProperty, 20u);
            await this.SetLocalValue(HealthComponent.HealthProperty, GetValue(HealthComponent.MaxHealthProperty));
            await this.SetLocalValue(FoodComponent.FoodProperty, GetValue(FoodComponent.MaxFoodProperty));
        }

        /*
        public async Task OnCreated()
        {
            var chunkPos = new EntityWorldPos(Position.X, Position.Y, Position.Z).ToChunkWorldPos();
            var tracker = GrainFactory.GetGrain<IChunkTrackingHub>(World.MakeChunkTrackingHubKey(chunkPos.X, chunkPos.Z));
            await tracker.Subscribe(this);
        }

        public async Task Destroy()
        {
            var chunkPos = new EntityWorldPos(Position.X, Position.Y, Position.Z).ToChunkWorldPos();
            var tracker = GrainFactory.GetGrain<IChunkTrackingHub>(World.MakeChunkTrackingHubKey(chunkPos.X, chunkPos.Z));
            await tracker.Unsubscribe(this);
        }

        public Task Move(Vector3 pos)
        {
            _prevPosition = Position;
            SetPosition(pos);
            return Task.CompletedTask;
        }

        public Task Look(Vector3 pos)
        {
            Vector3 v = pos - Position;
            Vector3.Normalize(v);

            double tmpYaw = -Math.Atan2(v.X, v.Z) / Math.PI * 180;
            if (tmpYaw < 0)
                tmpYaw = 360 - tmpYaw;
            double tmppitch = -Math.Asin(v.Y) / Math.PI * 180;

            _yaw = (byte)(tmpYaw * 255 / 360);
            _pitch = (byte)(tmppitch * 255 / 360);
            return Task.CompletedTask;
        }

        public Task SetMobType(MobType type)
        {
            _mobType = type;
            return Task.CompletedTask;
        }

        public Task SetName(string name)
        {
            _name = name;
            return Task.CompletedTask;
        }

        public Task SetLook(byte yaw, byte pitch, bool onGround)
        {
            _pitch = pitch;
            _yaw = yaw;
            _onGround = onGround;
            return Task.CompletedTask;
        }

        public Task Attacked(int attackPowerValue)
        {
            _health -= (uint)attackPowerValue;
            if (_health <= 0)
            {
                // TODO 生物死亡
            }

            return Task.CompletedTask;
        }

        public async Task Spawn(Guid uuid, Vector3 position, MobType type)
        {
            UUID = uuid;
            _health = MaxHealth;
            _mobType = type;
            await SetPosition(position);
            await GetBroadcastGenerator().SpawnMob(EntityId, uuid, (byte)_mobType, position, 0, 0, new EntityMetadata.Entity { });

            var chunkPos = GetChunkPosition();
            await GrainFactory.GetGrain<IEntityFinder>(World.MakeEntityFinderKey(chunkPos.x, chunkPos.z)).Register(this);
        }

        public async Task OnGameTick(TimeSpan deltaTime, long worldAge)
        {
            ++_yaw;
            if (_yaw % 20 == 0)
            {
                await GetBroadcastGenerator().EntityLook(EntityId, _yaw, _pitch, _onGround);
            }
        }

        protected Task<Vector3> GetDeltaPosition(Vector3 current, Vector3 prev)
        {
            return Task.FromResult(
                Vector3.Multiply(
                    Vector3.Subtract(
                        Vector3.Multiply(current, 32),
                        Vector3.Multiply(prev, 32)),
                    128));
        }

        public Task AddTask(CreatureTask task)
        {
            _tasks.Enqueue(task);
            return Task.CompletedTask;
        }

        public Task RemoveAllTask()
        {
            _tasks.Clear();
            return Task.CompletedTask;
        }

        public Task<CreatureTask> GetCreatureTask()
        {
            throw new NotImplementedException();
        }
        */
    }
}
