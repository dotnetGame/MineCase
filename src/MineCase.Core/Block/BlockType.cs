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
        Dirt = 9,
        CoarseDirt = 10,
        Podzol = 11,
        Cobblestone = 12,
        OakPlanks = 13,
        SprucePlanks = 14,
        BirchPlanks = 15,
        JunglePlanks = 16,
        AcaciaPlanks = 17,
        DarkOakPlanks = 18,
        OakSapling = 19,
        SpruceSapling = 20,
        BirchSapling = 21,
        JungleSapling = 22,
        AcaciaSapling = 23,
        DarkOakSapling = 24,
        Bedrock = 25,
        Water = 26,
        Lava = 27,
        Sand = 28,
        RedSand = 29,
        Gravel = 30,
        GoldOre = 31,
        IronOre = 32,
        CoalOre = 33,
        OakLog = 34,
        SpruceLog = 35,
        BirchLog = 36,
        JungleLog = 37,
        AcaciaLog = 38,
        DarkOakLog = 39,
        StrippedSpruceLog = 40,
        StrippedBirchLog = 41,
        StrippedJungleLog = 42,
        StrippedAcaciaLog = 43,
        StrippedDarkOakLog = 44,
        StrippedOakLog = 45,
        OakWood = 46,
        SpruceWood = 47,
        BirchWood = 48,
        JungleWood = 49,
        AcaciaWood = 50,
        DarkOakWood = 51,
        StrippedOakWood = 52,
        StrippedSpruceWood = 53,
        StrippedBirchWood = 54,
        StrippedJungleWood = 55,
        StrippedAcaciaWood = 56,
        StrippedDarkOakWood = 57,
        OakLeaves = 58,
        SpruceLeaves = 59,
        BirchLeaves = 60,
        JungleLeaves = 61,
        AcaciaLeaves = 62,
        DarkOakLeaves = 63,
        Sponge = 64,
        WetSponge = 65,
        Glass = 66,
        LapisOre = 67,
        LapisBlock = 68,
        Dispenser = 69,
        Sandstone = 70,
        ChiseledSandstone = 71,
        CutSandstone = 72,
        NoteBlock = 73,
        WhiteBed = 74,
        OrangeBed = 75,
        MagentaBed = 76,
        LightBlueBed = 77,
        YellowBed = 78,
        LimeBed = 79,
        PinkBed = 80,
        GrayBed = 81,
        LightGrayBed = 82,
        CyanBed = 83,
        PurpleBed = 84,
        BlueBed = 85,
        BrownBed = 86,
        GreenBed = 87,
        RedBed = 88,
        BlackBed = 89,
        PoweredRail = 90,
        DetectorRail = 91,
        StickyPiston = 92,
        Cobweb = 93,
        Grass = 94,
        Fern = 95,
        DeadBush = 96,
        Seagrass = 97,
        TallSeagrass = 98,
        Piston = 99,
        PistonHead = 100,
        WhiteWool = 101,
        OrangeWool = 102,
        MagentaWool = 103,
        LightBlueWool = 104,
        YellowWool = 105,
        LimeWool = 106,
        PinkWool = 107,
        GrayWool = 108,
        LightGrayWool = 109,
        CyanWool = 110,
        PurpleWool = 111,
        BlueWool = 112,
        BrownWool = 113,
        GreenWool = 114,
        RedWool = 115,
        BlackWool = 116,
        MovingPiston = 117,
        Dandelion = 118,
        Poppy = 119,
        BlueOrchid = 120,
        Allium = 121,
        AzureBluet = 122,
        RedTulip = 123,
        OrangeTulip = 124,
        WhiteTulip = 125,
        PinkTulip = 126,
        OxeyeDaisy = 127,
        Cornflower = 128,
        WitherRose = 129,
        LilyOfTheValley = 130,
        BrownMushroom = 131,
        RedMushroom = 132,
        GoldBlock = 133,
        IronBlock = 134,
        Bricks = 135,
        Tnt = 136,
        Bookshelf = 137,
        MossyCobblestone = 138,
        Obsidian = 139,
        Torch = 140,
        WallTorch = 141,
        Fire = 142,
        Spawner = 143,
        OakStairs = 144,
        Chest = 145,
        RedstoneWire = 146,
        DiamondOre = 147,
        DiamondBlock = 148,
        CraftingTable = 149,
        Wheat = 150,
        Farmland = 151,
        Furnace = 152,
        OakSign = 153,
        SpruceSign = 154,
        BirchSign = 155,
        AcaciaSign = 156,
        JungleSign = 157,
        DarkOakSign = 158,
        OakDoor = 159,
        Ladder = 160,
        Rail = 161,
        CobblestoneStairs = 162,
        OakWallSign = 163,
        SpruceWallSign = 164,
        BirchWallSign = 165,
        AcaciaWallSign = 166,
        JungleWallSign = 167,
        DarkOakWallSign = 168,
        Lever = 169,
        StonePressurePlate = 170,
        IronDoor = 171,
        OakPressurePlate = 172,
        SprucePressurePlate = 173,
        BirchPressurePlate = 174,
        JunglePressurePlate = 175,
        AcaciaPressurePlate = 176,
        DarkOakPressurePlate = 177,
        RedstoneOre = 178,
        RedstoneTorch = 179,
        RedstoneWallTorch = 180,
        StoneButton = 181,
        Snow = 182,
        Ice = 183,
        SnowBlock = 184,
        Cactus = 185,
        Clay = 186,
        SugarCane = 187,
        Jukebox = 188,
        OakFence = 189,
        Pumpkin = 190,
        Netherrack = 191,
        SoulSand = 192,
        Glowstone = 193,
        NetherPortal = 194,
        CarvedPumpkin = 195,
        JackOLantern = 196,
        Cake = 197,
        Repeater = 198,
        WhiteStainedGlass = 199,
        OrangeStainedGlass = 200,
        MagentaStainedGlass = 201,
        LightBlueStainedGlass = 202,
        YellowStainedGlass = 203,
        LimeStainedGlass = 204,
        PinkStainedGlass = 205,
        GrayStainedGlass = 206,
        LightGrayStainedGlass = 207,
        CyanStainedGlass = 208,
        PurpleStainedGlass = 209,
        BlueStainedGlass = 210,
        BrownStainedGlass = 211,
        GreenStainedGlass = 212,
        RedStainedGlass = 213,
        BlackStainedGlass = 214,
        OakTrapdoor = 215,
        SpruceTrapdoor = 216,
        BirchTrapdoor = 217,
        JungleTrapdoor = 218,
        AcaciaTrapdoor = 219,
        DarkOakTrapdoor = 220,
        StoneBricks = 221,
        MossyStoneBricks = 222,
        CrackedStoneBricks = 223,
        ChiseledStoneBricks = 224,
        InfestedStone = 225,
        InfestedCobblestone = 226,
        InfestedStoneBricks = 227,
        InfestedMossyStoneBricks = 228,
        InfestedCrackedStoneBricks = 229,
        InfestedChiseledStoneBricks = 230,
        BrownMushroomBlock = 231,
        RedMushroomBlock = 232,
        MushroomStem = 233,
        IronBars = 234,
        GlassPane = 235,
        Melon = 236,
        AttachedPumpkinStem = 237,
        AttachedMelonStem = 238,
        PumpkinStem = 239,
        MelonStem = 240,
        Vine = 241,
        OakFenceGate = 242,
        BrickStairs = 243,
        StoneBrickStairs = 244,
        Mycelium = 245,
        LilyPad = 246,
        NetherBricks = 247,
        NetherBrickFence = 248,
        NetherBrickStairs = 249,
        NetherWart = 250,
        EnchantingTable = 251,
        BrewingStand = 252,
        Cauldron = 253,
        EndPortal = 254,
        EndPortalFrame = 255,
        EndStone = 256,
        DragonEgg = 257,
        RedstoneLamp = 258,
        Cocoa = 259,
        SandstoneStairs = 260,
        EmeraldOre = 261,
        EnderChest = 262,
        TripwireHook = 263,
        Tripwire = 264,
        EmeraldBlock = 265,
        SpruceStairs = 266,
        BirchStairs = 267,
        JungleStairs = 268,
        CommandBlock = 269,
        Beacon = 270,
        CobblestoneWall = 271,
        MossyCobblestoneWall = 272,
        FlowerPot = 273,
        PottedOakSapling = 274,
        PottedSpruceSapling = 275,
        PottedBirchSapling = 276,
        PottedJungleSapling = 277,
        PottedAcaciaSapling = 278,
        PottedDarkOakSapling = 279,
        PottedFern = 280,
        PottedDandelion = 281,
        PottedPoppy = 282,
        PottedBlueOrchid = 283,
        PottedAllium = 284,
        PottedAzureBluet = 285,
        PottedRedTulip = 286,
        PottedOrangeTulip = 287,
        PottedWhiteTulip = 288,
        PottedPinkTulip = 289,
        PottedOxeyeDaisy = 290,
        PottedCornflower = 291,
        PottedLilyOfTheValley = 292,
        PottedWitherRose = 293,
        PottedRedMushroom = 294,
        PottedBrownMushroom = 295,
        PottedDeadBush = 296,
        PottedCactus = 297,
        Carrots = 298,
        Potatoes = 299,
        OakButton = 300,
        SpruceButton = 301,
        BirchButton = 302,
        JungleButton = 303,
        AcaciaButton = 304,
        DarkOakButton = 305,
        SkeletonSkull = 306,
        SkeletonWallSkull = 307,
        WitherSkeletonSkull = 308,
        WitherSkeletonWallSkull = 309,
        ZombieHead = 310,
        ZombieWallHead = 311,
        PlayerHead = 312,
        PlayerWallHead = 313,
        CreeperHead = 314,
        CreeperWallHead = 315,
        DragonHead = 316,
        DragonWallHead = 317,
        Anvil = 318,
        ChippedAnvil = 319,
        DamagedAnvil = 320,
        TrappedChest = 321,
        LightWeightedPressurePlate = 322,
        HeavyWeightedPressurePlate = 323,
        Comparator = 324,
        DaylightDetector = 325,
        RedstoneBlock = 326,
        NetherQuartzOre = 327,
        Hopper = 328,
        QuartzBlock = 329,
        ChiseledQuartzBlock = 330,
        QuartzPillar = 331,
        QuartzStairs = 332,
        ActivatorRail = 333,
        Dropper = 334,
        WhiteTerracotta = 335,
        OrangeTerracotta = 336,
        MagentaTerracotta = 337,
        LightBlueTerracotta = 338,
        YellowTerracotta = 339,
        LimeTerracotta = 340,
        PinkTerracotta = 341,
        GrayTerracotta = 342,
        LightGrayTerracotta = 343,
        CyanTerracotta = 344,
        PurpleTerracotta = 345,
        BlueTerracotta = 346,
        BrownTerracotta = 347,
        GreenTerracotta = 348,
        RedTerracotta = 349,
        BlackTerracotta = 350,
        WhiteStainedGlassPane = 351,
        OrangeStainedGlassPane = 352,
        MagentaStainedGlassPane = 353,
        LightBlueStainedGlassPane = 354,
        YellowStainedGlassPane = 355,
        LimeStainedGlassPane = 356,
        PinkStainedGlassPane = 357,
        GrayStainedGlassPane = 358,
        LightGrayStainedGlassPane = 359,
        CyanStainedGlassPane = 360,
        PurpleStainedGlassPane = 361,
        BlueStainedGlassPane = 362,
        BrownStainedGlassPane = 363,
        GreenStainedGlassPane = 364,
        RedStainedGlassPane = 365,
        BlackStainedGlassPane = 366,
        AcaciaStairs = 367,
        DarkOakStairs = 368,
        SlimeBlock = 369,
        Barrier = 370,
        IronTrapdoor = 371,
        Prismarine = 372,
        PrismarineBricks = 373,
        DarkPrismarine = 374,
        PrismarineStairs = 375,
        PrismarineBrickStairs = 376,
        DarkPrismarineStairs = 377,
        PrismarineSlab = 378,
        PrismarineBrickSlab = 379,
        DarkPrismarineSlab = 380,
        SeaLantern = 381,
        HayBlock = 382,
        WhiteCarpet = 383,
        OrangeCarpet = 384,
        MagentaCarpet = 385,
        LightBlueCarpet = 386,
        YellowCarpet = 387,
        LimeCarpet = 388,
        PinkCarpet = 389,
        GrayCarpet = 390,
        LightGrayCarpet = 391,
        CyanCarpet = 392,
        PurpleCarpet = 393,
        BlueCarpet = 394,
        BrownCarpet = 395,
        GreenCarpet = 396,
        RedCarpet = 397,
        BlackCarpet = 398,
        Terracotta = 399,
        CoalBlock = 400,
        PackedIce = 401,
        Sunflower = 402,
        Lilac = 403,
        RoseBush = 404,
        Peony = 405,
        TallGrass = 406,
        LargeFern = 407,
        WhiteBanner = 408,
        OrangeBanner = 409,
        MagentaBanner = 410,
        LightBlueBanner = 411,
        YellowBanner = 412,
        LimeBanner = 413,
        PinkBanner = 414,
        GrayBanner = 415,
        LightGrayBanner = 416,
        CyanBanner = 417,
        PurpleBanner = 418,
        BlueBanner = 419,
        BrownBanner = 420,
        GreenBanner = 421,
        RedBanner = 422,
        BlackBanner = 423,
        WhiteWallBanner = 424,
        OrangeWallBanner = 425,
        MagentaWallBanner = 426,
        LightBlueWallBanner = 427,
        YellowWallBanner = 428,
        LimeWallBanner = 429,
        PinkWallBanner = 430,
        GrayWallBanner = 431,
        LightGrayWallBanner = 432,
        CyanWallBanner = 433,
        PurpleWallBanner = 434,
        BlueWallBanner = 435,
        BrownWallBanner = 436,
        GreenWallBanner = 437,
        RedWallBanner = 438,
        BlackWallBanner = 439,
        RedSandstone = 440,
        ChiseledRedSandstone = 441,
        CutRedSandstone = 442,
        RedSandstoneStairs = 443,
        OakSlab = 444,
        SpruceSlab = 445,
        BirchSlab = 446,
        JungleSlab = 447,
        AcaciaSlab = 448,
        DarkOakSlab = 449,
        StoneSlab = 450,
        SmoothStoneSlab = 451,
        SandstoneSlab = 452,
        CutSandstoneSlab = 453,
        PetrifiedOakSlab = 454,
        CobblestoneSlab = 455,
        BrickSlab = 456,
        StoneBrickSlab = 457,
        NetherBrickSlab = 458,
        QuartzSlab = 459,
        RedSandstoneSlab = 460,
        CutRedSandstoneSlab = 461,
        PurpurSlab = 462,
        SmoothStone = 463,
        SmoothSandstone = 464,
        SmoothQuartz = 465,
        SmoothRedSandstone = 466,
        SpruceFenceGate = 467,
        BirchFenceGate = 468,
        JungleFenceGate = 469,
        AcaciaFenceGate = 470,
        DarkOakFenceGate = 471,
        SpruceFence = 472,
        BirchFence = 473,
        JungleFence = 474,
        AcaciaFence = 475,
        DarkOakFence = 476,
        SpruceDoor = 477,
        BirchDoor = 478,
        JungleDoor = 479,
        AcaciaDoor = 480,
        DarkOakDoor = 481,
        EndRod = 482,
        ChorusPlant = 483,
        ChorusFlower = 484,
        PurpurBlock = 485,
        PurpurPillar = 486,
        PurpurStairs = 487,
        EndStoneBricks = 488,
        Beetroots = 489,
        GrassPath = 490,
        EndGateway = 491,
        RepeatingCommandBlock = 492,
        ChainCommandBlock = 493,
        FrostedIce = 494,
        MagmaBlock = 495,
        NetherWartBlock = 496,
        RedNetherBricks = 497,
        BoneBlock = 498,
        StructureVoid = 499,
        Observer = 500,
        ShulkerBox = 501,
        WhiteShulkerBox = 502,
        OrangeShulkerBox = 503,
        MagentaShulkerBox = 504,
        LightBlueShulkerBox = 505,
        YellowShulkerBox = 506,
        LimeShulkerBox = 507,
        PinkShulkerBox = 508,
        GrayShulkerBox = 509,
        LightGrayShulkerBox = 510,
        CyanShulkerBox = 511,
        PurpleShulkerBox = 512,
        BlueShulkerBox = 513,
        BrownShulkerBox = 514,
        GreenShulkerBox = 515,
        RedShulkerBox = 516,
        BlackShulkerBox = 517,
        WhiteGlazedTerracotta = 518,
        OrangeGlazedTerracotta = 519,
        MagentaGlazedTerracotta = 520,
        LightBlueGlazedTerracotta = 521,
        YellowGlazedTerracotta = 522,
        LimeGlazedTerracotta = 523,
        PinkGlazedTerracotta = 524,
        GrayGlazedTerracotta = 525,
        LightGrayGlazedTerracotta = 526,
        CyanGlazedTerracotta = 527,
        PurpleGlazedTerracotta = 528,
        BlueGlazedTerracotta = 529,
        BrownGlazedTerracotta = 530,
        GreenGlazedTerracotta = 531,
        RedGlazedTerracotta = 532,
        BlackGlazedTerracotta = 533,
        WhiteConcrete = 534,
        OrangeConcrete = 535,
        MagentaConcrete = 536,
        LightBlueConcrete = 537,
        YellowConcrete = 538,
        LimeConcrete = 539,
        PinkConcrete = 540,
        GrayConcrete = 541,
        LightGrayConcrete = 542,
        CyanConcrete = 543,
        PurpleConcrete = 544,
        BlueConcrete = 545,
        BrownConcrete = 546,
        GreenConcrete = 547,
        RedConcrete = 548,
        BlackConcrete = 549,
        WhiteConcretePowder = 550,
        OrangeConcretePowder = 551,
        MagentaConcretePowder = 552,
        LightBlueConcretePowder = 553,
        YellowConcretePowder = 554,
        LimeConcretePowder = 555,
        PinkConcretePowder = 556,
        GrayConcretePowder = 557,
        LightGrayConcretePowder = 558,
        CyanConcretePowder = 559,
        PurpleConcretePowder = 560,
        BlueConcretePowder = 561,
        BrownConcretePowder = 562,
        GreenConcretePowder = 563,
        RedConcretePowder = 564,
        BlackConcretePowder = 565,
        Kelp = 566,
        KelpPlant = 567,
        DriedKelpBlock = 568,
        TurtleEgg = 569,
        DeadTubeCoralBlock = 570,
        DeadBrainCoralBlock = 571,
        DeadBubbleCoralBlock = 572,
        DeadFireCoralBlock = 573,
        DeadHornCoralBlock = 574,
        TubeCoralBlock = 575,
        BrainCoralBlock = 576,
        BubbleCoralBlock = 577,
        FireCoralBlock = 578,
        HornCoralBlock = 579,
        DeadTubeCoral = 580,
        DeadBrainCoral = 581,
        DeadBubbleCoral = 582,
        DeadFireCoral = 583,
        DeadHornCoral = 584,
        TubeCoral = 585,
        BrainCoral = 586,
        BubbleCoral = 587,
        FireCoral = 588,
        HornCoral = 589,
        DeadTubeCoralFan = 590,
        DeadBrainCoralFan = 591,
        DeadBubbleCoralFan = 592,
        DeadFireCoralFan = 593,
        DeadHornCoralFan = 594,
        TubeCoralFan = 595,
        BrainCoralFan = 596,
        BubbleCoralFan = 597,
        FireCoralFan = 598,
        HornCoralFan = 599,
        DeadTubeCoralWallFan = 600,
        DeadBrainCoralWallFan = 601,
        DeadBubbleCoralWallFan = 602,
        DeadFireCoralWallFan = 603,
        DeadHornCoralWallFan = 604,
        TubeCoralWallFan = 605,
        BrainCoralWallFan = 606,
        BubbleCoralWallFan = 607,
        FireCoralWallFan = 608,
        HornCoralWallFan = 609,
        SeaPickle = 610,
        BlueIce = 611,
        Conduit = 612,
        BambooSapling = 613,
        Bamboo = 614,
        PottedBamboo = 615,
        VoidAir = 616,
        CaveAir = 617,
        BubbleColumn = 618,
        PolishedGraniteStairs = 619,
        SmoothRedSandstoneStairs = 620,
        MossyStoneBrickStairs = 621,
        PolishedDioriteStairs = 622,
        MossyCobblestoneStairs = 623,
        EndStoneBrickStairs = 624,
        StoneStairs = 625,
        SmoothSandstoneStairs = 626,
        SmoothQuartzStairs = 627,
        GraniteStairs = 628,
        AndesiteStairs = 629,
        RedNetherBrickStairs = 630,
        PolishedAndesiteStairs = 631,
        DioriteStairs = 632,
        PolishedGraniteSlab = 633,
        SmoothRedSandstoneSlab = 634,
        MossyStoneBrickSlab = 635,
        PolishedDioriteSlab = 636,
        MossyCobblestoneSlab = 637,
        EndStoneBrickSlab = 638,
        SmoothSandstoneSlab = 639,
        SmoothQuartzSlab = 640,
        GraniteSlab = 641,
        AndesiteSlab = 642,
        RedNetherBrickSlab = 643,
        PolishedAndesiteSlab = 644,
        DioriteSlab = 645,
        BrickWall = 646,
        PrismarineWall = 647,
        RedSandstoneWall = 648,
        MossyStoneBrickWall = 649,
        GraniteWall = 650,
        StoneBrickWall = 651,
        NetherBrickWall = 652,
        AndesiteWall = 653,
        RedNetherBrickWall = 654,
        SandstoneWall = 655,
        EndStoneBrickWall = 656,
        DioriteWall = 657,
        Scaffolding = 658,
        Loom = 659,
        Barrel = 660,
        Smoker = 661,
        BlastFurnace = 662,
        CartographyTable = 663,
        FletchingTable = 664,
        Grindstone = 665,
        Lectern = 666,
        SmithingTable = 667,
        Stonecutter = 668,
        Bell = 669,
        Lantern = 670,
        Campfire = 671,
        SweetBerryBush = 672,
        StructureBlock = 673,
        Jigsaw = 674,
        Composter = 675,
        BeeNest = 676,
        Beehive = 677,
        HoneyBlock = 678,
        HoneycombBlock = 679,
    }

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
        Level1 = 0,
        Level2 = 1,
        Level3 = 2,
        Level4 = 3,
        Level5 = 4,
        Level6 = 5,
        Level7 = 6,
        Level8 = 7,

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
