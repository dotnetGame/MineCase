using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase.Server.Engine
{
    public struct BigWorldPosition
    {
        public float X { get; set; }

        public float Y { get; set; }

        public float Z { get; set; }

        public BigWorldPosition(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }
    }

    public class BigWorldEntityRef
    {
        private Guid _id;
        private Guid _spaceId;
        private BigWorldPosition _position;
        private bool _isDestroyed;
    }

    public enum BigWorldEntityLocation
    {
        Client,
        Base,
        Cell
    }

    public enum BigWorldEntityType
    {
        Real,
        Ghost
    }

    public class BigWorldEntity
    {
        private Guid _id;
        private Guid _spaceId;
        private BigWorldEntityLocation _entityLocation;
        private BigWorldEntityType _entityType;
        private BigWorldPosition _position;
        private bool _isDestroyed;
    }
}
