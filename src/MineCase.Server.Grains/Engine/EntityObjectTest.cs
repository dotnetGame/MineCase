using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Util.Math;

namespace MineCase.Server.Engine
{
    public class EntityObjectTest : BigWorldObject
    {
        private BigWorldPropertyEntry<int> _viewDistance;

        public EntityObjectTest()
        {
            _viewDistance = Properties.Register<int>("ViewDistance");
        }

        public Task<int> GetViewDistance()
        {
            return Task.FromResult(Properties.GetValue(_viewDistance));
        }
    }
}
