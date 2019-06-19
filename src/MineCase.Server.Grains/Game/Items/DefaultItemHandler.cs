using System;
using System.Collections.Generic;
using System.Text;
using MineCase.Item;

namespace MineCase.Server.Game.Items
{
    public class DefaultItemHandler : ItemHandler
    {
        public DefaultItemHandler()
            : base(new ItemState { Id = 0, MetaValue = 0 })
        {
        }

        public DefaultItemHandler(ItemState item)
            : base(item)
        {
        }

        public override bool IsUsable => false;

        public override bool IsPlaceable => true;
    }
}
