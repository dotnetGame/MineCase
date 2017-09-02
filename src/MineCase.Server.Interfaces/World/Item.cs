﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase.Server.World
{
    public enum ItemId : uint
    {
        IronShovel = 256,
        IronPickaxe = 257,
        IronAxe = 258,
        FlintandSteel = 259,
        Apple = 260,
        Bow = 261,
        Arrow = 262,
        Coal = 263,
        Diamond = 264,
        IronIngot = 265,
        GoldIngot = 266,
        IronSword = 267,
        WoodenSword = 268,
        WoodenShovel = 269,
        WoodenPickaxe = 270,
        WoodenAxe = 271,
        StoneSword = 272,
        StoneShovel = 273,
        StonePickaxe = 274,
        StoneAxe = 275,
        DiamondSword = 276,
        DiamondShovel = 277,
        DiamondPickaxe = 278,
        DiamondAxe = 279,
        Stick = 280,
        Bowl = 281,
        MushroomStew = 282,
        GoldenSword = 283,
        GoldenShovel = 284,
        GoldenPickaxe = 285,
        GoldenAxe = 286,
        String = 287,
        Feather = 288,
        Gunpowder = 289,
        WoodenHoe = 290,
        StoneHoe = 291,
        IronHoe = 292,
        DiamondHoe = 293,
        GoldenHoe = 294,
        Seeds = 295,
        Wheat = 296,
        Bread = 297,
        LeatherCap = 298,
        LeatherTunic = 299,
        LeatherPants = 300,
        LeatherBoots = 301,
        ChainHelmet = 302,
        ChainChestplate = 303,
        ChainLeggings = 304,
        ChainBoots = 305,
        IronHelmet = 306,
        IronChestplate = 307,
        IronLeggings = 308,
        IronBoots = 309,
        DiamondHelmet = 310,
        DiamondChestplate = 311,
        DiamondLeggings = 312,
        DiamondBoots = 313,
        GoldenHelmet = 314,
        GoldenChestplate = 315,
        GoldenLeggings = 316,
        GoldenBoots = 317,
        Flint = 318,
        RawPorkchop = 319,
        CookedPorkchop = 320,
        Painting = 321,
        GoldenApple = 322,
        Sign = 323,
        OakDoor = 324,
        Bucket = 325,
        WaterBucket = 326,
        LavaBucket = 327,
        Minecart = 328,
        Saddle = 329,
        IronDoor = 330,
        Redstone = 331,
        Snowball = 332,
        Boat = 333,
        Leather = 334,
        Milk = 335,
        Brick = 336,
        Clay = 337,
        SugarCane = 338,
        Paper = 339,
        Book = 340,
        Slimeball = 341,
        MinecartwithChest = 342,
        MinecartwithFurnace = 343,
        Egg = 344,
        Compass = 345,
        FishingRod = 346,
        Clock = 347,
        GlowstoneDust = 348,
        RawFish = 349,
        CookedFish = 350,
        Dye = 351,
        Bone = 352,
        Sugar = 353,
        Cake = 354,
        Bed = 355,
        RedstoneRepeater = 356,
        Cookie = 357,
        Map = 358,
        Shears = 359,
        Melon = 360,
        PumpkinSeeds = 361,
        MelonSeeds = 362,
        RawBeef = 363,
        Steak = 364,
        RawChicken = 365,
        CookedChicken = 366,
        RottenFlesh = 367,
        EnderPearl = 368,
        BlazeRod = 369,
        GhastTear = 370,
        GoldNugget = 371,
        NetherWart = 372,
        Potion = 373,
        GlassBottle = 374,
        SpiderEye = 375,
        FermentedSpiderEye = 376,
        BlazePowder = 377,
        MagmaCream = 378,
        BrewingStand = 379,
        Cauldron = 380,
        EyeofEnder = 381,
        GlisteringMelon = 382,
        SpawnEgg = 383,
        BottleEnchanting = 384,
        FireCharge = 385,
        BookandQuill = 386,
        WrittenBook = 387,
        Emerald = 388,
        ItemFrame = 389,
        FlowerPot = 390,
        Carrot = 391,
        Potato = 392,
        BakedPotato = 393,
        PoisonousPotato = 394,
        EmptyMap = 395,
        GoldenCarrot = 396,
        Mobhead = 397,
        CarrotonaStick = 398,
        NetherStar = 399,
        PumpkinPie = 400,
        FireworkRocket = 401,
        FireworkStar = 402,
        EnchantedBook = 403,
        RedstoneComparator = 404,
        NetherBrick = 405,
        NetherQuartz = 406,
        MinecartwithTNT = 407,
        MinecartwithHopper = 408,
        PrismarineShard = 409,
        PrismarineCrystals = 410,
        RawRabbit = 411,
        CookedRabbit = 412,
        RabbitStew = 413,
        RabbitFoot = 414,
        RabbitHide = 415,
        ArmorStand = 416,
        IronHorseArmor = 417,
        GoldenHorseArmor = 418,
        DiamondHorseArmor = 419,
        Lead = 420,
        NameTag = 421,
        MinecartwithCommandBlock = 422,
        RawMutton = 423,
        CookedMutton = 424,
        Banner = 425,
        EndCrystal = 426,
        SpruceDoor = 427,
        BirchDoor = 428,
        JungleDoor = 429,
        AcaciaDoor = 430,
        DarkOakDoor = 431,
        ChorusFruit = 432,
        PoppedChorusFruit = 433,
        Beetroot = 434,
        BeetrootSeeds = 435,
        BeetrootSoup = 436,
        DragonBreath = 437,
        SplashPotion = 438,
        SpectralArrow = 439,
        TippedArrow = 440,
        LingeringPotion = 441,
        Shield = 442,
        Elytra = 443,
        SpruceBoat = 444,
        BirchBoat = 445,
        JungleBoat = 446,
        AcaciaBoat = 447,
        DarkOakBoat = 448,
        TotemofUndying = 449,
        ShulkerShell = 450,
        IronNugget = 452,
        Disc13 = 2256,
        CatDisc = 2257,
        BlocksDisc = 2258,
        ChirpDisc = 2259,
        FarDisc = 2260,
        MallDisc = 2261,
        MellohiDisc = 2262,
        StalDisc = 2263,
        StradDisc = 2264,
        WardDisc = 2265,
        Disc11 = 2266,
        WaitDisc = 2267
    }

    public enum CoalType : uint
    {
        Coal = 0,
        Charcoal = 1
    }

    public enum GoldenAppleType : uint
    {
        GoldenApple = 0,
        EnchantedGoldenApple = 1
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

    public enum HeadForItemType : uint
    {
        SkeletonSkull = 0,
        WitherSkeletonSkull = 1,
        ZombieHead = 2,
        Head = 3,
        CreeperHead = 4,
        DragonHead = 5
    }

    public enum PotionNameType : uint
    {
        WaterBottleOrMundanePotion = 0,
        PotionOfRegeneration1 = 1,
        PotionOfSwiftness1 = 2,
        PotionOfFireResistance1 = 3,
        PotionOfPoison1 = 4,
        PotionOfHealing1 = 5,
        PotionOfNightVision1 = 6,
        ClearPotion = 7,
        PotionOfWeakness1 = 8,
        PotionOfStrength1 = 9,
        PotionOfSlowness1 = 10,
        PotionOfLeaping1 = 11,
        PotionOfHarming1 = 12,
        PotionOfWaterBreathing1 = 13,
        PotionOfInvisibility1 = 14,
        ThinPotion = 15,
        AwkwardPotion = 16,
        PotionOfRegeneration2 = 17,
        PotionOfSwiftness2 = 18,
        PotionOfFireResistance2 = 19,
        PotionOfPoison2 = 20,
        PotionOfHealing2 = 21,
        PotionOfNightVision2 = 22,
        BunglingPotion = 23,
        PotionOfWeakness2 = 24,
        PotionOfStrength2 = 25,
        PotionOfSlowness2 = 26,
        PotionOfLeaping2 = 27,
        PotionOfHarming2 = 28,
        PotionOfWaterBreathing2 = 29,
        PotionOfInvisibility2 = 30,
        DebonairPotion = 31,
        ThickPotion = 32,
        PotionOfRegeneration3 = 33,
        PotionOfSwiftness3 = 34,
        PotionOfFireResistance3 = 35,
        PotionOfPoison3 = 36,
        PotionOfHealing3 = 37,
        PotionOfNightVision3 = 38,
        CharmingPotion = 39,
        PotionOfWeakness3 = 40,
        PotionOfStrength3 = 41,
        PotionOfSlowness3 = 42,
        PotionOfLeaping3 = 43,
        PotionOfHarming3 = 44,
        PotionOfWaterBreathing3 = 45,
        PotionOfInvisibility3 = 46,
        SparklingPotion = 47,
        PotentPotion = 48,
        PotionOfRegeneration4 = 49,
        PotionOfSwiftness4 = 50,
        PotionOfFireResistance4 = 51,
        PotionOfPoison4 = 52,
        PotionOfHealing4 = 53,
        PotionOfNightVision4 = 54,
        RankPotion = 55,
        PotionOfWeakness4 = 56,
        PotionOfStrength4 = 57,
        PotionOfSlowness4 = 58,
        PotionOfLeaping4 = 59,
        PotionOfHarming4 = 60,
        PotionOfWaterBreathing4 = 61,
        PotionOfInvisibility4 = 62,
        StinkyPotion = 63
    }

    public enum PotionEffectType : uint
    {
        None = 0,
        Regeneration = 1,
        Speed = 2,
        FireResistance = 3,
        Poison = 4,
        InstantHealth = 5,
        NightVision = 6,
        Weakness = 8,
        Strength = 9,
        Slowness = 10,
        JumpBoost = 11,
        InstantDamage = 12,
        WaterBreathing = 13,
        Invisibility = 14
    }

    public enum PotionTierType : uint
    {
        Base = 0,
        LessDuration = 32
    }

    public enum PotionExtendedDurationType : uint
    {
        Base = 0,
        MoreDuration = 64
    }

    public enum SplashPotionType : uint
    {
        DrinkablePotion = 0,
        SplashPotion = 16384
    }

    public struct ItemState : IEquatable<ItemState>
    {
        public uint Id { get; set; }

        public uint MetaValue { get; set; }

        public override bool Equals(object obj)
        {
            return obj is ItemState && Equals((ItemState)obj);
        }

        public bool Equals(ItemState other)
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

        public static bool operator ==(ItemState state1, ItemState state2)
        {
            return state1.Equals(state2);
        }

        public static bool operator !=(ItemState state1, ItemState state2)
        {
            return !(state1 == state2);
        }
    }
}
