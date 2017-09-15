using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MineCase.Engine
{
    public interface IHandle<TMessage>
        where TMessage : IEntityMessage
    {
        Task Handle(TMessage message);
    }

    public interface IHandle<TMessage, TResponse>
        where TMessage : IEntityMessage<TResponse>
    {
        Task<TResponse> Handle(TMessage message);
    }
}
