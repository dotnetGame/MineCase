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

            var entry = new DropBlockEntry();
            var resultString = resultSpan.ToString();
            string[] splittedItems = resultString.Split(',');

            if (splittedItems.Length != 11)
                throw new ArgumentOutOfRangeException(nameof(splittedItems));

            Enum.TryParse(splittedItems[0], out BlockId droppedBlock);
            entry.DroppedBlock = new BlockState { Id = (uint)droppedBlock };
            entry.Hardness = float.Parse(splittedItems[1]);
            Enum.TryParse(splittedItems[2], out ItemId tool);
            entry.Tool = new ItemState { Id = (uint)tool };
            entry.Hand = float.Parse(splittedItems[3]);
            entry.Wooden = float.Parse(splittedItems[4]);
            entry.Stone = float.Parse(splittedItems[5]);
            entry.Iron = float.Parse(splittedItems[6]);
            entry.Diamand = float.Parse(splittedItems[7]);
            entry.Golden = float.Parse(splittedItems[8]);
            entry.Shears = float.Parse(splittedItems[9]);
            entry.Sword = float.Parse(splittedItems[10]);

            Enum.TryParse(resultSpan.ToString(), out BlockId targetBlock);
            Mapping.Add(new BlockState { Id = (uint)targetBlock }, entry);
        }

        public static Tools ItemsToTools(ItemState state)
        {
            if ((int)state.Id == -1)
            {
                return Tools.Hand;
            }
            else if (state.Id == (uint)ItemId.WoodenAxe ||
                state.Id == (uint)ItemId.StoneAxe ||
                state.Id == (uint)ItemId.IronAxe ||
                state.Id == (uint)ItemId.GoldenAxe ||
                state.Id == (uint)ItemId.DiamondAxe)
            {
                return Tools.Axes;
            }
            else if (state.Id == (uint)ItemId.WoodenHoe ||
                state.Id == (uint)ItemId.StoneHoe ||
                state.Id == (uint)ItemId.IronHoe ||
                state.Id == (uint)ItemId.GoldenHoe ||
                state.Id == (uint)ItemId.DiamondHoe)
            {
                return Tools.Hoes;
            }
            else if (state.Id == (uint)ItemId.WoodenPickaxe ||
                state.Id == (uint)ItemId.StonePickaxe ||
                state.Id == (uint)ItemId.IronPickaxe ||
                state.Id == (uint)ItemId.GoldenPickaxe ||
                state.Id == (uint)ItemId.DiamondPickaxe)
            {
                return Tools.PickAxes;
            }
            else if (state.Id == (uint)ItemId.WoodenShovel ||
                state.Id == (uint)ItemId.StoneShovel ||
                state.Id == (uint)ItemId.IronShovel ||
                state.Id == (uint)ItemId.GoldenShovel ||
                state.Id == (uint)ItemId.DiamondShovel)
            {
                return Tools.Shovels;
            }
            else if (state.Id == (uint)ItemId.WoodenSword ||
                state.Id == (uint)ItemId.StoneSword ||
                state.Id == (uint)ItemId.IronSword ||
                state.Id == (uint)ItemId.GoldenSword ||
                state.Id == (uint)ItemId.DiamondSword)
            {
                return Tools.Swords;
            }
            else
            {
                return Tools.Hand;
            }
        }

        public static ToolMaterial ItemsToToolMaterial(ItemState state)
        {
            if ((int)state.Id == -1)
            {
                return ToolMaterial.Hand;
            }
            else if (state.Id == (uint)ItemId.WoodenAxe ||
                state.Id == (uint)ItemId.WoodenHoe ||
                state.Id == (uint)ItemId.WoodenPickaxe ||
                state.Id == (uint)ItemId.WoodenShovel ||
                state.Id == (uint)ItemId.WoodenSword)
            {
                return ToolMaterial.Wooden;
            }
            else if (state.Id == (uint)ItemId.StoneAxe ||
                state.Id == (uint)ItemId.StoneHoe ||
                state.Id == (uint)ItemId.StonePickaxe ||
                state.Id == (uint)ItemId.StoneShovel ||
                state.Id == (uint)ItemId.StoneSword)
            {
                return ToolMaterial.Stone;
            }
            else if (state.Id == (uint)ItemId.IronAxe ||
                state.Id == (uint)ItemId.IronHoe ||
                state.Id == (uint)ItemId.IronPickaxe ||
                state.Id == (uint)ItemId.IronShovel ||
                state.Id == (uint)ItemId.IronSword)
            {
                return ToolMaterial.Iron;
            }
            else if (state.Id == (uint)ItemId.GoldenAxe ||
                state.Id == (uint)ItemId.GoldenHoe ||
                state.Id == (uint)ItemId.GoldenPickaxe ||
                state.Id == (uint)ItemId.GoldenShovel ||
                state.Id == (uint)ItemId.GoldenSword)
            {
                return ToolMaterial.Golden;
            }
            else if (state.Id == (uint)ItemId.DiamondAxe ||
                state.Id == (uint)ItemId.DiamondHoe ||
                state.Id == (uint)ItemId.DiamondPickaxe ||
                state.Id == (uint)ItemId.DiamondShovel ||
                state.Id == (uint)ItemId.DiamondSword)
            {
                return ToolMaterial.Diamand;
            }
            else
            {
                return ToolMaterial.Hand;
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
        Hoes,
        Swords
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
        public ItemState Tool;
        public float Hand;
        public float Wooden;
        public float Stone;
        public float Iron;
        public float Diamand;
        public float Golden;
        public float Shears;
        public float Sword;
    }
}
