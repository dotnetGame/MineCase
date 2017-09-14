using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using MineCase.Server.World;
using MineCase.World;

namespace MineCase.Server.Game.Entities
{
    internal class PassiveMobGrain : EntityGrain, IPassiveMob
    {
        private string _name;

        private uint _health;
        private const uint MaxHealth = 20;

        private Vector3 _prevPosition;
        private byte _pitch;
        private byte _yaw;
        private bool _onGround;

        private MobType _mobType;

        public override Task OnActivateAsync()
        {
            _pitch = 0;
            _yaw = 0;
            _onGround = false;
            return base.OnActivateAsync();
        }

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
            _prevPosition = Position;
            await GetBroadcastGenerator().SpawnMob(EntityId, uuid, (byte)_mobType, position, 0, 0, new EntityMetadata.Entity { });

            var chunkPos = GetChunkPosition();
            await GrainFactory.GetGrain<IEntityFinder>(World.MakeEntityFinderKey(chunkPos.x, chunkPos.z)).Register(this);
        }

        public async Task OnGameTick(TimeSpan deltaTime, long worldAge)
        {
            if (worldAge % 32 == 0)
            {
                await GetBroadcastGenerator().EntityHeadLook(EntityId, _yaw);
                await GetBroadcastGenerator().EntityLook(EntityId, _yaw, _pitch, _onGround);
                Vector3 delta = await GetDeltaPosition(Position, _prevPosition);
                await GetBroadcastGenerator().EntityRelativeMove(EntityId, (short)delta.X, (short)delta.Y, (short)delta.Z, _onGround);
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
    }
}
