using System;
using System.Collections.Generic;
using System.Text;
using MineCase.Algorithm;
using MineCase.Graphics;
using Xunit;

namespace MineCase.UnitTest
{
    public class CollisionTest
    {
        [Fact]
        public void TestCollsion()
        {
            var shape1 = new Cuboid(new Point3d(0f, 0f, 0f), new Size(1f, 1f, 2f));
            var shape2 = new Cuboid(new Point3d(0.9f, 0.9f, 1f), new Size(1f, 1f, 1f));
            var result = Collision.IsCollided(shape1, shape2);
            Assert.True(result);

            shape1 = new Cuboid(new Point3d(0f, 0f, 0f), new Size(1f, 1f, 1f));
            shape2 = new Cuboid(new Point3d(-0.5f, 0.3f, 0.5f), new Size(3f, 0.1f, 0.1f));
            result = Collision.IsCollided(shape1, shape2);
            Assert.True(result);
        }

        [Fact]
        public void TestNotCollsion()
        {
            var shape1 = new Cuboid(new Point3d(0f, 0f, 0f), new Size(1f, 1f, 2f));
            var shape2 = new Cuboid(new Point3d(2f, 2f, 1f), new Size(1f, 1f, 1f));
            var result = Collision.IsCollided(shape1, shape2);
            Assert.False(result);

            shape1 = new Cuboid(new Point3d(0f, 0f, 0f), new Size(1f, 1f, 1f));
            shape2 = new Cuboid(new Point3d(0f, 0f, 2f), new Size(1f, 1f, 1f));
            result = Collision.IsCollided(shape1, shape2);
            Assert.False(result);
        }

        [Fact]
        public void TestBoundaryCollsion()
        {
            var shape1 = new Cuboid(new Point3d(0f, 0f, 0f), new Size(1f, 1f, 2f));
            var shape2 = new Cuboid(new Point3d(1f, 0f, 0f), new Size(1f, 1f, 1f));
            var result = Collision.IsCollided(shape1, shape2);
            Assert.True(result);

            shape1 = new Cuboid(new Point3d(0f, 0f, 0f), new Size(1f, 1f, 1f));
            shape2 = new Cuboid(new Point3d(0f, 0f, 1f), new Size(1f, 1f, 1f));
            result = Collision.IsCollided(shape1, shape2);
            Assert.True(result);

            shape1 = new Cuboid(new Point3d(0f, 0f, 0f), new Size(1f, 1f, 1f));
            shape2 = new Cuboid(new Point3d(1f, 1f, 1f), new Size(1f, 1f, 1f));
            result = Collision.IsCollided(shape1, shape2);
            Assert.True(result);
        }
    }
}
