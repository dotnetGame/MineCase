using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase.Engine
{
    public interface IEntityMessage
    {
    }

    public interface IEntityMessage<TResponse>
    {
    }

    public class ReceiverNotFoundException : Exception
    {
    }
}
