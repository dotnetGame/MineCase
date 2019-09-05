using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase.Engine
{
    public interface ICleanupSystem : ISystem
    {
        void Cleanup();
    }
}
