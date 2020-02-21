using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MineCase.Nbt;
using MineCase.Nbt.Tags;
using Xunit;

namespace MineCase.UnitTest
{
    public class NbtTest
    {
        private class OutputVisitor : INbtTagVisitor
        {
            private readonly TextWriter _tw;
            private readonly List<bool> _lastVisitedList = new List<bool>();
            private int _curDepth;

            public OutputVisitor(TextWriter tw)
            {
                _tw = tw;
            }

            public void StartChild()
            {
                _tw.Write($"{Enumerable.Repeat('\t', _curDepth).Aggregate("", (s, c) => string.Concat(s, c))}");
                _tw.WriteLine(_lastVisitedList.Last() ? '[' : '{');
                ++_curDepth;
            }

            public void EndChild()
            {
                --_curDepth;
                _tw.Write($"{Enumerable.Repeat('\t', _curDepth).Aggregate("", (s, c) => string.Concat(s, c))}");
                _tw.WriteLine(_lastVisitedList.Last() ? ']' : '}');
                _lastVisitedList.RemoveAt(_lastVisitedList.Count - 1);
            }

            public void VisitTag(NbtTag tag)
            {
                if (tag.TagType == NbtTagType.List || tag.TagType == NbtTagType.Compound)
                {
                    _lastVisitedList.Add(tag.TagType == NbtTagType.List);
                }

                _tw.Write($"{Enumerable.Repeat('\t', _curDepth).Aggregate("", (s, c) => string.Concat(s, c))}{tag.Name ?? "(null)"} : ");
                switch (tag.TagType)
                {
                    case NbtTagType.End:
                        break;
                    case NbtTagType.Byte:
                        _tw.WriteLine(((NbtByte)tag).Value);
                        break;
                    case NbtTagType.Short:
                        _tw.WriteLine(((NbtShort)tag).Value);
                        break;
                    case NbtTagType.Int:
                        _tw.WriteLine(((NbtInt)tag).Value);
                        break;
                    case NbtTagType.Long:
                        _tw.WriteLine(((NbtLong)tag).Value);
                        break;
                    case NbtTagType.Float:
                        _tw.WriteLine(((NbtFloat)tag).Value);
                        break;
                    case NbtTagType.Double:
                        _tw.WriteLine(((NbtDouble)tag).Value);
                        break;
                    case NbtTagType.ByteArray:
                        _tw.WriteLine("(ByteArray)");
                        break;
                    case NbtTagType.String:
                        _tw.WriteLine(((NbtString)tag).Value);
                        break;
                    case NbtTagType.List:
                        _tw.WriteLine();
                        break;
                    case NbtTagType.Compound:
                        _tw.WriteLine();
                        break;
                    case NbtTagType.IntArray:
                        _tw.WriteLine("(IntArray)");
                        break;
                }
            }
        }

        [Fact]
        public void Test1()
        {
            var nbtFile = new NbtFile();
            nbtFile.RootTag.Add("23333", new NbtInt(1));
            nbtFile.RootTag.Add("test", new NbtCompound());
            var testCompound = nbtFile.RootTag["test"] as NbtCompound;
            Assert.NotNull(testCompound);
            var testList = new NbtList(NbtTagType.Int);
            testCompound.Add("testList", testList);
            testList.Add(new NbtInt(2));
            testList.Add(new NbtInt(4));
            testCompound.Add("testLong", new NbtLong(0x000000FFFFFFFFFF));

            using (var sw = new StringWriter())
            {
                nbtFile.RootTag.Accept(new OutputVisitor(sw));
                var str = sw.ToString();
                Console.WriteLine(str);
            }

            var stream = new FileStream("test.bin", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            stream.SetLength(0);
            nbtFile.WriteTo(stream);

            stream.Seek(0, SeekOrigin.Begin);
            var nbtFile2 = new NbtFile(stream);

            using (var sw = new StringWriter())
            {
                nbtFile2.RootTag.Accept(new OutputVisitor(sw));
                Console.WriteLine(sw.ToString());
            }
        }
    }
}