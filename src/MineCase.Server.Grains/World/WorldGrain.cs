using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Server.Game;
using MineCase.Server.Persistence;
using MineCase.Server.Persistence.Components;
using MineCase.Server.Settings;
using MineCase.World.Generation;
using Orleans;
using Orleans.Concurrency;

namespace MineCase.Server.World
{
    [PersistTableName("world")]
    [Reentrant]
    internal class WorldGrain : PersistableDependencyObject, IWorld
    {
        private uint _nextAvailEId;
        private long _worldAge;
        private GeneratorSettings _genSettings; // 生成设置
        private string _seed; // 世界种子

        private StateHolder State => GetValue(StateComponent<StateHolder>.StateProperty);

        protected override async Task InitializePreLoadComponent()
        {
            await SetComponent(new StateComponent<StateHolder>());

            var serverSettings = GrainFactory.GetGrain<IServerSettings>(0);
            _nextAvailEId = 0;
            _genSettings = new GeneratorSettings();
            await InitGeneratorSettings(_genSettings);
            _seed = (await serverSettings.GetSettings()).LevelSeed;
        }

        public Task<WorldTime> GetTime()
        {
            return Task.FromResult(new WorldTime { WorldAge = _worldAge, TimeOfDay = _worldAge % 24000 });
        }

        public Task<uint> NewEntityId()
        {
            var id = _nextAvailEId++;
            return Task.FromResult(id);
        }

        public Task OnGameTick(TimeSpan deltaTime)
        {
            _worldAge++;
            foreach (var partition in State.ActivedPartitions)
                partition.InvokeOneWay(p => p.OnGameTick(deltaTime, _worldAge));
            return Task.CompletedTask;
        }

        public Task<long> GetAge() => Task.FromResult(_worldAge);

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
            State.ActivedPartitions.Add(worldPartition);
            return Task.CompletedTask;
        }

        public Task DeactivePartition(IWorldPartition worldPartition)
        {
            State.ActivedPartitions.Remove(worldPartition);
            return Task.CompletedTask;
        }

        internal class StateHolder
        {
            public HashSet<IWorldPartition> ActivedPartitions { get; set; }

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
