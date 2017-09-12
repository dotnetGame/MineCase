using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using MineCase.Server.Network.Play;
using MineCase.Server.World;

namespace MineCase.Server.Game.Entities
{
    internal class MonsterGrain : EntityGrain, IMonster
    {
        private string _name;

        private uint _health;
        private const uint MaxHealth = 20;

        private float _pitch;
        private float _yaw;

        private MobType _mobType;

        public override Task OnActivateAsync()
        {
            return base.OnActivateAsync();
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

        public Task SetLook(float yaw, float pitch, bool onGround)
        {
            _pitch = pitch;
            _yaw = yaw;
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
    }
}
