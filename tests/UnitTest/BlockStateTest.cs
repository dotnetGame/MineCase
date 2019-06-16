using System;
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
    }
}