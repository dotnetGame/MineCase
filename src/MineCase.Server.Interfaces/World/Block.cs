﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase.Server.World
{
    public enum BlockId : uint
    {
        Air = 0,
        Stone = 1,
        GrassBlock = 2,
        Dirt = 3,
        Cobblestone = 4,
        WoodPlanks = 5,
        Sapling = 6,
        Bedrock = 7,
        Water = 8,
        StationaryWater = 9,
        Lava = 10,
        StationaryLava = 11,
        Sand = 12,
        Gravel = 13,
        GoldOre = 14,
        IronOre = 15,
        CoalOre = 16,
        Wood = 17,
        Leaves = 18,
        Sponge = 19,
        Glass = 20,
        LapisLazuliOre = 21,
        LapisLazuliBlock = 22,
        Dispenser = 23,
        Sandstone = 24,
        NoteBlock = 25,
        Bed = 26,
        PoweredRail = 27,
        DetectorRail = 28,
        StickyPiston = 29,
        Cobweb = 30,
        Grass = 31,
        DeadBush = 32,
        Piston = 33,
        PistonHead = 34,
        Wool = 35,
        BlockMovedByPiston = 36,
        Dandelion = 37,
        Poppy = 38,
        BrownMushroom = 39,
        RedMushroom = 40,
        BlockOfGold = 41,
        BlockOfIron = 42,
        DoubleStoneSlab = 43,
        StoneSlab = 44,
        Bricks = 45,
        TNT = 46,
        Bookshelf = 47,
        MossStone = 48,
        Obsidian = 49,
        Torch = 50,
        Fire = 51,
        MonsterSpawner = 52,
        OakWoodStairs = 53,
        Chest = 54,
        RedstoneWire = 55,
        DiamondOre = 56,
        BlockOfDiamond = 57,
        CraftingTable = 58,
        Wheat = 59,
        Farmland = 60,
        Furnace = 61,
        BurningFurnace = 62,
        StandingSign = 63,
        OakDoor = 64,
        Ladder = 65,
        Rail = 66,
        CobblestoneStairs = 67,
        WallSign = 68,
        Lever = 69,
        StonePressurePlate = 70,
        IronDoor = 71,
        WoodenPressurePlate = 72,
        RedstoneOre = 73,
        GlowingRedstoneOre = 74,
        RedstoneTorchInactive = 75,
        RedstoneTorchActive = 76,
        StoneButton = 77,
        SnowLayer = 78,
        Ice = 79,
        Snow = 80,
        Cactus = 81,
        Clay = 82,
        SugarCane = 83,
        Jukebox = 84,
        OakFence = 85,
        Pumpkin = 86,
        Netherrack = 87,
        SoulSand = 88,
        Glowstone = 89,
        NetherPortal = 90,
        JackLantern = 91,
        Cake = 92,
        RedstoneRepeaterInactive = 93,
        RedstoneRepeaterActive = 94,
        StainedGlass = 95,
        Trapdoor = 96,
        MonsterEgg = 97,
        StoneBricks = 98,
        BrownMushroomBlock = 99,
        RedMushroomBlock = 100,
        IronBars = 101,
        GlassPane = 102,
        Melon = 103,
        PumpkinStem = 104,
        MelonStem = 105,
        Vines = 106,
        FenceGate = 107,
        BrickStairs = 108,
        StoneBrickStairs = 109,
        Mycelium = 110,
        LilyPad = 111,
        NetherBrick = 112,
        NetherBrickFence = 113,
        NetherBrickStairs = 114,
        NetherWart = 115,
        EnchantmentTable = 116,
        BrewingStand = 117,
        Cauldron = 118,
        EndPortal = 119,
        EndPortalFrame = 120,
        EndStone = 121,
        DragonEgg = 122,
        RedstoneLampInactive = 123,
        RedstoneLampActive = 124,
        DoubleWoodenSlab = 125,
        WoodenSlab = 126,
        Cocoa = 127,
        SandstoneStairs = 128,
        EmeraldOre = 129,
        EnderChest = 130,
        TripwireHook = 131,
        Tripwire = 132,
        BlockOfEmerald = 133,
        SpruceWoodStairs = 134,
        BirchWoodStairs = 135,
        JungleWoodStairs = 136,
        CommandBlock = 137,
        Beacon = 138,
        CobblestoneWall = 139,
        FlowerPot = 140,
        Carrot = 141,
        Potato = 142,
        WoodenButton = 143,
        Mobhead = 144,
        Anvil = 145,
        TrappedChest = 146,
        WeightedPressurePlateLight = 147,
        WeightedPressurePlateHeavy = 148,
        RedstoneComparator = 149,
        RedstoneComparatorDeprecated = 150,
        DaylightSensor = 151,
        BlockOfRedstone = 152,
        NetherQuartzOre = 153,
        Hopper = 154,
        BlockOfQuartz = 155,
        QuartzStairs = 156,
        ActivatorRail = 157,
        Dropper = 158,
        StainedClay = 159,
        StainedGlassPane = 160,
        Leaves2 = 161,
        Wood2 = 162,
        AcaciaWoodStairs = 163,
        DarkOakWoodStairs = 164,
        SlimeBlock = 165,
        Barrier = 166,
        IronTrapdoor = 167,
        Prismarine = 168,
        SeaLantern = 169,
        HayBale = 170,
        Carpet = 171,
        HardenedClay = 172,
        BlockOfCoal = 173,
        PackedIce = 174,
        LargeFlowers = 175,
        StandingBanner = 176,
        WallBanner = 177,
        InvertedDaylightSensor = 178,
        RedSandstone = 179,
        RedSandstoneStairs = 180,
        DoubleRedSandstoneSlab = 181,
        RedSandstoneSlab = 182,
        SpruceFenceGate = 183,
        BirchFenceGate = 184,
        JungleFenceGate = 185,
        DarkOakFenceGate = 186,
        AcaciaFenceGate = 187,
        SpruceFence = 188,
        BirchFence = 189,
        JungleFence = 190,
        DarkOakFence = 191,
        AcaciaFence = 192,
        SpruceDoor = 193,
        BirchDoor = 194,
        JungleDoor = 195,
        AcaciaDoor = 196,
        DarkOakDoor = 197,
        EndRod = 198,
        ChorusPlant = 199,
        ChorusFlower = 200,
        PurpurBlock = 201,
        PurpurPillar = 202,
        PurpurStairs = 203,
        PurpurDoubleSlab = 204,
        PurpurSlab = 205,
        EndStoneBricks = 206,
        BeetrootSeeds = 207,
        GrassPath = 208,
        EndGateway = 209,
        RepeatingCommandBlock = 210,
        ChainCommandBlock = 211,
        FrostedIce = 212,
        MagmaBlock = 213,
        NetherWartBlock = 214,
        RedNetherBrick = 215,
        BoneBlock = 216,
        StructureVoid = 217,
        Observer = 218,
        WhiteShulkerBox = 219,
        OrangeShulkerBox = 220,
        MagentaShulkerBox = 221,
        LightBlueShulkerBox = 222,
        YellowShulkerBox = 223,
        LimeShulkerBox = 224,
        PinkShulkerBox = 225,
        GrayShulkerBox = 226,
        LightGrayShulkerBox = 227,
        CyanShulkerBox = 228,
        PurpleShulkerBox = 229,
        BlueShulkerBox = 230,
        BrownShulkerBox = 231,
        GreenShulkerBox = 232,
        RedShulkerBox = 233,
        BlackShulkerBox = 234,
        WhiteGlazedTerracotta = 235,
        OrangeGlazedTerracotta = 236,
        MagentaGlazedTerracotta = 237,
        LightBlueGlazedTerracotta = 238,
        YellowGlazedTerracotta = 239,
        LimeGlazedTerracotta = 240,
        PinkGlazedTerracotta = 241,
        GrayGlazedTerracotta = 242,
        LightGrayGlazedTerracotta = 243,
        CyanGlazedTerracotta = 244,
        PurpleGlazedTerracotta = 245,
        BlueGlazedTerracotta = 246,
        BrownGlazedTerracotta = 247,
        GreenGlazedTerracotta = 248,
        RedGlazedTerracotta = 249,
        BlackGlazedTerracotta = 250,
        Concrete = 251,
        ConcretePowder = 252,
        StructureBlock = 255
    }

    public enum WoodPlanksType : uint
    {
        OakWoodPlanks = 0,
        SpruceWoodPlanks = 1,
        BirchWoodPlanks = 2,
        JungleWoodPlanks = 3,
        AcaciaWoodPlanks = 4,
        DarkOakWoodPlanks = 5
    }

    public enum StoneType : uint
    {
        Stone = 0,
        Granite = 1,
        PolishedGranite = 2,
        Diorite = 3,
        PolishedDiorite = 4,
        Andesite = 5,
        PolishedAndesite = 6
    }

    public enum DirtType : uint
    {
        Dirt = 0,
        CoarseDirt = 1,
        Podzol = 2
    }

    public enum SaplingsType : uint
    {
        OakSapling = 0,
        SpruceSapling = 1,
        BirchSapling = 2,
        JungleSapling = 3,
        AcaciaSapling = 4,
        DarkOakSapling = 5,
        // 0x8 bit field : Set if sapling is ready to grow into a tree
        ReadyForTree = 0x8
    }

    /// <summary>
    /// Attributes of water and lava.
    /// 
    /// If Falling is set, the lower bits are essentially ignored,
    /// since this block is then at its highest fluid level.
    /// Level1 is the highest fluid level(not necessarily filling the block - 
    /// this depends on the neighboring fluid blocks above each upper corner of the block)
    /// </summary>
    public enum FluidType : uint
    {
        Level1 = 0,
        Level2 = 1,
        Level3 = 2,
        Level4 = 3,
        Level5 = 4,
        Level6 = 5,
        Level7 = 6,
        Level8 = 7,
        // 0x8 bit field : Set if sapling is ready to grow into a tree
        Falling = 0x8
    }

    public enum SandType : uint
    {
        Sand = 0,
        RedSand = 1
    }

    public enum WoodType : uint
    {
        OakWoodOrAcaciaWood = 0,
        SpruceWoodOrDarkOakWood = 1,
        BirchWood = 2,
        JungleWood = 3,
        // 0x4~0x8 bits specifying the orientation of the wood
        FacingUp = 0x0,
        FacingEast = 0x4,
        FacingNorth = 0x8,
        Onlybark = 0xC
    }

    public enum LeavesType : uint
    {
        OakLeaves = 0,
        SpruceLeaves = 1,
        BirchLeaves = 2,
        JungleLeaves = 3,
        OakLeavesNoDecay = 4,
        SpruceLeavesNoDecay = 5,
        BirchLeavesNoDecay = 6,
        JungleLeavesNoDecay = 7,
        OakLeavesCheckDecay = 8,
        SpruceLeavesCheckDecay = 9,
        BirchLeavesCheckDecay = 10,
        JungleLeavesCheckDecay = 11,
        OakLeavesNoDecayAndCheckDecay = 12,
        SpruceLeavesNoDecayAndCheckDecay = 13,
        BirchLeavesNoDecayAndCheckDecay = 14,
        JungleLeavesNoDecayAndCheckDecay = 15
    }

    /// <summary>
    /// Specifies the color of the wool, stained terracotta, stained glass and carpet. 
    /// </summary>
    public enum ColorType : uint
    {
        White = 0,
        Orange = 1,
        Magenta = 2,
        LightBlue = 3,
        Yellow = 4,
        Lime = 5,
        Pink = 6,
        Gray = 7,
        LightGray = 8,
        Cyan = 9,
        Purple = 10,
        Blue = 11,
        Brown = 12,
        Green = 13,
        Red = 14,
        Black = 15
    }

    public enum TorchesType : uint
    {
        FacingEast = 1,
        FacingWest = 2,
        FacingSouth = 3,
        FacingNorth = 4,
        FacingUp = 5
    }

    public enum DoubleStoneSlabsType : uint
    {
        DoubleStoneSlab = 0,
        DoubleSandstoneSlab = 1,
        DoubleWoodenSlab = 2,
        DoubleCobblestoneSlab = 3,
        DoubleBricksSlab = 4,
        DoubleStoneBrickSlab = 5,
        DoubleNetherBrickSlab = 6,
        DoubleQuartzSlab = 7,
        SmoothDoubleStoneSlab = 8,
        SmoothDoubleSandstoneSlab = 9,
        TileDoubleQuartzSlab = 10
    }

    public enum StoneSlabsType : uint
    {
        StoneSlab = 0,
        SandstoneSlab = 1,
        WoodenSlab = 2,
        CobblestoneSlab = 3,
        BricksSlab = 4,
        StoneBrickSlab = 5,
        NetherBrickSlab = 6,
        QuartzSlab = 7,
        UpperStoneSlab = 8,
        UpperSandstoneSlab = 9,
        UpperWoodenSlab = 10,
        UpperCobblestoneSlab = 11,
        UpperBricksSlab = 12,
        UpperStoneBrickSlab = 13,
        UpperNetherBrickSlab = 14,
        UpperQuartzSlab = 15
    }

    public enum DoubleWoodenSlabsType : uint
    {

    }

    public enum WoodenSlabsType : uint
    {

    }

    public enum FireType : uint
    {
        Placed = 0x0,
        Eternal = 0xF
    }

    public enum SandstoneType : uint
    {
        Sandstone = 0,
        ChiseledSandstone = 1,
        SmoothSandstone = 2
    }

    public enum RedSandstoneType : uint
    {
        RedSandstone = 0,
        ChiseledRedSandstone = 1,
        SmoothRedSandstone = 2
    }

    public enum BedType : uint
    {
        HeadFacingSouth = 0,
        HeadFacingWest = 1,
        HeadFacingNorth = 2,
        HeadFacingEast = 3,
        // 0x4 bit : When 0, the bed is empty, otherwise, the bed is occupied
        Occupied = 0x4,
        // 0x8 bit : When 0, the foot of the bed, otherwise, the head of the bed
        Head = 0x8
    }

    public enum GrassType : uint
    {
        Shrub = 0,
        TallGrass = 1,
        Fern = 2
    }

    public enum FlowersType : uint
    {
        Poppy = 0,
        BlueOrchid = 1,
        Allium = 2,
        AzureBluet = 3,
        RedTulip = 4,
        OrangeTulip = 5,
        WhiteTulip = 6,
        PinkTulip = 7,
        OxeyeDaisy = 8
    }

    public enum LargeFlowersType : uint
    {
        Sunflower = 0,
        Lilac = 1,
        DoubleTallgrass = 2,
        LargeFern = 3,
        RoseBush = 4,
        Peony = 5,
        // Top Half of any Large Plant; low three bits 0x7 are derived from the block below
        TopHalf = 0x8
    }

    public enum PistionType : uint
    {
        Down = 0,
        Up = 1,
        North = 2,
        South = 3,
        West = 4,
        East = 5,
        // bit field determines whether the piston is pushed out or not. 1 for pushed out, 0 for retracted
        PushedOut = 0x8
    }

    public enum PistionExType : uint
    {
        Down = 0,
        Up = 1,
        North = 2,
        South = 3,
        West = 4,
        East = 5,
        // bit field determines whether the head is sticky or not(note that the Piston Body actually
        // has completely different block types for Sticky and Regular). 1 is sticky, 0 is regular
        Sticky = 0x8
    }

    public enum StairsType : uint
    {
        East = 0,
        West = 1,
        South = 2,
        North = 3,
        // 0x4 bit field : Set if stairs are upside-down
        UpsideDown = 0x4
    }

    /// <summary>
    /// Specifies the redstone dust's current redstone power level.
    /// </summary>
    public enum RedstoneWireType : uint
    {
        Level1 = 0,
        Level2 = 1,
        Level3 = 2,
        Level4 = 3,
        Level5 = 4,
        Level6 = 5,
        Level7 = 6,
        Level8 = 7,
        Level9 = 8,
        Level10 = 9,
        Level11 = 10,
        Level12 = 11,
        Level13 = 12,
        Level14 = 13,
        Level15 = 14,
        Level16 = 15,
    }

    /// <summary>
    /// Represents the daylight sensor's current redstone power level.
    /// </summary>
    public enum DaylightSensorType : uint
    {
        Level1 = 0,
        Level2 = 1,
        Level3 = 2,
        Level4 = 3,
        Level5 = 4,
        Level6 = 5,
        Level7 = 6,
        Level8 = 7,
        Level9 = 8,
        Level10 = 9,
        Level11 = 10,
        Level12 = 11,
        Level13 = 12,
        Level14 = 13,
        Level15 = 14,
        Level16 = 15,
    }

    /// <summary>
    /// Crops grow from 0x0 to 0x7. Carrots, beetroot and potatoes appear to have 4 stages,
    /// but actually grow identically to wheat, merely using the same texture for multiple stages.
    /// </summary>
    public enum CropsType : uint
    {
        GrowStage1 = 0,
        GrowStage2 = 1,
        GrowStage3 = 2,
        GrowStage4 = 3,
        GrowStage5 = 4,
        GrowStage6 = 5,
        GrowStage7 = 6,
        GrowStage8 = 7,
    }

    public enum FarmlandType : uint
    {
        DryLand = 0,
        WetnessLevel1 = 1,
        WetnessLevel2 = 2,
        WetnessLevel3 = 3,
        WetnessLevel4 = 4,
        WetnessLevel5 = 5,
        WetnessLevel6 = 6,
        WetnessLevel7 = 7,
    }

    public enum BannerStandingType : uint
    {

    }

    public enum BannerWallType : uint
    {

    }

    // TODO: door 

    public enum RailsType : uint
    {
        StraightRailConnectNS = 0,
        StraightRailConnectEW = 1,
        SlopedRailAscendEast = 2,
        SlopedRailAscendWest = 3,
        SlopedRailAscendNorth = 4,
        SlopedRailAscendSouth = 5,
        CurvedRailConnectSE = 6,
        CurvedRailConnectSW = 7,
        CurvedRailConnectNW = 8,
        CurvedRailConnectNE = 9
    }

    /// <summary>
    /// For Activator Rails, Detector Rails, and Powered Rails
    /// </summary>
    public enum RailsExType : uint
    {
        FlatTrackGoingNS = 0,
        FlatTrackGoingWE = 1,
        SlopedTrackAscendEast = 2,
        SlopedTrackAscendWest = 3,
        SlopedTrackAscendNorth = 4,
        SlopedTrackAscendSouth = 5,
    }

    /// <summary>
    /// For Ladders, Furnaces, Chests, Trapped Chests
    /// </summary>
    public enum FacingDirectionType : uint
    {
        FacingNorth = 2,
        FacingSouth = 3,
        FacingWest = 4,
        FacingEast = 5,
    }

    public enum SignStandingType : uint
    {
        South = 0,
        SouthToSouthwest = 1,
        Southwest = 2,
        WestToSouthwest = 3,
        West = 4,
        WestToNorthwest = 5,
        Northwest = 6,
        NorthToNorthwest = 7,
        North = 8,
        NorthToNortheast = 9,
        Northeast = 10,
        EastToNortheast = 11,
        East = 12,
        EastToSoutheast = 13,
        Southeast = 14,
        SouthToSoutheast = 15
    }

    public enum SignWallType : uint
    {
        North = 2,
        South = 3,
        West = 4,
        East = 5
    }

    /// <summary>
    /// 0x1 ~ 0x4 bits specifying which direction the dispenser is facing
    /// </summary>
    public enum DispenserType : uint
    {
        FacingDown = 0,
        FacingUp = 1,
        FacingNorth = 2,
        FacingSouth = 3,
        FacingWest = 4,
        FacingEast = 5,
        // If set, the dispenser is activated
        Activated = 0x8
    }

    /// <summary>
    /// 0x1 ~ 0x4 bits specifying which direction the dropper is facing
    /// </summary>
    public enum DropperType : uint
    {
        FacingDown = 0,
        FacingUp = 1,
        FacingNorth = 2,
        FacingSouth = 3,
        FacingWest = 4,
        FacingEast = 5,
        // If set, the dropper is activated
        Activated = 0x8
    }

    public enum HopperType : uint
    {

    }

    public enum LeverType : uint
    {
        BottomPointsEastWhenOff = 0,
        SideFacingEast = 1,
        SideFacingWest = 2,
        SideFacingSouth = 3,
        SideFacingNorth = 4,
        TopPointsSouthWhenOff =5,
        TopPointsEastWhenOff = 6,
        BottomPointsSouthWhenOff = 7,
        // 0x8 bit field : If this bit is set, the lever is active
        Active = 0x8
    }

    // TODO:

    public enum SpongeType : uint
    {
        Sponge = 0,
        WetSponge = 1,
    }

    // TODO:

    public struct BlockState : IEquatable<BlockState>
    {
        public uint Id { get; set; }

        public uint MetaValue { get; set; }

        public override bool Equals(object obj)
        {
            return obj is BlockState && Equals((BlockState)obj);
        }

        public bool Equals(BlockState other)
        {
            return Id == other.Id &&
                   MetaValue == other.MetaValue;
        }

        public override int GetHashCode()
        {
            var hashCode = -81208087;
            hashCode = hashCode * -1521134295 + base.GetHashCode();
            hashCode = hashCode * -1521134295 + Id.GetHashCode();
            hashCode = hashCode * -1521134295 + MetaValue.GetHashCode();
            return hashCode;
        }

        public static bool operator ==(BlockState state1, BlockState state2)
        {
            return state1.Equals(state2);
        }

        public static bool operator !=(BlockState state1, BlockState state2)
        {
            return !(state1 == state2);
        }
    }
}
