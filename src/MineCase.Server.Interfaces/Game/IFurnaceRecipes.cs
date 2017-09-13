using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Algorithm;
using Orleans;

namespace MineCase.Server.Game
{
    public interface IFurnaceRecipes : IGrainWithIntegerKey
    {
        Task<FindFurnaceRecipeResult> FindRecipe(Slot input, Slot fuel);
    }
}
