using System;
using System.Diagnostics.Contracts;
using System.IO;
using MineCase.Nbt.Serialization;
using MineCase.Nbt.Tags;

namespace MineCase.Nbt
{
    // TODO: 实现 NbtFile 的其他接口，实现从压缩的数据中读取 Tag
    public class NbtFile
    {
        private readonly NbtCompound _rootTag;

        /// <summary>
        /// Initializes a new instance of the <see cref="NbtFile"/> class.<para />
        /// 默认构造函数
        /// </summary>
        public NbtFile()
        {
            _rootTag = new NbtCompound();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NbtFile"/> class.<para />
        /// 从流中初始化 Nbt 数据
        /// </summary>
        /// <param name="stream">要从中初始化数据的流</param>
        public NbtFile(Stream stream)
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

            using (var br = new BinaryReader(stream))
            {
                _rootTag = NbtTagSerializer.DeserializeTag<NbtCompound>(br, false);
            }
        }

        /// <summary>
        /// 将内容写入流中
        /// </summary>
        /// <param name="stream">要写入到的流</param>
        public void WriteTo(Stream stream)
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

            using (var bw = new BinaryWriter(stream))
            {
                NbtTagSerializer.SerializeTag(_rootTag, bw);
            }
        }
    }
}
