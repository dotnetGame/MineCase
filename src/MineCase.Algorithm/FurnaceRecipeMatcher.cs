using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase.Algorithm
{
    public class FindFurnaceRecipeResult
    {
        public FurnaceRecipe Recipe { get; set; }

        public FurnaceFuel Fuel { get; set; }
    }

    public class FurnaceRecipeMatcher
    {
        private List<FurnaceRecipe> _recipes;
        private List<FurnaceFuel> _fuels;

        public FurnaceRecipeMatcher(List<FurnaceRecipe> recipes, List<FurnaceFuel> fuels)
        {
            _recipes = recipes;
            _fuels = fuels;
        }

        public FindFurnaceRecipeResult FindRecipe(Slot input, Slot fuel)
        {
            if (input.IsEmpty || fuel.IsEmpty) return null;

            FurnaceRecipe recipe = null;
            foreach (var item in _recipes)
            {
                if (item.Input.BlockId == input.BlockId
                    && (item.Input.ItemDamage == -1 || item.Input.ItemDamage == input.ItemDamage)
                    && item.Input.ItemCount <= input.ItemCount)
                {
                    recipe = item;
                    break;
                }
            }

            if (recipe == null) return null;

            foreach (var item in _fuels)
            {
                if (item.Slot.BlockId == input.BlockId
                    && (item.Slot.ItemDamage == -1 || item.Slot.ItemDamage == input.ItemDamage)
                    && item.Slot.ItemCount <= input.ItemCount)
                {
                    return new FindFurnaceRecipeResult
                    {
                        Recipe = recipe,
                        Fuel = item
                    };
                }
            }

            return null;
        }
    }
}
