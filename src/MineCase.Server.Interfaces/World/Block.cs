using System;
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

    public enum WoodPlankType : uint
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

    [Flags]
    public enum SaplingsType : uint
    {
        OakSapling = 0,
        SpruceSapling = 1,
        BirchSapling = 2,
        JungleSapling = 3,
        AcaciaSapling = 4,
        DarkOakSapling = 5,

        // 0x8 bit field : Set if sapling is ready to grow into a tree
        ReadyForTreeFlag = 0x8
    }

    /// <summary>
    /// Attributes of water and lava.
    ///
    /// If Falling is set, the lower bits are essentially ignored,
    /// since this block is then at its highest fluid level.
    /// Level1 is the highest fluid level(not necessarily filling the block -
    /// this depends on the neighboring fluid blocks above each upper corner of the block)
    /// </summary>
    [Flags]
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
        FallingFlag = 0x8
    }

    public enum SandType : uint
    {
        Sand = 0,
        RedSand = 1
    }

    [Flags]
    public enum WoodType : uint
    {
        OakWoodOrAcaciaWood = 0,
        SpruceWoodOrDarkOakWood = 1,
        BirchWood = 2,
        JungleWood = 3,

        // 0x4 ~ 0x8 bits field specifying the orientation of the wood
        FacingUpFlag = 0x0,
        FacingEastFlag = 0x4,
        FacingNorthFlag = 0x8,
        OnlybarkFlag = 0xC
    }

    public enum LeaveType : uint
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

    public enum DoubleStoneSlabType : uint
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

    public enum StoneSlabType : uint
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

    public enum DoubleWoodenSlabType : uint
    {
        DoubleOak = 0,
        DoubleSpruce = 1,
        DoubleBirch = 2,
        DoubleJungle = 3,
        DoubleAcacia = 4,
        DoubleDarkOak = 5
    }

    public enum WoodenSlabType : uint
    {
        Oak = 0,
        Spruce = 1,
        Birch = 2,
        Jungle = 3,
        Acacia = 4,
        DarkOak = 5,
        UpperOak = 8,
        UpperSpruce = 9,
        UpperBirch = 10,
        UpperJungle = 11,
        UpperAcacia = 12,
        UpperDarkOak = 13
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

    [Flags]
    public enum BedType : uint
    {
        HeadFacingSouth = 0,
        HeadFacingWest = 1,
        HeadFacingNorth = 2,
        HeadFacingEast = 3,

        // 0x4 bit field : When 0, the bed is empty, otherwise, the bed is occupied
        OccupiedFlag = 0x4,

        // 0x8 bit field : When 0, the foot of the bed, otherwise, the head of the bed
        HeadFlag = 0x8
    }

    public enum GrassType : uint
    {
        Shrub = 0,
        TallGrass = 1,
        Fern = 2
    }

    public enum FlowerType : uint
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

    [Flags]
    public enum LargeFlowerType : uint
    {
        Sunflower = 0,
        Lilac = 1,
        DoubleTallgrass = 2,
        LargeFern = 3,
        RoseBush = 4,
        Peony = 5,

        // 0x8 bit field : Top Half of any Large Plant; low three bits 0x7 are derived from the block below
        TopHalfFlag = 0x8
    }

    [Flags]
    public enum PistionType : uint
    {
        Down = 0,
        Up = 1,
        North = 2,
        South = 3,
        West = 4,
        East = 5,

        // 0x8 bit field : Determines whether the piston is pushed out or not. 1 for pushed out, 0 for retracted
        PushedOutFlag = 0x8
    }

    [Flags]
    public enum PistionExType : uint
    {
        Down = 0,
        Up = 1,
        North = 2,
        South = 3,
        West = 4,
        East = 5,

        // 0x8 bit field : Determines whether the head is sticky or not(note that the Piston Body actually
        // has completely different block types for Sticky and Regular). 1 is sticky, 0 is regular
        StickyFlag = 0x8
    }

    [Flags]
    public enum StairsType : uint
    {
        East = 0,
        West = 1,
        South = 2,
        North = 3,

        // 0x4 bit field : Set if stairs are upside-down
        UpsideDownFlag = 0x4
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

    public enum StandingBannerType : uint
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

    public enum WallBannerType : uint
    {
        North = 2,
        South = 3,
        West = 4,
        East = 5
    }

    [Flags]
    public enum DoorType : uint
    {
        NorthwestCorner = 0,
        NortheastCorner = 1,
        SoutheastCorner = 2,
        SouthwestCorner = 3,

        // 0x4 bit field : If set, the door has swung counterclockwise around its hinge
        HasSwungCounterclockwiseFlag = 0x4,

        // 0x8 bit field : If set, this is the top half of a door (else the bottom half of the door)
        TopHalfOfTheDoorFlag = 0x8
    }

    public enum RailType : uint
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
    public enum RailExType : uint
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

    public enum StandingSignType : uint
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

    public enum WallSignType : uint
    {
        North = 2,
        South = 3,
        West = 4,
        East = 5
    }

    /// <summary>
    /// 0x1 ~ 0x4 bits specifying which direction the dispenser is facing
    /// </summary>
    [Flags]
    public enum DispenserType : uint
    {
        FacingDown = 0,
        FacingUp = 1,
        FacingNorth = 2,
        FacingSouth = 3,
        FacingWest = 4,
        FacingEast = 5,

        // 0x8 bit field : If set, the dispenser is activated
        ActivatedFlag = 0x8
    }

    /// <summary>
    /// 0x1 ~ 0x4 bits specifying which direction the dropper is facing
    /// </summary>
    [Flags]
    public enum DropperType : uint
    {
        FacingDown = 0,
        FacingUp = 1,
        FacingNorth = 2,
        FacingSouth = 3,
        FacingWest = 4,
        FacingEast = 5,

        // 0x8 bit field : If set, the dropper is activated
        ActivatedFlag = 0x8
    }

    [Flags]
    public enum HopperType : uint
    {
        FacingDown = 0,
        FacingNorth = 2,
        FacingSouth = 3,
        FacingWest = 4,
        FacingEast = 5,

        // 0x8 bit field : Set if activated/disabled
        ActivatedFlag = 0x8
    }

    [Flags]
    public enum LeverType : uint
    {
        BottomPointsEastWhenOff = 0,
        SideFacingEast = 1,
        SideFacingWest = 2,
        SideFacingSouth = 3,
        SideFacingNorth = 4,
        TopPointsSouthWhenOff = 5,
        TopPointsEastWhenOff = 6,
        BottomPointsSouthWhenOff = 7,

        // 0x8 bit field : If set, the lever is active
        ActivatedFlag = 0x8
    }

    public enum PressurePlatesType : uint
    {
        None = 0,
        Active = 1
    }

    /// <summary>
    /// Represents the weighted pressure plate's current redstone power level.
    /// </summary>
    public enum WeightedPressurePlateType : uint
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

    [Flags]
    public enum ButtonType : uint
    {
        BottomFacingDown = 0,
        SideFacingEast = 1,
        SideFacingWest = 2,
        SideFacingSouth = 3,
        SideFacingNorth = 4,
        TopFacingUp = 5,

        // 0x8 bit field : If set, the button is currently active
        ActivatedFlag = 0x8
    }

    public enum SnowType : uint
    {
        OneLayer = 0,
        TwoLayers = 1,
        ThreeLayers = 2,
        FourLayers = 3,
        FiveLayers = 4,
        SixLayers = 5,
        SevenLayers = 6,
        EightLayers = 7
    }

    public enum CactusType : uint
    {
        FreshlyPlantedCactus = 0,
        Interval1 = 1,
        Interval2 = 2,
        Interval3 = 3,
        Interval4 = 4,
        Interval5 = 5,
        Interval6 = 6,
        Interval7 = 7,
        Interval8 = 8,
        Interval9 = 9,
        Interval10 = 10,
        Interval11 = 11,
        Interval12 = 12,
        Interval13 = 13,
        Interval14 = 14,
        Interval15 = 15
    }

    public enum SugarCaneType : uint
    {
        FreshlyPlantedSugarCane = 0,
        Interval1 = 1,
        Interval2 = 2,
        Interval3 = 3,
        Interval4 = 4,
        Interval5 = 5,
        Interval6 = 6,
        Interval7 = 7,
        Interval8 = 8,
        Interval9 = 9,
        Interval10 = 10,
        Interval11 = 11,
        Interval12 = 12,
        Interval13 = 13,
        Interval14 = 14,
        Interval15 = 15
    }

    public enum JuckboxType : uint
    {
        NoDiscInserted = 0,
        ContainsADisc = 1
    }

    public enum PumpkinType : uint
    {
        FacingSouth = 0,
        FacingWest = 1,
        FacingNorth = 2,
        FacingEast = 3,
        WithoutFace = 4
    }

    public enum JackLanternType : uint
    {
        FacingSouth = 0,
        FacingWest = 1,
        FacingNorth = 2,
        FacingEast = 3,
        WithoutFace = 4
    }

    public enum CakeType : uint
    {
        Eat0Pieces = 0,
        Eat1Piece = 1,
        Eat2Pieces = 2,
        Eat3Pieces = 3,
        Eat4Pieces = 4,
        Eat5Pieces = 5,
        Eat6Pieces = 6,
    }

    [Flags]
    public enum RedstoneRepeaterType : uint
    {
        FacingNorth = 0,
        FacingEast = 1,
        FacingSouth = 2,
        FacingWest = 3,

        // 0x4 ~ 0x8 bit field specifying the redstone repeater's delay
        RedstoneTick1Flag = 0x0,
        RedstoneTick2Flag = 0x4,
        RedstoneTick3Flag = 0x8,
        RedstoneTick4Flag = 0xC
    }

    [Flags]
    public enum RedstoneComparatorType : uint
    {
        FacingNorth = 0,
        FacingEast = 1,
        FacingSouth = 2,
        FacingWest = 3,

        // 0x4 bit field Set if in subtraction mode (front torch up and powered)
        SubtractionModeFlag = 0x4,

        // 0x8 bit field Set if powered (at any power level)
        PoweredFlag = 0x8
    }

    [Flags]
    public enum TrapdoorType : uint
    {
        SouthSide = 0,
        NorthSide = 1,
        EastSide = 2,
        WestSide = 3,

        // 0x4 bit field : If set, the trapdoor is open
        OpenFlag = 0x4,

        // 0x8 bit field : If set, the trapdoor is on the top half of a block. Otherwise, it is on the bottom half
        TopHalfFlag = 0x8
    }

    public enum MonsterEggType : uint
    {
        Stone = 0,
        Cobblestone = 1,
        StoneBrick = 2,
        MossyStoneBrick = 3,
        CrackedStoneBrick = 4,
        ChiseledStoneBrick = 5
    }

    public enum StoneBrickType : uint
    {
        StoneBrick = 0,
        MossyStoneBrick = 1,
        CrackedStoneBrick = 2,
        ChiseledStoneBrick = 3
    }

    public enum PrismarineType : uint
    {
        Prismarine = 0,
        PrismarineBricks = 1,
        DarkPrismarine = 2
    }

    public enum SpongeType : uint
    {
        Sponge = 0,
        WetSponge = 1
    }

    public enum MushroomBlockType : uint
    {
        PoresOnAllSides = 0,
        CapTextureOnTopWestNorth = 1,
        CapTextureOnTopNorth = 2,
        CapTextureOnTopNorthEast = 3,
        CapTextureOnTopWest = 4,
        CapTextureOnTop = 5,
        CapTextureOnTopEast = 6,
        CapTextureOnTopSouthWest = 7,
        CapTextureOnTopSouth = 8,
        CapTextureOnTopEastSouth = 9,
        StemTextureOnAllFourSidesPoresOnTopBottom = 10,
        CapTextureOnAllSixSides = 11,
        StemTextureOnAllSixSides = 12
    }

    public enum PumpkinMelonStemType : uint
    {
        FreshlyPlantedStem = 0,
        FirstStageOfGrowth = 1,
        SecondStageOfGrowth = 2,
        ThirdStageOfGrowth = 3,
        FourthStageOfGrowth = 4,
        FifthStageOfGrowth = 5,
        SixthStageOfGrowth = 6,
        SeventhStageOfGrowth = 7
    }

    /// <summary>
    /// Determines the face against which the vine is anchored.
    /// </summary>
    public enum VineType : uint
    {
        South = 1,
        West = 2,
        North = 4,
        East = 8
    }

    [Flags]
    public enum FenceGateType : uint
    {
        FacingSouth = 0,
        FacingWest = 1,
        FacingNorth = 2,
        FacingEast = 3,

        // 0x4 bit field : 0 if the gate is closed, 1 if open
        OpenFlag = 0x4
    }

    public enum NetherWartType : uint
    {
        GrowthStage1 = 0,
        GrowthStage2 = 1,
        GrowthStage3 = 2,
        GrowthStage4 = 3,
    }

    public enum BrewingStandType : uint
    {
        TheSlotPointingSast = 0x1,
        TheSlotPointingSouthwest = 0x2,
        TheSlotPointingNorthwest = 0x4
    }

    public enum CauldronType : uint
    {
        Empty = 0,
        LittleFilled = 1,
        ManyFilled = 2,
        FullFilled = 3
    }

    [Flags]
    public enum EndPortalFrameType : uint
    {
        South = 0,
        West = 1,
        North = 2,
        East = 3,

        // 0x4 bit field : 0 is an "empty" frame block, 1 is a block with an Eye of Ender inserted
        EyeOfEnderFlag = 0x4
    }

    [Flags]
    public enum CocoaType : uint
    {
        North = 0,
        East = 1,
        South = 2,
        West = 3,

        // 0x4 ~ 0x8 bit field specifying the stage of growth of the plant
        FirstStageFlag = 0x0,
        SecondStageFlag = 0x4,
        FinalStageFlag = 0x8
    }

    [Flags]
    public enum TripwireHookType : uint
    {
        FacingSouth = 0,
        FacingWest = 1,
        FacingNorth = 2,
        FacingEast = 3,

        // 0x4 bit field : If set, the tripwire hook is connected and ready to trip ("middle" position)
        ConnectedFlag = 0x4,

        // 0x8 bit field : If set, the tripwire hook is currently activated ("down" position)
        ActivatedFlag = 0x8
    }

    [Flags]
    public enum TripwireType : uint
    {
        None = 0,

        // 0x1 bit field : Set if tripwire is activated (an entity is intersecting its collision mask)
        ActivatedFlag = 0x1,

        // 0x4 bit field : Set if tripwire is attached to a valid tripwire circuit
        AttachedFlag = 0x4,

        // 0x8 bit field : Set if tripwire is disarmed
        DisarmedFlag = 0x8
    }

    public enum CobblestoneWallType : uint
    {
        CobblestoneWall = 0,
        MossyCobblestoneWall = 1
    }

    public enum FlowerPotType : uint
    {
        Empty = 0,
        Poppy = 1,
        Dandelion = 2,
        OakSapling = 3,
        SpruceSapling = 4,
        BirchSapling = 5,
        JungleSapling = 6,
        RedMushroom = 7,
        BrownMushroom = 8,
        Cactus = 9,
        DeadBush = 10,
        Fern = 11,
        AcaciaSapling = 12,
        DarkOakSapling = 13
    }

    public enum HeadForBlockType : uint
    {
        OnTheFloor = 1,
        OnAWallFacingNorth = 2,
        OnAWallFacingSouth = 3,
        OnAWallFacingEast = 4,
        OnAWallFacingWest = 5
    }

    public enum HeadForItemType : uint
    {
        SkeletonSkull = 0,
        WitherSkeletonSkull = 1,
        ZombieHead = 2,
        Head = 3,
        CreeperHead = 4,
        DragonHead = 5
    }

    public enum BlockOfQuartzType : uint
    {
        BlockOfQuartz = 0,
        ChiseledQuartzBlock = 1,
        PillarQuartzBlockVertical = 2,
        PillarQuartzBlockNorthSouth = 3,
        PillarQuartzBlockEastWest = 4
    }

    public enum CoalType : uint
    {
        Coal = 0,
        Charcoal = 1
    }

    public enum DyeType : uint
    {
        InkSac = 0,
        RoseRed = 1,
        CactusGreen = 2,
        CocoaBeans = 3,
        LapisLazuli = 4,
        PurpleDye = 5,
        CyanDye = 6,
        LightGrayDye = 7,
        GrayDye = 8,
        PinkDye = 9,
        LimeDye = 10,
        DandelionYellow = 11,
        LightBlueDye = 12,
        MagentaDye = 13,
        OrangeDye = 14,
        BoneMeal = 15
    }

    public enum FishType : uint
    {
        RawFishOrCookedFish = 0,
        RawSalmonOrCookedSalmon = 1,
        Clownfish = 2,
        Pufferfish = 3
    }

    public enum AnvilForItemType : uint
    {
        Anvil = 0,
        SlightlyDamagedAnvil = 1,
        VeryDamagedAnvil = 2
    }

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
