using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Algorithm;
using Orleans;
using Orleans.Concurrency;

namespace MineCase.Server.Game
{
    public interface ICraftingRecipes : IGrainWithIntegerKey
    {
        Task<FindCraftingRecipeResult> FindRecipe(Immutable<Slot[,]> craftingGrid);
    }
}
