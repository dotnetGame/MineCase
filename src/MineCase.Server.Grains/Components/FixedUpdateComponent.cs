using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MineCase.Engine;
using MineCase.Server.World;
using MineCase.World;

namespace MineCase.Server.Components
{
    public class FixedUpdateComponent : Component
    {
        private IDisposable _tickTimer;
        private Stopwatch _stopwatch;
        private long _worldAge;
        private long _actualAge;
        private TimeSpan _lastUpdate;
        private static readonly long _updateMs = 50;

        public event AsyncEventHandler<GameTickArgs> Tick;

        public FixedUpdateComponent(string name = "fixedUpdate")
            : base(name)
        {
        }

        public async Task Start(IWorld world)
        {
            _tickTimer?.Dispose();
            _worldAge = await world.GetAge();
            _actualAge = 0;
            _stopwatch = new Stopwatch();
            _stopwatch.Start();
            _tickTimer = AttachedObject.RegisterTimer(OnTick, null, TimeSpan.Zero, TimeSpan.FromMilliseconds(1));
        }

        private async Task OnTick(object arg)
        {
            var expectedAge = (_stopwatch.ElapsedMilliseconds + _updateMs - 1) / _updateMs;
            var updateTimes = expectedAge - _actualAge;

            if (updateTimes > 0)
            {
                var e = new GameTickArgs { DeltaTime = TimeSpan.FromMilliseconds(50) };
                for (int i = 0; i < updateTimes; i++)
                {
                    e.WorldAge = _worldAge;
                    e.TimeOfDay = _worldAge % 24000;
                    await Tick.InvokeSerial(this, e);
                    _worldAge++;
                    _actualAge++;
                }

                var now = _stopwatch.Elapsed;
                var deltaTime = now - _lastUpdate;
                _lastUpdate = now;
            }
        }

        public void Stop()
        {
            _tickTimer?.Dispose();
            _tickTimer = null;
        }
    }
}
