using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MineCase
{
    public class DropBlockMappingLoader
    {
        public Dictionary<BlockState, DropBlockEntry> Mapping { get; } = new Dictionary<BlockState, DropBlockEntry>();

        public async Task LoadMapping(StreamReader streamReader)
        {
            while (!streamReader.EndOfStream)
            {
                var line = await streamReader.ReadLineAsync();
                ParseLine(line);
            }
        }

        private unsafe void ParseLine(string line)
        {
            var lineSpan = line.AsSpan();
            var commentIndex = lineSpan.IndexOf('#');
            if (commentIndex != -1)
                lineSpan = lineSpan.Slice(0, commentIndex);
            if (lineSpan.IsEmpty) return;

            char* buffer = stackalloc char[lineSpan.Length];
            int bufLen = 0;

            // 删除空格
            {
                for (int i = 0; i < lineSpan.Length; i++)
                {
                    if (!char.IsWhiteSpace(lineSpan[i]))
                        buffer[bufLen++] = lineSpan[i];
                }
            }

            var normLine = new Span<char>(buffer, bufLen);
            if (normLine.IsEmpty) return;
            var splitter = normLine.IndexOf('=');

            var resultSpan = normLine.Slice(0, splitter);
            var ingredientsSpan = normLine.Slice(splitter + 1);

            var recipe = new CraftingRecipe();
            var resultSplitter = resultSpan.IndexOf(',');
            if (resultSplitter == -1)
            {
                ParseItem(resultSpan, ref recipe.Output);
                recipe.Output.ItemCount = 1;
            }
            else
            {
                ParseItem(resultSpan.Slice(0, resultSplitter), ref recipe.Output);
                recipe.Output.ItemCount = byte.Parse(ToString(resultSpan.Slice(resultSplitter + 1)));
            }

            var recipeSlots = new List<CraftingRecipeSlot>();
            var restSpan = ingredientsSpan;
            while (!restSpan.IsEmpty)
            {
                var ingredientSplitter = restSpan.IndexOf('|');
                var ingredientSpan = ingredientSplitter == -1 ? restSpan : restSpan.Slice(0, ingredientSplitter);
                ParseIngredient(ingredientSpan, recipeSlots);
                if (ingredientSplitter == -1)
                    break;
                else
                    restSpan = restSpan.Slice(ingredientSplitter + 1);
            }

            recipe.Inputs = recipeSlots.ToArray();
            NormalizeIngredients(recipe);

            // Recipes.Add(recipe);
        }

        private void ParseIngredient(Span<char> ingredientSpan, ICollection<CraftingRecipeSlot> recipeSlots)
        {
            var slot = new Slot { ItemCount = 1 };
            var splitter = ingredientSpan.IndexOf(',');
            ParseItem(ingredientSpan.Slice(0, splitter), ref slot);
            var distributionSpan = ingredientSpan.Slice(splitter + 1);

            do
            {
                var positionSplitter = distributionSpan.IndexOf(',');
                var positionSpan = positionSplitter == -1 ? distributionSpan : distributionSpan.Slice(0, positionSplitter);

                var recipeSlot = new CraftingRecipeSlot { Slot = slot };
                if (positionSpan.Length == 1 && positionSpan[0] == '*')
                {
                    recipeSlot.X = -1;
                    recipeSlot.Y = -1;
                }
                else
                {
                    splitter = positionSpan.IndexOf(':');

                    int ParsePoint(ReadOnlySpan<char> span)
                    {
                        if (span.Length != 1)
                            throw new ArgumentOutOfRangeException(nameof(span));

                        switch (span[0])
                        {
                            case '1':
                                return 0;
                            case '2':
                                return 1;
                            case '3':
                                return 2;
                            case '*':
                                return -1;
                            default:
                                throw new ArgumentOutOfRangeException(nameof(span));
                        }
                    }

                    recipeSlot.X = ParsePoint(positionSpan.Slice(0, splitter));
                    recipeSlot.Y = ParsePoint(positionSpan.Slice(splitter + 1));
                }

                recipeSlots.Add(recipeSlot);

                if (positionSplitter == -1)
                    break;
                else
                    distributionSpan = distributionSpan.Slice(positionSplitter + 1);
            }
            while (true);
        }

        private void NormalizeIngredients(CraftingRecipe recipe)
        {
            int minX = 2, minY = 2;
            int maxX = 0, maxY = 0;

            for (int i = 0; i < recipe.Inputs.Length; i++)
            {
                ref var recipeSlot = ref recipe.Inputs[i];
                if (recipeSlot.X >= 0)
                {
                    minX = Math.Min(minX, recipeSlot.X);
                    maxX = Math.Max(maxX, recipeSlot.X);
                }

                if (recipeSlot.Y >= 0)
                {
                    minY = Math.Min(minY, recipeSlot.Y);
                    maxY = Math.Max(maxY, recipeSlot.Y);
                }
            }

            // 移动到左上角
            for (int i = 0; i < recipe.Inputs.Length; i++)
            {
                ref var recipeSlot = ref recipe.Inputs[i];
                if (recipeSlot.X >= 0)
                    recipeSlot.X -= minX;

                if (recipeSlot.Y >= 0)
                    recipeSlot.Y -= minY;
            }

            recipe.Width = Math.Max(1, maxX - minX + 1);
            recipe.Height = Math.Max(1, maxY - minY + 1);
        }

        private void ParseItem(Span<char> span, ref Slot slot)
        {
            var metaSplitter = span.IndexOf('^');
            var idSpan = metaSplitter == -1 ? span : span.Slice(0, metaSplitter);

            bool isBlock;
            var text = ToString(idSpan);
            if (Enum.TryParse(text, out ItemId item))
            {
                slot.BlockId = (short)item;
                isBlock = false;
            }
            else if (Enum.TryParse(text, out BlockId block))
            {
                slot.BlockId = (short)block;
                isBlock = true;
            }
            else
            {
                throw new ArgumentOutOfRangeException($"Invalid item name: {text}.");
            }

            if (metaSplitter != -1)
            {
                var metaText = ToString(span.Slice(metaSplitter + 1));
                if (short.TryParse(metaText, out var value))
                {
                    slot.ItemDamage = value;
                }
                else
                {
                    var itemsType = isBlock ? typeof(BlockStates) : typeof(ItemStates);
                    var paramEnum = itemsType.GetMethod(text).GetParameters()[0].ParameterType;
                    slot.ItemDamage = Convert.ToInt16(Enum.Parse(paramEnum, metaText));
                }
            }
            else
            {
                slot.ItemDamage = 0;
            }
        }

        private static unsafe string ToString(ReadOnlySpan<char> span)
        {
            return new string((char*)Unsafe.AsPointer(ref span.DangerousGetPinnableReference()), 0, span.Length);
        }
    }

    public enum Tools
    {
        Hand,
        Axes,
        PickAxes,
        Shovels,
        Hoes
    }

    public enum ToolMaterial
    {
        Hand,
        Wooden,
        Stone,
        Iron,
        Diamand,
        Golden
    }

    public class DropBlockEntry
    {
        public BlockState TargetBlock;
        public BlockState DroppedBlock;
        public float Hardness;
        public Tools Tool;
        public float Shears;
        public float Sword;
    }
}
