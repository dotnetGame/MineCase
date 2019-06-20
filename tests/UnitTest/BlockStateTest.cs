﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MineCase.Block;
using MineCase.Serialization;
using MineCase.World;
using Xunit;

namespace MineCase.UnitTest
{
    public class BlockStateTest
    {
        [Fact]
        public void IsSameIdTest()
        {
            BlockState state1 = BlockStates.Dirt();
            BlockState state2 = BlockStates.Dirt();
            BlockState state3 = BlockStates.Wood(WoodType.Birch);
            BlockState state4 = BlockStates.Wood(WoodType.Oak);

            Assert.True(state1 == state2);
            Assert.False(state1 == state3);
            Assert.False(state3.IsSameId(state2));
            Assert.True(state3.IsSameId(state4));
        }

        [Fact]
        public void IsIdTest()
        {
            BlockState state1 = BlockStates.Stone(StoneType.Diorite);
            BlockState state2 = BlockStates.Leaves(LeaveType.Birch);
            BlockState state3 = BlockStates.Wood(WoodType.Birch);
            BlockState state4 = BlockStates.Wood(WoodType.Oak);

            Assert.True(state1.IsId(BlockId.Stone));
            Assert.True(state2.IsId(BlockId.Leaves));
            Assert.True(state3.IsId(BlockId.Wood));
            Assert.True(state4.IsId(BlockId.Wood));

            Assert.False(state1.IsId(BlockId.Cobblestone));
            Assert.False(state2.IsId(BlockId.Dirt));
            Assert.False(state3.IsId(BlockId.Stone));
            Assert.False(state4.IsId(BlockId.Stone));
        }
    }
}