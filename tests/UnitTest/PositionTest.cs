using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MineCase.Serialization;
using MineCase.World;
using Xunit;

namespace MineCase.UnitTest
{
    public class PositionTest
    {
        private readonly BlockWorldPos _bwPos1 = new BlockWorldPos(0, 0, 0);
        private readonly BlockWorldPos _bwPos2 = new BlockWorldPos(1, 1, 1);
        private readonly BlockWorldPos _bwPos3 = new BlockWorldPos(17, 17, 17);
        private readonly BlockWorldPos _bwPos4 = new BlockWorldPos(-1, 1, -1);
        private readonly BlockWorldPos _bwPos5 = new BlockWorldPos(-17, 17, -17);

        private readonly BlockChunkPos _bcPos1 = new BlockChunkPos(0, 0, 0);
        private readonly BlockChunkPos _bcPos2 = new BlockChunkPos(1, 1, 1);
        private readonly BlockChunkPos _bcPos3 = new BlockChunkPos(1, 17, 1);
        private readonly BlockChunkPos _bcPos4 = new BlockChunkPos(15, 1, 15);
        private readonly BlockChunkPos _bcPos5 = new BlockChunkPos(15, 17, 15);

        private readonly ChunkWorldPos _cwPos1 = new ChunkWorldPos(0, 0);
        private readonly ChunkWorldPos _cwPos2 = new ChunkWorldPos(0, 0);
        private readonly ChunkWorldPos _cwPos3 = new ChunkWorldPos(1, 1);
        private readonly ChunkWorldPos _cwPos4 = new ChunkWorldPos(-1, -1);
        private readonly ChunkWorldPos _cwPos5 = new ChunkWorldPos(-2, -2);

        private readonly BlockWorldPos _cbwPos1 = new BlockWorldPos(0, 0, 0);
        private readonly BlockWorldPos _cbwPos2 = new BlockWorldPos(0, 0, 0);
        private readonly BlockWorldPos _cbwPos3 = new BlockWorldPos(16, 0, 16);
        private readonly BlockWorldPos _cbwPos4 = new BlockWorldPos(-16, 0, -16);
        private readonly BlockWorldPos _cbwPos5 = new BlockWorldPos(-32, 0, -32);

        private readonly EntityWorldPos _ewPos1 = new EntityWorldPos(0, 0, 0);
        private readonly EntityWorldPos _ewPos2 = new EntityWorldPos(1.5f, 1.5f, 1.5f);
        private readonly EntityWorldPos _ewPos3 = new EntityWorldPos(17.5f, 17.5f, 17.5f);
        private readonly EntityWorldPos _ewPos4 = new EntityWorldPos(-0.5f, 1.5f, -0.5f);
        private readonly EntityWorldPos _ewPos5 = new EntityWorldPos(-16.5f, 17.5f, -16.5f);

        private readonly EntityChunkPos _ecPos1 = new EntityChunkPos(0, 0, 0);
        private readonly EntityChunkPos _ecPos2 = new EntityChunkPos(1.5f, 1.5f, 1.5f);
        private readonly EntityChunkPos _ecPos3 = new EntityChunkPos(1.5f, 17.5f, 1.5f);
        private readonly EntityChunkPos _ecPos4 = new EntityChunkPos(15.5f, 1.5f, 15.5f);
        private readonly EntityChunkPos _ecPos5 = new EntityChunkPos(15.5f, 17.5f, 15.5f);

        [Fact]
        public void TestBlockWorldPos()
        {
            Assert.Equal(_bcPos1, _bwPos1.ToBlockChunkPos());
            Assert.Equal(_bcPos2, _bwPos2.ToBlockChunkPos());
            Assert.Equal(_bcPos3, _bwPos3.ToBlockChunkPos());
            Assert.Equal(_bcPos4, _bwPos4.ToBlockChunkPos());
            Assert.Equal(_bcPos5, _bwPos5.ToBlockChunkPos());

            Assert.Equal(_cwPos1, _bwPos1.ToChunkWorldPos());
            Assert.Equal(_cwPos2, _bwPos2.ToChunkWorldPos());
            Assert.Equal(_cwPos3, _bwPos3.ToChunkWorldPos());
            Assert.Equal(_cwPos4, _bwPos4.ToChunkWorldPos());
            Assert.Equal(_cwPos5, _bwPos5.ToChunkWorldPos());
        }

        [Fact]
        public void TestBlockWorldPosExtension()
        {
            Assert.Equal(_bwPos1.Up(), new BlockWorldPos(0, 1, 0));
            Assert.Equal(_bwPos1.Down(), new BlockWorldPos(0, -1, 0));
            Assert.Equal(_bwPos1.East(), new BlockWorldPos(1, 0, 0));
            Assert.Equal(_bwPos1.West(), new BlockWorldPos(-1, 0, 0));
            Assert.Equal(_bwPos1.North(), new BlockWorldPos(0, 0, -1));
            Assert.Equal(_bwPos1.South(), new BlockWorldPos(0, 0, 1));

            Assert.Equal(_bwPos1.Up(2), new BlockWorldPos(0, 2, 0));
            Assert.Equal(_bwPos1.Down(2), new BlockWorldPos(0, -2, 0));
            Assert.Equal(_bwPos1.East(2), new BlockWorldPos(2, 0, 0));
            Assert.Equal(_bwPos1.West(2), new BlockWorldPos(-2, 0, 0));
            Assert.Equal(_bwPos1.North(2), new BlockWorldPos(0, 0, -2));
            Assert.Equal(_bwPos1.South(2), new BlockWorldPos(0, 0, 2));
        }

