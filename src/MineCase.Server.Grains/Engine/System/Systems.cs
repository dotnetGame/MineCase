using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase.Engine
{
    public class Systems
    {
        private List<IInitializeSystem> _initializeSystems;
        private List<IExecuteSystem> _executeSystems;
        private List<ICleanupSystem> _cleanupSystems;
        private List<ITearDownSystem> _tearDownSystems;
        public Systems()
        {
            _initializeSystems = new List<IInitializeSystem>();
            _executeSystems = new List<IExecuteSystem>();
            _cleanupSystems = new List<ICleanupSystem>();
            _tearDownSystems = new List<ITearDownSystem>();
        }

        public void Add(ISystem system)
        {
            if (system is IInitializeSystem)
                _initializeSystems.Add((IInitializeSystem)system);
            else if (system is IExecuteSystem)
                _executeSystems.Add((IExecuteSystem)system);
            else if (system is ICleanupSystem)
                _cleanupSystems.Add((ICleanupSystem)system);
            else if (system is ITearDownSystem)
                _tearDownSystems.Add((ITearDownSystem)system);
            else
                throw new ArgumentException("Unknown system type");
        }
        
    }
}
