using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase.Block.Material
{
    public class Material
    {
        public MaterialColor Color { get; set; }
        public bool Liquid { get; set; }
        public bool Solid { get; set; }
        public bool BlocksMovement { get; set; }
        public bool Opaque { get; set; }
        public bool RequiresNoTool { get; set; }
        public bool Flammable { get; set; }
        public bool Replaceable { get; set; }
        public bool PushReaction { get; set; }
        public Material(
            MaterialColor color,
            bool liquid,
            bool solid,
            bool movement,
            bool opaque,
            bool canBurn,
            bool requiresNoTool,
            bool replaceable,
            bool mobilityFlag
            )
        {
            Color = color;
            Liquid = liquid;
            Solid = solid;
            BlocksMovement = movement;
            Opaque = opaque;
            RequiresNoTool = requiresNoTool;
            Flammable = canBurn;
            Replaceable = replaceable;
            PushReaction = mobilityFlag;
        }
    }
}
