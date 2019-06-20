using System;
using System.Diagnostics.Contracts;
using System.IO;
using System.Text;
using MineCase.Nbt.Serialization;
using MineCase.Nbt.Tags;

namespace MineCase.Nbt
{
    // TODO: 实现 NbtFile 的其他接口，实现从压缩的数据中读取 Tag
    public class NbtFile
    {
        public NbtCompound RootTag { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="NbtFile"/> class.<para />
        /// 默认构造函数.
        /// </summary>
        public NbtFile()
        {
            RootTag = new NbtCompound();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NbtFile"/> class.<para />
        /// 从流中初始化 Nbt 数据.
        /// </summary>
        /// <param name="stream">要从中初始化数据的流.</param>
        /// <param name="leaveOpen">读取完成后保持流的打开状态.</param>
        public NbtFile(Stream stream, bool leaveOpen = true)
        {
            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            if (!stream.CanRead)
            {
                throw new ArgumentException("流不可读", nameof(stream));
            }

            Contract.EndContractBlock();

            using (var br = new BinaryReader(stream, Encoding.UTF8, leaveOpen))
            {
                RootTag = NbtTagSerializer.DeserializeTag<NbtCompound>(br);
            }
        }

        /// <summary>
        /// 将内容写入流中.
        /// </summary>
        /// <param name="stream">要写入到的流.</param>
        /// <param name="leaveOpen">写入完成后保持流的打开状态.</param>
        public void WriteTo(Stream stream, bool leaveOpen = true)
        {
            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            if (!stream.CanWrite)
            {
                throw new ArgumentException("流不可写", nameof(stream));
            }

            Contract.EndContractBlock();

            using (var bw = new BinaryWriter(stream, Encoding.UTF8, leaveOpen))
            {
                NbtTagSerializer.SerializeTag(RootTag, bw);
            }
        }
    }
}
