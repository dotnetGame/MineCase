using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Orleans;

namespace MineCase.Server.Game
{
    public class FindCraftingRecipeResult
    {
        public Slot Result { get; set; }

        public Slot[,] AfterTake { get; set; }
    }

    public interface ICraftingRecipes : IGrainWithIntegerKey
    {
        Task<FindCraftingRecipeResult> FindRecipe(Slot[,] craftingGrid);
    }
}
