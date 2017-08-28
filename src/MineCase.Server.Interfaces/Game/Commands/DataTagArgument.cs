using System;
using System.Collections.Generic;
using System.Text;
using MineCase.Nbt.Tags;

namespace MineCase.Server.Game.Commands
{
    public class DataTagArgument : UnresolvedArgument
    {
        public NbtCompound Tag { get; }

        public DataTagArgument(string rawContent)
            : base(rawContent)
        {
            throw new NotImplementedException();
        }
    }
}
