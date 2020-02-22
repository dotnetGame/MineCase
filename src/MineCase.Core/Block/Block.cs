using MineCase.Block.Material;
using MineCase.World.Storage.Loot;
using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase.Block
{
    public class BlockProperties
    {
        public Material.Material Material { get; set; }
        public MaterialColor MapColor { get; set; }
        public bool BlocksMovement { get; set; } = true;
        public SoundType SoundType { get; set; }
        public int LightValue { get; set; }
        public float Resistance { get; set; }
        public float Hardness { get; set; }
        public bool TicksRandomly { get; set; }
        public float Slipperiness { get; set; } = 0.6F;
        public LootTable LootTable { get; set; }
    }

    public class Block
    {
        // Default state
        private BlockState _defaultState;

        // Properties
        protected readonly int _lightValue;
        protected readonly float _hardness;
        protected readonly float _resistance;
        protected readonly bool _ticksRandomly;
        private readonly float _slipperiness;
        protected readonly LootTable _lootTable;

        public Block(BlockProperties properties)
        {
            _lightValue = properties.LightValue;
            _hardness = properties.Hardness;
            _resistance = properties.Resistance;
            _ticksRandomly = properties.TicksRandomly;
            _slipperiness = properties.Slipperiness;

            _defaultState = new BlockState(this);
        }

        public BlockState GetDefaultState()
        {
            return _defaultState;
        }
    }
}
