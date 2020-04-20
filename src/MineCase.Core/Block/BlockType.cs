using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase.Block
{
    public enum BlockId : uint
    {
        Air = 0,
        Stone = 1,
        Granite = 2,
        PolishedGranite = 3,
        Diorite = 4,
        PolishedDiorite = 5,
        Andesite = 6,
        PolishedAndesite = 7,
        GrassBlock = 8,
        Dirt = 10,
        CoarseDirt = 11,
        Podzol = 12,
        Cobblestone = 14,
        OakPlanks = 15,
        SprucePlanks = 16,
        BirchPlanks = 17,
        JunglePlanks = 18,
        AcaciaPlanks = 19,
        DarkOakPlanks = 20,
        OakSapling = 21,
        SpruceSapling = 23,
        BirchSapling = 25,
        JungleSapling = 27,
        AcaciaSapling = 29,
        DarkOakSapling = 31,
        Bedrock = 33,
        Water = 34,
        Lava = 50,
        Sand = 66,
        RedSand = 67,
        Gravel = 68,
        GoldOre = 69,
        IronOre = 70,
        CoalOre = 71,
        OakLog = 72,
        SpruceLog = 75,
        BirchLog = 78,
        JungleLog = 81,
        AcaciaLog = 84,
        DarkOakLog = 87,
        StrippedSpruceLog = 90,
        StrippedBirchLog = 93,
        StrippedJungleLog = 96,
        StrippedAcaciaLog = 99,
        StrippedDarkOakLog = 102,
        StrippedOakLog = 105,
        OakWood = 108,
        SpruceWood = 111,
        BirchWood = 114,
        JungleWood = 117,
        AcaciaWood = 120,
        DarkOakWood = 123,
        StrippedOakWood = 126,
        StrippedSpruceWood = 129,
        StrippedBirchWood = 132,
        StrippedJungleWood = 135,
        StrippedAcaciaWood = 138,
        StrippedDarkOakWood = 141,
        OakLeaves = 144,
        SpruceLeaves = 158,
        BirchLeaves = 172,
        JungleLeaves = 186,
        AcaciaLeaves = 200,
        DarkOakLeaves = 214,
        Sponge = 228,
        WetSponge = 229,
        Glass = 230,
        LapisOre = 231,
        LapisBlock = 232,
        Dispenser = 233,
        Sandstone = 245,
        ChiseledSandstone = 246,
        CutSandstone = 247,
        NoteBlock = 248,
        WhiteBed = 1048,
        OrangeBed = 1064,
        MagentaBed = 1080,
        LightBlueBed = 1096,
        YellowBed = 1112,
        LimeBed = 1128,
        PinkBed = 1144,
        GrayBed = 1160,
        LightGrayBed = 1176,
        CyanBed = 1192,
        PurpleBed = 1208,
        BlueBed = 1224,
        BrownBed = 1240,
        GreenBed = 1256,
        RedBed = 1272,
        BlackBed = 1288,
        PoweredRail = 1304,
        DetectorRail = 1316,
        StickyPiston = 1328,
        Cobweb = 1340,
        Grass = 1341,
        Fern = 1342,
        DeadBush = 1343,
        Seagrass = 1344,
        TallSeagrass = 1345,
        Piston = 1347,
        PistonHead = 1359,
        WhiteWool = 1383,
        OrangeWool = 1384,
        MagentaWool = 1385,
        LightBlueWool = 1386,
        YellowWool = 1387,
        LimeWool = 1388,
        PinkWool = 1389,
        GrayWool = 1390,
        LightGrayWool = 1391,
        CyanWool = 1392,
        PurpleWool = 1393,
        BlueWool = 1394,
        BrownWool = 1395,
        GreenWool = 1396,
        RedWool = 1397,
        BlackWool = 1398,
        MovingPiston = 1399,
        Dandelion = 1411,
        Poppy = 1412,
        BlueOrchid = 1413,
        Allium = 1414,
        AzureBluet = 1415,
        RedTulip = 1416,
        OrangeTulip = 1417,
        WhiteTulip = 1418,
        PinkTulip = 1419,
        OxeyeDaisy = 1420,
        Cornflower = 1421,
        WitherRose = 1422,
        LilyOfTheValley = 1423,
        BrownMushroom = 1424,
        RedMushroom = 1425,
        GoldBlock = 1426,
        IronBlock = 1427,
        Bricks = 1428,
        Tnt = 1429,
        Bookshelf = 1431,
        MossyCobblestone = 1432,
        Obsidian = 1433,
        Torch = 1434,
        WallTorch = 1435,
        Fire = 1439,
        Spawner = 1951,
        OakStairs = 1952,
        Chest = 2032,
        RedstoneWire = 2056,
        DiamondOre = 3352,
        DiamondBlock = 3353,
        CraftingTable = 3354,
        Wheat = 3355,
        Farmland = 3363,
        Furnace = 3371,
        OakSign = 3379,
        SpruceSign = 3411,
        BirchSign = 3443,
        AcaciaSign = 3475,
        JungleSign = 3507,
        DarkOakSign = 3539,
        OakDoor = 3571,
        Ladder = 3635,
        Rail = 3643,
        CobblestoneStairs = 3653,
        OakWallSign = 3733,
        SpruceWallSign = 3741,
        BirchWallSign = 3749,
        AcaciaWallSign = 3757,
        JungleWallSign = 3765,
        DarkOakWallSign = 3773,
        Lever = 3781,
        StonePressurePlate = 3805,
        IronDoor = 3807,
        OakPressurePlate = 3871,
        SprucePressurePlate = 3873,
        BirchPressurePlate = 3875,
        JunglePressurePlate = 3877,
        AcaciaPressurePlate = 3879,
        DarkOakPressurePlate = 3881,
        RedstoneOre = 3883,
        RedstoneTorch = 3885,
        RedstoneWallTorch = 3887,
        StoneButton = 3895,
        Snow = 3919,
        Ice = 3927,
        SnowBlock = 3928,
        Cactus = 3929,
        Clay = 3945,
        SugarCane = 3946,
        Jukebox = 3962,
        OakFence = 3964,
        Pumpkin = 3996,
        Netherrack = 3997,
        SoulSand = 3998,
        Glowstone = 3999,
        NetherPortal = 4000,
        CarvedPumpkin = 4002,
        JackOLantern = 4006,
        Cake = 4010,
        Repeater = 4017,
        WhiteStainedGlass = 4081,
        OrangeStainedGlass = 4082,
        MagentaStainedGlass = 4083,
        LightBlueStainedGlass = 4084,
        YellowStainedGlass = 4085,
        LimeStainedGlass = 4086,
        PinkStainedGlass = 4087,
        GrayStainedGlass = 4088,
        LightGrayStainedGlass = 4089,
        CyanStainedGlass = 4090,
        PurpleStainedGlass = 4091,
        BlueStainedGlass = 4092,
        BrownStainedGlass = 4093,
        GreenStainedGlass = 4094,
        RedStainedGlass = 4095,
        BlackStainedGlass = 4096,
        OakTrapdoor = 4097,
        SpruceTrapdoor = 4161,
        BirchTrapdoor = 4225,
        JungleTrapdoor = 4289,
        AcaciaTrapdoor = 4353,
        DarkOakTrapdoor = 4417,
        StoneBricks = 4481,
        MossyStoneBricks = 4482,
        CrackedStoneBricks = 4483,
        ChiseledStoneBricks = 4484,
        InfestedStone = 4485,
        InfestedCobblestone = 4486,
        InfestedStoneBricks = 4487,
        InfestedMossyStoneBricks = 4488,
        InfestedCrackedStoneBricks = 4489,
        InfestedChiseledStoneBricks = 4490,
        BrownMushroomBlock = 4491,
        RedMushroomBlock = 4555,
        MushroomStem = 4619,
        IronBars = 4683,
        GlassPane = 4715,
        Melon = 4747,
        AttachedPumpkinStem = 4748,
        AttachedMelonStem = 4752,
        PumpkinStem = 4756,
        MelonStem = 4764,
        Vine = 4772,
        OakFenceGate = 4804,
        BrickStairs = 4836,
        StoneBrickStairs = 4916,
        Mycelium = 4996,
        LilyPad = 4998,
        NetherBricks = 4999,
        NetherBrickFence = 5000,
        NetherBrickStairs = 5032,
        NetherWart = 5112,
        EnchantingTable = 5116,
        BrewingStand = 5117,
        Cauldron = 5125,
        EndPortal = 5129,
        EndPortalFrame = 5130,
        EndStone = 5138,
        DragonEgg = 5139,
        RedstoneLamp = 5140,
        Cocoa = 5142,
        SandstoneStairs = 5154,
        EmeraldOre = 5234,
        EnderChest = 5235,
        TripwireHook = 5243,
        Tripwire = 5259,
        EmeraldBlock = 5387,
        SpruceStairs = 5388,
        BirchStairs = 5468,
        JungleStairs = 5548,
        CommandBlock = 5628,
        Beacon = 5640,
        CobblestoneWall = 5641,
        MossyCobblestoneWall = 5705,
        FlowerPot = 5769,
        PottedOakSapling = 5770,
        PottedSpruceSapling = 5771,
        PottedBirchSapling = 5772,
        PottedJungleSapling = 5773,
        PottedAcaciaSapling = 5774,
        PottedDarkOakSapling = 5775,
        PottedFern = 5776,
        PottedDandelion = 5777,
        PottedPoppy = 5778,
        PottedBlueOrchid = 5779,
        PottedAllium = 5780,
        PottedAzureBluet = 5781,
        PottedRedTulip = 5782,
        PottedOrangeTulip = 5783,
        PottedWhiteTulip = 5784,
        PottedPinkTulip = 5785,
        PottedOxeyeDaisy = 5786,
        PottedCornflower = 5787,
        PottedLilyOfTheValley = 5788,
        PottedWitherRose = 5789,
        PottedRedMushroom = 5790,
        PottedBrownMushroom = 5791,
        PottedDeadBush = 5792,
        PottedCactus = 5793,
        Carrots = 5794,
        Potatoes = 5802,
        OakButton = 5810,
        SpruceButton = 5834,
        BirchButton = 5858,
        JungleButton = 5882,
        AcaciaButton = 5906,
        DarkOakButton = 5930,
        SkeletonSkull = 5954,
        SkeletonWallSkull = 5970,
        WitherSkeletonSkull = 5974,
        WitherSkeletonWallSkull = 5990,
        ZombieHead = 5994,
        ZombieWallHead = 6010,
        PlayerHead = 6014,
        PlayerWallHead = 6030,
        CreeperHead = 6034,
        CreeperWallHead = 6050,
        DragonHead = 6054,
        DragonWallHead = 6070,
        Anvil = 6074,
        ChippedAnvil = 6078,
        DamagedAnvil = 6082,
        TrappedChest = 6086,
        LightWeightedPressurePlate = 6110,
        HeavyWeightedPressurePlate = 6126,
        Comparator = 6142,
        DaylightDetector = 6158,
        RedstoneBlock = 6190,
        NetherQuartzOre = 6191,
        Hopper = 6192,
        QuartzBlock = 6202,
        ChiseledQuartzBlock = 6203,
        QuartzPillar = 6204,
        QuartzStairs = 6207,
        ActivatorRail = 6287,
        Dropper = 6299,
        WhiteTerracotta = 6311,
        OrangeTerracotta = 6312,
        MagentaTerracotta = 6313,
        LightBlueTerracotta = 6314,
        YellowTerracotta = 6315,
        LimeTerracotta = 6316,
        PinkTerracotta = 6317,
        GrayTerracotta = 6318,
        LightGrayTerracotta = 6319,
        CyanTerracotta = 6320,
        PurpleTerracotta = 6321,
        BlueTerracotta = 6322,
        BrownTerracotta = 6323,
        GreenTerracotta = 6324,
        RedTerracotta = 6325,
        BlackTerracotta = 6326,
        WhiteStainedGlassPane = 6327,
        OrangeStainedGlassPane = 6359,
        MagentaStainedGlassPane = 6391,
        LightBlueStainedGlassPane = 6423,
        YellowStainedGlassPane = 6455,
        LimeStainedGlassPane = 6487,
        PinkStainedGlassPane = 6519,
        GrayStainedGlassPane = 6551,
        LightGrayStainedGlassPane = 6583,
        CyanStainedGlassPane = 6615,
        PurpleStainedGlassPane = 6647,
        BlueStainedGlassPane = 6679,
        BrownStainedGlassPane = 6711,
        GreenStainedGlassPane = 6743,
        RedStainedGlassPane = 6775,
        BlackStainedGlassPane = 6807,
        AcaciaStairs = 6839,
        DarkOakStairs = 6919,
        SlimeBlock = 6999,
        Barrier = 7000,
        IronTrapdoor = 7001,
        Prismarine = 7065,
        PrismarineBricks = 7066,
        DarkPrismarine = 7067,
        PrismarineStairs = 7068,
        PrismarineBrickStairs = 7148,
        DarkPrismarineStairs = 7228,
        PrismarineSlab = 7308,
        PrismarineBrickSlab = 7314,
        DarkPrismarineSlab = 7320,
        SeaLantern = 7326,
        HayBlock = 7327,
        WhiteCarpet = 7330,
        OrangeCarpet = 7331,
        MagentaCarpet = 7332,
        LightBlueCarpet = 7333,
        YellowCarpet = 7334,
        LimeCarpet = 7335,
        PinkCarpet = 7336,
        GrayCarpet = 7337,
        LightGrayCarpet = 7338,
        CyanCarpet = 7339,
        PurpleCarpet = 7340,
        BlueCarpet = 7341,
        BrownCarpet = 7342,
        GreenCarpet = 7343,
        RedCarpet = 7344,
        BlackCarpet = 7345,
        Terracotta = 7346,
        CoalBlock = 7347,
        PackedIce = 7348,
        Sunflower = 7349,
        Lilac = 7351,
        RoseBush = 7353,
        Peony = 7355,
        TallGrass = 7357,
        LargeFern = 7359,
        WhiteBanner = 7361,
        OrangeBanner = 7377,
        MagentaBanner = 7393,
        LightBlueBanner = 7409,
        YellowBanner = 7425,
        LimeBanner = 7441,
        PinkBanner = 7457,
        GrayBanner = 7473,
        LightGrayBanner = 7489,
        CyanBanner = 7505,
        PurpleBanner = 7521,
        BlueBanner = 7537,
        BrownBanner = 7553,
        GreenBanner = 7569,
        RedBanner = 7585,
        BlackBanner = 7601,
        WhiteWallBanner = 7617,
        OrangeWallBanner = 7621,
        MagentaWallBanner = 7625,
        LightBlueWallBanner = 7629,
        YellowWallBanner = 7633,
        LimeWallBanner = 7637,
        PinkWallBanner = 7641,
        GrayWallBanner = 7645,
        LightGrayWallBanner = 7649,
        CyanWallBanner = 7653,
        PurpleWallBanner = 7657,
        BlueWallBanner = 7661,
        BrownWallBanner = 7665,
        GreenWallBanner = 7669,
        RedWallBanner = 7673,
        BlackWallBanner = 7677,
        RedSandstone = 7681,
        ChiseledRedSandstone = 7682,
        CutRedSandstone = 7683,
        RedSandstoneStairs = 7684,
        OakSlab = 7764,
        SpruceSlab = 7770,
        BirchSlab = 7776,
        JungleSlab = 7782,
        AcaciaSlab = 7788,
        DarkOakSlab = 7794,
        StoneSlab = 7800,
        SmoothStoneSlab = 7806,
        SandstoneSlab = 7812,
        CutSandstoneSlab = 7818,
        PetrifiedOakSlab = 7824,
        CobblestoneSlab = 7830,
        BrickSlab = 7836,
        StoneBrickSlab = 7842,
        NetherBrickSlab = 7848,
        QuartzSlab = 7854,
        RedSandstoneSlab = 7860,
        CutRedSandstoneSlab = 7866,
        PurpurSlab = 7872,
        SmoothStone = 7878,
        SmoothSandstone = 7879,
        SmoothQuartz = 7880,
        SmoothRedSandstone = 7881,
        SpruceFenceGate = 7882,
        BirchFenceGate = 7914,
        JungleFenceGate = 7946,
        AcaciaFenceGate = 7978,
        DarkOakFenceGate = 8010,
        SpruceFence = 8042,
        BirchFence = 8074,
        JungleFence = 8106,
        AcaciaFence = 8138,
        DarkOakFence = 8170,
        SpruceDoor = 8202,
        BirchDoor = 8266,
        JungleDoor = 8330,
        AcaciaDoor = 8394,
        DarkOakDoor = 8458,
        EndRod = 8522,
        ChorusPlant = 8528,
        ChorusFlower = 8592,
        PurpurBlock = 8598,
        PurpurPillar = 8599,
        PurpurStairs = 8602,
        EndStoneBricks = 8682,
        Beetroots = 8683,
        GrassPath = 8687,
        EndGateway = 8688,
        RepeatingCommandBlock = 8689,
        ChainCommandBlock = 8701,
        FrostedIce = 8713,
        MagmaBlock = 8717,
        NetherWartBlock = 8718,
        RedNetherBricks = 8719,
        BoneBlock = 8720,
        StructureVoid = 8723,
        Observer = 8724,
        ShulkerBox = 8736,
        WhiteShulkerBox = 8742,
        OrangeShulkerBox = 8748,
        MagentaShulkerBox = 8754,
        LightBlueShulkerBox = 8760,
        YellowShulkerBox = 8766,
        LimeShulkerBox = 8772,
        PinkShulkerBox = 8778,
        GrayShulkerBox = 8784,
        LightGrayShulkerBox = 8790,
        CyanShulkerBox = 8796,
        PurpleShulkerBox = 8802,
        BlueShulkerBox = 8808,
        BrownShulkerBox = 8814,
        GreenShulkerBox = 8820,
        RedShulkerBox = 8826,
        BlackShulkerBox = 8832,
        WhiteGlazedTerracotta = 8838,
        OrangeGlazedTerracotta = 8842,
        MagentaGlazedTerracotta = 8846,
        LightBlueGlazedTerracotta = 8850,
        YellowGlazedTerracotta = 8854,
        LimeGlazedTerracotta = 8858,
        PinkGlazedTerracotta = 8862,
        GrayGlazedTerracotta = 8866,
        LightGrayGlazedTerracotta = 8870,
        CyanGlazedTerracotta = 8874,
        PurpleGlazedTerracotta = 8878,
        BlueGlazedTerracotta = 8882,
        BrownGlazedTerracotta = 8886,
        GreenGlazedTerracotta = 8890,
        RedGlazedTerracotta = 8894,
        BlackGlazedTerracotta = 8898,
        WhiteConcrete = 8902,
        OrangeConcrete = 8903,
        MagentaConcrete = 8904,
        LightBlueConcrete = 8905,
        YellowConcrete = 8906,
        LimeConcrete = 8907,
        PinkConcrete = 8908,
        GrayConcrete = 8909,
        LightGrayConcrete = 8910,
        CyanConcrete = 8911,
        PurpleConcrete = 8912,
        BlueConcrete = 8913,
        BrownConcrete = 8914,
        GreenConcrete = 8915,
        RedConcrete = 8916,
        BlackConcrete = 8917,
        WhiteConcretePowder = 8918,
        OrangeConcretePowder = 8919,
        MagentaConcretePowder = 8920,
        LightBlueConcretePowder = 8921,
        YellowConcretePowder = 8922,
        LimeConcretePowder = 8923,
        PinkConcretePowder = 8924,
        GrayConcretePowder = 8925,
        LightGrayConcretePowder = 8926,
        CyanConcretePowder = 8927,
        PurpleConcretePowder = 8928,
        BlueConcretePowder = 8929,
        BrownConcretePowder = 8930,
        GreenConcretePowder = 8931,
        RedConcretePowder = 8932,
        BlackConcretePowder = 8933,
        Kelp = 8934,
        KelpPlant = 8960,
        DriedKelpBlock = 8961,
        TurtleEgg = 8962,
        DeadTubeCoralBlock = 8974,
        DeadBrainCoralBlock = 8975,
        DeadBubbleCoralBlock = 8976,
        DeadFireCoralBlock = 8977,
        DeadHornCoralBlock = 8978,
        TubeCoralBlock = 8979,
        BrainCoralBlock = 8980,
        BubbleCoralBlock = 8981,
        FireCoralBlock = 8982,
        HornCoralBlock = 8983,
        DeadTubeCoral = 8984,
        DeadBrainCoral = 8986,
        DeadBubbleCoral = 8988,
        DeadFireCoral = 8990,
        DeadHornCoral = 8992,
        TubeCoral = 8994,
        BrainCoral = 8996,
        BubbleCoral = 8998,
        FireCoral = 9000,
        HornCoral = 9002,
        DeadTubeCoralFan = 9004,
        DeadBrainCoralFan = 9006,
        DeadBubbleCoralFan = 9008,
        DeadFireCoralFan = 9010,
        DeadHornCoralFan = 9012,
        TubeCoralFan = 9014,
        BrainCoralFan = 9016,
        BubbleCoralFan = 9018,
        FireCoralFan = 9020,
        HornCoralFan = 9022,
        DeadTubeCoralWallFan = 9024,
        DeadBrainCoralWallFan = 9032,
        DeadBubbleCoralWallFan = 9040,
        DeadFireCoralWallFan = 9048,
        DeadHornCoralWallFan = 9056,
        TubeCoralWallFan = 9064,
        BrainCoralWallFan = 9072,
        BubbleCoralWallFan = 9080,
        FireCoralWallFan = 9088,
        HornCoralWallFan = 9096,
        SeaPickle = 9104,
        BlueIce = 9112,
        Conduit = 9113,
        BambooSapling = 9115,
        Bamboo = 9116,
        PottedBamboo = 9128,
        VoidAir = 9129,
        CaveAir = 9130,
        BubbleColumn = 9131,
        PolishedGraniteStairs = 9133,
        SmoothRedSandstoneStairs = 9213,
        MossyStoneBrickStairs = 9293,
        PolishedDioriteStairs = 9373,
        MossyCobblestoneStairs = 9453,
        EndStoneBrickStairs = 9533,
        StoneStairs = 9613,
        SmoothSandstoneStairs = 9693,
        SmoothQuartzStairs = 9773,
        GraniteStairs = 9853,
        AndesiteStairs = 9933,
        RedNetherBrickStairs = 10013,
        PolishedAndesiteStairs = 10093,
        DioriteStairs = 10173,
        PolishedGraniteSlab = 10253,
        SmoothRedSandstoneSlab = 10259,
        MossyStoneBrickSlab = 10265,
        PolishedDioriteSlab = 10271,
        MossyCobblestoneSlab = 10277,
        EndStoneBrickSlab = 10283,
        SmoothSandstoneSlab = 10289,
        SmoothQuartzSlab = 10295,
        GraniteSlab = 10301,
        AndesiteSlab = 10307,
        RedNetherBrickSlab = 10313,
        PolishedAndesiteSlab = 10319,
        DioriteSlab = 10325,
        BrickWall = 10331,
        PrismarineWall = 10395,
        RedSandstoneWall = 10459,
        MossyStoneBrickWall = 10523,
        GraniteWall = 10587,
        StoneBrickWall = 10651,
        NetherBrickWall = 10715,
        AndesiteWall = 10779,
        RedNetherBrickWall = 10843,
        SandstoneWall = 10907,
        EndStoneBrickWall = 10971,
        DioriteWall = 11035,
        Scaffolding = 11099,
        Loom = 11131,
        Barrel = 11135,
        Smoker = 11147,
        BlastFurnace = 11155,
        CartographyTable = 11163,
        FletchingTable = 11164,
        Grindstone = 11165,
        Lectern = 11177,
        SmithingTable = 11193,
        Stonecutter = 11194,
        Bell = 11198,
        Lantern = 11230,
        Campfire = 11232,
        SweetBerryBush = 11264,
        StructureBlock = 11268,
        Jigsaw = 11272,
        Composter = 11278,
        BeeNest = 11287,
        Beehive = 11311,
        HoneyBlock = 11335,
        HoneycombBlock = 11336,
    }

    public static class BlockType
    {
        public const int BlockIdMax = 11337;

        public static uint[] BaseBlockIdTable { get; } = new uint[BlockIdMax];

        static BlockType()
        {
            for (int i = 0; i < BlockIdMax; ++i)
            {
                BaseBlockIdTable[i] = 0;
            }

            foreach (uint foo in Enum.GetValues(typeof(BlockId)))
            {
                BaseBlockIdTable[foo] = foo;
            }

            uint lastId = 0;
            for (int i = 0; i < BlockIdMax; ++i)
            {
                if (BaseBlockIdTable[i] != 0)
                {
                    lastId = BaseBlockIdTable[i];
                }

                BaseBlockIdTable[i] = lastId;
            }
        }

        public static (uint id, uint meta) ParseBlockStateId(uint value)
        {
            var id = BlockType.BaseBlockIdTable[value];
            var meta = value - id;
            return (id, meta);
        }
    }

    public enum GrassBlockSnowyType : uint
    {
        True = 0,
        False = 1,
    }

    public enum PodzolSnowyType : uint
    {
        True = 0,
        False = 1,
    }

    public enum OakSaplingStageType : uint
    {
        Stage0 = 0,
        Stage1 = 1,
    }

    public enum SpruceSaplingStageType : uint
    {
        Stage0 = 0,
        Stage1 = 1,
    }

    public enum BirchSaplingStageType : uint
    {
        Stage0 = 0,
        Stage1 = 1,
    }

    public enum JungleSaplingStageType : uint
    {
        Stage0 = 0,
        Stage1 = 1,
    }

    public enum AcaciaSaplingStageType : uint
    {
        Stage0 = 0,
        Stage1 = 1,
    }

    public enum DarkOakSaplingStageType : uint
    {
        Stage0 = 0,
        Stage1 = 1,
    }

    public enum WaterLevelType : uint
    {
        Level0 = 0,
        Level1 = 1,
        Level2 = 2,
        Level3 = 3,
        Level4 = 4,
        Level5 = 5,
        Level6 = 6,
        Level7 = 7,
        Level8 = 8,
        Level9 = 9,
        Level10 = 10,
        Level11 = 11,
        Level12 = 12,
        Level13 = 13,
        Level14 = 14,
        Level15 = 15,

        // 0x8 bit field : If set, this liquid is "falling" and only spreads downward
        FallingFlag = 0x8
    }

    public enum LavaLevelType : uint
    {
        Level0 = 0,
        Level1 = 1,
        Level2 = 2,
        Level3 = 3,
        Level4 = 4,
        Level5 = 5,
        Level6 = 6,
        Level7 = 7,
        Level8 = 8,
        Level9 = 9,
        Level10 = 10,
        Level11 = 11,
        Level12 = 12,
        Level13 = 13,
        Level14 = 14,
        Level15 = 15,

        // 0x8 bit field : If set, this liquid is "falling" and only spreads downward
        FallingFlag = 0x8
    }

    public enum OakLogAxisType : uint
    {
        X = 0,
        Y = 1,
        Z = 2,
    }

    public enum SpruceLogAxisType : uint
    {
        X = 0,
        Y = 1,
        Z = 2,
    }

    public enum BirchLogAxisType : uint
    {
        X = 0,
        Y = 1,
        Z = 2,
    }

    public enum JungleLogAxisType : uint
    {
        X = 0,
        Y = 1,
        Z = 2,
    }

    public enum AcaciaLogAxisType : uint
    {
        X = 0,
        Y = 1,
        Z = 2,
    }

    public enum DarkOakLogAxisType : uint
    {
        X = 0,
        Y = 1,
        Z = 2,
    }

    public enum StrippedSpruceLogAxisType : uint
    {
        X = 0,
        Y = 1,
        Z = 2,
    }

    public enum StrippedBirchLogAxisType : uint
    {
        X = 0,
        Y = 1,
        Z = 2,
    }

    public enum StrippedJungleLogAxisType : uint
    {
        X = 0,
        Y = 1,
        Z = 2,
    }

    public enum StrippedAcaciaLogAxisType : uint
    {
        X = 0,
        Y = 1,
        Z = 2,
    }

    public enum StrippedDarkOakLogAxisType : uint
    {
        X = 0,
        Y = 1,
        Z = 2,
    }

    public enum StrippedOakLogAxisType : uint
    {
        X = 0,
        Y = 1,
        Z = 2,
    }

    public enum OakWoodAxisType : uint
    {
        X = 0,
        Y = 1,
        Z = 2,
    }

    public enum SpruceWoodAxisType : uint
    {
        X = 0,
        Y = 1,
        Z = 2,
    }

    public enum BirchWoodAxisType : uint
    {
        X = 0,
        Y = 1,
        Z = 2,
    }

    public enum JungleWoodAxisType : uint
    {
        X = 0,
        Y = 1,
        Z = 2,
    }

    public enum AcaciaWoodAxisType : uint
    {
        X = 0,
        Y = 1,
        Z = 2,
    }

    public enum DarkOakWoodAxisType : uint
    {
        X = 0,
        Y = 1,
        Z = 2,
    }

    public enum StrippedOakWoodAxisType : uint
    {
        X = 0,
        Y = 1,
        Z = 2,
    }

    public enum StrippedSpruceWoodAxisType : uint
    {
        X = 0,
        Y = 1,
        Z = 2,
    }

    public enum StrippedBirchWoodAxisType : uint
    {
        X = 0,
        Y = 1,
        Z = 2,
    }

    public enum StrippedJungleWoodAxisType : uint
    {
        X = 0,
        Y = 1,
        Z = 2,
    }

    public enum StrippedAcaciaWoodAxisType : uint
    {
        X = 0,
        Y = 1,
        Z = 2,
    }

    public enum StrippedDarkOakWoodAxisType : uint
    {
        X = 0,
        Y = 1,
        Z = 2,
    }

    public enum OakLeavesDistanceType : uint
    {
        Distance1 = 0,
        Distance2 = 1,
        Distance3 = 2,
        Distance4 = 3,
        Distance5 = 4,
        Distance6 = 5,
        Distance7 = 6,
    }

    public enum OakLeavesPersistentType : uint
    {
        True = 0,
        False = 1,
    }

    public enum SpruceLeavesDistanceType : uint
    {
        Distance1 = 0,
        Distance2 = 1,
        Distance3 = 2,
        Distance4 = 3,
        Distance5 = 4,
        Distance6 = 5,
        Distance7 = 6,
    }

    public enum SpruceLeavesPersistentType : uint
    {
        True = 0,
        False = 1,
    }

    public enum BirchLeavesDistanceType : uint
    {
        Distance1 = 0,
        Distance2 = 1,
        Distance3 = 2,
        Distance4 = 3,
        Distance5 = 4,
        Distance6 = 5,
        Distance7 = 6,
    }

    public enum BirchLeavesPersistentType : uint
    {
        True = 0,
        False = 1,
    }

    public enum JungleLeavesDistanceType : uint
    {
        Distance1 = 0,
        Distance2 = 1,
        Distance3 = 2,
        Distance4 = 3,
        Distance5 = 4,
        Distance6 = 5,
        Distance7 = 6,
    }

    public enum JungleLeavesPersistentType : uint
    {
        True = 0,
        False = 1,
    }

    public enum AcaciaLeavesDistanceType : uint
    {
        Distance1 = 0,
        Distance2 = 1,
        Distance3 = 2,
        Distance4 = 3,
        Distance5 = 4,
        Distance6 = 5,
        Distance7 = 6,
    }

    public enum AcaciaLeavesPersistentType : uint
    {
        True = 0,
        False = 1,
    }

    public enum DarkOakLeavesDistanceType : uint
    {
        Distance1 = 0,
        Distance2 = 1,
        Distance3 = 2,
        Distance4 = 3,
        Distance5 = 4,
        Distance6 = 5,
        Distance7 = 6,
    }

    public enum DarkOakLeavesPersistentType : uint
    {
        True = 0,
        False = 1,
    }

    public enum DispenserFacingType : uint
    {
        North = 0,
        East = 1,
        South = 2,
        West = 3,
        Up = 4,
        Down = 5,
    }

    public enum DispenserTriggeredType : uint
    {
        True = 0,
        False = 1,
    }

    public enum NoteBlockInstrumentType : uint
    {
        Harp = 0,
        Basedrum = 1,
        Snare = 2,
        Hat = 3,
        Bass = 4,
        Flute = 5,
        Bell = 6,
        Guitar = 7,
        Chime = 8,
        Xylophone = 9,
        IronXylophone = 10,
        CowBell = 11,
        Didgeridoo = 12,
        Bit = 13,
        Banjo = 14,
        Pling = 15,
    }

    public enum NoteBlockNoteType : uint
    {
        Note0 = 0,
        Note1 = 1,
        Note2 = 2,
        Note3 = 3,
        Note4 = 4,
        Note5 = 5,
        Note6 = 6,
        Note7 = 7,
        Note8 = 8,
        Note9 = 9,
        Note10 = 10,
        Note11 = 11,
        Note12 = 12,
        Note13 = 13,
        Note14 = 14,
        Note15 = 15,
        Note16 = 16,
        Note17 = 17,
        Note18 = 18,
        Note19 = 19,
        Note20 = 20,
        Note21 = 21,
        Note22 = 22,
        Note23 = 23,
        Note24 = 24,
    }

    public enum NoteBlockPoweredType : uint
    {
        True = 0,
        False = 1,
    }

    public enum WhiteBedFacingType : uint
    {
        North = 0,
        South = 1,
        West = 2,
        East = 3,
    }

    public enum WhiteBedOccupiedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum WhiteBedPartType : uint
    {
        Head = 0,
        Foot = 1,
    }

    public enum OrangeBedFacingType : uint
    {
        North = 0,
        South = 1,
        West = 2,
        East = 3,
    }

    public enum OrangeBedOccupiedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum OrangeBedPartType : uint
    {
        Head = 0,
        Foot = 1,
    }

    public enum MagentaBedFacingType : uint
    {
        North = 0,
        South = 1,
        West = 2,
        East = 3,
    }

    public enum MagentaBedOccupiedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum MagentaBedPartType : uint
    {
        Head = 0,
        Foot = 1,
    }

    public enum LightBlueBedFacingType : uint
    {
        North = 0,
        South = 1,
        West = 2,
        East = 3,
    }

    public enum LightBlueBedOccupiedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum LightBlueBedPartType : uint
    {
        Head = 0,
        Foot = 1,
    }

    public enum YellowBedFacingType : uint
    {
        North = 0,
        South = 1,
        West = 2,
        East = 3,
    }

    public enum YellowBedOccupiedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum YellowBedPartType : uint
    {
        Head = 0,
        Foot = 1,
    }

    public enum LimeBedFacingType : uint
    {
        North = 0,
        South = 1,
        West = 2,
        East = 3,
    }

    public enum LimeBedOccupiedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum LimeBedPartType : uint
    {
        Head = 0,
        Foot = 1,
    }

    public enum PinkBedFacingType : uint
    {
        North = 0,
        South = 1,
        West = 2,
        East = 3,
    }

    public enum PinkBedOccupiedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum PinkBedPartType : uint
    {
        Head = 0,
        Foot = 1,
    }

    public enum GrayBedFacingType : uint
    {
        North = 0,
        South = 1,
        West = 2,
        East = 3,
    }

    public enum GrayBedOccupiedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum GrayBedPartType : uint
    {
        Head = 0,
        Foot = 1,
    }

    public enum LightGrayBedFacingType : uint
    {
        North = 0,
        South = 1,
        West = 2,
        East = 3,
    }

    public enum LightGrayBedOccupiedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum LightGrayBedPartType : uint
    {
        Head = 0,
        Foot = 1,
    }

    public enum CyanBedFacingType : uint
    {
        North = 0,
        South = 1,
        West = 2,
        East = 3,
    }

    public enum CyanBedOccupiedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum CyanBedPartType : uint
    {
        Head = 0,
        Foot = 1,
    }

    public enum PurpleBedFacingType : uint
    {
        North = 0,
        South = 1,
        West = 2,
        East = 3,
    }

    public enum PurpleBedOccupiedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum PurpleBedPartType : uint
    {
        Head = 0,
        Foot = 1,
    }

    public enum BlueBedFacingType : uint
    {
        North = 0,
        South = 1,
        West = 2,
        East = 3,
    }

    public enum BlueBedOccupiedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum BlueBedPartType : uint
    {
        Head = 0,
        Foot = 1,
    }

    public enum BrownBedFacingType : uint
    {
        North = 0,
        South = 1,
        West = 2,
        East = 3,
    }

    public enum BrownBedOccupiedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum BrownBedPartType : uint
    {
        Head = 0,
        Foot = 1,
    }

    public enum GreenBedFacingType : uint
    {
        North = 0,
        South = 1,
        West = 2,
        East = 3,
    }

    public enum GreenBedOccupiedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum GreenBedPartType : uint
    {
        Head = 0,
        Foot = 1,
    }

    public enum RedBedFacingType : uint
    {
        North = 0,
        South = 1,
        West = 2,
        East = 3,
    }

    public enum RedBedOccupiedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum RedBedPartType : uint
    {
        Head = 0,
        Foot = 1,
    }

    public enum BlackBedFacingType : uint
    {
        North = 0,
        South = 1,
        West = 2,
        East = 3,
    }

    public enum BlackBedOccupiedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum BlackBedPartType : uint
    {
        Head = 0,
        Foot = 1,
    }

    public enum PoweredRailPoweredType : uint
    {
        True = 0,
        False = 1,
    }

    public enum PoweredRailShapeType : uint
    {
        NorthSouth = 0,
        EastWest = 1,
        AscendingEast = 2,
        AscendingWest = 3,
        AscendingNorth = 4,
        AscendingSouth = 5,
    }

    public enum DetectorRailPoweredType : uint
    {
        True = 0,
        False = 1,
    }

    public enum DetectorRailShapeType : uint
    {
        NorthSouth = 0,
        EastWest = 1,
        AscendingEast = 2,
        AscendingWest = 3,
        AscendingNorth = 4,
        AscendingSouth = 5,
    }

    public enum StickyPistonExtendedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum StickyPistonFacingType : uint
    {
        North = 0,
        East = 1,
        South = 2,
        West = 3,
        Up = 4,
        Down = 5,
    }

    public enum TallSeagrassHalfType : uint
    {
        Upper = 0,
        Lower = 1,
    }

    public enum PistonExtendedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum PistonFacingType : uint
    {
        North = 0,
        East = 1,
        South = 2,
        West = 3,
        Up = 4,
        Down = 5,
    }

    public enum PistonHeadFacingType : uint
    {
        North = 0,
        East = 1,
        South = 2,
        West = 3,
        Up = 4,
        Down = 5,
    }

    public enum PistonHeadShortType : uint
    {
        True = 0,
        False = 1,
    }

    public enum PistonHeadTypeType : uint
    {
        Normal = 0,
        Sticky = 1,
    }

    public enum MovingPistonFacingType : uint
    {
        North = 0,
        East = 1,
        South = 2,
        West = 3,
        Up = 4,
        Down = 5,
    }

    public enum MovingPistonTypeType : uint
    {
        Normal = 0,
        Sticky = 1,
    }

    public enum TntUnstableType : uint
    {
        True = 0,
        False = 1,
    }

    public enum WallTorchFacingType : uint
    {
        North = 0,
        South = 1,
        West = 2,
        East = 3,
    }

    public enum FireAgeType : uint
    {
        Age0 = 0,
        Age1 = 1,
        Age2 = 2,
        Age3 = 3,
        Age4 = 4,
        Age5 = 5,
        Age6 = 6,
        Age7 = 7,
        Age8 = 8,
        Age9 = 9,
        Age10 = 10,
        Age11 = 11,
        Age12 = 12,
        Age13 = 13,
        Age14 = 14,
        Age15 = 15,
    }

    public enum FireEastType : uint
    {
        True = 0,
        False = 1,
    }

    public enum FireNorthType : uint
    {
        True = 0,
        False = 1,
    }

    public enum FireSouthType : uint
    {
        True = 0,
        False = 1,
    }

    public enum FireUpType : uint
    {
        True = 0,
        False = 1,
    }

    public enum FireWestType : uint
    {
        True = 0,
        False = 1,
    }

    public enum OakStairsFacingType : uint
    {
        North = 0,
        South = 1,
        West = 2,
        East = 3,
    }

    public enum OakStairsHalfType : uint
    {
        Top = 0,
        Bottom = 1,
    }

    public enum OakStairsShapeType : uint
    {
        Straight = 0,
        InnerLeft = 1,
        InnerRight = 2,
        OuterLeft = 3,
        OuterRight = 4,
    }

    public enum OakStairsWaterloggedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum ChestFacingType : uint
    {
        North = 0,
        South = 1,
        West = 2,
        East = 3,
    }

    public enum ChestTypeType : uint
    {
        Single = 0,
        Left = 1,
        Right = 2,
    }

    public enum ChestWaterloggedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum RedstoneWireEastType : uint
    {
        Up = 0,
        Side = 1,
        None = 2,
    }

    public enum RedstoneWireNorthType : uint
    {
        Up = 0,
        Side = 1,
        None = 2,
    }

    public enum RedstoneWirePowerType : uint
    {
        Power0 = 0,
        Power1 = 1,
        Power2 = 2,
        Power3 = 3,
        Power4 = 4,
        Power5 = 5,
        Power6 = 6,
        Power7 = 7,
        Power8 = 8,
        Power9 = 9,
        Power10 = 10,
        Power11 = 11,
        Power12 = 12,
        Power13 = 13,
        Power14 = 14,
        Power15 = 15,
    }

    public enum RedstoneWireSouthType : uint
    {
        Up = 0,
        Side = 1,
        None = 2,
    }

    public enum RedstoneWireWestType : uint
    {
        Up = 0,
        Side = 1,
        None = 2,
    }

    public enum WheatAgeType : uint
    {
        Age0 = 0,
        Age1 = 1,
        Age2 = 2,
        Age3 = 3,
        Age4 = 4,
        Age5 = 5,
        Age6 = 6,
        Age7 = 7,
    }

    public enum FarmlandMoistureType : uint
    {
        Moisture0 = 0,
        Moisture1 = 1,
        Moisture2 = 2,
        Moisture3 = 3,
        Moisture4 = 4,
        Moisture5 = 5,
        Moisture6 = 6,
        Moisture7 = 7,
    }

    public enum FurnaceFacingType : uint
    {
        North = 0,
        South = 1,
        West = 2,
        East = 3,
    }

    public enum FurnaceLitType : uint
    {
        True = 0,
        False = 1,
    }

    public enum OakSignRotationType : uint
    {
        Rotation0 = 0,
        Rotation1 = 1,
        Rotation2 = 2,
        Rotation3 = 3,
        Rotation4 = 4,
        Rotation5 = 5,
        Rotation6 = 6,
        Rotation7 = 7,
        Rotation8 = 8,
        Rotation9 = 9,
        Rotation10 = 10,
        Rotation11 = 11,
        Rotation12 = 12,
        Rotation13 = 13,
        Rotation14 = 14,
        Rotation15 = 15,
    }

    public enum OakSignWaterloggedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum SpruceSignRotationType : uint
    {
        Rotation0 = 0,
        Rotation1 = 1,
        Rotation2 = 2,
        Rotation3 = 3,
        Rotation4 = 4,
        Rotation5 = 5,
        Rotation6 = 6,
        Rotation7 = 7,
        Rotation8 = 8,
        Rotation9 = 9,
        Rotation10 = 10,
        Rotation11 = 11,
        Rotation12 = 12,
        Rotation13 = 13,
        Rotation14 = 14,
        Rotation15 = 15,
    }

    public enum SpruceSignWaterloggedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum BirchSignRotationType : uint
    {
        Rotation0 = 0,
        Rotation1 = 1,
        Rotation2 = 2,
        Rotation3 = 3,
        Rotation4 = 4,
        Rotation5 = 5,
        Rotation6 = 6,
        Rotation7 = 7,
        Rotation8 = 8,
        Rotation9 = 9,
        Rotation10 = 10,
        Rotation11 = 11,
        Rotation12 = 12,
        Rotation13 = 13,
        Rotation14 = 14,
        Rotation15 = 15,
    }

    public enum BirchSignWaterloggedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum AcaciaSignRotationType : uint
    {
        Rotation0 = 0,
        Rotation1 = 1,
        Rotation2 = 2,
        Rotation3 = 3,
        Rotation4 = 4,
        Rotation5 = 5,
        Rotation6 = 6,
        Rotation7 = 7,
        Rotation8 = 8,
        Rotation9 = 9,
        Rotation10 = 10,
        Rotation11 = 11,
        Rotation12 = 12,
        Rotation13 = 13,
        Rotation14 = 14,
        Rotation15 = 15,
    }

    public enum AcaciaSignWaterloggedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum JungleSignRotationType : uint
    {
        Rotation0 = 0,
        Rotation1 = 1,
        Rotation2 = 2,
        Rotation3 = 3,
        Rotation4 = 4,
        Rotation5 = 5,
        Rotation6 = 6,
        Rotation7 = 7,
        Rotation8 = 8,
        Rotation9 = 9,
        Rotation10 = 10,
        Rotation11 = 11,
        Rotation12 = 12,
        Rotation13 = 13,
        Rotation14 = 14,
        Rotation15 = 15,
    }

    public enum JungleSignWaterloggedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum DarkOakSignRotationType : uint
    {
        Rotation0 = 0,
        Rotation1 = 1,
        Rotation2 = 2,
        Rotation3 = 3,
        Rotation4 = 4,
        Rotation5 = 5,
        Rotation6 = 6,
        Rotation7 = 7,
        Rotation8 = 8,
        Rotation9 = 9,
        Rotation10 = 10,
        Rotation11 = 11,
        Rotation12 = 12,
        Rotation13 = 13,
        Rotation14 = 14,
        Rotation15 = 15,
    }

    public enum DarkOakSignWaterloggedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum OakDoorFacingType : uint
    {
        North = 0,
        South = 1,
        West = 2,
        East = 3,
    }

    public enum OakDoorHalfType : uint
    {
        Upper = 0,
        Lower = 1,
    }

    public enum OakDoorHingeType : uint
    {
        Left = 0,
        Right = 1,
    }

    public enum OakDoorOpenType : uint
    {
        True = 0,
        False = 1,
    }

    public enum OakDoorPoweredType : uint
    {
        True = 0,
        False = 1,
    }

    public enum LadderFacingType : uint
    {
        North = 0,
        South = 1,
        West = 2,
        East = 3,
    }

    public enum LadderWaterloggedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum RailShapeType : uint
    {
        NorthSouth = 0,
        EastWest = 1,
        AscendingEast = 2,
        AscendingWest = 3,
        AscendingNorth = 4,
        AscendingSouth = 5,
        SouthEast = 6,
        SouthWest = 7,
        NorthWest = 8,
        NorthEast = 9,
    }

    public enum CobblestoneStairsFacingType : uint
    {
        North = 0,
        South = 1,
        West = 2,
        East = 3,
    }

    public enum CobblestoneStairsHalfType : uint
    {
        Top = 0,
        Bottom = 1,
    }

    public enum CobblestoneStairsShapeType : uint
    {
        Straight = 0,
        InnerLeft = 1,
        InnerRight = 2,
        OuterLeft = 3,
        OuterRight = 4,
    }

    public enum CobblestoneStairsWaterloggedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum OakWallSignFacingType : uint
    {
        North = 0,
        South = 1,
        West = 2,
        East = 3,
    }

    public enum OakWallSignWaterloggedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum SpruceWallSignFacingType : uint
    {
        North = 0,
        South = 1,
        West = 2,
        East = 3,
    }

    public enum SpruceWallSignWaterloggedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum BirchWallSignFacingType : uint
    {
        North = 0,
        South = 1,
        West = 2,
        East = 3,
    }

    public enum BirchWallSignWaterloggedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum AcaciaWallSignFacingType : uint
    {
        North = 0,
        South = 1,
        West = 2,
        East = 3,
    }

    public enum AcaciaWallSignWaterloggedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum JungleWallSignFacingType : uint
    {
        North = 0,
        South = 1,
        West = 2,
        East = 3,
    }

    public enum JungleWallSignWaterloggedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum DarkOakWallSignFacingType : uint
    {
        North = 0,
        South = 1,
        West = 2,
        East = 3,
    }

    public enum DarkOakWallSignWaterloggedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum LeverFaceType : uint
    {
        Floor = 0,
        Wall = 1,
        Ceiling = 2,
    }

    public enum LeverFacingType : uint
    {
        North = 0,
        South = 1,
        West = 2,
        East = 3,
    }

    public enum LeverPoweredType : uint
    {
        True = 0,
        False = 1,
    }

    public enum StonePressurePlatePoweredType : uint
    {
        True = 0,
        False = 1,
    }

    public enum IronDoorFacingType : uint
    {
        North = 0,
        South = 1,
        West = 2,
        East = 3,
    }

    public enum IronDoorHalfType : uint
    {
        Upper = 0,
        Lower = 1,
    }

    public enum IronDoorHingeType : uint
    {
        Left = 0,
        Right = 1,
    }

    public enum IronDoorOpenType : uint
    {
        True = 0,
        False = 1,
    }

    public enum IronDoorPoweredType : uint
    {
        True = 0,
        False = 1,
    }

    public enum OakPressurePlatePoweredType : uint
    {
        True = 0,
        False = 1,
    }

    public enum SprucePressurePlatePoweredType : uint
    {
        True = 0,
        False = 1,
    }

    public enum BirchPressurePlatePoweredType : uint
    {
        True = 0,
        False = 1,
    }

    public enum JunglePressurePlatePoweredType : uint
    {
        True = 0,
        False = 1,
    }

    public enum AcaciaPressurePlatePoweredType : uint
    {
        True = 0,
        False = 1,
    }

    public enum DarkOakPressurePlatePoweredType : uint
    {
        True = 0,
        False = 1,
    }

    public enum RedstoneOreLitType : uint
    {
        True = 0,
        False = 1,
    }

    public enum RedstoneTorchLitType : uint
    {
        True = 0,
        False = 1,
    }

    public enum RedstoneWallTorchFacingType : uint
    {
        North = 0,
        South = 1,
        West = 2,
        East = 3,
    }

    public enum RedstoneWallTorchLitType : uint
    {
        True = 0,
        False = 1,
    }

    public enum StoneButtonFaceType : uint
    {
        Floor = 0,
        Wall = 1,
        Ceiling = 2,
    }

    public enum StoneButtonFacingType : uint
    {
        North = 0,
        South = 1,
        West = 2,
        East = 3,
    }

    public enum StoneButtonPoweredType : uint
    {
        True = 0,
        False = 1,
    }

    public enum SnowLayersType : uint
    {
        Layers1 = 0,
        Layers2 = 1,
        Layers3 = 2,
        Layers4 = 3,
        Layers5 = 4,
        Layers6 = 5,
        Layers7 = 6,
        Layers8 = 7,
    }

    public enum CactusAgeType : uint
    {
        Age0 = 0,
        Age1 = 1,
        Age2 = 2,
        Age3 = 3,
        Age4 = 4,
        Age5 = 5,
        Age6 = 6,
        Age7 = 7,
        Age8 = 8,
        Age9 = 9,
        Age10 = 10,
        Age11 = 11,
        Age12 = 12,
        Age13 = 13,
        Age14 = 14,
        Age15 = 15,
    }

    public enum SugarCaneAgeType : uint
    {
        Age0 = 0,
        Age1 = 1,
        Age2 = 2,
        Age3 = 3,
        Age4 = 4,
        Age5 = 5,
        Age6 = 6,
        Age7 = 7,
        Age8 = 8,
        Age9 = 9,
        Age10 = 10,
        Age11 = 11,
        Age12 = 12,
        Age13 = 13,
        Age14 = 14,
        Age15 = 15,
    }

    public enum JukeboxHasRecordType : uint
    {
        True = 0,
        False = 1,
    }

    public enum OakFenceEastType : uint
    {
        True = 0,
        False = 1,
    }

    public enum OakFenceNorthType : uint
    {
        True = 0,
        False = 1,
    }

    public enum OakFenceSouthType : uint
    {
        True = 0,
        False = 1,
    }

    public enum OakFenceWaterloggedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum OakFenceWestType : uint
    {
        True = 0,
        False = 1,
    }

    public enum NetherPortalAxisType : uint
    {
        X = 0,
        Z = 1,
    }

    public enum CarvedPumpkinFacingType : uint
    {
        North = 0,
        South = 1,
        West = 2,
        East = 3,
    }

    public enum JackOLanternFacingType : uint
    {
        North = 0,
        South = 1,
        West = 2,
        East = 3,
    }

    public enum CakeBitesType : uint
    {
        Bites0 = 0,
        Bites1 = 1,
        Bites2 = 2,
        Bites3 = 3,
        Bites4 = 4,
        Bites5 = 5,
        Bites6 = 6,
    }

    public enum RepeaterDelayType : uint
    {
        Delay1 = 0,
        Delay2 = 1,
        Delay3 = 2,
        Delay4 = 3,
    }

    public enum RepeaterFacingType : uint
    {
        North = 0,
        South = 1,
        West = 2,
        East = 3,
    }

    public enum RepeaterLockedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum RepeaterPoweredType : uint
    {
        True = 0,
        False = 1,
    }

    public enum OakTrapdoorFacingType : uint
    {
        North = 0,
        South = 1,
        West = 2,
        East = 3,
    }

    public enum OakTrapdoorHalfType : uint
    {
        Top = 0,
        Bottom = 1,
    }

    public enum OakTrapdoorOpenType : uint
    {
        True = 0,
        False = 1,
    }

    public enum OakTrapdoorPoweredType : uint
    {
        True = 0,
        False = 1,
    }

    public enum OakTrapdoorWaterloggedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum SpruceTrapdoorFacingType : uint
    {
        North = 0,
        South = 1,
        West = 2,
        East = 3,
    }

    public enum SpruceTrapdoorHalfType : uint
    {
        Top = 0,
        Bottom = 1,
    }

    public enum SpruceTrapdoorOpenType : uint
    {
        True = 0,
        False = 1,
    }

    public enum SpruceTrapdoorPoweredType : uint
    {
        True = 0,
        False = 1,
    }

    public enum SpruceTrapdoorWaterloggedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum BirchTrapdoorFacingType : uint
    {
        North = 0,
        South = 1,
        West = 2,
        East = 3,
    }

    public enum BirchTrapdoorHalfType : uint
    {
        Top = 0,
        Bottom = 1,
    }

    public enum BirchTrapdoorOpenType : uint
    {
        True = 0,
        False = 1,
    }

    public enum BirchTrapdoorPoweredType : uint
    {
        True = 0,
        False = 1,
    }

    public enum BirchTrapdoorWaterloggedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum JungleTrapdoorFacingType : uint
    {
        North = 0,
        South = 1,
        West = 2,
        East = 3,
    }

    public enum JungleTrapdoorHalfType : uint
    {
        Top = 0,
        Bottom = 1,
    }

    public enum JungleTrapdoorOpenType : uint
    {
        True = 0,
        False = 1,
    }

    public enum JungleTrapdoorPoweredType : uint
    {
        True = 0,
        False = 1,
    }

    public enum JungleTrapdoorWaterloggedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum AcaciaTrapdoorFacingType : uint
    {
        North = 0,
        South = 1,
        West = 2,
        East = 3,
    }

    public enum AcaciaTrapdoorHalfType : uint
    {
        Top = 0,
        Bottom = 1,
    }

    public enum AcaciaTrapdoorOpenType : uint
    {
        True = 0,
        False = 1,
    }

    public enum AcaciaTrapdoorPoweredType : uint
    {
        True = 0,
        False = 1,
    }

    public enum AcaciaTrapdoorWaterloggedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum DarkOakTrapdoorFacingType : uint
    {
        North = 0,
        South = 1,
        West = 2,
        East = 3,
    }

    public enum DarkOakTrapdoorHalfType : uint
    {
        Top = 0,
        Bottom = 1,
    }

    public enum DarkOakTrapdoorOpenType : uint
    {
        True = 0,
        False = 1,
    }

    public enum DarkOakTrapdoorPoweredType : uint
    {
        True = 0,
        False = 1,
    }

    public enum DarkOakTrapdoorWaterloggedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum BrownMushroomBlockDownType : uint
    {
        True = 0,
        False = 1,
    }

    public enum BrownMushroomBlockEastType : uint
    {
        True = 0,
        False = 1,
    }

    public enum BrownMushroomBlockNorthType : uint
    {
        True = 0,
        False = 1,
    }

    public enum BrownMushroomBlockSouthType : uint
    {
        True = 0,
        False = 1,
    }

    public enum BrownMushroomBlockUpType : uint
    {
        True = 0,
        False = 1,
    }

    public enum BrownMushroomBlockWestType : uint
    {
        True = 0,
        False = 1,
    }

    public enum RedMushroomBlockDownType : uint
    {
        True = 0,
        False = 1,
    }

    public enum RedMushroomBlockEastType : uint
    {
        True = 0,
        False = 1,
    }

    public enum RedMushroomBlockNorthType : uint
    {
        True = 0,
        False = 1,
    }

    public enum RedMushroomBlockSouthType : uint
    {
        True = 0,
        False = 1,
    }

    public enum RedMushroomBlockUpType : uint
    {
        True = 0,
        False = 1,
    }

    public enum RedMushroomBlockWestType : uint
    {
        True = 0,
        False = 1,
    }

    public enum MushroomStemDownType : uint
    {
        True = 0,
        False = 1,
    }

    public enum MushroomStemEastType : uint
    {
        True = 0,
        False = 1,
    }

    public enum MushroomStemNorthType : uint
    {
        True = 0,
        False = 1,
    }

    public enum MushroomStemSouthType : uint
    {
        True = 0,
        False = 1,
    }

    public enum MushroomStemUpType : uint
    {
        True = 0,
        False = 1,
    }

    public enum MushroomStemWestType : uint
    {
        True = 0,
        False = 1,
    }

    public enum IronBarsEastType : uint
    {
        True = 0,
        False = 1,
    }

    public enum IronBarsNorthType : uint
    {
        True = 0,
        False = 1,
    }

    public enum IronBarsSouthType : uint
    {
        True = 0,
        False = 1,
    }

    public enum IronBarsWaterloggedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum IronBarsWestType : uint
    {
        True = 0,
        False = 1,
    }

    public enum GlassPaneEastType : uint
    {
        True = 0,
        False = 1,
    }

    public enum GlassPaneNorthType : uint
    {
        True = 0,
        False = 1,
    }

    public enum GlassPaneSouthType : uint
    {
        True = 0,
        False = 1,
    }

    public enum GlassPaneWaterloggedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum GlassPaneWestType : uint
    {
        True = 0,
        False = 1,
    }

    public enum AttachedPumpkinStemFacingType : uint
    {
        North = 0,
        South = 1,
        West = 2,
        East = 3,
    }

    public enum AttachedMelonStemFacingType : uint
    {
        North = 0,
        South = 1,
        West = 2,
        East = 3,
    }

    public enum PumpkinStemAgeType : uint
    {
        Age0 = 0,
        Age1 = 1,
        Age2 = 2,
        Age3 = 3,
        Age4 = 4,
        Age5 = 5,
        Age6 = 6,
        Age7 = 7,
    }

    public enum MelonStemAgeType : uint
    {
        Age0 = 0,
        Age1 = 1,
        Age2 = 2,
        Age3 = 3,
        Age4 = 4,
        Age5 = 5,
        Age6 = 6,
        Age7 = 7,
    }

    public class VineType
    {
        public VineEastType East { get; set; } = VineEastType.False;

        public VineNorthType North { get; set; } = VineNorthType.False;

        public VineSouthType South { get; set; } = VineSouthType.False;

        public VineUpType Up { get; set; } = VineUpType.False;

        public VineWestType West { get; set; } = VineWestType.False;
    }

    public enum VineEastType : uint
    {
        True = 0,
        False = 1,
    }

    public enum VineNorthType : uint
    {
        True = 0,
        False = 1,
    }

    public enum VineSouthType : uint
    {
        True = 0,
        False = 1,
    }

    public enum VineUpType : uint
    {
        True = 0,
        False = 1,
    }

    public enum VineWestType : uint
    {
        True = 0,
        False = 1,
    }

    public enum OakFenceGateFacingType : uint
    {
        North = 0,
        South = 1,
        West = 2,
        East = 3,
    }

    public enum OakFenceGateInWallType : uint
    {
        True = 0,
        False = 1,
    }

    public enum OakFenceGateOpenType : uint
    {
        True = 0,
        False = 1,
    }

    public enum OakFenceGatePoweredType : uint
    {
        True = 0,
        False = 1,
    }

    public enum BrickStairsFacingType : uint
    {
        North = 0,
        South = 1,
        West = 2,
        East = 3,
    }

    public enum BrickStairsHalfType : uint
    {
        Top = 0,
        Bottom = 1,
    }

    public enum BrickStairsShapeType : uint
    {
        Straight = 0,
        InnerLeft = 1,
        InnerRight = 2,
        OuterLeft = 3,
        OuterRight = 4,
    }

    public enum BrickStairsWaterloggedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum StoneBrickStairsFacingType : uint
    {
        North = 0,
        South = 1,
        West = 2,
        East = 3,
    }

    public enum StoneBrickStairsHalfType : uint
    {
        Top = 0,
        Bottom = 1,
    }

    public enum StoneBrickStairsShapeType : uint
    {
        Straight = 0,
        InnerLeft = 1,
        InnerRight = 2,
        OuterLeft = 3,
        OuterRight = 4,
    }

    public enum StoneBrickStairsWaterloggedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum MyceliumSnowyType : uint
    {
        True = 0,
        False = 1,
    }

    public enum NetherBrickFenceEastType : uint
    {
        True = 0,
        False = 1,
    }

    public enum NetherBrickFenceNorthType : uint
    {
        True = 0,
        False = 1,
    }

    public enum NetherBrickFenceSouthType : uint
    {
        True = 0,
        False = 1,
    }

    public enum NetherBrickFenceWaterloggedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum NetherBrickFenceWestType : uint
    {
        True = 0,
        False = 1,
    }

    public enum NetherBrickStairsFacingType : uint
    {
        North = 0,
        South = 1,
        West = 2,
        East = 3,
    }

    public enum NetherBrickStairsHalfType : uint
    {
        Top = 0,
        Bottom = 1,
    }

    public enum NetherBrickStairsShapeType : uint
    {
        Straight = 0,
        InnerLeft = 1,
        InnerRight = 2,
        OuterLeft = 3,
        OuterRight = 4,
    }

    public enum NetherBrickStairsWaterloggedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum NetherWartAgeType : uint
    {
        Age0 = 0,
        Age1 = 1,
        Age2 = 2,
        Age3 = 3,
    }

    public enum BrewingStandHasBottle0Type : uint
    {
        True = 0,
        False = 1,
    }

    public enum BrewingStandHasBottle1Type : uint
    {
        True = 0,
        False = 1,
    }

    public enum BrewingStandHasBottle2Type : uint
    {
        True = 0,
        False = 1,
    }

    public enum CauldronLevelType : uint
    {
        Level0 = 0,
        Level1 = 1,
        Level2 = 2,
        Level3 = 3,
    }

    public enum EndPortalFrameEyeType : uint
    {
        True = 0,
        False = 1,
    }

    public enum EndPortalFrameFacingType : uint
    {
        North = 0,
        South = 1,
        West = 2,
        East = 3,
    }

    public enum RedstoneLampLitType : uint
    {
        True = 0,
        False = 1,
    }

    public enum CocoaAgeType : uint
    {
        Age0 = 0,
        Age1 = 1,
        Age2 = 2,
    }

    public enum CocoaFacingType : uint
    {
        North = 0,
        South = 1,
        West = 2,
        East = 3,
    }

    public enum SandstoneStairsFacingType : uint
    {
        North = 0,
        South = 1,
        West = 2,
        East = 3,
    }

    public enum SandstoneStairsHalfType : uint
    {
        Top = 0,
        Bottom = 1,
    }

    public enum SandstoneStairsShapeType : uint
    {
        Straight = 0,
        InnerLeft = 1,
        InnerRight = 2,
        OuterLeft = 3,
        OuterRight = 4,
    }

    public enum SandstoneStairsWaterloggedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum EnderChestFacingType : uint
    {
        North = 0,
        South = 1,
        West = 2,
        East = 3,
    }

    public enum EnderChestWaterloggedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum TripwireHookAttachedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum TripwireHookFacingType : uint
    {
        North = 0,
        South = 1,
        West = 2,
        East = 3,
    }

    public enum TripwireHookPoweredType : uint
    {
        True = 0,
        False = 1,
    }

    public enum TripwireAttachedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum TripwireDisarmedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum TripwireEastType : uint
    {
        True = 0,
        False = 1,
    }

    public enum TripwireNorthType : uint
    {
        True = 0,
        False = 1,
    }

    public enum TripwirePoweredType : uint
    {
        True = 0,
        False = 1,
    }

    public enum TripwireSouthType : uint
    {
        True = 0,
        False = 1,
    }

    public enum TripwireWestType : uint
    {
        True = 0,
        False = 1,
    }

    public enum SpruceStairsFacingType : uint
    {
        North = 0,
        South = 1,
        West = 2,
        East = 3,
    }

    public enum SpruceStairsHalfType : uint
    {
        Top = 0,
        Bottom = 1,
    }

    public enum SpruceStairsShapeType : uint
    {
        Straight = 0,
        InnerLeft = 1,
        InnerRight = 2,
        OuterLeft = 3,
        OuterRight = 4,
    }

    public enum SpruceStairsWaterloggedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum BirchStairsFacingType : uint
    {
        North = 0,
        South = 1,
        West = 2,
        East = 3,
    }

    public enum BirchStairsHalfType : uint
    {
        Top = 0,
        Bottom = 1,
    }

    public enum BirchStairsShapeType : uint
    {
        Straight = 0,
        InnerLeft = 1,
        InnerRight = 2,
        OuterLeft = 3,
        OuterRight = 4,
    }

    public enum BirchStairsWaterloggedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum JungleStairsFacingType : uint
    {
        North = 0,
        South = 1,
        West = 2,
        East = 3,
    }

    public enum JungleStairsHalfType : uint
    {
        Top = 0,
        Bottom = 1,
    }

    public enum JungleStairsShapeType : uint
    {
        Straight = 0,
        InnerLeft = 1,
        InnerRight = 2,
        OuterLeft = 3,
        OuterRight = 4,
    }

    public enum JungleStairsWaterloggedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum CommandBlockConditionalType : uint
    {
        True = 0,
        False = 1,
    }

    public enum CommandBlockFacingType : uint
    {
        North = 0,
        East = 1,
        South = 2,
        West = 3,
        Up = 4,
        Down = 5,
    }

    public enum CobblestoneWallEastType : uint
    {
        True = 0,
        False = 1,
    }

    public enum CobblestoneWallNorthType : uint
    {
        True = 0,
        False = 1,
    }

    public enum CobblestoneWallSouthType : uint
    {
        True = 0,
        False = 1,
    }

    public enum CobblestoneWallUpType : uint
    {
        True = 0,
        False = 1,
    }

    public enum CobblestoneWallWaterloggedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum CobblestoneWallWestType : uint
    {
        True = 0,
        False = 1,
    }

    public enum MossyCobblestoneWallEastType : uint
    {
        True = 0,
        False = 1,
    }

    public enum MossyCobblestoneWallNorthType : uint
    {
        True = 0,
        False = 1,
    }

    public enum MossyCobblestoneWallSouthType : uint
    {
        True = 0,
        False = 1,
    }

    public enum MossyCobblestoneWallUpType : uint
    {
        True = 0,
        False = 1,
    }

    public enum MossyCobblestoneWallWaterloggedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum MossyCobblestoneWallWestType : uint
    {
        True = 0,
        False = 1,
    }

    public enum CarrotsAgeType : uint
    {
        Age0 = 0,
        Age1 = 1,
        Age2 = 2,
        Age3 = 3,
        Age4 = 4,
        Age5 = 5,
        Age6 = 6,
        Age7 = 7,
    }

    public enum PotatoesAgeType : uint
    {
        Age0 = 0,
        Age1 = 1,
        Age2 = 2,
        Age3 = 3,
        Age4 = 4,
        Age5 = 5,
        Age6 = 6,
        Age7 = 7,
    }

    public enum OakButtonFaceType : uint
    {
        Floor = 0,
        Wall = 1,
        Ceiling = 2,
    }

    public enum OakButtonFacingType : uint
    {
        North = 0,
        South = 1,
        West = 2,
        East = 3,
    }

    public enum OakButtonPoweredType : uint
    {
        True = 0,
        False = 1,
    }

    public enum SpruceButtonFaceType : uint
    {
        Floor = 0,
        Wall = 1,
        Ceiling = 2,
    }

    public enum SpruceButtonFacingType : uint
    {
        North = 0,
        South = 1,
        West = 2,
        East = 3,
    }

    public enum SpruceButtonPoweredType : uint
    {
        True = 0,
        False = 1,
    }

    public enum BirchButtonFaceType : uint
    {
        Floor = 0,
        Wall = 1,
        Ceiling = 2,
    }

    public enum BirchButtonFacingType : uint
    {
        North = 0,
        South = 1,
        West = 2,
        East = 3,
    }

    public enum BirchButtonPoweredType : uint
    {
        True = 0,
        False = 1,
    }

    public enum JungleButtonFaceType : uint
    {
        Floor = 0,
        Wall = 1,
        Ceiling = 2,
    }

    public enum JungleButtonFacingType : uint
    {
        North = 0,
        South = 1,
        West = 2,
        East = 3,
    }

    public enum JungleButtonPoweredType : uint
    {
        True = 0,
        False = 1,
    }

    public enum AcaciaButtonFaceType : uint
    {
        Floor = 0,
        Wall = 1,
        Ceiling = 2,
    }

    public enum AcaciaButtonFacingType : uint
    {
        North = 0,
        South = 1,
        West = 2,
        East = 3,
    }

    public enum AcaciaButtonPoweredType : uint
    {
        True = 0,
        False = 1,
    }

    public enum DarkOakButtonFaceType : uint
    {
        Floor = 0,
        Wall = 1,
        Ceiling = 2,
    }

    public enum DarkOakButtonFacingType : uint
    {
        North = 0,
        South = 1,
        West = 2,
        East = 3,
    }

    public enum DarkOakButtonPoweredType : uint
    {
        True = 0,
        False = 1,
    }

    public enum SkeletonSkullRotationType : uint
    {
        Rotation0 = 0,
        Rotation1 = 1,
        Rotation2 = 2,
        Rotation3 = 3,
        Rotation4 = 4,
        Rotation5 = 5,
        Rotation6 = 6,
        Rotation7 = 7,
        Rotation8 = 8,
        Rotation9 = 9,
        Rotation10 = 10,
        Rotation11 = 11,
        Rotation12 = 12,
        Rotation13 = 13,
        Rotation14 = 14,
        Rotation15 = 15,
    }

    public enum SkeletonWallSkullFacingType : uint
    {
        North = 0,
        South = 1,
        West = 2,
        East = 3,
    }

    public enum WitherSkeletonSkullRotationType : uint
    {
        Rotation0 = 0,
        Rotation1 = 1,
        Rotation2 = 2,
        Rotation3 = 3,
        Rotation4 = 4,
        Rotation5 = 5,
        Rotation6 = 6,
        Rotation7 = 7,
        Rotation8 = 8,
        Rotation9 = 9,
        Rotation10 = 10,
        Rotation11 = 11,
        Rotation12 = 12,
        Rotation13 = 13,
        Rotation14 = 14,
        Rotation15 = 15,
    }

    public enum WitherSkeletonWallSkullFacingType : uint
    {
        North = 0,
        South = 1,
        West = 2,
        East = 3,
    }

    public enum ZombieHeadRotationType : uint
    {
        Rotation0 = 0,
        Rotation1 = 1,
        Rotation2 = 2,
        Rotation3 = 3,
        Rotation4 = 4,
        Rotation5 = 5,
        Rotation6 = 6,
        Rotation7 = 7,
        Rotation8 = 8,
        Rotation9 = 9,
        Rotation10 = 10,
        Rotation11 = 11,
        Rotation12 = 12,
        Rotation13 = 13,
        Rotation14 = 14,
        Rotation15 = 15,
    }

    public enum ZombieWallHeadFacingType : uint
    {
        North = 0,
        South = 1,
        West = 2,
        East = 3,
    }

    public enum PlayerHeadRotationType : uint
    {
        Rotation0 = 0,
        Rotation1 = 1,
        Rotation2 = 2,
        Rotation3 = 3,
        Rotation4 = 4,
        Rotation5 = 5,
        Rotation6 = 6,
        Rotation7 = 7,
        Rotation8 = 8,
        Rotation9 = 9,
        Rotation10 = 10,
        Rotation11 = 11,
        Rotation12 = 12,
        Rotation13 = 13,
        Rotation14 = 14,
        Rotation15 = 15,
    }

    public enum PlayerWallHeadFacingType : uint
    {
        North = 0,
        South = 1,
        West = 2,
        East = 3,
    }

    public enum CreeperHeadRotationType : uint
    {
        Rotation0 = 0,
        Rotation1 = 1,
        Rotation2 = 2,
        Rotation3 = 3,
        Rotation4 = 4,
        Rotation5 = 5,
        Rotation6 = 6,
        Rotation7 = 7,
        Rotation8 = 8,
        Rotation9 = 9,
        Rotation10 = 10,
        Rotation11 = 11,
        Rotation12 = 12,
        Rotation13 = 13,
        Rotation14 = 14,
        Rotation15 = 15,
    }

    public enum CreeperWallHeadFacingType : uint
    {
        North = 0,
        South = 1,
        West = 2,
        East = 3,
    }

    public enum DragonHeadRotationType : uint
    {
        Rotation0 = 0,
        Rotation1 = 1,
        Rotation2 = 2,
        Rotation3 = 3,
        Rotation4 = 4,
        Rotation5 = 5,
        Rotation6 = 6,
        Rotation7 = 7,
        Rotation8 = 8,
        Rotation9 = 9,
        Rotation10 = 10,
        Rotation11 = 11,
        Rotation12 = 12,
        Rotation13 = 13,
        Rotation14 = 14,
        Rotation15 = 15,
    }

    public enum DragonWallHeadFacingType : uint
    {
        North = 0,
        South = 1,
        West = 2,
        East = 3,
    }

    public enum AnvilFacingType : uint
    {
        North = 0,
        South = 1,
        West = 2,
        East = 3,
    }

    public enum ChippedAnvilFacingType : uint
    {
        North = 0,
        South = 1,
        West = 2,
        East = 3,
    }

    public enum DamagedAnvilFacingType : uint
    {
        North = 0,
        South = 1,
        West = 2,
        East = 3,
    }

    public enum TrappedChestFacingType : uint
    {
        North = 0,
        South = 1,
        West = 2,
        East = 3,
    }

    public enum TrappedChestTypeType : uint
    {
        Single = 0,
        Left = 1,
        Right = 2,
    }

    public enum TrappedChestWaterloggedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum LightWeightedPressurePlatePowerType : uint
    {
        Power0 = 0,
        Power1 = 1,
        Power2 = 2,
        Power3 = 3,
        Power4 = 4,
        Power5 = 5,
        Power6 = 6,
        Power7 = 7,
        Power8 = 8,
        Power9 = 9,
        Power10 = 10,
        Power11 = 11,
        Power12 = 12,
        Power13 = 13,
        Power14 = 14,
        Power15 = 15,
    }

    public enum HeavyWeightedPressurePlatePowerType : uint
    {
        Power0 = 0,
        Power1 = 1,
        Power2 = 2,
        Power3 = 3,
        Power4 = 4,
        Power5 = 5,
        Power6 = 6,
        Power7 = 7,
        Power8 = 8,
        Power9 = 9,
        Power10 = 10,
        Power11 = 11,
        Power12 = 12,
        Power13 = 13,
        Power14 = 14,
        Power15 = 15,
    }

    public enum ComparatorFacingType : uint
    {
        North = 0,
        South = 1,
        West = 2,
        East = 3,
    }

    public enum ComparatorModeType : uint
    {
        Compare = 0,
        Subtract = 1,
    }

    public enum ComparatorPoweredType : uint
    {
        True = 0,
        False = 1,
    }

    public enum DaylightDetectorInvertedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum DaylightDetectorPowerType : uint
    {
        Power0 = 0,
        Power1 = 1,
        Power2 = 2,
        Power3 = 3,
        Power4 = 4,
        Power5 = 5,
        Power6 = 6,
        Power7 = 7,
        Power8 = 8,
        Power9 = 9,
        Power10 = 10,
        Power11 = 11,
        Power12 = 12,
        Power13 = 13,
        Power14 = 14,
        Power15 = 15,
    }

    public enum HopperEnabledType : uint
    {
        True = 0,
        False = 1,
    }

    public enum HopperFacingType : uint
    {
        Down = 0,
        North = 1,
        South = 2,
        West = 3,
        East = 4,
    }

    public enum QuartzPillarAxisType : uint
    {
        X = 0,
        Y = 1,
        Z = 2,
    }

    public enum QuartzStairsFacingType : uint
    {
        North = 0,
        South = 1,
        West = 2,
        East = 3,
    }

    public enum QuartzStairsHalfType : uint
    {
        Top = 0,
        Bottom = 1,
    }

    public enum QuartzStairsShapeType : uint
    {
        Straight = 0,
        InnerLeft = 1,
        InnerRight = 2,
        OuterLeft = 3,
        OuterRight = 4,
    }

    public enum QuartzStairsWaterloggedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum ActivatorRailPoweredType : uint
    {
        True = 0,
        False = 1,
    }

    public enum ActivatorRailShapeType : uint
    {
        NorthSouth = 0,
        EastWest = 1,
        AscendingEast = 2,
        AscendingWest = 3,
        AscendingNorth = 4,
        AscendingSouth = 5,
    }

    public enum DropperFacingType : uint
    {
        North = 0,
        East = 1,
        South = 2,
        West = 3,
        Up = 4,
        Down = 5,
    }

    public enum DropperTriggeredType : uint
    {
        True = 0,
        False = 1,
    }

    public enum WhiteStainedGlassPaneEastType : uint
    {
        True = 0,
        False = 1,
    }

    public enum WhiteStainedGlassPaneNorthType : uint
    {
        True = 0,
        False = 1,
    }

    public enum WhiteStainedGlassPaneSouthType : uint
    {
        True = 0,
        False = 1,
    }

    public enum WhiteStainedGlassPaneWaterloggedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum WhiteStainedGlassPaneWestType : uint
    {
        True = 0,
        False = 1,
    }

    public enum OrangeStainedGlassPaneEastType : uint
    {
        True = 0,
        False = 1,
    }

    public enum OrangeStainedGlassPaneNorthType : uint
    {
        True = 0,
        False = 1,
    }

    public enum OrangeStainedGlassPaneSouthType : uint
    {
        True = 0,
        False = 1,
    }

    public enum OrangeStainedGlassPaneWaterloggedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum OrangeStainedGlassPaneWestType : uint
    {
        True = 0,
        False = 1,
    }

    public enum MagentaStainedGlassPaneEastType : uint
    {
        True = 0,
        False = 1,
    }

    public enum MagentaStainedGlassPaneNorthType : uint
    {
        True = 0,
        False = 1,
    }

    public enum MagentaStainedGlassPaneSouthType : uint
    {
        True = 0,
        False = 1,
    }

    public enum MagentaStainedGlassPaneWaterloggedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum MagentaStainedGlassPaneWestType : uint
    {
        True = 0,
        False = 1,
    }

    public enum LightBlueStainedGlassPaneEastType : uint
    {
        True = 0,
        False = 1,
    }

    public enum LightBlueStainedGlassPaneNorthType : uint
    {
        True = 0,
        False = 1,
    }

    public enum LightBlueStainedGlassPaneSouthType : uint
    {
        True = 0,
        False = 1,
    }

    public enum LightBlueStainedGlassPaneWaterloggedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum LightBlueStainedGlassPaneWestType : uint
    {
        True = 0,
        False = 1,
    }

    public enum YellowStainedGlassPaneEastType : uint
    {
        True = 0,
        False = 1,
    }

    public enum YellowStainedGlassPaneNorthType : uint
    {
        True = 0,
        False = 1,
    }

    public enum YellowStainedGlassPaneSouthType : uint
    {
        True = 0,
        False = 1,
    }

    public enum YellowStainedGlassPaneWaterloggedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum YellowStainedGlassPaneWestType : uint
    {
        True = 0,
        False = 1,
    }

    public enum LimeStainedGlassPaneEastType : uint
    {
        True = 0,
        False = 1,
    }

    public enum LimeStainedGlassPaneNorthType : uint
    {
        True = 0,
        False = 1,
    }

    public enum LimeStainedGlassPaneSouthType : uint
    {
        True = 0,
        False = 1,
    }

    public enum LimeStainedGlassPaneWaterloggedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum LimeStainedGlassPaneWestType : uint
    {
        True = 0,
        False = 1,
    }

    public enum PinkStainedGlassPaneEastType : uint
    {
        True = 0,
        False = 1,
    }

    public enum PinkStainedGlassPaneNorthType : uint
    {
        True = 0,
        False = 1,
    }

    public enum PinkStainedGlassPaneSouthType : uint
    {
        True = 0,
        False = 1,
    }

    public enum PinkStainedGlassPaneWaterloggedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum PinkStainedGlassPaneWestType : uint
    {
        True = 0,
        False = 1,
    }

    public enum GrayStainedGlassPaneEastType : uint
    {
        True = 0,
        False = 1,
    }

    public enum GrayStainedGlassPaneNorthType : uint
    {
        True = 0,
        False = 1,
    }

    public enum GrayStainedGlassPaneSouthType : uint
    {
        True = 0,
        False = 1,
    }

    public enum GrayStainedGlassPaneWaterloggedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum GrayStainedGlassPaneWestType : uint
    {
        True = 0,
        False = 1,
    }

    public enum LightGrayStainedGlassPaneEastType : uint
    {
        True = 0,
        False = 1,
    }

    public enum LightGrayStainedGlassPaneNorthType : uint
    {
        True = 0,
        False = 1,
    }

    public enum LightGrayStainedGlassPaneSouthType : uint
    {
        True = 0,
        False = 1,
    }

    public enum LightGrayStainedGlassPaneWaterloggedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum LightGrayStainedGlassPaneWestType : uint
    {
        True = 0,
        False = 1,
    }

    public enum CyanStainedGlassPaneEastType : uint
    {
        True = 0,
        False = 1,
    }

    public enum CyanStainedGlassPaneNorthType : uint
    {
        True = 0,
        False = 1,
    }

    public enum CyanStainedGlassPaneSouthType : uint
    {
        True = 0,
        False = 1,
    }

    public enum CyanStainedGlassPaneWaterloggedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum CyanStainedGlassPaneWestType : uint
    {
        True = 0,
        False = 1,
    }

    public enum PurpleStainedGlassPaneEastType : uint
    {
        True = 0,
        False = 1,
    }

    public enum PurpleStainedGlassPaneNorthType : uint
    {
        True = 0,
        False = 1,
    }

    public enum PurpleStainedGlassPaneSouthType : uint
    {
        True = 0,
        False = 1,
    }

    public enum PurpleStainedGlassPaneWaterloggedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum PurpleStainedGlassPaneWestType : uint
    {
        True = 0,
        False = 1,
    }

    public enum BlueStainedGlassPaneEastType : uint
    {
        True = 0,
        False = 1,
    }

    public enum BlueStainedGlassPaneNorthType : uint
    {
        True = 0,
        False = 1,
    }

    public enum BlueStainedGlassPaneSouthType : uint
    {
        True = 0,
        False = 1,
    }

    public enum BlueStainedGlassPaneWaterloggedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum BlueStainedGlassPaneWestType : uint
    {
        True = 0,
        False = 1,
    }

    public enum BrownStainedGlassPaneEastType : uint
    {
        True = 0,
        False = 1,
    }

    public enum BrownStainedGlassPaneNorthType : uint
    {
        True = 0,
        False = 1,
    }

    public enum BrownStainedGlassPaneSouthType : uint
    {
        True = 0,
        False = 1,
    }

    public enum BrownStainedGlassPaneWaterloggedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum BrownStainedGlassPaneWestType : uint
    {
        True = 0,
        False = 1,
    }

    public enum GreenStainedGlassPaneEastType : uint
    {
        True = 0,
        False = 1,
    }

    public enum GreenStainedGlassPaneNorthType : uint
    {
        True = 0,
        False = 1,
    }

    public enum GreenStainedGlassPaneSouthType : uint
    {
        True = 0,
        False = 1,
    }

    public enum GreenStainedGlassPaneWaterloggedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum GreenStainedGlassPaneWestType : uint
    {
        True = 0,
        False = 1,
    }

    public enum RedStainedGlassPaneEastType : uint
    {
        True = 0,
        False = 1,
    }

    public enum RedStainedGlassPaneNorthType : uint
    {
        True = 0,
        False = 1,
    }

    public enum RedStainedGlassPaneSouthType : uint
    {
        True = 0,
        False = 1,
    }

    public enum RedStainedGlassPaneWaterloggedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum RedStainedGlassPaneWestType : uint
    {
        True = 0,
        False = 1,
    }

    public enum BlackStainedGlassPaneEastType : uint
    {
        True = 0,
        False = 1,
    }

    public enum BlackStainedGlassPaneNorthType : uint
    {
        True = 0,
        False = 1,
    }

    public enum BlackStainedGlassPaneSouthType : uint
    {
        True = 0,
        False = 1,
    }

    public enum BlackStainedGlassPaneWaterloggedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum BlackStainedGlassPaneWestType : uint
    {
        True = 0,
        False = 1,
    }

    public enum AcaciaStairsFacingType : uint
    {
        North = 0,
        South = 1,
        West = 2,
        East = 3,
    }

    public enum AcaciaStairsHalfType : uint
    {
        Top = 0,
        Bottom = 1,
    }

    public enum AcaciaStairsShapeType : uint
    {
        Straight = 0,
        InnerLeft = 1,
        InnerRight = 2,
        OuterLeft = 3,
        OuterRight = 4,
    }

    public enum AcaciaStairsWaterloggedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum DarkOakStairsFacingType : uint
    {
        North = 0,
        South = 1,
        West = 2,
        East = 3,
    }

    public enum DarkOakStairsHalfType : uint
    {
        Top = 0,
        Bottom = 1,
    }

    public enum DarkOakStairsShapeType : uint
    {
        Straight = 0,
        InnerLeft = 1,
        InnerRight = 2,
        OuterLeft = 3,
        OuterRight = 4,
    }

    public enum DarkOakStairsWaterloggedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum IronTrapdoorFacingType : uint
    {
        North = 0,
        South = 1,
        West = 2,
        East = 3,
    }

    public enum IronTrapdoorHalfType : uint
    {
        Top = 0,
        Bottom = 1,
    }

    public enum IronTrapdoorOpenType : uint
    {
        True = 0,
        False = 1,
    }

    public enum IronTrapdoorPoweredType : uint
    {
        True = 0,
        False = 1,
    }

    public enum IronTrapdoorWaterloggedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum PrismarineStairsFacingType : uint
    {
        North = 0,
        South = 1,
        West = 2,
        East = 3,
    }

    public enum PrismarineStairsHalfType : uint
    {
        Top = 0,
        Bottom = 1,
    }

    public enum PrismarineStairsShapeType : uint
    {
        Straight = 0,
        InnerLeft = 1,
        InnerRight = 2,
        OuterLeft = 3,
        OuterRight = 4,
    }

    public enum PrismarineStairsWaterloggedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum PrismarineBrickStairsFacingType : uint
    {
        North = 0,
        South = 1,
        West = 2,
        East = 3,
    }

    public enum PrismarineBrickStairsHalfType : uint
    {
        Top = 0,
        Bottom = 1,
    }

    public enum PrismarineBrickStairsShapeType : uint
    {
        Straight = 0,
        InnerLeft = 1,
        InnerRight = 2,
        OuterLeft = 3,
        OuterRight = 4,
    }

    public enum PrismarineBrickStairsWaterloggedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum DarkPrismarineStairsFacingType : uint
    {
        North = 0,
        South = 1,
        West = 2,
        East = 3,
    }

    public enum DarkPrismarineStairsHalfType : uint
    {
        Top = 0,
        Bottom = 1,
    }

    public enum DarkPrismarineStairsShapeType : uint
    {
        Straight = 0,
        InnerLeft = 1,
        InnerRight = 2,
        OuterLeft = 3,
        OuterRight = 4,
    }

    public enum DarkPrismarineStairsWaterloggedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum PrismarineSlabTypeType : uint
    {
        Top = 0,
        Bottom = 1,
        Double = 2,
    }

    public enum PrismarineSlabWaterloggedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum PrismarineBrickSlabTypeType : uint
    {
        Top = 0,
        Bottom = 1,
        Double = 2,
    }

    public enum PrismarineBrickSlabWaterloggedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum DarkPrismarineSlabTypeType : uint
    {
        Top = 0,
        Bottom = 1,
        Double = 2,
    }

    public enum DarkPrismarineSlabWaterloggedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum HayBlockAxisType : uint
    {
        X = 0,
        Y = 1,
        Z = 2,
    }

    public enum SunflowerHalfType : uint
    {
        Upper = 0,
        Lower = 1,
    }

    public enum LilacHalfType : uint
    {
        Upper = 0,
        Lower = 1,
    }

    public enum RoseBushHalfType : uint
    {
        Upper = 0,
        Lower = 1,
    }

    public enum PeonyHalfType : uint
    {
        Upper = 0,
        Lower = 1,
    }

    public enum TallGrassHalfType : uint
    {
        Upper = 0,
        Lower = 1,
    }

    public enum LargeFernHalfType : uint
    {
        Upper = 0,
        Lower = 1,
    }

    public enum WhiteBannerRotationType : uint
    {
        Rotation0 = 0,
        Rotation1 = 1,
        Rotation2 = 2,
        Rotation3 = 3,
        Rotation4 = 4,
        Rotation5 = 5,
        Rotation6 = 6,
        Rotation7 = 7,
        Rotation8 = 8,
        Rotation9 = 9,
        Rotation10 = 10,
        Rotation11 = 11,
        Rotation12 = 12,
        Rotation13 = 13,
        Rotation14 = 14,
        Rotation15 = 15,
    }

    public enum OrangeBannerRotationType : uint
    {
        Rotation0 = 0,
        Rotation1 = 1,
        Rotation2 = 2,
        Rotation3 = 3,
        Rotation4 = 4,
        Rotation5 = 5,
        Rotation6 = 6,
        Rotation7 = 7,
        Rotation8 = 8,
        Rotation9 = 9,
        Rotation10 = 10,
        Rotation11 = 11,
        Rotation12 = 12,
        Rotation13 = 13,
        Rotation14 = 14,
        Rotation15 = 15,
    }

    public enum MagentaBannerRotationType : uint
    {
        Rotation0 = 0,
        Rotation1 = 1,
        Rotation2 = 2,
        Rotation3 = 3,
        Rotation4 = 4,
        Rotation5 = 5,
        Rotation6 = 6,
        Rotation7 = 7,
        Rotation8 = 8,
        Rotation9 = 9,
        Rotation10 = 10,
        Rotation11 = 11,
        Rotation12 = 12,
        Rotation13 = 13,
        Rotation14 = 14,
        Rotation15 = 15,
    }

    public enum LightBlueBannerRotationType : uint
    {
        Rotation0 = 0,
        Rotation1 = 1,
        Rotation2 = 2,
        Rotation3 = 3,
        Rotation4 = 4,
        Rotation5 = 5,
        Rotation6 = 6,
        Rotation7 = 7,
        Rotation8 = 8,
        Rotation9 = 9,
        Rotation10 = 10,
        Rotation11 = 11,
        Rotation12 = 12,
        Rotation13 = 13,
        Rotation14 = 14,
        Rotation15 = 15,
    }

    public enum YellowBannerRotationType : uint
    {
        Rotation0 = 0,
        Rotation1 = 1,
        Rotation2 = 2,
        Rotation3 = 3,
        Rotation4 = 4,
        Rotation5 = 5,
        Rotation6 = 6,
        Rotation7 = 7,
        Rotation8 = 8,
        Rotation9 = 9,
        Rotation10 = 10,
        Rotation11 = 11,
        Rotation12 = 12,
        Rotation13 = 13,
        Rotation14 = 14,
        Rotation15 = 15,
    }

    public enum LimeBannerRotationType : uint
    {
        Rotation0 = 0,
        Rotation1 = 1,
        Rotation2 = 2,
        Rotation3 = 3,
        Rotation4 = 4,
        Rotation5 = 5,
        Rotation6 = 6,
        Rotation7 = 7,
        Rotation8 = 8,
        Rotation9 = 9,
        Rotation10 = 10,
        Rotation11 = 11,
        Rotation12 = 12,
        Rotation13 = 13,
        Rotation14 = 14,
        Rotation15 = 15,
    }

    public enum PinkBannerRotationType : uint
    {
        Rotation0 = 0,
        Rotation1 = 1,
        Rotation2 = 2,
        Rotation3 = 3,
        Rotation4 = 4,
        Rotation5 = 5,
        Rotation6 = 6,
        Rotation7 = 7,
        Rotation8 = 8,
        Rotation9 = 9,
        Rotation10 = 10,
        Rotation11 = 11,
        Rotation12 = 12,
        Rotation13 = 13,
        Rotation14 = 14,
        Rotation15 = 15,
    }

    public enum GrayBannerRotationType : uint
    {
        Rotation0 = 0,
        Rotation1 = 1,
        Rotation2 = 2,
        Rotation3 = 3,
        Rotation4 = 4,
        Rotation5 = 5,
        Rotation6 = 6,
        Rotation7 = 7,
        Rotation8 = 8,
        Rotation9 = 9,
        Rotation10 = 10,
        Rotation11 = 11,
        Rotation12 = 12,
        Rotation13 = 13,
        Rotation14 = 14,
        Rotation15 = 15,
    }

    public enum LightGrayBannerRotationType : uint
    {
        Rotation0 = 0,
        Rotation1 = 1,
        Rotation2 = 2,
        Rotation3 = 3,
        Rotation4 = 4,
        Rotation5 = 5,
        Rotation6 = 6,
        Rotation7 = 7,
        Rotation8 = 8,
        Rotation9 = 9,
        Rotation10 = 10,
        Rotation11 = 11,
        Rotation12 = 12,
        Rotation13 = 13,
        Rotation14 = 14,
        Rotation15 = 15,
    }

    public enum CyanBannerRotationType : uint
    {
        Rotation0 = 0,
        Rotation1 = 1,
        Rotation2 = 2,
        Rotation3 = 3,
        Rotation4 = 4,
        Rotation5 = 5,
        Rotation6 = 6,
        Rotation7 = 7,
        Rotation8 = 8,
        Rotation9 = 9,
        Rotation10 = 10,
        Rotation11 = 11,
        Rotation12 = 12,
        Rotation13 = 13,
        Rotation14 = 14,
        Rotation15 = 15,
    }

    public enum PurpleBannerRotationType : uint
    {
        Rotation0 = 0,
        Rotation1 = 1,
        Rotation2 = 2,
        Rotation3 = 3,
        Rotation4 = 4,
        Rotation5 = 5,
        Rotation6 = 6,
        Rotation7 = 7,
        Rotation8 = 8,
        Rotation9 = 9,
        Rotation10 = 10,
        Rotation11 = 11,
        Rotation12 = 12,
        Rotation13 = 13,
        Rotation14 = 14,
        Rotation15 = 15,
    }

    public enum BlueBannerRotationType : uint
    {
        Rotation0 = 0,
        Rotation1 = 1,
        Rotation2 = 2,
        Rotation3 = 3,
        Rotation4 = 4,
        Rotation5 = 5,
        Rotation6 = 6,
        Rotation7 = 7,
        Rotation8 = 8,
        Rotation9 = 9,
        Rotation10 = 10,
        Rotation11 = 11,
        Rotation12 = 12,
        Rotation13 = 13,
        Rotation14 = 14,
        Rotation15 = 15,
    }

    public enum BrownBannerRotationType : uint
    {
        Rotation0 = 0,
        Rotation1 = 1,
        Rotation2 = 2,
        Rotation3 = 3,
        Rotation4 = 4,
        Rotation5 = 5,
        Rotation6 = 6,
        Rotation7 = 7,
        Rotation8 = 8,
        Rotation9 = 9,
        Rotation10 = 10,
        Rotation11 = 11,
        Rotation12 = 12,
        Rotation13 = 13,
        Rotation14 = 14,
        Rotation15 = 15,
    }

    public enum GreenBannerRotationType : uint
    {
        Rotation0 = 0,
        Rotation1 = 1,
        Rotation2 = 2,
        Rotation3 = 3,
        Rotation4 = 4,
        Rotation5 = 5,
        Rotation6 = 6,
        Rotation7 = 7,
        Rotation8 = 8,
        Rotation9 = 9,
        Rotation10 = 10,
        Rotation11 = 11,
        Rotation12 = 12,
        Rotation13 = 13,
        Rotation14 = 14,
        Rotation15 = 15,
    }

    public enum RedBannerRotationType : uint
    {
        Rotation0 = 0,
        Rotation1 = 1,
        Rotation2 = 2,
        Rotation3 = 3,
        Rotation4 = 4,
        Rotation5 = 5,
        Rotation6 = 6,
        Rotation7 = 7,
        Rotation8 = 8,
        Rotation9 = 9,
        Rotation10 = 10,
        Rotation11 = 11,
        Rotation12 = 12,
        Rotation13 = 13,
        Rotation14 = 14,
        Rotation15 = 15,
    }

    public enum BlackBannerRotationType : uint
    {
        Rotation0 = 0,
        Rotation1 = 1,
        Rotation2 = 2,
        Rotation3 = 3,
        Rotation4 = 4,
        Rotation5 = 5,
        Rotation6 = 6,
        Rotation7 = 7,
        Rotation8 = 8,
        Rotation9 = 9,
        Rotation10 = 10,
        Rotation11 = 11,
        Rotation12 = 12,
        Rotation13 = 13,
        Rotation14 = 14,
        Rotation15 = 15,
    }

    public enum WhiteWallBannerFacingType : uint
    {
        North = 0,
        South = 1,
        West = 2,
        East = 3,
    }

    public enum OrangeWallBannerFacingType : uint
    {
        North = 0,
        South = 1,
        West = 2,
        East = 3,
    }

    public enum MagentaWallBannerFacingType : uint
    {
        North = 0,
        South = 1,
        West = 2,
        East = 3,
    }

    public enum LightBlueWallBannerFacingType : uint
    {
        North = 0,
        South = 1,
        West = 2,
        East = 3,
    }

    public enum YellowWallBannerFacingType : uint
    {
        North = 0,
        South = 1,
        West = 2,
        East = 3,
    }

    public enum LimeWallBannerFacingType : uint
    {
        North = 0,
        South = 1,
        West = 2,
        East = 3,
    }

    public enum PinkWallBannerFacingType : uint
    {
        North = 0,
        South = 1,
        West = 2,
        East = 3,
    }

    public enum GrayWallBannerFacingType : uint
    {
        North = 0,
        South = 1,
        West = 2,
        East = 3,
    }

    public enum LightGrayWallBannerFacingType : uint
    {
        North = 0,
        South = 1,
        West = 2,
        East = 3,
    }

    public enum CyanWallBannerFacingType : uint
    {
        North = 0,
        South = 1,
        West = 2,
        East = 3,
    }

    public enum PurpleWallBannerFacingType : uint
    {
        North = 0,
        South = 1,
        West = 2,
        East = 3,
    }

    public enum BlueWallBannerFacingType : uint
    {
        North = 0,
        South = 1,
        West = 2,
        East = 3,
    }

    public enum BrownWallBannerFacingType : uint
    {
        North = 0,
        South = 1,
        West = 2,
        East = 3,
    }

    public enum GreenWallBannerFacingType : uint
    {
        North = 0,
        South = 1,
        West = 2,
        East = 3,
    }

    public enum RedWallBannerFacingType : uint
    {
        North = 0,
        South = 1,
        West = 2,
        East = 3,
    }

    public enum BlackWallBannerFacingType : uint
    {
        North = 0,
        South = 1,
        West = 2,
        East = 3,
    }

    public enum RedSandstoneStairsFacingType : uint
    {
        North = 0,
        South = 1,
        West = 2,
        East = 3,
    }

    public enum RedSandstoneStairsHalfType : uint
    {
        Top = 0,
        Bottom = 1,
    }

    public enum RedSandstoneStairsShapeType : uint
    {
        Straight = 0,
        InnerLeft = 1,
        InnerRight = 2,
        OuterLeft = 3,
        OuterRight = 4,
    }

    public enum RedSandstoneStairsWaterloggedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum OakSlabTypeType : uint
    {
        Top = 0,
        Bottom = 1,
        Double = 2,
    }

    public enum OakSlabWaterloggedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum SpruceSlabTypeType : uint
    {
        Top = 0,
        Bottom = 1,
        Double = 2,
    }

    public enum SpruceSlabWaterloggedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum BirchSlabTypeType : uint
    {
        Top = 0,
        Bottom = 1,
        Double = 2,
    }

    public enum BirchSlabWaterloggedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum JungleSlabTypeType : uint
    {
        Top = 0,
        Bottom = 1,
        Double = 2,
    }

    public enum JungleSlabWaterloggedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum AcaciaSlabTypeType : uint
    {
        Top = 0,
        Bottom = 1,
        Double = 2,
    }

    public enum AcaciaSlabWaterloggedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum DarkOakSlabTypeType : uint
    {
        Top = 0,
        Bottom = 1,
        Double = 2,
    }

    public enum DarkOakSlabWaterloggedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum StoneSlabTypeType : uint
    {
        Top = 0,
        Bottom = 1,
        Double = 2,
    }

    public enum StoneSlabWaterloggedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum SmoothStoneSlabTypeType : uint
    {
        Top = 0,
        Bottom = 1,
        Double = 2,
    }

    public enum SmoothStoneSlabWaterloggedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum SandstoneSlabTypeType : uint
    {
        Top = 0,
        Bottom = 1,
        Double = 2,
    }

    public enum SandstoneSlabWaterloggedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum CutSandstoneSlabTypeType : uint
    {
        Top = 0,
        Bottom = 1,
        Double = 2,
    }

    public enum CutSandstoneSlabWaterloggedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum PetrifiedOakSlabTypeType : uint
    {
        Top = 0,
        Bottom = 1,
        Double = 2,
    }

    public enum PetrifiedOakSlabWaterloggedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum CobblestoneSlabTypeType : uint
    {
        Top = 0,
        Bottom = 1,
        Double = 2,
    }

    public enum CobblestoneSlabWaterloggedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum BrickSlabTypeType : uint
    {
        Top = 0,
        Bottom = 1,
        Double = 2,
    }

    public enum BrickSlabWaterloggedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum StoneBrickSlabTypeType : uint
    {
        Top = 0,
        Bottom = 1,
        Double = 2,
    }

    public enum StoneBrickSlabWaterloggedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum NetherBrickSlabTypeType : uint
    {
        Top = 0,
        Bottom = 1,
        Double = 2,
    }

    public enum NetherBrickSlabWaterloggedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum QuartzSlabTypeType : uint
    {
        Top = 0,
        Bottom = 1,
        Double = 2,
    }

    public enum QuartzSlabWaterloggedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum RedSandstoneSlabTypeType : uint
    {
        Top = 0,
        Bottom = 1,
        Double = 2,
    }

    public enum RedSandstoneSlabWaterloggedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum CutRedSandstoneSlabTypeType : uint
    {
        Top = 0,
        Bottom = 1,
        Double = 2,
    }

    public enum CutRedSandstoneSlabWaterloggedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum PurpurSlabTypeType : uint
    {
        Top = 0,
        Bottom = 1,
        Double = 2,
    }

    public enum PurpurSlabWaterloggedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum SpruceFenceGateFacingType : uint
    {
        North = 0,
        South = 1,
        West = 2,
        East = 3,
    }

    public enum SpruceFenceGateInWallType : uint
    {
        True = 0,
        False = 1,
    }

    public enum SpruceFenceGateOpenType : uint
    {
        True = 0,
        False = 1,
    }

    public enum SpruceFenceGatePoweredType : uint
    {
        True = 0,
        False = 1,
    }

    public enum BirchFenceGateFacingType : uint
    {
        North = 0,
        South = 1,
        West = 2,
        East = 3,
    }

    public enum BirchFenceGateInWallType : uint
    {
        True = 0,
        False = 1,
    }

    public enum BirchFenceGateOpenType : uint
    {
        True = 0,
        False = 1,
    }

    public enum BirchFenceGatePoweredType : uint
    {
        True = 0,
        False = 1,
    }

    public enum JungleFenceGateFacingType : uint
    {
        North = 0,
        South = 1,
        West = 2,
        East = 3,
    }

    public enum JungleFenceGateInWallType : uint
    {
        True = 0,
        False = 1,
    }

    public enum JungleFenceGateOpenType : uint
    {
        True = 0,
        False = 1,
    }

    public enum JungleFenceGatePoweredType : uint
    {
        True = 0,
        False = 1,
    }

    public enum AcaciaFenceGateFacingType : uint
    {
        North = 0,
        South = 1,
        West = 2,
        East = 3,
    }

    public enum AcaciaFenceGateInWallType : uint
    {
        True = 0,
        False = 1,
    }

    public enum AcaciaFenceGateOpenType : uint
    {
        True = 0,
        False = 1,
    }

    public enum AcaciaFenceGatePoweredType : uint
    {
        True = 0,
        False = 1,
    }

    public enum DarkOakFenceGateFacingType : uint
    {
        North = 0,
        South = 1,
        West = 2,
        East = 3,
    }

    public enum DarkOakFenceGateInWallType : uint
    {
        True = 0,
        False = 1,
    }

    public enum DarkOakFenceGateOpenType : uint
    {
        True = 0,
        False = 1,
    }

    public enum DarkOakFenceGatePoweredType : uint
    {
        True = 0,
        False = 1,
    }

    public enum SpruceFenceEastType : uint
    {
        True = 0,
        False = 1,
    }

    public enum SpruceFenceNorthType : uint
    {
        True = 0,
        False = 1,
    }

    public enum SpruceFenceSouthType : uint
    {
        True = 0,
        False = 1,
    }

    public enum SpruceFenceWaterloggedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum SpruceFenceWestType : uint
    {
        True = 0,
        False = 1,
    }

    public enum BirchFenceEastType : uint
    {
        True = 0,
        False = 1,
    }

    public enum BirchFenceNorthType : uint
    {
        True = 0,
        False = 1,
    }

    public enum BirchFenceSouthType : uint
    {
        True = 0,
        False = 1,
    }

    public enum BirchFenceWaterloggedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum BirchFenceWestType : uint
    {
        True = 0,
        False = 1,
    }

    public enum JungleFenceEastType : uint
    {
        True = 0,
        False = 1,
    }

    public enum JungleFenceNorthType : uint
    {
        True = 0,
        False = 1,
    }

    public enum JungleFenceSouthType : uint
    {
        True = 0,
        False = 1,
    }

    public enum JungleFenceWaterloggedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum JungleFenceWestType : uint
    {
        True = 0,
        False = 1,
    }

    public enum AcaciaFenceEastType : uint
    {
        True = 0,
        False = 1,
    }

    public enum AcaciaFenceNorthType : uint
    {
        True = 0,
        False = 1,
    }

    public enum AcaciaFenceSouthType : uint
    {
        True = 0,
        False = 1,
    }

    public enum AcaciaFenceWaterloggedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum AcaciaFenceWestType : uint
    {
        True = 0,
        False = 1,
    }

    public enum DarkOakFenceEastType : uint
    {
        True = 0,
        False = 1,
    }

    public enum DarkOakFenceNorthType : uint
    {
        True = 0,
        False = 1,
    }

    public enum DarkOakFenceSouthType : uint
    {
        True = 0,
        False = 1,
    }

    public enum DarkOakFenceWaterloggedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum DarkOakFenceWestType : uint
    {
        True = 0,
        False = 1,
    }

    public enum SpruceDoorFacingType : uint
    {
        North = 0,
        South = 1,
        West = 2,
        East = 3,
    }

    public enum SpruceDoorHalfType : uint
    {
        Upper = 0,
        Lower = 1,
    }

    public enum SpruceDoorHingeType : uint
    {
        Left = 0,
        Right = 1,
    }

    public enum SpruceDoorOpenType : uint
    {
        True = 0,
        False = 1,
    }

    public enum SpruceDoorPoweredType : uint
    {
        True = 0,
        False = 1,
    }

    public enum BirchDoorFacingType : uint
    {
        North = 0,
        South = 1,
        West = 2,
        East = 3,
    }

    public enum BirchDoorHalfType : uint
    {
        Upper = 0,
        Lower = 1,
    }

    public enum BirchDoorHingeType : uint
    {
        Left = 0,
        Right = 1,
    }

    public enum BirchDoorOpenType : uint
    {
        True = 0,
        False = 1,
    }

    public enum BirchDoorPoweredType : uint
    {
        True = 0,
        False = 1,
    }

    public enum JungleDoorFacingType : uint
    {
        North = 0,
        South = 1,
        West = 2,
        East = 3,
    }

    public enum JungleDoorHalfType : uint
    {
        Upper = 0,
        Lower = 1,
    }

    public enum JungleDoorHingeType : uint
    {
        Left = 0,
        Right = 1,
    }

    public enum JungleDoorOpenType : uint
    {
        True = 0,
        False = 1,
    }

    public enum JungleDoorPoweredType : uint
    {
        True = 0,
        False = 1,
    }

    public enum AcaciaDoorFacingType : uint
    {
        North = 0,
        South = 1,
        West = 2,
        East = 3,
    }

    public enum AcaciaDoorHalfType : uint
    {
        Upper = 0,
        Lower = 1,
    }

    public enum AcaciaDoorHingeType : uint
    {
        Left = 0,
        Right = 1,
    }

    public enum AcaciaDoorOpenType : uint
    {
        True = 0,
        False = 1,
    }

    public enum AcaciaDoorPoweredType : uint
    {
        True = 0,
        False = 1,
    }

    public enum DarkOakDoorFacingType : uint
    {
        North = 0,
        South = 1,
        West = 2,
        East = 3,
    }

    public enum DarkOakDoorHalfType : uint
    {
        Upper = 0,
        Lower = 1,
    }

    public enum DarkOakDoorHingeType : uint
    {
        Left = 0,
        Right = 1,
    }

    public enum DarkOakDoorOpenType : uint
    {
        True = 0,
        False = 1,
    }

    public enum DarkOakDoorPoweredType : uint
    {
        True = 0,
        False = 1,
    }

    public enum EndRodFacingType : uint
    {
        North = 0,
        East = 1,
        South = 2,
        West = 3,
        Up = 4,
        Down = 5,
    }

    public enum ChorusPlantDownType : uint
    {
        True = 0,
        False = 1,
    }

    public enum ChorusPlantEastType : uint
    {
        True = 0,
        False = 1,
    }

    public enum ChorusPlantNorthType : uint
    {
        True = 0,
        False = 1,
    }

    public enum ChorusPlantSouthType : uint
    {
        True = 0,
        False = 1,
    }

    public enum ChorusPlantUpType : uint
    {
        True = 0,
        False = 1,
    }

    public enum ChorusPlantWestType : uint
    {
        True = 0,
        False = 1,
    }

    public enum ChorusFlowerAgeType : uint
    {
        Age0 = 0,
        Age1 = 1,
        Age2 = 2,
        Age3 = 3,
        Age4 = 4,
        Age5 = 5,
    }

    public enum PurpurPillarAxisType : uint
    {
        X = 0,
        Y = 1,
        Z = 2,
    }

    public enum PurpurStairsFacingType : uint
    {
        North = 0,
        South = 1,
        West = 2,
        East = 3,
    }

    public enum PurpurStairsHalfType : uint
    {
        Top = 0,
        Bottom = 1,
    }

    public enum PurpurStairsShapeType : uint
    {
        Straight = 0,
        InnerLeft = 1,
        InnerRight = 2,
        OuterLeft = 3,
        OuterRight = 4,
    }

    public enum PurpurStairsWaterloggedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum BeetrootsAgeType : uint
    {
        Age0 = 0,
        Age1 = 1,
        Age2 = 2,
        Age3 = 3,
    }

    public enum RepeatingCommandBlockConditionalType : uint
    {
        True = 0,
        False = 1,
    }

    public enum RepeatingCommandBlockFacingType : uint
    {
        North = 0,
        East = 1,
        South = 2,
        West = 3,
        Up = 4,
        Down = 5,
    }

    public enum ChainCommandBlockConditionalType : uint
    {
        True = 0,
        False = 1,
    }

    public enum ChainCommandBlockFacingType : uint
    {
        North = 0,
        East = 1,
        South = 2,
        West = 3,
        Up = 4,
        Down = 5,
    }

    public enum FrostedIceAgeType : uint
    {
        Age0 = 0,
        Age1 = 1,
        Age2 = 2,
        Age3 = 3,
    }

    public enum BoneBlockAxisType : uint
    {
        X = 0,
        Y = 1,
        Z = 2,
    }

    public enum ObserverFacingType : uint
    {
        North = 0,
        East = 1,
        South = 2,
        West = 3,
        Up = 4,
        Down = 5,
    }

    public enum ObserverPoweredType : uint
    {
        True = 0,
        False = 1,
    }

    public enum ShulkerBoxFacingType : uint
    {
        North = 0,
        East = 1,
        South = 2,
        West = 3,
        Up = 4,
        Down = 5,
    }

    public enum WhiteShulkerBoxFacingType : uint
    {
        North = 0,
        East = 1,
        South = 2,
        West = 3,
        Up = 4,
        Down = 5,
    }

    public enum OrangeShulkerBoxFacingType : uint
    {
        North = 0,
        East = 1,
        South = 2,
        West = 3,
        Up = 4,
        Down = 5,
    }

    public enum MagentaShulkerBoxFacingType : uint
    {
        North = 0,
        East = 1,
        South = 2,
        West = 3,
        Up = 4,
        Down = 5,
    }

    public enum LightBlueShulkerBoxFacingType : uint
    {
        North = 0,
        East = 1,
        South = 2,
        West = 3,
        Up = 4,
        Down = 5,
    }

    public enum YellowShulkerBoxFacingType : uint
    {
        North = 0,
        East = 1,
        South = 2,
        West = 3,
        Up = 4,
        Down = 5,
    }

    public enum LimeShulkerBoxFacingType : uint
    {
        North = 0,
        East = 1,
        South = 2,
        West = 3,
        Up = 4,
        Down = 5,
    }

    public enum PinkShulkerBoxFacingType : uint
    {
        North = 0,
        East = 1,
        South = 2,
        West = 3,
        Up = 4,
        Down = 5,
    }

    public enum GrayShulkerBoxFacingType : uint
    {
        North = 0,
        East = 1,
        South = 2,
        West = 3,
        Up = 4,
        Down = 5,
    }

    public enum LightGrayShulkerBoxFacingType : uint
    {
        North = 0,
        East = 1,
        South = 2,
        West = 3,
        Up = 4,
        Down = 5,
    }

    public enum CyanShulkerBoxFacingType : uint
    {
        North = 0,
        East = 1,
        South = 2,
        West = 3,
        Up = 4,
        Down = 5,
    }

    public enum PurpleShulkerBoxFacingType : uint
    {
        North = 0,
        East = 1,
        South = 2,
        West = 3,
        Up = 4,
        Down = 5,
    }

    public enum BlueShulkerBoxFacingType : uint
    {
        North = 0,
        East = 1,
        South = 2,
        West = 3,
        Up = 4,
        Down = 5,
    }

    public enum BrownShulkerBoxFacingType : uint
    {
        North = 0,
        East = 1,
        South = 2,
        West = 3,
        Up = 4,
        Down = 5,
    }

    public enum GreenShulkerBoxFacingType : uint
    {
        North = 0,
        East = 1,
        South = 2,
        West = 3,
        Up = 4,
        Down = 5,
    }

    public enum RedShulkerBoxFacingType : uint
    {
        North = 0,
        East = 1,
        South = 2,
        West = 3,
        Up = 4,
        Down = 5,
    }

    public enum BlackShulkerBoxFacingType : uint
    {
        North = 0,
        East = 1,
        South = 2,
        West = 3,
        Up = 4,
        Down = 5,
    }

    public enum WhiteGlazedTerracottaFacingType : uint
    {
        North = 0,
        South = 1,
        West = 2,
        East = 3,
    }

    public enum OrangeGlazedTerracottaFacingType : uint
    {
        North = 0,
        South = 1,
        West = 2,
        East = 3,
    }

    public enum MagentaGlazedTerracottaFacingType : uint
    {
        North = 0,
        South = 1,
        West = 2,
        East = 3,
    }

    public enum LightBlueGlazedTerracottaFacingType : uint
    {
        North = 0,
        South = 1,
        West = 2,
        East = 3,
    }

    public enum YellowGlazedTerracottaFacingType : uint
    {
        North = 0,
        South = 1,
        West = 2,
        East = 3,
    }

    public enum LimeGlazedTerracottaFacingType : uint
    {
        North = 0,
        South = 1,
        West = 2,
        East = 3,
    }

    public enum PinkGlazedTerracottaFacingType : uint
    {
        North = 0,
        South = 1,
        West = 2,
        East = 3,
    }

    public enum GrayGlazedTerracottaFacingType : uint
    {
        North = 0,
        South = 1,
        West = 2,
        East = 3,
    }

    public enum LightGrayGlazedTerracottaFacingType : uint
    {
        North = 0,
        South = 1,
        West = 2,
        East = 3,
    }

    public enum CyanGlazedTerracottaFacingType : uint
    {
        North = 0,
        South = 1,
        West = 2,
        East = 3,
    }

    public enum PurpleGlazedTerracottaFacingType : uint
    {
        North = 0,
        South = 1,
        West = 2,
        East = 3,
    }

    public enum BlueGlazedTerracottaFacingType : uint
    {
        North = 0,
        South = 1,
        West = 2,
        East = 3,
    }

    public enum BrownGlazedTerracottaFacingType : uint
    {
        North = 0,
        South = 1,
        West = 2,
        East = 3,
    }

    public enum GreenGlazedTerracottaFacingType : uint
    {
        North = 0,
        South = 1,
        West = 2,
        East = 3,
    }

    public enum RedGlazedTerracottaFacingType : uint
    {
        North = 0,
        South = 1,
        West = 2,
        East = 3,
    }

    public enum BlackGlazedTerracottaFacingType : uint
    {
        North = 0,
        South = 1,
        West = 2,
        East = 3,
    }

    public enum KelpAgeType : uint
    {
        Age0 = 0,
        Age1 = 1,
        Age2 = 2,
        Age3 = 3,
        Age4 = 4,
        Age5 = 5,
        Age6 = 6,
        Age7 = 7,
        Age8 = 8,
        Age9 = 9,
        Age10 = 10,
        Age11 = 11,
        Age12 = 12,
        Age13 = 13,
        Age14 = 14,
        Age15 = 15,
        Age16 = 16,
        Age17 = 17,
        Age18 = 18,
        Age19 = 19,
        Age20 = 20,
        Age21 = 21,
        Age22 = 22,
        Age23 = 23,
        Age24 = 24,
        Age25 = 25,
    }

    public enum TurtleEggEggsType : uint
    {
        Eggs1 = 0,
        Eggs2 = 1,
        Eggs3 = 2,
        Eggs4 = 3,
    }

    public enum TurtleEggHatchType : uint
    {
        Hatch0 = 0,
        Hatch1 = 1,
        Hatch2 = 2,
    }

    public enum DeadTubeCoralWaterloggedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum DeadBrainCoralWaterloggedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum DeadBubbleCoralWaterloggedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum DeadFireCoralWaterloggedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum DeadHornCoralWaterloggedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum TubeCoralWaterloggedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum BrainCoralWaterloggedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum BubbleCoralWaterloggedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum FireCoralWaterloggedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum HornCoralWaterloggedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum DeadTubeCoralFanWaterloggedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum DeadBrainCoralFanWaterloggedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum DeadBubbleCoralFanWaterloggedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum DeadFireCoralFanWaterloggedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum DeadHornCoralFanWaterloggedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum TubeCoralFanWaterloggedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum BrainCoralFanWaterloggedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum BubbleCoralFanWaterloggedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum FireCoralFanWaterloggedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum HornCoralFanWaterloggedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum DeadTubeCoralWallFanFacingType : uint
    {
        North = 0,
        South = 1,
        West = 2,
        East = 3,
    }

    public enum DeadTubeCoralWallFanWaterloggedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum DeadBrainCoralWallFanFacingType : uint
    {
        North = 0,
        South = 1,
        West = 2,
        East = 3,
    }

    public enum DeadBrainCoralWallFanWaterloggedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum DeadBubbleCoralWallFanFacingType : uint
    {
        North = 0,
        South = 1,
        West = 2,
        East = 3,
    }

    public enum DeadBubbleCoralWallFanWaterloggedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum DeadFireCoralWallFanFacingType : uint
    {
        North = 0,
        South = 1,
        West = 2,
        East = 3,
    }

    public enum DeadFireCoralWallFanWaterloggedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum DeadHornCoralWallFanFacingType : uint
    {
        North = 0,
        South = 1,
        West = 2,
        East = 3,
    }

    public enum DeadHornCoralWallFanWaterloggedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum TubeCoralWallFanFacingType : uint
    {
        North = 0,
        South = 1,
        West = 2,
        East = 3,
    }

    public enum TubeCoralWallFanWaterloggedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum BrainCoralWallFanFacingType : uint
    {
        North = 0,
        South = 1,
        West = 2,
        East = 3,
    }

    public enum BrainCoralWallFanWaterloggedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum BubbleCoralWallFanFacingType : uint
    {
        North = 0,
        South = 1,
        West = 2,
        East = 3,
    }

    public enum BubbleCoralWallFanWaterloggedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum FireCoralWallFanFacingType : uint
    {
        North = 0,
        South = 1,
        West = 2,
        East = 3,
    }

    public enum FireCoralWallFanWaterloggedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum HornCoralWallFanFacingType : uint
    {
        North = 0,
        South = 1,
        West = 2,
        East = 3,
    }

    public enum HornCoralWallFanWaterloggedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum SeaPicklePicklesType : uint
    {
        Pickles1 = 0,
        Pickles2 = 1,
        Pickles3 = 2,
        Pickles4 = 3,
    }

    public enum SeaPickleWaterloggedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum ConduitWaterloggedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum BambooAgeType : uint
    {
        Age0 = 0,
        Age1 = 1,
    }

    public enum BambooLeavesType : uint
    {
        None = 0,
        Small = 1,
        Large = 2,
    }

    public enum BambooStageType : uint
    {
        Stage0 = 0,
        Stage1 = 1,
    }

    public enum BubbleColumnDragType : uint
    {
        True = 0,
        False = 1,
    }

    public enum PolishedGraniteStairsFacingType : uint
    {
        North = 0,
        South = 1,
        West = 2,
        East = 3,
    }

    public enum PolishedGraniteStairsHalfType : uint
    {
        Top = 0,
        Bottom = 1,
    }

    public enum PolishedGraniteStairsShapeType : uint
    {
        Straight = 0,
        InnerLeft = 1,
        InnerRight = 2,
        OuterLeft = 3,
        OuterRight = 4,
    }

    public enum PolishedGraniteStairsWaterloggedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum SmoothRedSandstoneStairsFacingType : uint
    {
        North = 0,
        South = 1,
        West = 2,
        East = 3,
    }

    public enum SmoothRedSandstoneStairsHalfType : uint
    {
        Top = 0,
        Bottom = 1,
    }

    public enum SmoothRedSandstoneStairsShapeType : uint
    {
        Straight = 0,
        InnerLeft = 1,
        InnerRight = 2,
        OuterLeft = 3,
        OuterRight = 4,
    }

    public enum SmoothRedSandstoneStairsWaterloggedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum MossyStoneBrickStairsFacingType : uint
    {
        North = 0,
        South = 1,
        West = 2,
        East = 3,
    }

    public enum MossyStoneBrickStairsHalfType : uint
    {
        Top = 0,
        Bottom = 1,
    }

    public enum MossyStoneBrickStairsShapeType : uint
    {
        Straight = 0,
        InnerLeft = 1,
        InnerRight = 2,
        OuterLeft = 3,
        OuterRight = 4,
    }

    public enum MossyStoneBrickStairsWaterloggedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum PolishedDioriteStairsFacingType : uint
    {
        North = 0,
        South = 1,
        West = 2,
        East = 3,
    }

    public enum PolishedDioriteStairsHalfType : uint
    {
        Top = 0,
        Bottom = 1,
    }

    public enum PolishedDioriteStairsShapeType : uint
    {
        Straight = 0,
        InnerLeft = 1,
        InnerRight = 2,
        OuterLeft = 3,
        OuterRight = 4,
    }

    public enum PolishedDioriteStairsWaterloggedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum MossyCobblestoneStairsFacingType : uint
    {
        North = 0,
        South = 1,
        West = 2,
        East = 3,
    }

    public enum MossyCobblestoneStairsHalfType : uint
    {
        Top = 0,
        Bottom = 1,
    }

    public enum MossyCobblestoneStairsShapeType : uint
    {
        Straight = 0,
        InnerLeft = 1,
        InnerRight = 2,
        OuterLeft = 3,
        OuterRight = 4,
    }

    public enum MossyCobblestoneStairsWaterloggedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum EndStoneBrickStairsFacingType : uint
    {
        North = 0,
        South = 1,
        West = 2,
        East = 3,
    }

    public enum EndStoneBrickStairsHalfType : uint
    {
        Top = 0,
        Bottom = 1,
    }

    public enum EndStoneBrickStairsShapeType : uint
    {
        Straight = 0,
        InnerLeft = 1,
        InnerRight = 2,
        OuterLeft = 3,
        OuterRight = 4,
    }

    public enum EndStoneBrickStairsWaterloggedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum StoneStairsFacingType : uint
    {
        North = 0,
        South = 1,
        West = 2,
        East = 3,
    }

    public enum StoneStairsHalfType : uint
    {
        Top = 0,
        Bottom = 1,
    }

    public enum StoneStairsShapeType : uint
    {
        Straight = 0,
        InnerLeft = 1,
        InnerRight = 2,
        OuterLeft = 3,
        OuterRight = 4,
    }

    public enum StoneStairsWaterloggedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum SmoothSandstoneStairsFacingType : uint
    {
        North = 0,
        South = 1,
        West = 2,
        East = 3,
    }

    public enum SmoothSandstoneStairsHalfType : uint
    {
        Top = 0,
        Bottom = 1,
    }

    public enum SmoothSandstoneStairsShapeType : uint
    {
        Straight = 0,
        InnerLeft = 1,
        InnerRight = 2,
        OuterLeft = 3,
        OuterRight = 4,
    }

    public enum SmoothSandstoneStairsWaterloggedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum SmoothQuartzStairsFacingType : uint
    {
        North = 0,
        South = 1,
        West = 2,
        East = 3,
    }

    public enum SmoothQuartzStairsHalfType : uint
    {
        Top = 0,
        Bottom = 1,
    }

    public enum SmoothQuartzStairsShapeType : uint
    {
        Straight = 0,
        InnerLeft = 1,
        InnerRight = 2,
        OuterLeft = 3,
        OuterRight = 4,
    }

    public enum SmoothQuartzStairsWaterloggedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum GraniteStairsFacingType : uint
    {
        North = 0,
        South = 1,
        West = 2,
        East = 3,
    }

    public enum GraniteStairsHalfType : uint
    {
        Top = 0,
        Bottom = 1,
    }

    public enum GraniteStairsShapeType : uint
    {
        Straight = 0,
        InnerLeft = 1,
        InnerRight = 2,
        OuterLeft = 3,
        OuterRight = 4,
    }

    public enum GraniteStairsWaterloggedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum AndesiteStairsFacingType : uint
    {
        North = 0,
        South = 1,
        West = 2,
        East = 3,
    }

    public enum AndesiteStairsHalfType : uint
    {
        Top = 0,
        Bottom = 1,
    }

    public enum AndesiteStairsShapeType : uint
    {
        Straight = 0,
        InnerLeft = 1,
        InnerRight = 2,
        OuterLeft = 3,
        OuterRight = 4,
    }

    public enum AndesiteStairsWaterloggedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum RedNetherBrickStairsFacingType : uint
    {
        North = 0,
        South = 1,
        West = 2,
        East = 3,
    }

    public enum RedNetherBrickStairsHalfType : uint
    {
        Top = 0,
        Bottom = 1,
    }

    public enum RedNetherBrickStairsShapeType : uint
    {
        Straight = 0,
        InnerLeft = 1,
        InnerRight = 2,
        OuterLeft = 3,
        OuterRight = 4,
    }

    public enum RedNetherBrickStairsWaterloggedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum PolishedAndesiteStairsFacingType : uint
    {
        North = 0,
        South = 1,
        West = 2,
        East = 3,
    }

    public enum PolishedAndesiteStairsHalfType : uint
    {
        Top = 0,
        Bottom = 1,
    }

    public enum PolishedAndesiteStairsShapeType : uint
    {
        Straight = 0,
        InnerLeft = 1,
        InnerRight = 2,
        OuterLeft = 3,
        OuterRight = 4,
    }

    public enum PolishedAndesiteStairsWaterloggedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum DioriteStairsFacingType : uint
    {
        North = 0,
        South = 1,
        West = 2,
        East = 3,
    }

    public enum DioriteStairsHalfType : uint
    {
        Top = 0,
        Bottom = 1,
    }

    public enum DioriteStairsShapeType : uint
    {
        Straight = 0,
        InnerLeft = 1,
        InnerRight = 2,
        OuterLeft = 3,
        OuterRight = 4,
    }

    public enum DioriteStairsWaterloggedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum PolishedGraniteSlabTypeType : uint
    {
        Top = 0,
        Bottom = 1,
        Double = 2,
    }

    public enum PolishedGraniteSlabWaterloggedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum SmoothRedSandstoneSlabTypeType : uint
    {
        Top = 0,
        Bottom = 1,
        Double = 2,
    }

    public enum SmoothRedSandstoneSlabWaterloggedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum MossyStoneBrickSlabTypeType : uint
    {
        Top = 0,
        Bottom = 1,
        Double = 2,
    }

    public enum MossyStoneBrickSlabWaterloggedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum PolishedDioriteSlabTypeType : uint
    {
        Top = 0,
        Bottom = 1,
        Double = 2,
    }

    public enum PolishedDioriteSlabWaterloggedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum MossyCobblestoneSlabTypeType : uint
    {
        Top = 0,
        Bottom = 1,
        Double = 2,
    }

    public enum MossyCobblestoneSlabWaterloggedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum EndStoneBrickSlabTypeType : uint
    {
        Top = 0,
        Bottom = 1,
        Double = 2,
    }

    public enum EndStoneBrickSlabWaterloggedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum SmoothSandstoneSlabTypeType : uint
    {
        Top = 0,
        Bottom = 1,
        Double = 2,
    }

    public enum SmoothSandstoneSlabWaterloggedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum SmoothQuartzSlabTypeType : uint
    {
        Top = 0,
        Bottom = 1,
        Double = 2,
    }

    public enum SmoothQuartzSlabWaterloggedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum GraniteSlabTypeType : uint
    {
        Top = 0,
        Bottom = 1,
        Double = 2,
    }

    public enum GraniteSlabWaterloggedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum AndesiteSlabTypeType : uint
    {
        Top = 0,
        Bottom = 1,
        Double = 2,
    }

    public enum AndesiteSlabWaterloggedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum RedNetherBrickSlabTypeType : uint
    {
        Top = 0,
        Bottom = 1,
        Double = 2,
    }

    public enum RedNetherBrickSlabWaterloggedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum PolishedAndesiteSlabTypeType : uint
    {
        Top = 0,
        Bottom = 1,
        Double = 2,
    }

    public enum PolishedAndesiteSlabWaterloggedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum DioriteSlabTypeType : uint
    {
        Top = 0,
        Bottom = 1,
        Double = 2,
    }

    public enum DioriteSlabWaterloggedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum BrickWallEastType : uint
    {
        True = 0,
        False = 1,
    }

    public enum BrickWallNorthType : uint
    {
        True = 0,
        False = 1,
    }

    public enum BrickWallSouthType : uint
    {
        True = 0,
        False = 1,
    }

    public enum BrickWallUpType : uint
    {
        True = 0,
        False = 1,
    }

    public enum BrickWallWaterloggedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum BrickWallWestType : uint
    {
        True = 0,
        False = 1,
    }

    public enum PrismarineWallEastType : uint
    {
        True = 0,
        False = 1,
    }

    public enum PrismarineWallNorthType : uint
    {
        True = 0,
        False = 1,
    }

    public enum PrismarineWallSouthType : uint
    {
        True = 0,
        False = 1,
    }

    public enum PrismarineWallUpType : uint
    {
        True = 0,
        False = 1,
    }

    public enum PrismarineWallWaterloggedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum PrismarineWallWestType : uint
    {
        True = 0,
        False = 1,
    }

    public enum RedSandstoneWallEastType : uint
    {
        True = 0,
        False = 1,
    }

    public enum RedSandstoneWallNorthType : uint
    {
        True = 0,
        False = 1,
    }

    public enum RedSandstoneWallSouthType : uint
    {
        True = 0,
        False = 1,
    }

    public enum RedSandstoneWallUpType : uint
    {
        True = 0,
        False = 1,
    }

    public enum RedSandstoneWallWaterloggedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum RedSandstoneWallWestType : uint
    {
        True = 0,
        False = 1,
    }

    public enum MossyStoneBrickWallEastType : uint
    {
        True = 0,
        False = 1,
    }

    public enum MossyStoneBrickWallNorthType : uint
    {
        True = 0,
        False = 1,
    }

    public enum MossyStoneBrickWallSouthType : uint
    {
        True = 0,
        False = 1,
    }

    public enum MossyStoneBrickWallUpType : uint
    {
        True = 0,
        False = 1,
    }

    public enum MossyStoneBrickWallWaterloggedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum MossyStoneBrickWallWestType : uint
    {
        True = 0,
        False = 1,
    }

    public enum GraniteWallEastType : uint
    {
        True = 0,
        False = 1,
    }

    public enum GraniteWallNorthType : uint
    {
        True = 0,
        False = 1,
    }

    public enum GraniteWallSouthType : uint
    {
        True = 0,
        False = 1,
    }

    public enum GraniteWallUpType : uint
    {
        True = 0,
        False = 1,
    }

    public enum GraniteWallWaterloggedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum GraniteWallWestType : uint
    {
        True = 0,
        False = 1,
    }

    public enum StoneBrickWallEastType : uint
    {
        True = 0,
        False = 1,
    }

    public enum StoneBrickWallNorthType : uint
    {
        True = 0,
        False = 1,
    }

    public enum StoneBrickWallSouthType : uint
    {
        True = 0,
        False = 1,
    }

    public enum StoneBrickWallUpType : uint
    {
        True = 0,
        False = 1,
    }

    public enum StoneBrickWallWaterloggedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum StoneBrickWallWestType : uint
    {
        True = 0,
        False = 1,
    }

    public enum NetherBrickWallEastType : uint
    {
        True = 0,
        False = 1,
    }

    public enum NetherBrickWallNorthType : uint
    {
        True = 0,
        False = 1,
    }

    public enum NetherBrickWallSouthType : uint
    {
        True = 0,
        False = 1,
    }

    public enum NetherBrickWallUpType : uint
    {
        True = 0,
        False = 1,
    }

    public enum NetherBrickWallWaterloggedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum NetherBrickWallWestType : uint
    {
        True = 0,
        False = 1,
    }

    public enum AndesiteWallEastType : uint
    {
        True = 0,
        False = 1,
    }

    public enum AndesiteWallNorthType : uint
    {
        True = 0,
        False = 1,
    }

    public enum AndesiteWallSouthType : uint
    {
        True = 0,
        False = 1,
    }

    public enum AndesiteWallUpType : uint
    {
        True = 0,
        False = 1,
    }

    public enum AndesiteWallWaterloggedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum AndesiteWallWestType : uint
    {
        True = 0,
        False = 1,
    }

    public enum RedNetherBrickWallEastType : uint
    {
        True = 0,
        False = 1,
    }

    public enum RedNetherBrickWallNorthType : uint
    {
        True = 0,
        False = 1,
    }

    public enum RedNetherBrickWallSouthType : uint
    {
        True = 0,
        False = 1,
    }

    public enum RedNetherBrickWallUpType : uint
    {
        True = 0,
        False = 1,
    }

    public enum RedNetherBrickWallWaterloggedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum RedNetherBrickWallWestType : uint
    {
        True = 0,
        False = 1,
    }

    public enum SandstoneWallEastType : uint
    {
        True = 0,
        False = 1,
    }

    public enum SandstoneWallNorthType : uint
    {
        True = 0,
        False = 1,
    }

    public enum SandstoneWallSouthType : uint
    {
        True = 0,
        False = 1,
    }

    public enum SandstoneWallUpType : uint
    {
        True = 0,
        False = 1,
    }

    public enum SandstoneWallWaterloggedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum SandstoneWallWestType : uint
    {
        True = 0,
        False = 1,
    }

    public enum EndStoneBrickWallEastType : uint
    {
        True = 0,
        False = 1,
    }

    public enum EndStoneBrickWallNorthType : uint
    {
        True = 0,
        False = 1,
    }

    public enum EndStoneBrickWallSouthType : uint
    {
        True = 0,
        False = 1,
    }

    public enum EndStoneBrickWallUpType : uint
    {
        True = 0,
        False = 1,
    }

    public enum EndStoneBrickWallWaterloggedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum EndStoneBrickWallWestType : uint
    {
        True = 0,
        False = 1,
    }

    public enum DioriteWallEastType : uint
    {
        True = 0,
        False = 1,
    }

    public enum DioriteWallNorthType : uint
    {
        True = 0,
        False = 1,
    }

    public enum DioriteWallSouthType : uint
    {
        True = 0,
        False = 1,
    }

    public enum DioriteWallUpType : uint
    {
        True = 0,
        False = 1,
    }

    public enum DioriteWallWaterloggedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum DioriteWallWestType : uint
    {
        True = 0,
        False = 1,
    }

    public enum ScaffoldingBottomType : uint
    {
        True = 0,
        False = 1,
    }

    public enum ScaffoldingDistanceType : uint
    {
        Distance0 = 0,
        Distance1 = 1,
        Distance2 = 2,
        Distance3 = 3,
        Distance4 = 4,
        Distance5 = 5,
        Distance6 = 6,
        Distance7 = 7,
    }

    public enum ScaffoldingWaterloggedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum LoomFacingType : uint
    {
        North = 0,
        South = 1,
        West = 2,
        East = 3,
    }

    public enum BarrelFacingType : uint
    {
        North = 0,
        East = 1,
        South = 2,
        West = 3,
        Up = 4,
        Down = 5,
    }

    public enum BarrelOpenType : uint
    {
        True = 0,
        False = 1,
    }

    public enum SmokerFacingType : uint
    {
        North = 0,
        South = 1,
        West = 2,
        East = 3,
    }

    public enum SmokerLitType : uint
    {
        True = 0,
        False = 1,
    }

    public enum BlastFurnaceFacingType : uint
    {
        North = 0,
        South = 1,
        West = 2,
        East = 3,
    }

    public enum BlastFurnaceLitType : uint
    {
        True = 0,
        False = 1,
    }

    public enum GrindstoneFaceType : uint
    {
        Floor = 0,
        Wall = 1,
        Ceiling = 2,
    }

    public enum GrindstoneFacingType : uint
    {
        North = 0,
        South = 1,
        West = 2,
        East = 3,
    }

    public enum LecternFacingType : uint
    {
        North = 0,
        South = 1,
        West = 2,
        East = 3,
    }

    public enum LecternHasBookType : uint
    {
        True = 0,
        False = 1,
    }

    public enum LecternPoweredType : uint
    {
        True = 0,
        False = 1,
    }

    public enum StonecutterFacingType : uint
    {
        North = 0,
        South = 1,
        West = 2,
        East = 3,
    }

    public enum BellAttachmentType : uint
    {
        Floor = 0,
        Ceiling = 1,
        SingleWall = 2,
        DoubleWall = 3,
    }

    public enum BellFacingType : uint
    {
        North = 0,
        South = 1,
        West = 2,
        East = 3,
    }

    public enum BellPoweredType : uint
    {
        True = 0,
        False = 1,
    }

    public enum LanternHangingType : uint
    {
        True = 0,
        False = 1,
    }

    public enum CampfireFacingType : uint
    {
        North = 0,
        South = 1,
        West = 2,
        East = 3,
    }

    public enum CampfireLitType : uint
    {
        True = 0,
        False = 1,
    }

    public enum CampfireSignalFireType : uint
    {
        True = 0,
        False = 1,
    }

    public enum CampfireWaterloggedType : uint
    {
        True = 0,
        False = 1,
    }

    public enum SweetBerryBushAgeType : uint
    {
        Age0 = 0,
        Age1 = 1,
        Age2 = 2,
        Age3 = 3,
    }

    public enum StructureBlockModeType : uint
    {
        Save = 0,
        Load = 1,
        Corner = 2,
        Data = 3,
    }

    public enum JigsawFacingType : uint
    {
        North = 0,
        East = 1,
        South = 2,
        West = 3,
        Up = 4,
        Down = 5,
    }

    public enum ComposterLevelType : uint
    {
        Level0 = 0,
        Level1 = 1,
        Level2 = 2,
        Level3 = 3,
        Level4 = 4,
        Level5 = 5,
        Level6 = 6,
        Level7 = 7,
        Level8 = 8,
    }

    public enum BeeNestFacingType : uint
    {
        North = 0,
        South = 1,
        West = 2,
        East = 3,
    }

    public enum BeeNestHoneyLevelType : uint
    {
        Level0 = 0,
        Level1 = 1,
        Level2 = 2,
        Level3 = 3,
        Level4 = 4,
        Level5 = 5,
    }

    public enum BeehiveFacingType : uint
    {
        North = 0,
        South = 1,
        West = 2,
        East = 3,
    }

    public enum BeehiveHoneyLevelType : uint
    {
        Level0 = 0,
        Level1 = 1,
        Level2 = 2,
        Level3 = 3,
        Level4 = 4,
        Level5 = 5,
    }
}
