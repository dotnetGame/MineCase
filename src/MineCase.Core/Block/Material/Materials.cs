using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase.Block.Material
{
    public class Materials
    {
        public static readonly Material Air = new Material { Color = MaterialColors.Air, BlocksMovement = false, Opaque = false, Solid = false, Replaceable = true };
        public static readonly Material StructureVoid = new Material { Color = MaterialColors.Air, BlocksMovement = false, Opaque = false, Solid = false, Replaceable = true };
        public static readonly Material Portal = new Material { Color = MaterialColors.Air, BlocksMovement = false, Opaque = false, Solid = false, PushReaction = PushReaction.Block };
        public static readonly Material Carpet = new Material { Color = MaterialColors.Wool, BlocksMovement = false, Opaque = false, Solid = false, Flammable = true };
        public static readonly Material Plants = new Material { Color = MaterialColors.Foliage, BlocksMovement = false, Opaque = false, Solid = false, PushReaction = PushReaction.Destroy };
        public static readonly Material OceanPlant = new Material { Color = MaterialColors.Water, BlocksMovement = false, Opaque = false, Solid = false, PushReaction = PushReaction.Destroy };
        public static readonly Material TallPlants = new Material { Color = MaterialColors.Foliage, BlocksMovement = false, Opaque = false, Solid = false, PushReaction = PushReaction.Destroy, Replaceable = true, Flammable = true };
        public static readonly Material SeaGrass = new Material { Color = MaterialColors.Water, BlocksMovement = false, Opaque = false, Solid = false, PushReaction = PushReaction.Destroy, Replaceable = true };
        public static readonly Material Water = new Material { Color = MaterialColors.Water, BlocksMovement = false, Opaque = false, Solid = false, PushReaction = PushReaction.Destroy, Replaceable = true, Liquid = true };
        public static readonly Material BubbleColumn = new Material { Color = MaterialColors.Water, BlocksMovement = false, Opaque = false, Solid = false, PushReaction = PushReaction.Destroy, Replaceable = true, Liquid = true };
        public static readonly Material Lava = new Material { Color = MaterialColors.Tnt, BlocksMovement = false, Opaque = true, Solid = false, PushReaction = PushReaction.Destroy, Replaceable = true, Liquid = true };
        public static readonly Material Snow = new Material { Color = MaterialColors.Snow, BlocksMovement = false, Opaque = false, Solid = false, PushReaction = PushReaction.Destroy, Replaceable = true, RequiresNoTool = false };
        public static readonly Material Fire = new Material { Color = MaterialColors.Air, BlocksMovement = false, Opaque = false, Solid = false, PushReaction = PushReaction.Destroy, Replaceable = true };
        public static readonly Material Miscellaneous = new Material { Color = MaterialColors.Air, BlocksMovement = false, Opaque = false, Solid = false, PushReaction = PushReaction.Destroy };
        public static readonly Material Web = new Material { Color = MaterialColors.Wool, BlocksMovement = false, Opaque = false, PushReaction = PushReaction.Destroy, RequiresNoTool = false };
        public static readonly Material RedstoneLight = new Material { Color = MaterialColors.Air };
        public static readonly Material Clay = new Material { Color = MaterialColors.Clay };
        public static readonly Material Earth = new Material { Color = MaterialColors.Dirt };
        public static readonly Material Organic = new Material { Color = MaterialColors.Grass };
        public static readonly Material PackedIce = new Material { Color = MaterialColors.Ice };
        public static readonly Material Sand = new Material { Color = MaterialColors.Sand };
        public static readonly Material Sponge = new Material { Color = MaterialColors.Yellow };
        public static readonly Material Shulker = new Material { Color = MaterialColors.Purple };
        public static readonly Material Wood = new Material { Color = MaterialColors.Wood, Flammable = true };
        public static readonly Material BambooSapling = new Material { Color = MaterialColors.Wood, Flammable = true, PushReaction = PushReaction.Destroy, BlocksMovement = false };
        public static readonly Material Bamboo = new Material { Color = MaterialColors.Wood, Flammable = true, PushReaction = PushReaction.Destroy };
        public static readonly Material Wool = new Material { Color = MaterialColors.Wool, Flammable = true };
        public static readonly Material Tnt = new Material { Color = MaterialColors.Tnt, Flammable = true, Opaque = true };
        public static readonly Material Leaves = new Material { Color = MaterialColors.Foliage, Flammable = true, Opaque = false, PushReaction = PushReaction.Destroy };
        public static readonly Material Glass = new Material { Color = MaterialColors.Air, Opaque = true };
        public static readonly Material Ice = new Material { Color = MaterialColors.Ice, Opaque = true };
        public static readonly Material Cactus = new Material { Color = MaterialColors.Foliage, Opaque = false, PushReaction = PushReaction.Destroy };
        public static readonly Material Rock = new Material { Color = MaterialColors.Stone, RequiresNoTool = false };
        public static readonly Material Iron = new Material { Color = MaterialColors.Iron, RequiresNoTool = false };
        public static readonly Material SnowBlock = new Material { Color = MaterialColors.Snow, RequiresNoTool = false };
        public static readonly Material Anvil = new Material { Color = MaterialColors.Iron, RequiresNoTool = false, PushReaction = PushReaction.Block };
        public static readonly Material Barrier = new Material { Color = MaterialColors.Air, RequiresNoTool = false, PushReaction = PushReaction.Block };
        public static readonly Material Pison = new Material { Color = MaterialColors.Stone, PushReaction = PushReaction.Block };
        public static readonly Material Coral = new Material { Color = MaterialColors.Foliage, PushReaction = PushReaction.Destroy };
        public static readonly Material Gourd = new Material { Color = MaterialColors.Foliage, PushReaction = PushReaction.Destroy };
        public static readonly Material DragonEgg = new Material { Color = MaterialColors.Foliage, PushReaction = PushReaction.Destroy };
        public static readonly Material Cake = new Material { Color = MaterialColors.Air, PushReaction = PushReaction.Destroy };
    }
}
