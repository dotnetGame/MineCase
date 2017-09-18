using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MineCase.Engine
{
    public interface IDependencyObject
    {
        Task Tell(IEntityMessage message);

        Task<TResponse> Ask<TResponse>(IEntityMessage<TResponse> message);
    }
}
