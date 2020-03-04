using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase.Block.State
{
    public class BooleanProperty : StateProperty<bool>
    {
        public BooleanProperty()
        {
        }

        public override int StateNumber()
        {
            return 2;
        }

        public override int ToInt(bool value)
        {
            if (value)
                return 1;
            else
                return 0;
        }
    }
}
