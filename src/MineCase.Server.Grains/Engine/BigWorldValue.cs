using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase.Server.Engine
{
    public class IBigWorldValue
    {
    }

    public class BigWorldValue<T> : IBigWorldValue
    {
        public T Value { get; set; }
    }
}
