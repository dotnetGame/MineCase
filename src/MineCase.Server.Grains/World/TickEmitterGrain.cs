using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MineCase.Engine;
using MineCase.Server.Components;
using MineCase.Server.Game.Entities;
using MineCase.Server.Persistence;
using MineCase.Server.Persistence.Components;
using MineCase.World;
using Orleans;
using Orleans.Concurrency;
using Orleans.Streams;

namespace MineCase.Server.World
{
    [Reentrant]
    internal class TickEmitterGrain : AddressByPartitionGrain, ITickEmitter
    {
        private ImmutableHashSet<IDependencyObject> _tickables = ImmutableHashSet<IDependencyObject>.Empty;

        private FixedUpdateComponent _fixedUpdate;

        protected override void InitializeComponents()
        {
            SetComponent(new PeriodicSaveStateComponent(TimeSpan.FromMinutes(1)));

            _fixedUpdate = new FixedUpdateComponent();
            _fixedUpdate.Tick += OnFixedUpdate;
            SetComponent(_fixedUpdate);
        }

        private Task OnFixedUpdate(object sender, GameTickArgs e)
        {
            var msg = new GameTick { Args = e };
            return Task.WhenAll(from t in _tickables select t.Tell(msg));
        }

        public async Task Subscribe(IDependencyObject observer)
        {
            bool active = _tickables.IsEmpty;
            _tickables = _tickables.Add(observer);

            if (active)
            {
                await _fixedUpdate.Start(World);
            }
        }

        public Task Unsubscribe(IDependencyObject observer)
        {
            _tickables = _tickables.Remove(observer);
            if (_tickables.IsEmpty)
            {
                _fixedUpdate.Stop();
            }

            return Task.CompletedTask;
        }
    }
}
