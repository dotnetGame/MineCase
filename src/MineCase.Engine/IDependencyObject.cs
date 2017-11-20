using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Orleans;
using Orleans.Concurrency;

namespace MineCase.Engine
{
    [Immutable]
    public sealed class AskResult<TResponse>
    {
        public static readonly AskResult<TResponse> Failed = new AskResult<TResponse> { Succeeded = false };

        public bool Succeeded;

        public TResponse Response;
    }

    public interface IDependencyObject : IGrain
    {
        Task Tell(IEntityMessage message);

        Task<TResponse> Ask<TResponse>(IEntityMessage<TResponse> message);

        Task<AskResult<TResponse>> TryAsk<TResponse>(IEntityMessage<TResponse> message);
    }
}
