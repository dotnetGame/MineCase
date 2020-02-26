using System;
using System.Collections.Generic;
using System.Text;
using Orleans;

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
        public Guid Id { get; set; }
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

    public class BigWorldEntityState
    {
        public Guid Id { get; set; }

        public Guid AppId { get; set; }

        public Guid SpaceId { get; set; }

        public BigWorldEntityType EntityType { get; set; }

        public BigWorldPosition Position { get; set; }

        public bool IsDestroyed { get; set; }

        public Dictionary<string, IBigWorldValue> Properties { get; set; }
    }

    public class BigWorldEntity
    {
        public BigWorldEntityState State { get; set; }

        public BigWorldEntityLocation EntityLocation { get; set; }

        private IGrainFactory _grainFactory;

        public BigWorldEntity(IGrainFactory grainFactory)
        {
            _grainFactory = grainFactory;
        }
    }
}
