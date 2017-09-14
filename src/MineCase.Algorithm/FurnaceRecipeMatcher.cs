using System.Collections.Generic;

namespace MineCase.Algorithm
{
    public class FurnaceRecipeMatcher
    {
        private List<FurnaceRecipe> _recipes;
        private List<FurnaceFuel> _fuels;

        public FurnaceRecipeMatcher(List<FurnaceRecipe> recipes, List<FurnaceFuel> fuels)
        {
            _recipes = recipes;
            _fuels = fuels;
        }

        public FurnaceRecipe FindRecipe(Slot input)
        {
            if (input.IsEmpty) return null;

            foreach (var item in _recipes)
            {
                if (item.Input.BlockId == input.BlockId
                    && (item.Input.ItemDamage == -1 || item.Input.ItemDamage == input.ItemDamage)
                    && item.Input.ItemCount <= input.ItemCount)
                {
                    return item;
                }
            }

            return null;
        }

        public FurnaceFuel FindFuel(Slot fuel)
        {
            if (fuel.IsEmpty) return null;

            foreach (var item in _fuels)
            {
                if (item.Slot.BlockId == fuel.BlockId
                    && (item.Slot.ItemDamage == -1 || item.Slot.ItemDamage == fuel.ItemDamage)
                    && item.Slot.ItemCount <= fuel.ItemCount)
                {
                    return item;
                }
            }

            return null;
        }
    }
}
