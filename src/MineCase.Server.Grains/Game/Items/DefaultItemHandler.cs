using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase.Server.Game.Items
{
    public class DefaultItemHandler : ItemHandler
    {
        public DefaultItemHandler()
            : base(0)
        {
        }

        public override bool IsUsable => false;

        public override bool IsPlaceable => true;
    }
}
