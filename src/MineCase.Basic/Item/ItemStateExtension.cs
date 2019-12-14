using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase.Item
{
    public static class ItemStateExtension
    {
        public static bool IsTool(this ItemState item)
        {
            return item.IsPickaxe() || item.IsAxe();
        }

        public static bool IsPickaxe(this ItemState item)
        {
            return item == ItemStates.DiamondPickaxe() ||
                item == ItemStates.GoldenPickaxe() ||
                item == ItemStates.IronPickaxe() ||
                item == ItemStates.StonePickaxe() ||
                item == ItemStates.WoodenPickaxe();
        }

        public static bool IsAxe(this ItemState item)
        {
            return item == ItemStates.DiamondAxe() ||
                item == ItemStates.GoldenAxe() ||
                item == ItemStates.IronAxe() ||
                item == ItemStates.StoneAxe() ||
                item == ItemStates.WoodenAxe();
        }
    }
}
