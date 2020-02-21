using System;
using System.Collections.Generic;
using System.Text;
using MineCase.Util.Collections;
using Xunit;

namespace MineCase.UnitTest
{
    public class TinyIntArrayTest
    {
        [Fact]
        public void Test1()
        {
            TinyIntArray array1 = new TinyIntArray(3, 3);
            array1[0] = 1;
            array1[1] = 2;
            array1[2] = 3;

            Assert.True(array1[0] == 1);
            Assert.True(array1[1] == 2);
            Assert.True(array1[2] == 3);

            TinyIntArray array2 = new TinyIntArray(4, 4);
            array1[0] = 1;
            array1[1] = 2;
            array1[2] = 3;

            Assert.True(array1[0] == 1);
            Assert.True(array1[1] == 2);
            Assert.True(array1[2] == 3);

            TinyIntArray array3 = new TinyIntArray(8, 3);
            array1[0] = 1;
            array1[1] = 2;
            array1[2] = 3;

            Assert.True(array1[0] == 1);
            Assert.True(array1[1] == 2);
            Assert.True(array1[2] == 3);

            TinyIntArray array4 = new TinyIntArray(31, 3);
            array1[0] = 1;
            array1[1] = 2;
            array1[2] = 3;

            Assert.True(array1[0] == 1);
            Assert.True(array1[1] == 2);
            Assert.True(array1[2] == 3);
        }
    }
}
