using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase
{
    public class FindCraftingRecipeResult
    {
        public Slot Result { get; set; }

        public Slot[,] AfterTake { get; set; }
    }
}
