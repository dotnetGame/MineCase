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

            entry.DroppedBlock = BlockStates.FromString(splittedItems[0]);
            entry.Hardness = float.Parse(splittedItems[1]);
            entry.Tool = ItemStates.FromString(splittedItems[2]);
            entry.Hand = float.Parse(splittedItems[3]);
            entry.Wooden = float.Parse(splittedItems[4]);
            entry.Stone = float.Parse(splittedItems[5]);
            entry.Iron = float.Parse(splittedItems[6]);
            entry.Diamand = float.Parse(splittedItems[7]);
            entry.Golden = float.Parse(splittedItems[8]);
            entry.Shears = float.Parse(splittedItems[9]);
            entry.Sword = float.Parse(splittedItems[10]);

            Mapping.Add(BlockStates.FromString(resultSpan.ToString()), entry);
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
