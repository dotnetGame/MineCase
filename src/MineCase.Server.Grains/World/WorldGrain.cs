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
        private AutoSaveStateComponent _autoSave;

        private StateHolder State => GetValue(StateComponent<StateHolder>.StateProperty);

        protected override async Task InitializePreLoadComponent()
        {
            await SetComponent(new StateComponent<StateHolder>());

            var serverSettings = GrainFactory.GetGrain<IServerSettings>(0);
            _genSettings = new GeneratorSettings();
            await InitGeneratorSettings(_genSettings);
            _seed = (await serverSettings.GetSettings()).LevelSeed;
        }

        protected override async Task InitializeComponents()
        {
            _autoSave = new AutoSaveStateComponent(AutoSaveStateComponent.PerMinute);
            await SetComponent(_autoSave);
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

        public async Task OnGameTick(GameTickArgs e)
        {
            State.WorldAge++;
            MarkDirty();
            await Task.WhenAll(from p in State.ActivedPartitions select p.OnGameTick(e));
            await _autoSave.OnGameTick(this, e);
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
            if (State.ActivedPartitions.Add(worldPartition))
                MarkDirty();
            return Task.CompletedTask;
        }

        public Task DeactivePartition(IWorldPartition worldPartition)
        {
            if (State.ActivedPartitions.Remove(worldPartition))
                MarkDirty();
            return Task.CompletedTask;
        }

        private void MarkDirty()
        {
            _autoSave.IsDirty = true;
        }

        internal class StateHolder
        {
            public HashSet<IWorldPartition> ActivedPartitions { get; set; }

            public long WorldAge { get; set; }

            public uint NextAvailEId { get; set; }

            public StateHolder()
            {
            }

            public StateHolder(InitializeStateMark mark)
            {
                ActivedPartitions = new HashSet<IWorldPartition>();
            }
        }
    }
}
