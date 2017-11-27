﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Server.Game;
using MineCase.Server.Settings;
using MineCase.Server.World.Generation;
using MineCase.World.Generation;
using Orleans;
using Orleans.Concurrency;

namespace MineCase.Server.World
{
    [Reentrant]
    internal class WorldGrain : Grain, IWorld
    {
        private uint _nextAvailEId;
        private long _worldAge;
        private GeneratorSettings _genSettings; // 生成设置
        private string _seed; // 世界种子
        private HashSet<IWorldPartition> _activedPartitions;

        public override async Task OnActivateAsync()
        {
            IServerSettings serverSettings = GrainFactory.GetGrain<IServerSettings>(0);
            _nextAvailEId = 0;
            _genSettings = new GeneratorSettings();
            await InitGeneratorSettings(_genSettings);
            _seed = (await serverSettings.GetSettings()).LevelSeed;
            _activedPartitions = new HashSet<IWorldPartition>();
            await base.OnActivateAsync();
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
            foreach (var partition in _activedPartitions)
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
            _activedPartitions.Add(worldPartition);
            return Task.CompletedTask;
        }

        public Task DeactivePartition(IWorldPartition worldPartition)
        {
            _activedPartitions.Remove(worldPartition);
            return Task.CompletedTask;
        }
    }
}
