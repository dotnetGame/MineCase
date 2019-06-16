using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace MineCase.World
{
    public enum Plane
    {
        XY,
        XZ,
        YZ
    }

    public enum Axis
    {
        X,
        Y,
        Z
    }

    public enum AxisDirection : int
    {
        Positive = 1,
        Negative = -1
    }

    public enum EnumFacing
    {
        Up,
        Down,
        West,
        East,
        North,
        South
    }

    public class Facing : IEquatable<Facing>
    {
        private EnumFacing _facing;

        private Axis _axis;

        public Facing(EnumFacing facing)
        {
            _facing = facing;
            if (facing == EnumFacing.Up || facing == EnumFacing.Down)
            {
                _axis = Axis.Y;
            }
            else if (facing == EnumFacing.East || facing == EnumFacing.West)
            {
                _axis = Axis.X;
            }
            else if (facing == EnumFacing.North || facing == EnumFacing.South)
            {
                _axis = Axis.Z;
            }
        }

        public Axis GetAxis()
        {
            return _axis;
        }

        bool IEquatable<Facing>.Equals(Facing other)
        {
            return _facing == other._facing;
        }

        public BlockVector ToBlockVector()
        {
            if (_facing == EnumFacing.Up)
            {
                return new BlockVector(0, 1, 0);
            }
            else if (_facing == EnumFacing.East)
            {
                return new BlockVector(1, 0, 0);
            }
            else if (_facing == EnumFacing.North)
            {
                return new BlockVector(0, 0, -1);
            }
            else if (_facing == EnumFacing.Down)
            {
                return new BlockVector(0, -1, 0);
            }
            else if (_facing == EnumFacing.West)
            {
                return new BlockVector(-1, 0, 0);
            }
            else if (_facing == EnumFacing.South)
            {
                return new BlockVector(0, 0, 1);
            }
            else
            {
                return new BlockVector(0, 0, 0);
            }
        }

        public static Facing RadomFacing(Random random)
        {
            var v = random.Next(6);
            switch (v)
            {
                case 0:
                    return new Facing(EnumFacing.Down);
                case 1:
                    return new Facing(EnumFacing.East);
                case 2:
                    return new Facing(EnumFacing.North);
                case 3:
                    return new Facing(EnumFacing.South);
                case 4:
                    return new Facing(EnumFacing.Up);
                case 5:
                    return new Facing(EnumFacing.West);
                default:
                    throw new System.NotSupportedException("It should never reach it here.");
            }
        }

        public static Facing RadomFacing(Random random, Axis axis)
        {
            var v = random.Next(2);
            if (axis == Axis.X)
            {
                switch (v)
                {
                    case 0:
                        return new Facing(EnumFacing.East);
                    case 1:
                        return new Facing(EnumFacing.West);
                    default:
                        throw new System.NotSupportedException("It should never reach it here.");
                }
            }
            else if (axis == Axis.Y)
            {
                switch (v)
                {
                    case 0:
                        return new Facing(EnumFacing.Up);
                    case 1:
                        return new Facing(EnumFacing.Down);
                    default:
                        throw new System.NotSupportedException("It should never reach it here.");
                }
            }
            else if (axis == Axis.Z)
            {
                switch (v)
                {
                    case 0:
                        return new Facing(EnumFacing.North);
                    case 1:
                        return new Facing(EnumFacing.South);
                    default:
                        throw new System.NotSupportedException("It should never reach it here.");
                }
            }
            else
            {
                throw new System.NotSupportedException("It should never reach it here.");
            }
        }

        public static Facing RadomFacing(Random random, Plane plane)
        {
            var v = random.Next(4);
            if (plane == Plane.XY)
            {
                switch (v)
                {
                    case 0:
                        return new Facing(EnumFacing.East);
                    case 1:
                        return new Facing(EnumFacing.West);
                    case 2:
                        return new Facing(EnumFacing.Up);
                    case 3:
                        return new Facing(EnumFacing.Down);
                    default:
                        throw new System.NotSupportedException("It should never reach it here.");
                }
            }
            else if (plane == Plane.XZ)
            {
                switch (v)
                {
                    case 0:
                        return new Facing(EnumFacing.East);
                    case 1:
                        return new Facing(EnumFacing.West);
                    case 2:
                        return new Facing(EnumFacing.North);
                    case 3:
                        return new Facing(EnumFacing.South);
                    default:
                        throw new System.NotSupportedException("It should never reach it here.");
                }
            }
            else if (plane == Plane.YZ)
            {
                switch (v)
                {
                    case 0:
                        return new Facing(EnumFacing.South);
                    case 1:
                        return new Facing(EnumFacing.North);
                    case 2:
                        return new Facing(EnumFacing.Up);
                    case 3:
                        return new Facing(EnumFacing.Down);
                    default:
                        throw new System.NotSupportedException("It should never reach it here.");
                }
            }
            else
            {
                throw new System.NotSupportedException("It should never reach it here.");
            }
        }
    }
}
