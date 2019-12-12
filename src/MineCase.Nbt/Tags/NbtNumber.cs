using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase.Nbt.Tags
{
    public abstract class NbtNumber : NbtTag
    {
        public abstract long GetLong();

        public abstract int GetInt();

        public abstract short GetShort();

        public abstract byte GetByte();

        public abstract double GetDouble();

        public abstract float GetFloat();
    }
}
