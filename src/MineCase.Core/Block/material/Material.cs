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
        public PushReaction PushReaction { get; set; }
        public Material(
            MaterialColor color,
            bool liquid,
            bool solid,
            bool blocksMovement,
            bool opaque,
            bool canBurn,
            bool requiresNoTool,
            bool replaceable,
            PushReaction mobilityFlag
            )
        {
            Color = color;
            Liquid = liquid;
            Solid = solid;
            BlocksMovement = blocksMovement;
            Opaque = opaque;
            RequiresNoTool = requiresNoTool;
            Flammable = canBurn;
            Replaceable = replaceable;
            PushReaction = mobilityFlag;
        }
    }
    public class MaterialBuilder
    {
        private MaterialColor _color;
        private bool _blocksMovement = true;
        private bool _canBurn = false;
        private bool _requiresNoTool = true;
        private bool _isLiquid = false;
        private bool _isReplaceable = false;
        private bool _isSolid = true;
        private bool _isOpaque = true;
        private PushReaction _pushReaction = PushReaction.Normal;
        public static MaterialBuilder Create(MaterialColor color)
        {
            MaterialBuilder builder = new MaterialBuilder();
            builder._color = color;
            return builder;
        }

        public MaterialBuilder Liquid()
        {
            _isLiquid = true;
            return this;
        }

        public MaterialBuilder NotSolid()
        {
            _isSolid = false;
            return this;
        }

        public MaterialBuilder NotBlocksMovement()
        {
            _blocksMovement = false;
            return this;
        }

        public MaterialBuilder NotOpaque()
        {
            _isOpaque = false;
            return this;
        }

        public MaterialBuilder Replaceable()
        {
            _isReplaceable = true;
            return this;
        }

        public Material Build()
        {
            Material material = new Material(
                _color,
                _isLiquid,
                _isSolid,
                _blocksMovement,
                _isOpaque,
                _canBurn,
                _requiresNoTool,
                _isReplaceable,
                _pushReaction
            );
            return material;
        }
    }
}
