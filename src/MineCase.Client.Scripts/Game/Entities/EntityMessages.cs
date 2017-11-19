using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MineCase.Engine;
using UnityEngine;

namespace MineCase.Client.Game.Entities
{
    public sealed class PositionMove : IEntityMessage
    {
        public float Horizontal { get; set; }

        public float Vertical { get; set; }
    }

    public sealed class LookAt : IEntityMessage
    {
        public Vector3 Position { get; set; }
    }
}
