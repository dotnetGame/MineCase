using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MineCase.Server.Game;
using MineCase.Server.Persistence;
using MineCase.Server.Persistence.Components;
using MineCase.Server.Settings;
using MineCase.World;
using MineCase.World.Generation;
using Orleans;
using Orleans.Concurrency;

namespace MineCase.Server.World
{
    [PersistTableName("world")]
    [Reentrant]
    internal class WorldGrain : PersistableDependencyObject, IWorld
    {
        private GeneratorSettings _genSettings; // 生成设置
        private string _seed; // 世界种子

        // private EntityWorldPos _spawnPos; // 出生点
        private readonly HashSet<IWorldPartition> _activedPartitions = new HashSet<IWorldPartition>();

        private StateHolder State => GetValue(StateComponent<StateHolder>.StateProperty);

        protected override void InitializePreLoadComponent()
        {
            SetComponent(new StateComponent<StateHolder>());
        }

        protected override void InitializeComponents()
        {
            SetComponent(new PeriodicSaveStateComponent(TimeSpan.FromMinutes(1)));
        }

        public override async Task OnActivateAsync()
        {
            await base.OnActivateAsync();

            var serverSettings = GrainFactory.GetGrain<IServerSettings>(0);
            _genSettings = new GeneratorSettings();
            await InitGeneratorSettings(_genSettings);
            _seed = (await serverSettings.GetSettings()).LevelSeed;
        }

        public Task<WorldTime> GetTime()
        {
            return Task.FromResult(new WorldTime { WorldAge = State.WorldAge, TimeOfDay = State.WorldAge % 24000 });
        }

        public Task<uint> NewEntityId()
        {
            var id = State.NextAvailEId++;
            MarkDirty();
            return Task.FromResult(id);
        }

        public Task OnGameTick(GameTickArgs e)
        {
            State.WorldAge++;
            MarkDirty();
            return Task.CompletedTask;
        }

        public Task<long> GetAge() => Task.FromResult(State.WorldAge);

        public Task<int> GetSeed()
        {
            int result = 0;
            foreach (char c in _seed)
            {
                result = result * 127 + (int)c;
            }

            return Task.FromResult(result);
        }

        public Task<GeneratorSettings> GetGeneratorSettings()
        {
            return Task.FromResult(_genSettings);
        }

        private Task InitGeneratorSettings(GeneratorSettings settings)
        {
            IServerSettings serverSettings = GrainFactory.GetGrain<IServerSettings>(0);

            // TODO move server settings to generator settings
            return Task.CompletedTask;
        }

        public Task ActivePartition(IWorldPartition worldPartition)
        {
            _activedPartitions.Add(worldPartition);
            return Task.CompletedTask;
        }

        public Task DeactivePartition(IWorldPartition worldPartition)
        {
            _activedPartitions.Remove(worldPartition);
            return Task.CompletedTask;
        }

        public async Task<EntityWorldPos> GetSpawnPosition()
        {
            EntityWorldPos retval = new EntityWorldPos(8, 256, 8);
            int height = await this.GetHeight(GrainFactory, new BlockWorldPos(8, 0, 8));
            return new EntityWorldPos(8, height, 8);
        }

        private void MarkDirty()
        {
            ValueStorage.IsDirty = true;
        }

        internal class StateHolder
        {
            public long WorldAge { get; set; }

            public uint NextAvailEId { get; set; }

            public StateHolder()
            {
            }

            public StateHolder(InitializeStateMark mark)
            {
            }
        }
    }
}
