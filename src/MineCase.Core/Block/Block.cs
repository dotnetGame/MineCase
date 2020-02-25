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
        // Name
        public string Name { get; set; }
        // Default state
        private BlockState _defaultState;

        // Properties
        protected readonly int _lightValue;
        protected readonly float _hardness;
        protected readonly float _resistance;
        protected readonly bool _ticksRandomly;
        private readonly float _slipperiness;
        protected readonly LootTable _lootTable;

        public BlockState Default { get => _defaultState; }

        public Block(string name, BlockProperties properties)
        {
            Name = name;

            _lightValue = properties.LightValue;
            _hardness = properties.Hardness;
            _resistance = properties.Resistance;
            _ticksRandomly = properties.TicksRandomly;
            _slipperiness = properties.Slipperiness;

            _defaultState = new BlockState(this);
        }


        public override bool Equals(object obj)
        {
            return (obj is Block || obj.GetType().IsSubclassOf(typeof(Block))) && Equals((Block)obj);
        }

        public bool Equals(Block other)
        {
            return Name == other.Name;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        public static bool operator ==(Block pos1, Block pos2)
        {
            return pos1.Equals(pos2);
        }

        public static bool operator !=(Block pos1, Block pos2)
        {
            return !(pos1 == pos2);
        }

        [ObsoleteAttribute("This method will soon be deprecated. Use Default property instead.")]
        public BlockState GetDefaultState()
        {
            return _defaultState;
        }
    }
}
