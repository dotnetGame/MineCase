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

    // New enums
    public enum GrassBlockSnowy : uint
    {
        True = 0,
        False = 1,
    }

    public enum OakLogAxis : uint
    {
        X = 0,
        Y = 1,
        Z = 2,
    }

    // Old enums
    public enum WoodPlankType : uint
    {
        Oak = 0,
        Spruce = 1,
        Birch = 2,
        Jungle = 3,
        Acacia = 4,
        DarkOak = 5
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
        Oak = 0,
        Spruce = 1,
        Birch = 2,
        Jungle = 3,
        Acacia = 4,
        DarkOak = 5,

        // 0x8 bit field : Set if sapling is ready to grow into a tree
        ReadyForTreeFlag = 0x8
    }

    /// <summary>
    /// Attributes of water and lava.
    ///
    /// If FallingFlag is set, the lower bits are essentially ignored,
    /// since this block is then at its highest fluid level.
    /// Level1 is the highest fluid level(not necessarily filling the block -
    /// this depends on the neighboring fluid blocks above each upper corner of the block).
    /// </summary>
    [Flags]
    public enum FluidType : uint
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

    public enum SandType : uint
    {
        Sand = 0,
        RedSand = 1
    }

    [Flags]
    public enum WoodType : uint
    {
        Oak = 0,
        Spruce = 1,
        Birch = 2,
        Jungle = 3,

        // 0x4 ~ 0x8 bits field specifying the orientation of the wood
        FacingUpFlag = 0x0,
        FacingEastFlag = 0x4,
        FacingNorthFlag = 0x8,
        OnlybarkFlag = 0xC
    }

    [Flags]
    public enum Wood2Type : uint
    {
        Acacia = 0,
        DarkOak = 1,

        // 0x4 ~ 0x8 bits field specifying the orientation of the wood
        FacingUpFlag = 0x0,
        FacingEastFlag = 0x4,
        FacingNorthFlag = 0x8,
        OnlybarkFlag = 0xC
    }

    public enum LeaveType : uint
    {
        Oak = 0,
        Spruce = 1,
        Birch = 2,
        Jungle = 3,
        OakLeaves = 4,
        SpruceNoDecay = 5,
        BirchNoDecay = 6,
        JungleNoDecay = 7,
        OakCheckDecay = 8,
        SpruceCheckDecay = 9,
        BirchCheckDecay = 10,
        JungleCheckDecay = 11,
        OakNoDecayAndCheckDecay = 12,
        SpruceNoDecayAndCheckDecay = 13,
        BirchNoDecayAndCheckDecay = 14,
        JungleNoDecayAndCheckDecay = 15
    }

    public enum Leave2Type : uint
    {
        Acacia = 0,
        DarkOak = 1,
        AcaciaNoDecay = 4,
        DarkOakNoDecay = 5,
        AcaciaCheckDecay = 8,
        DarkOakCheckDecay = 9,
        AcaciaNoDecayAndCheckDecay = 12,
        DarkOakNoDecayAndCheckDecay = 13
    }

    /// <summary>
    /// Specifies the color of the wool, stained terracotta, stained glass and carpet.
    /// </summary>
    public enum BlockColorType : uint
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
        Stone = 0,
        Sandstone = 1,
        Wooden = 2,
        Cobblestone = 3,
        Bricks = 4,
        StoneBrick = 5,
        NetherBrick = 6,
        Quartz = 7,
        SmoothStone = 8,
        SmoothSandstone = 9,
        TileQuartz = 15
    }

    public enum DoubleRedSandstoneSlabType : uint
    {
        Normal = 0,
        Smooth = 8
    }

    public enum StoneSlabType : uint
    {
        Stone = 0,
        Sandstone = 1,
        Wooden = 2,
        Cobblestone = 3,
        Bricks = 4,
        StoneBrick = 5,
        NetherBrick = 6,
        Quartz = 7,
        UpperStone = 8,
        UpperSandstone = 9,
        UpperWooden = 10,
        UpperCobblestone = 11,
        UpperBricks = 12,
        UpperStoneBrick = 13,
        UpperNetherBrick = 14,
        UpperQuartz = 15
    }

    public enum RedSandstoneSlabType : uint
    {
        Normal = 0,
        Upper = 1
    }

    public enum DoubleWoodenSlabType : uint
    {
        Oak = 0,
        Spruce = 1,
        Birch = 2,
        Jungle = 3,
        Acacia = 4,
        DarkOak = 5
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
        Normal = 0,
        Chiseled = 1,
        Smooth = 2
    }

    public enum RedSandstoneType : uint
    {
        Normal = 0,
        Chiseled = 1,
        Smooth = 2
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
    /// For Activator Rails, Detector Rails, and Powered Rails.
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
    /// For Ladders, Furnaces, Chests, Trapped Chests.
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
    /// 0x1 ~ 0x4 bits specifying which direction the dispenser is facing.
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
    /// 0x1 ~ 0x4 bits specifying which direction the dropper is facing.
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
        FreshlyPlanted = 0,
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
        FreshlyPlanted = 0,
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
        Tick1Flag = 0x0,
        Tick2Flag = 0x4,
        Tick3Flag = 0x8,
        Tick4Flag = 0xC
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
        Normal = 0,
        Mossy = 1,
        Cracked = 2,
        Chiseled = 3
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
        OnWallFacingNorth = 2,
        OnWallFacingSouth = 3,
        OnWallFacingEast = 4,
        OnWallFacingWest = 5
    }

    public enum BlockOfQuartzType : uint
    {
        Normal = 0,
        Chiseled = 1,
        Vertical = 2,
        NorthSouth = 3,
        EastWest = 4
    }

    public enum AnvilForBlockType : uint
    {
        NorthSouth = 0,
        EastWest = 1,
        SouthNorth = 2,
        WestEast = 3,
        SlightlyDamagedNS = 4,
        SlightlyDamagedEW = 5,
        SlightlyDamagedWE = 6,
        SlightlyDamagedSN = 7,
        VeryDamagedNS = 8,
        VeryDamagedEW = 9,
        VeryDamagedWE = 10,
        VeryDamagedSN = 11
    }
}
