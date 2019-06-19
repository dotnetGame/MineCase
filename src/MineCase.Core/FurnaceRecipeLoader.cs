using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using MineCase.Block;
using MineCase.Item;

namespace MineCase
{
    public class FurnaceRecipeLoader
    {
        public List<FurnaceRecipe> Recipes { get; } = new List<FurnaceRecipe>();

        public List<FurnaceFuel> Fuels { get; } = new List<FurnaceFuel>();

        public async Task LoadRecipes(StreamReader streamReader)
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

            if (normLine[0] == '!')
                ParseFuel(normLine.Slice(1));
            else
                ParseRecipe(normLine);
        }

        private void ParseFuel(ReadOnlySpan<char> line)
        {
            var splitter = line.IndexOf('=');

            var fuelSpan = line.Slice(0, splitter);
            var timeSpan = line.Slice(splitter + 1);

            var fuel = new FurnaceFuel();
            ParseItem(fuelSpan, ref fuel.Slot);
            fuel.Time = int.Parse(ToString(timeSpan));
            Fuels.Add(fuel);
        }

        private void ParseRecipe(ReadOnlySpan<char> line)
        {
            var splitter = line.IndexOf('=');

            var ingredientsSpan = line.Slice(0, splitter);
            var resultSpan = line.Slice(splitter + 1);

            var recipe = new FurnaceRecipe();
            var ingredientSplitter = resultSpan.IndexOf('@');
            if (ingredientSplitter == -1)
            {
                ParseItem(ingredientsSpan, ref recipe.Input);
                recipe.Time = 200;
            }
            else
            {
                ParseItem(ingredientsSpan.Slice(0, ingredientSplitter), ref recipe.Output);
                recipe.Time = int.Parse(ToString(ingredientsSpan.Slice(ingredientSplitter + 1)));
            }

            ParseItem(resultSpan, ref recipe.Output);
            Recipes.Add(recipe);
        }

        private void ParseItem(ReadOnlySpan<char> span, ref Slot slot)
        {
            var countSplitter = span.IndexOf(',');
            slot.ItemCount = countSplitter == -1 ? (byte)1 : byte.Parse(ToString(span.Slice(countSplitter + 1)));

            var itemSpan = countSplitter == -1 ? span : span.Slice(0, countSplitter);
            var metaSplitter = itemSpan.IndexOf('^');
            var idSpan = metaSplitter == -1 ? itemSpan : itemSpan.Slice(0, metaSplitter);

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
            return new string((char*)Unsafe.AsPointer(ref MemoryMarshal.GetReference(span)), 0, span.Length);
        }
    }

    public class FurnaceRecipe
    {
        public Slot Input;
        public int Time;
        public Slot Output;
    }

    public class FurnaceFuel
    {
        public Slot Slot;
        public int Time;
    }
}
