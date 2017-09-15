using System;
using System.Collections.Generic;
using System.Text;
using Orleans.Concurrency;

namespace MineCase.Engine.Entities
{
    [Reentrant]
    public class EntityGrain : DependencyObject, IEntity
    {
    }
}