        [Fact]
        public void TestBlockVector()
        {
            BlockVector bvPos1 = new BlockVector(0, 0, 0);
            BlockVector bvPos2 = new BlockVector(1, 1, 1);
            BlockVector bvPos3 = new BlockVector(-1, -1, -1);
            BlockVector bvPos4 = new BlockVector(6, 7, 8);

            Assert.Equal(0, bvPos1.X);
            Assert.Equal(0, bvPos1.Y);
            Assert.Equal(0, bvPos1.Z);

            Assert.Equal(1, bvPos2.X);
            Assert.Equal(1, bvPos2.Y);
            Assert.Equal(1, bvPos2.Z);

            Assert.Equal(-1, bvPos3.X);
            Assert.Equal(-1, bvPos3.Y);
            Assert.Equal(-1, bvPos3.Z);

            Assert.Equal(6, bvPos4.X);
            Assert.Equal(7, bvPos4.Y);
            Assert.Equal(8, bvPos4.Z);

            Assert.Equal(bvPos2, bvPos1 + bvPos2);
            Assert.Equal(bvPos3, bvPos1 + bvPos3);
            Assert.Equal(bvPos1, bvPos2 + bvPos3);
        }

        [Fact]
        public void TestBlockChunkPos()
        {
            Assert.Equal(_bwPos1, _bcPos1.ToBlockWorldPos(_cwPos1));
            Assert.Equal(_bwPos2, _bcPos2.ToBlockWorldPos(_cwPos2));
            Assert.Equal(_bwPos3, _bcPos3.ToBlockWorldPos(_cwPos3));
            Assert.Equal(_bwPos4, _bcPos4.ToBlockWorldPos(_cwPos4));
            Assert.Equal(_bwPos5, _bcPos5.ToBlockWorldPos(_cwPos5));
        }

        [Fact]
        public void TestChunkWorldPos()
        {
            Assert.Equal(_cbwPos1, _cwPos1.ToBlockWorldPos());
            Assert.Equal(_cbwPos2, _cwPos2.ToBlockWorldPos());
            Assert.Equal(_cbwPos3, _cwPos3.ToBlockWorldPos());
            Assert.Equal(_cbwPos4, _cwPos4.ToBlockWorldPos());
            Assert.Equal(_cbwPos5, _cwPos5.ToBlockWorldPos());
        }

        [Fact]
        public void TestEntityWorldPos_ToChunkWorldPos()
        {
            Assert.Equal(_cwPos1, _ewPos1.ToChunkWorldPos());
            Assert.Equal(_cwPos2, _ewPos2.ToChunkWorldPos());
            Assert.Equal(_cwPos3, _ewPos3.ToChunkWorldPos());
            Assert.Equal(_cwPos4, _ewPos4.ToChunkWorldPos());
            Assert.Equal(_cwPos5, _ewPos5.ToChunkWorldPos());
        }

        [Fact]
        public void TestEntityWorldPos_ToBlockWorldPos()
        {
            Assert.Equal(_bwPos1, _ewPos1.ToBlockWorldPos());
            Assert.Equal(_bwPos2, _ewPos2.ToBlockWorldPos());
            Assert.Equal(_bwPos3, _ewPos3.ToBlockWorldPos());
            Assert.Equal(_bwPos4, _ewPos4.ToBlockWorldPos());
            Assert.Equal(_bwPos5, _ewPos5.ToBlockWorldPos());
        }

        [Fact]
        public void TestEntityWorldPos_ToEntityChunkPos()
        {
            Assert.Equal(_ecPos1, _ewPos1.ToEntityChunkPos());
            Assert.Equal(_ecPos2, _ewPos2.ToEntityChunkPos());
            Assert.Equal(_ecPos3, _ewPos3.ToEntityChunkPos());
            Assert.Equal(_ecPos4, _ewPos4.ToEntityChunkPos());
            Assert.Equal(_ecPos5, _ewPos5.ToEntityChunkPos());
        }

        [Fact]
        public void TestEntityChunkPos_ToEntityWorldPos()
        {
            Assert.Equal(_ewPos1, _ecPos1.ToEntityWorldPos(_cwPos1));
            Assert.Equal(_ewPos2, _ecPos2.ToEntityWorldPos(_cwPos2));
            Assert.Equal(_ewPos3, _ecPos3.ToEntityWorldPos(_cwPos3));
            Assert.Equal(_ewPos4, _ecPos4.ToEntityWorldPos(_cwPos4));
            Assert.Equal(_ewPos5, _ecPos5.ToEntityWorldPos(_cwPos5));
        }

        [Fact]
        public void TestPositionReadWrites()
        {
            TestPositionReadWrite(_bwPos1);
            TestPositionReadWrite(_bwPos2);
            TestPositionReadWrite(_bwPos3);
            TestPositionReadWrite(_bwPos4);
            TestPositionReadWrite(_bwPos5);
        }

        private void TestPositionReadWrite(Position position)
        {
            using (var mem = new MemoryStream())
            using (var bw = new BinaryWriter(mem, Encoding.UTF8, true))
            {
                bw.WriteAsPosition(position);
                var br = new SpanReader(mem.ToArray());
                Assert.Equal(position, br.ReadAsPosition());
            }
        }
    }
}
