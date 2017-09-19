using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Orleans;

namespace MineCase.Engine
{
    public interface IDependencyObject : IGrain
    {
        Task Tell(IEntityMessage message);

        Task<TResponse> Ask<TResponse>(IEntityMessage<TResponse> message);
    }
}
