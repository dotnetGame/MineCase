using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Orleans;

namespace MineCase.Server.Game
{
    public interface IFurnaceRecipes : IGrainWithIntegerKey
    {
        Task<FurnaceRecipe> FindRecipe(Slot input);

        Task<FurnaceFuel> FindFuel(Slot fuel);
    }
}
