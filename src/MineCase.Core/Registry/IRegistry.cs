using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase.Registry
{
    public interface IRegistry<TK, TV>
    {
        TV GetObject(TK name);

        // Register an object on this registry.
        void PutObject(TK key, TV value);
    }
}
