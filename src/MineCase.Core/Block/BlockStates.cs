using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase.Block
{
    public static class BlockStates
    {
        public static BlockState Air()
        {
            return new BlockState
            {
                Id = (uint)BlockId.Air,
                MetaValue = 0,
            };
        }

        public static BlockState Stone()
        {
            return new BlockState
            {
                Id = (uint)BlockId.Stone,
                MetaValue = 0,
            };
        }

        public static BlockState Granite()
        {
            return new BlockState
            {
                Id = (uint)BlockId.Granite,
                MetaValue = 0,
            };
        }

        public static BlockState PolishedGranite()
        {
            return new BlockState
            {
                Id = (uint)BlockId.PolishedGranite,
                MetaValue = 0,
            };
        }

        public static BlockState Diorite()
        {
            return new BlockState
            {
                Id = (uint)BlockId.Diorite,
                MetaValue = 0,
            };
        }

        public static BlockState PolishedDiorite()
        {
            return new BlockState
            {
                Id = (uint)BlockId.PolishedDiorite,
                MetaValue = 0,
            };
        }

        public static BlockState Andesite()
        {
            return new BlockState
            {
                Id = (uint)BlockId.Andesite,
                MetaValue = 0,
            };
        }

        public static BlockState PolishedAndesite()
        {
            return new BlockState
            {
                Id = (uint)BlockId.PolishedAndesite,
                MetaValue = 0,
            };
        }

        public static BlockState GrassBlock(GrassBlockSnowyType snowy = GrassBlockSnowyType.False)
        {
            return new BlockState
            {
                Id = (uint)BlockId.GrassBlock,
                MetaValue = (uint)snowy,
            };
        }

        public static BlockState Dirt()
        {
            return new BlockState
            {
                Id = (uint)BlockId.Dirt,
                MetaValue = 0,
            };
        }

        public static BlockState CoarseDirt()
        {
            return new BlockState
            {
                Id = (uint)BlockId.CoarseDirt,
                MetaValue = 0,
            };
        }

        public static BlockState Podzol(PodzolSnowyType snowy)
        {
            return new BlockState
            {
                Id = (uint)BlockId.Podzol,
                MetaValue = (uint)snowy,
            };
        }

        public static BlockState Cobblestone()
        {
            return new BlockState
            {
                Id = (uint)BlockId.Cobblestone,
                MetaValue = 0,
            };
        }

        public static BlockState OakPlanks()
        {
            return new BlockState
            {
                Id = (uint)BlockId.OakPlanks,
                MetaValue = 0,
            };
        }

        public static BlockState SprucePlanks()
        {
            return new BlockState
            {
                Id = (uint)BlockId.SprucePlanks,
                MetaValue = 0,
            };
        }

        public static BlockState BirchPlanks()
        {
            return new BlockState
            {
                Id = (uint)BlockId.BirchPlanks,
                MetaValue = 0,
            };
        }

        public static BlockState JunglePlanks()
        {
            return new BlockState
            {
                Id = (uint)BlockId.JunglePlanks,
                MetaValue = 0,
            };
        }

        public static BlockState AcaciaPlanks()
        {
            return new BlockState
            {
                Id = (uint)BlockId.AcaciaPlanks,
                MetaValue = 0,
            };
        }

        public static BlockState DarkOakPlanks()
        {
            return new BlockState
            {
                Id = (uint)BlockId.DarkOakPlanks,
                MetaValue = 0,
            };
        }

        public static BlockState OakSapling(OakSaplingStageType stage = OakSaplingStageType.Stage0)
        {
            return new BlockState
            {
                Id = (uint)BlockId.OakSapling,
                MetaValue = (uint)stage,
            };
        }

        public static BlockState SpruceSapling(SpruceSaplingStageType stage = SpruceSaplingStageType.Stage0)
        {
            return new BlockState
            {
                Id = (uint)BlockId.SpruceSapling,
                MetaValue = (uint)stage,
            };
        }

        public static BlockState BirchSapling(BirchSaplingStageType stage)
        {
            return new BlockState
            {
                Id = (uint)BlockId.BirchSapling,
                MetaValue = (uint)stage,
            };
        }

        public static BlockState JungleSapling(JungleSaplingStageType stage)
        {
            return new BlockState
            {
                Id = (uint)BlockId.JungleSapling,
                MetaValue = (uint)stage,
            };
        }

        public static BlockState AcaciaSapling(AcaciaSaplingStageType stage)
        {
            return new BlockState
            {
                Id = (uint)BlockId.AcaciaSapling,
                MetaValue = (uint)stage,
            };
        }

        public static BlockState DarkOakSapling(DarkOakSaplingStageType stage)
        {
            return new BlockState
            {
                Id = (uint)BlockId.DarkOakSapling,
                MetaValue = (uint)stage,
            };
        }

        public static BlockState Bedrock()
        {
            return new BlockState
            {
                Id = (uint)BlockId.Bedrock,
                MetaValue = 0,
            };
        }

        public static BlockState Water(WaterLevelType level = WaterLevelType.Level0)
        {
            return new BlockState
            {
                Id = (uint)BlockId.Water,
                MetaValue = (uint)level,
            };
        }

        public static BlockState Lava(LavaLevelType level = LavaLevelType.Level0)
        {
            return new BlockState
            {
                Id = (uint)BlockId.Lava,
                MetaValue = (uint)level,
            };
        }

        public static BlockState Sand()
        {
            return new BlockState
            {
                Id = (uint)BlockId.Sand,
                MetaValue = 0,
            };
        }

        public static BlockState RedSand()
        {
            return new BlockState
            {
                Id = (uint)BlockId.RedSand,
                MetaValue = 0,
            };
        }

        public static BlockState Gravel()
        {
            return new BlockState
            {
                Id = (uint)BlockId.Gravel,
                MetaValue = 0,
            };
        }

        public static BlockState GoldOre()
        {
            return new BlockState
            {
                Id = (uint)BlockId.GoldOre,
                MetaValue = 0,
            };
        }

        public static BlockState IronOre()
        {
            return new BlockState
            {
                Id = (uint)BlockId.IronOre,
                MetaValue = 0,
            };
        }

        public static BlockState CoalOre()
        {
            return new BlockState
            {
                Id = (uint)BlockId.CoalOre,
                MetaValue = 0,
            };
        }

        public static BlockState OakLog(OakLogAxisType axis = OakLogAxisType.Y)
        {
            return new BlockState
            {
                Id = (uint)BlockId.OakLog,
                MetaValue = (uint)axis,
            };
        }

        public static BlockState SpruceLog(SpruceLogAxisType axis = SpruceLogAxisType.Y)
        {
            return new BlockState
            {
                Id = (uint)BlockId.SpruceLog,
                MetaValue = (uint)axis,
            };
        }

        public static BlockState BirchLog(BirchLogAxisType axis = BirchLogAxisType.Y)
        {
            return new BlockState
            {
                Id = (uint)BlockId.BirchLog,
                MetaValue = (uint)axis,
            };
        }

        public static BlockState JungleLog(JungleLogAxisType axis = JungleLogAxisType.Y)
        {
            return new BlockState
            {
                Id = (uint)BlockId.JungleLog,
                MetaValue = (uint)axis,
            };
        }

        public static BlockState AcaciaLog(AcaciaLogAxisType axis = AcaciaLogAxisType.Y)
        {
            return new BlockState
            {
                Id = (uint)BlockId.AcaciaLog,
                MetaValue = (uint)axis,
            };
        }

        public static BlockState DarkOakLog(DarkOakLogAxisType axis)
        {
            return new BlockState
            {
                Id = (uint)BlockId.DarkOakLog,
                MetaValue = (uint)axis,
            };
        }

        public static BlockState StrippedSpruceLog(StrippedSpruceLogAxisType axis)
        {
            return new BlockState
            {
                Id = (uint)BlockId.StrippedSpruceLog,
                MetaValue = (uint)axis,
            };
        }

        public static BlockState StrippedBirchLog(StrippedBirchLogAxisType axis)
        {
            return new BlockState
            {
                Id = (uint)BlockId.StrippedBirchLog,
                MetaValue = (uint)axis,
            };
        }

        public static BlockState StrippedJungleLog(StrippedJungleLogAxisType axis)
        {
            return new BlockState
            {
                Id = (uint)BlockId.StrippedJungleLog,
                MetaValue = (uint)axis,
            };
        }

        public static BlockState StrippedAcaciaLog(StrippedAcaciaLogAxisType axis)
        {
            return new BlockState
            {
                Id = (uint)BlockId.StrippedAcaciaLog,
                MetaValue = (uint)axis,
            };
        }

        public static BlockState StrippedDarkOakLog(StrippedDarkOakLogAxisType axis)
        {
            return new BlockState
            {
                Id = (uint)BlockId.StrippedDarkOakLog,
                MetaValue = (uint)axis,
            };
        }

        public static BlockState StrippedOakLog(StrippedOakLogAxisType axis)
        {
            return new BlockState
            {
                Id = (uint)BlockId.StrippedOakLog,
                MetaValue = (uint)axis,
            };
        }

        public static BlockState OakWood(OakWoodAxisType axis)
        {
            return new BlockState
            {
                Id = (uint)BlockId.OakWood,
                MetaValue = (uint)axis,
            };
        }

        public static BlockState SpruceWood(SpruceWoodAxisType axis)
        {
            return new BlockState
            {
                Id = (uint)BlockId.SpruceWood,
                MetaValue = (uint)axis,
            };
        }

        public static BlockState BirchWood(BirchWoodAxisType axis)
        {
            return new BlockState
            {
                Id = (uint)BlockId.BirchWood,
                MetaValue = (uint)axis,
            };
        }

        public static BlockState JungleWood(JungleWoodAxisType axis)
        {
            return new BlockState
            {
                Id = (uint)BlockId.JungleWood,
                MetaValue = (uint)axis,
            };
        }

        public static BlockState AcaciaWood(AcaciaWoodAxisType axis)
        {
            return new BlockState
            {
                Id = (uint)BlockId.AcaciaWood,
                MetaValue = (uint)axis,
            };
        }

        public static BlockState DarkOakWood(DarkOakWoodAxisType axis)
        {
            return new BlockState
            {
                Id = (uint)BlockId.DarkOakWood,
                MetaValue = (uint)axis,
            };
        }

        public static BlockState StrippedOakWood(StrippedOakWoodAxisType axis)
        {
            return new BlockState
            {
                Id = (uint)BlockId.StrippedOakWood,
                MetaValue = (uint)axis,
            };
        }

        public static BlockState StrippedSpruceWood(StrippedSpruceWoodAxisType axis)
        {
            return new BlockState
            {
                Id = (uint)BlockId.StrippedSpruceWood,
                MetaValue = (uint)axis,
            };
        }

        public static BlockState StrippedBirchWood(StrippedBirchWoodAxisType axis)
        {
            return new BlockState
            {
                Id = (uint)BlockId.StrippedBirchWood,
                MetaValue = (uint)axis,
            };
        }

        public static BlockState StrippedJungleWood(StrippedJungleWoodAxisType axis)
        {
            return new BlockState
            {
                Id = (uint)BlockId.StrippedJungleWood,
                MetaValue = (uint)axis,
            };
        }

        public static BlockState StrippedAcaciaWood(StrippedAcaciaWoodAxisType axis)
        {
            return new BlockState
            {
                Id = (uint)BlockId.StrippedAcaciaWood,
                MetaValue = (uint)axis,
            };
        }

        public static BlockState StrippedDarkOakWood(StrippedDarkOakWoodAxisType axis)
        {
            return new BlockState
            {
                Id = (uint)BlockId.StrippedDarkOakWood,
                MetaValue = (uint)axis,
            };
        }

        public static BlockState OakLeaves(OakLeavesDistanceType distance, OakLeavesPersistentType persistent)
        {
            return new BlockState
            {
                Id = (uint)BlockId.OakLeaves,
                MetaValue = ((uint)distance) * 2 + (uint)persistent,
            };
        }

        public static BlockState SpruceLeaves(SpruceLeavesDistanceType distance, SpruceLeavesPersistentType persistent)
        {
            return new BlockState
            {
                Id = (uint)BlockId.SpruceLeaves,
                MetaValue = ((uint)distance) * 2 + (uint)persistent,
            };
        }

        public static BlockState BirchLeaves(BirchLeavesDistanceType distance, BirchLeavesPersistentType persistent)
        {
            return new BlockState
            {
                Id = (uint)BlockId.BirchLeaves,
                MetaValue = ((uint)distance) * 2 + (uint)persistent,
            };
        }

        public static BlockState JungleLeaves(JungleLeavesDistanceType distance, JungleLeavesPersistentType persistent)
        {
            return new BlockState
            {
                Id = (uint)BlockId.JungleLeaves,
                MetaValue = ((uint)distance) * 2 + (uint)persistent,
            };
        }

        public static BlockState AcaciaLeaves(AcaciaLeavesDistanceType distance, AcaciaLeavesPersistentType persistent)
        {
            return new BlockState
            {
                Id = (uint)BlockId.AcaciaLeaves,
                MetaValue = ((uint)distance) * 2 + (uint)persistent,
            };
        }

        public static BlockState DarkOakLeaves(DarkOakLeavesDistanceType distance, DarkOakLeavesPersistentType persistent)
        {
            return new BlockState
            {
                Id = (uint)BlockId.DarkOakLeaves,
                MetaValue = ((uint)distance) * 2 + (uint)persistent,
            };
        }

        public static BlockState Sponge()
        {
            return new BlockState
            {
                Id = (uint)BlockId.Sponge,
                MetaValue = 0,
            };
        }

        public static BlockState WetSponge()
        {
            return new BlockState
            {
                Id = (uint)BlockId.WetSponge,
                MetaValue = 0,
            };
        }

        public static BlockState Glass()
        {
            return new BlockState
            {
                Id = (uint)BlockId.Glass,
                MetaValue = 0,
            };
        }

        public static BlockState LapisOre()
        {
            return new BlockState
            {
                Id = (uint)BlockId.LapisOre,
                MetaValue = 0,
            };
        }

        public static BlockState LapisBlock()
        {
            return new BlockState
            {
                Id = (uint)BlockId.LapisBlock,
                MetaValue = 0,
            };
        }

        public static BlockState Dispenser(DispenserFacingType facing, DispenserTriggeredType triggered)
        {
            return new BlockState
            {
                Id = (uint)BlockId.Dispenser,
                MetaValue = ((uint)facing) * 2 + (uint)triggered,
            };
        }

        public static BlockState Sandstone()
        {
            return new BlockState
            {
                Id = (uint)BlockId.Sandstone,
                MetaValue = 0,
            };
        }

        public static BlockState ChiseledSandstone()
        {
            return new BlockState
            {
                Id = (uint)BlockId.ChiseledSandstone,
                MetaValue = 0,
            };
        }

        public static BlockState CutSandstone()
        {
            return new BlockState
            {
                Id = (uint)BlockId.CutSandstone,
                MetaValue = 0,
            };
        }

        public static BlockState NoteBlock(NoteBlockInstrumentType instrument, NoteBlockNoteType note, NoteBlockPoweredType powered)
        {
            return new BlockState
            {
                Id = (uint)BlockId.NoteBlock,
                MetaValue = (((uint)instrument) * 25 + (uint)note) * 2 + (uint)powered,
            };
        }

        public static BlockState WhiteBed(WhiteBedFacingType facing, WhiteBedOccupiedType occupied, WhiteBedPartType part)
        {
            return new BlockState
            {
                Id = (uint)BlockId.WhiteBed,
                MetaValue = (((uint)facing) * 2 + (uint)occupied) * 2 + (uint)part,
            };
        }

        public static BlockState OrangeBed(OrangeBedFacingType facing, OrangeBedOccupiedType occupied, OrangeBedPartType part)
        {
            return new BlockState
            {
                Id = (uint)BlockId.OrangeBed,
                MetaValue = (((uint)facing) * 2 + (uint)occupied) * 2 + (uint)part,
            };
        }

        public static BlockState MagentaBed(MagentaBedFacingType facing, MagentaBedOccupiedType occupied, MagentaBedPartType part)
        {
            return new BlockState
            {
                Id = (uint)BlockId.MagentaBed,
                MetaValue = (((uint)facing) * 2 + (uint)occupied) * 2 + (uint)part,
            };
        }

        public static BlockState LightBlueBed(LightBlueBedFacingType facing, LightBlueBedOccupiedType occupied, LightBlueBedPartType part)
        {
            return new BlockState
            {
                Id = (uint)BlockId.LightBlueBed,
                MetaValue = (((uint)facing) * 2 + (uint)occupied) * 2 + (uint)part,
            };
        }

        public static BlockState YellowBed(YellowBedFacingType facing, YellowBedOccupiedType occupied, YellowBedPartType part)
        {
            return new BlockState
            {
                Id = (uint)BlockId.YellowBed,
                MetaValue = (((uint)facing) * 2 + (uint)occupied) * 2 + (uint)part,
            };
        }

        public static BlockState LimeBed(LimeBedFacingType facing, LimeBedOccupiedType occupied, LimeBedPartType part)
        {
            return new BlockState
            {
                Id = (uint)BlockId.LimeBed,
                MetaValue = (((uint)facing) * 2 + (uint)occupied) * 2 + (uint)part,
            };
        }

        public static BlockState PinkBed(PinkBedFacingType facing, PinkBedOccupiedType occupied, PinkBedPartType part)
        {
            return new BlockState
            {
                Id = (uint)BlockId.PinkBed,
                MetaValue = (((uint)facing) * 2 + (uint)occupied) * 2 + (uint)part,
            };
        }

        public static BlockState GrayBed(GrayBedFacingType facing, GrayBedOccupiedType occupied, GrayBedPartType part)
        {
            return new BlockState
            {
                Id = (uint)BlockId.GrayBed,
                MetaValue = (((uint)facing) * 2 + (uint)occupied) * 2 + (uint)part,
            };
        }

        public static BlockState LightGrayBed(LightGrayBedFacingType facing, LightGrayBedOccupiedType occupied, LightGrayBedPartType part)
        {
            return new BlockState
            {
                Id = (uint)BlockId.LightGrayBed,
                MetaValue = (((uint)facing) * 2 + (uint)occupied) * 2 + (uint)part,
            };
        }

        public static BlockState CyanBed(CyanBedFacingType facing, CyanBedOccupiedType occupied, CyanBedPartType part)
        {
            return new BlockState
            {
                Id = (uint)BlockId.CyanBed,
                MetaValue = (((uint)facing) * 2 + (uint)occupied) * 2 + (uint)part,
            };
        }

        public static BlockState PurpleBed(PurpleBedFacingType facing, PurpleBedOccupiedType occupied, PurpleBedPartType part)
        {
            return new BlockState
            {
                Id = (uint)BlockId.PurpleBed,
                MetaValue = (((uint)facing) * 2 + (uint)occupied) * 2 + (uint)part,
            };
        }

        public static BlockState BlueBed(BlueBedFacingType facing, BlueBedOccupiedType occupied, BlueBedPartType part)
        {
            return new BlockState
            {
                Id = (uint)BlockId.BlueBed,
                MetaValue = (((uint)facing) * 2 + (uint)occupied) * 2 + (uint)part,
            };
        }

        public static BlockState BrownBed(BrownBedFacingType facing, BrownBedOccupiedType occupied, BrownBedPartType part)
        {
            return new BlockState
            {
                Id = (uint)BlockId.BrownBed,
                MetaValue = (((uint)facing) * 2 + (uint)occupied) * 2 + (uint)part,
            };
        }

        public static BlockState GreenBed(GreenBedFacingType facing, GreenBedOccupiedType occupied, GreenBedPartType part)
        {
            return new BlockState
            {
                Id = (uint)BlockId.GreenBed,
                MetaValue = (((uint)facing) * 2 + (uint)occupied) * 2 + (uint)part,
            };
        }

        public static BlockState RedBed(RedBedFacingType facing, RedBedOccupiedType occupied, RedBedPartType part)
        {
            return new BlockState
            {
                Id = (uint)BlockId.RedBed,
                MetaValue = (((uint)facing) * 2 + (uint)occupied) * 2 + (uint)part,
            };
        }

        public static BlockState BlackBed(BlackBedFacingType facing, BlackBedOccupiedType occupied, BlackBedPartType part)
        {
            return new BlockState
            {
                Id = (uint)BlockId.BlackBed,
                MetaValue = (((uint)facing) * 2 + (uint)occupied) * 2 + (uint)part,
            };
        }

        public static BlockState PoweredRail(PoweredRailPoweredType powered, PoweredRailShapeType shape)
        {
            return new BlockState
            {
                Id = (uint)BlockId.PoweredRail,
                MetaValue = ((uint)powered) * 6 + (uint)shape,
            };
        }

        public static BlockState DetectorRail(DetectorRailPoweredType powered, DetectorRailShapeType shape)
        {
            return new BlockState
            {
                Id = (uint)BlockId.DetectorRail,
                MetaValue = ((uint)powered) * 6 + (uint)shape,
            };
        }

        public static BlockState StickyPiston(StickyPistonExtendedType extended, StickyPistonFacingType facing)
        {
            return new BlockState
            {
                Id = (uint)BlockId.StickyPiston,
                MetaValue = ((uint)extended) * 6 + (uint)facing,
            };
        }

        public static BlockState Cobweb()
        {
            return new BlockState
            {
                Id = (uint)BlockId.Cobweb,
                MetaValue = 0,
            };
        }

        public static BlockState Grass()
        {
            return new BlockState
            {
                Id = (uint)BlockId.Grass,
                MetaValue = 0,
            };
        }

        public static BlockState Fern()
        {
            return new BlockState
            {
                Id = (uint)BlockId.Fern,
                MetaValue = 0,
            };
        }

        public static BlockState DeadBush()
        {
            return new BlockState
            {
                Id = (uint)BlockId.DeadBush,
                MetaValue = 0,
            };
        }

        public static BlockState Seagrass()
        {
            return new BlockState
            {
                Id = (uint)BlockId.Seagrass,
                MetaValue = 0,
            };
        }

        public static BlockState TallSeagrass(TallSeagrassHalfType half)
        {
            return new BlockState
            {
                Id = (uint)BlockId.TallSeagrass,
                MetaValue = (uint)half,
            };
        }

        public static BlockState Piston(PistonExtendedType extended, PistonFacingType facing)
        {
            return new BlockState
            {
                Id = (uint)BlockId.Piston,
                MetaValue = ((uint)extended) * 6 + (uint)facing,
            };
        }

        public static BlockState PistonHead(PistonHeadFacingType facing, PistonHeadShortType headShort, PistonHeadTypeType type)
        {
            return new BlockState
            {
                Id = (uint)BlockId.PistonHead,
                MetaValue = (((uint)facing) * 2 + (uint)headShort) * 2 + (uint)type,
            };
        }

        public static BlockState WhiteWool()
        {
            return new BlockState
            {
                Id = (uint)BlockId.WhiteWool,
                MetaValue = 0,
            };
        }

        public static BlockState OrangeWool()
        {
            return new BlockState
            {
                Id = (uint)BlockId.OrangeWool,
                MetaValue = 0,
            };
        }

        public static BlockState MagentaWool()
        {
            return new BlockState
            {
                Id = (uint)BlockId.MagentaWool,
                MetaValue = 0,
            };
        }

        public static BlockState LightBlueWool()
        {
            return new BlockState
            {
                Id = (uint)BlockId.LightBlueWool,
                MetaValue = 0,
            };
        }

        public static BlockState YellowWool()
        {
            return new BlockState
            {
                Id = (uint)BlockId.YellowWool,
                MetaValue = 0,
            };
        }

        public static BlockState LimeWool()
        {
            return new BlockState
            {
                Id = (uint)BlockId.LimeWool,
                MetaValue = 0,
            };
        }

        public static BlockState PinkWool()
        {
            return new BlockState
            {
                Id = (uint)BlockId.PinkWool,
                MetaValue = 0,
            };
        }

        public static BlockState GrayWool()
        {
            return new BlockState
            {
                Id = (uint)BlockId.GrayWool,
                MetaValue = 0,
            };
        }

        public static BlockState LightGrayWool()
        {
            return new BlockState
            {
                Id = (uint)BlockId.LightGrayWool,
                MetaValue = 0,
            };
        }

        public static BlockState CyanWool()
        {
            return new BlockState
            {
                Id = (uint)BlockId.CyanWool,
                MetaValue = 0,
            };
        }

        public static BlockState PurpleWool()
        {
            return new BlockState
            {
                Id = (uint)BlockId.PurpleWool,
                MetaValue = 0,
            };
        }

        public static BlockState BlueWool()
        {
            return new BlockState
            {
                Id = (uint)BlockId.BlueWool,
                MetaValue = 0,
            };
        }

        public static BlockState BrownWool()
        {
            return new BlockState
            {
                Id = (uint)BlockId.BrownWool,
                MetaValue = 0,
            };
        }

        public static BlockState GreenWool()
        {
            return new BlockState
            {
                Id = (uint)BlockId.GreenWool,
                MetaValue = 0,
            };
        }

        public static BlockState RedWool()
        {
            return new BlockState
            {
                Id = (uint)BlockId.RedWool,
                MetaValue = 0,
            };
        }

        public static BlockState BlackWool()
        {
            return new BlockState
            {
                Id = (uint)BlockId.BlackWool,
                MetaValue = 0,
            };
        }

        public static BlockState MovingPiston(MovingPistonFacingType facing, MovingPistonTypeType type)
        {
            return new BlockState
            {
                Id = (uint)BlockId.MovingPiston,
                MetaValue = ((uint)facing) * 2 + (uint)type,
            };
        }

        public static BlockState Dandelion()
        {
            return new BlockState
            {
                Id = (uint)BlockId.Dandelion,
                MetaValue = 0,
            };
        }

        public static BlockState Poppy()
        {
            return new BlockState
            {
                Id = (uint)BlockId.Poppy,
                MetaValue = 0,
            };
        }

        public static BlockState BlueOrchid()
        {
            return new BlockState
            {
                Id = (uint)BlockId.BlueOrchid,
                MetaValue = 0,
            };
        }

        public static BlockState Allium()
        {
            return new BlockState
            {
                Id = (uint)BlockId.Allium,
                MetaValue = 0,
            };
        }

        public static BlockState AzureBluet()
        {
            return new BlockState
            {
                Id = (uint)BlockId.AzureBluet,
                MetaValue = 0,
            };
        }

        public static BlockState RedTulip()
        {
            return new BlockState
            {
                Id = (uint)BlockId.RedTulip,
                MetaValue = 0,
            };
        }

        public static BlockState OrangeTulip()
        {
            return new BlockState
            {
                Id = (uint)BlockId.OrangeTulip,
                MetaValue = 0,
            };
        }

        public static BlockState WhiteTulip()
        {
            return new BlockState
            {
                Id = (uint)BlockId.WhiteTulip,
                MetaValue = 0,
            };
        }

        public static BlockState PinkTulip()
        {
            return new BlockState
            {
                Id = (uint)BlockId.PinkTulip,
                MetaValue = 0,
            };
        }

        public static BlockState OxeyeDaisy()
        {
            return new BlockState
            {
                Id = (uint)BlockId.OxeyeDaisy,
                MetaValue = 0,
            };
        }

        public static BlockState Cornflower()
        {
            return new BlockState
            {
                Id = (uint)BlockId.Cornflower,
                MetaValue = 0,
            };
        }

        public static BlockState WitherRose()
        {
            return new BlockState
            {
                Id = (uint)BlockId.WitherRose,
                MetaValue = 0,
            };
        }

        public static BlockState LilyOfTheValley()
        {
            return new BlockState
            {
                Id = (uint)BlockId.LilyOfTheValley,
                MetaValue = 0,
            };
        }

        public static BlockState BrownMushroom()
        {
            return new BlockState
            {
                Id = (uint)BlockId.BrownMushroom,
                MetaValue = 0,
            };
        }

        public static BlockState RedMushroom()
        {
            return new BlockState
            {
                Id = (uint)BlockId.RedMushroom,
                MetaValue = 0,
            };
        }

        public static BlockState GoldBlock()
        {
            return new BlockState
            {
                Id = (uint)BlockId.GoldBlock,
                MetaValue = 0,
            };
        }

        public static BlockState IronBlock()
        {
            return new BlockState
            {
                Id = (uint)BlockId.IronBlock,
                MetaValue = 0,
            };
        }

        public static BlockState Bricks()
        {
            return new BlockState
            {
                Id = (uint)BlockId.Bricks,
                MetaValue = 0,
            };
        }

        public static BlockState Tnt(TntUnstableType unstable)
        {
            return new BlockState
            {
                Id = (uint)BlockId.Tnt,
                MetaValue = (uint)unstable,
            };
        }

        public static BlockState Bookshelf()
        {
            return new BlockState
            {
                Id = (uint)BlockId.Bookshelf,
                MetaValue = 0,
            };
        }

        public static BlockState MossyCobblestone()
        {
            return new BlockState
            {
                Id = (uint)BlockId.MossyCobblestone,
                MetaValue = 0,
            };
        }

        public static BlockState Obsidian()
        {
            return new BlockState
            {
                Id = (uint)BlockId.Obsidian,
                MetaValue = 0,
            };
        }

        public static BlockState Torch()
        {
            return new BlockState
            {
                Id = (uint)BlockId.Torch,
                MetaValue = 0,
            };
        }

        public static BlockState WallTorch(WallTorchFacingType facing)
        {
            return new BlockState
            {
                Id = (uint)BlockId.WallTorch,
                MetaValue = (uint)facing,
            };
        }

        public static BlockState Fire(FireAgeType age, FireEastType east, FireNorthType north, FireSouthType south, FireUpType up, FireWestType west)
        {
            return new BlockState
            {
                Id = (uint)BlockId.Fire,
                MetaValue = ((((((uint)age) * 2 + (uint)east) * 2 + (uint)north) * 2 + (uint)south) * 2 + (uint)up) * 2 + (uint)west,
            };
        }

        public static BlockState Spawner()
        {
            return new BlockState
            {
                Id = (uint)BlockId.Spawner,
                MetaValue = 0,
            };
        }

        public static BlockState OakStairs(OakStairsFacingType facing, OakStairsHalfType half, OakStairsShapeType shape, OakStairsWaterloggedType waterlogged)
        {
            return new BlockState
            {
                Id = (uint)BlockId.OakStairs,
                MetaValue = ((((uint)facing) * 2 + (uint)half) * 5 + (uint)shape) * 2 + (uint)waterlogged,
            };
        }

        public static BlockState Chest(ChestFacingType facing, ChestTypeType type, ChestWaterloggedType waterlogged)
        {
            return new BlockState
            {
                Id = (uint)BlockId.Chest,
                MetaValue = (((uint)facing) * 3 + (uint)type) * 2 + (uint)waterlogged,
            };
        }

        public static BlockState RedstoneWire(RedstoneWireEastType east, RedstoneWireNorthType north, RedstoneWirePowerType power, RedstoneWireSouthType south, RedstoneWireWestType west)
        {
            return new BlockState
            {
                Id = (uint)BlockId.RedstoneWire,
                MetaValue = (((((uint)east) * 3 + (uint)north) * 16 + (uint)power) * 3 + (uint)south) * 3 + (uint)west,
            };
        }

        public static BlockState DiamondOre()
        {
            return new BlockState
            {
                Id = (uint)BlockId.DiamondOre,
                MetaValue = 0,
            };
        }

        public static BlockState DiamondBlock()
        {
            return new BlockState
            {
                Id = (uint)BlockId.DiamondBlock,
                MetaValue = 0,
            };
        }

        public static BlockState CraftingTable()
        {
            return new BlockState
            {
                Id = (uint)BlockId.CraftingTable,
                MetaValue = 0,
            };
        }

        public static BlockState Wheat(WheatAgeType age)
        {
            return new BlockState
            {
                Id = (uint)BlockId.Wheat,
                MetaValue = (uint)age,
            };
        }

        public static BlockState Farmland(FarmlandMoistureType moisture)
        {
            return new BlockState
            {
                Id = (uint)BlockId.Farmland,
                MetaValue = (uint)moisture,
            };
        }

        public static BlockState Furnace(FurnaceFacingType facing, FurnaceLitType lit)
        {
            return new BlockState
            {
                Id = (uint)BlockId.Furnace,
                MetaValue = ((uint)facing) * 2 + (uint)lit,
            };
        }

        public static BlockState OakSign(OakSignRotationType rotation, OakSignWaterloggedType waterlogged)
        {
            return new BlockState
            {
                Id = (uint)BlockId.OakSign,
                MetaValue = ((uint)rotation) * 2 + (uint)waterlogged,
            };
        }

        public static BlockState SpruceSign(SpruceSignRotationType rotation, SpruceSignWaterloggedType waterlogged)
        {
            return new BlockState
            {
                Id = (uint)BlockId.SpruceSign,
                MetaValue = ((uint)rotation) * 2 + (uint)waterlogged,
            };
        }

        public static BlockState BirchSign(BirchSignRotationType rotation, BirchSignWaterloggedType waterlogged)
        {
            return new BlockState
            {
                Id = (uint)BlockId.BirchSign,
                MetaValue = ((uint)rotation) * 2 + (uint)waterlogged,
            };
        }

        public static BlockState AcaciaSign(AcaciaSignRotationType rotation, AcaciaSignWaterloggedType waterlogged)
        {
            return new BlockState
            {
                Id = (uint)BlockId.AcaciaSign,
                MetaValue = ((uint)rotation) * 2 + (uint)waterlogged,
            };
        }

        public static BlockState JungleSign(JungleSignRotationType rotation, JungleSignWaterloggedType waterlogged)
        {
            return new BlockState
            {
                Id = (uint)BlockId.JungleSign,
                MetaValue = ((uint)rotation) * 2 + (uint)waterlogged,
            };
        }

        public static BlockState DarkOakSign(DarkOakSignRotationType rotation, DarkOakSignWaterloggedType waterlogged)
        {
            return new BlockState
            {
                Id = (uint)BlockId.DarkOakSign,
                MetaValue = ((uint)rotation) * 2 + (uint)waterlogged,
            };
        }

        public static BlockState OakDoor(OakDoorFacingType facing, OakDoorHalfType half, OakDoorHingeType hinge, OakDoorOpenType open, OakDoorPoweredType powered)
        {
            return new BlockState
            {
                Id = (uint)BlockId.OakDoor,
                MetaValue = (((((uint)facing) * 2 + (uint)half) * 2 + (uint)hinge) * 2 + (uint)open) * 2 + (uint)powered,
            };
        }

        public static BlockState Ladder(LadderFacingType facing, LadderWaterloggedType waterlogged)
        {
            return new BlockState
            {
                Id = (uint)BlockId.Ladder,
                MetaValue = ((uint)facing) * 2 + (uint)waterlogged,
            };
        }

        public static BlockState Rail(RailShapeType shape)
        {
            return new BlockState
            {
                Id = (uint)BlockId.Rail,
                MetaValue = (uint)shape,
            };
        }

        public static BlockState CobblestoneStairs(CobblestoneStairsFacingType facing, CobblestoneStairsHalfType half, CobblestoneStairsShapeType shape, CobblestoneStairsWaterloggedType waterlogged)
        {
            return new BlockState
            {
                Id = (uint)BlockId.CobblestoneStairs,
                MetaValue = ((((uint)facing) * 2 + (uint)half) * 5 + (uint)shape) * 2 + (uint)waterlogged,
            };
        }

        public static BlockState OakWallSign(OakWallSignFacingType facing, OakWallSignWaterloggedType waterlogged)
        {
            return new BlockState
            {
                Id = (uint)BlockId.OakWallSign,
                MetaValue = ((uint)facing) * 2 + (uint)waterlogged,
            };
        }

        public static BlockState SpruceWallSign(SpruceWallSignFacingType facing, SpruceWallSignWaterloggedType waterlogged)
        {
            return new BlockState
            {
                Id = (uint)BlockId.SpruceWallSign,
                MetaValue = ((uint)facing) * 2 + (uint)waterlogged,
            };
        }

        public static BlockState BirchWallSign(BirchWallSignFacingType facing, BirchWallSignWaterloggedType waterlogged)
        {
            return new BlockState
            {
                Id = (uint)BlockId.BirchWallSign,
                MetaValue = ((uint)facing) * 2 + (uint)waterlogged,
            };
        }

        public static BlockState AcaciaWallSign(AcaciaWallSignFacingType facing, AcaciaWallSignWaterloggedType waterlogged)
        {
            return new BlockState
            {
                Id = (uint)BlockId.AcaciaWallSign,
                MetaValue = ((uint)facing) * 2 + (uint)waterlogged,
            };
        }

        public static BlockState JungleWallSign(JungleWallSignFacingType facing, JungleWallSignWaterloggedType waterlogged)
        {
            return new BlockState
            {
                Id = (uint)BlockId.JungleWallSign,
                MetaValue = ((uint)facing) * 2 + (uint)waterlogged,
            };
        }

        public static BlockState DarkOakWallSign(DarkOakWallSignFacingType facing, DarkOakWallSignWaterloggedType waterlogged)
        {
            return new BlockState
            {
                Id = (uint)BlockId.DarkOakWallSign,
                MetaValue = ((uint)facing) * 2 + (uint)waterlogged,
            };
        }

        public static BlockState Lever(LeverFaceType face, LeverFacingType facing, LeverPoweredType powered)
        {
            return new BlockState
            {
                Id = (uint)BlockId.Lever,
                MetaValue = (((uint)face) * 4 + (uint)facing) * 2 + (uint)powered,
            };
        }

        public static BlockState StonePressurePlate(StonePressurePlatePoweredType powered)
        {
            return new BlockState
            {
                Id = (uint)BlockId.StonePressurePlate,
                MetaValue = (uint)powered,
            };
        }

        public static BlockState IronDoor(IronDoorFacingType facing, IronDoorHalfType half, IronDoorHingeType hinge, IronDoorOpenType open, IronDoorPoweredType powered)
        {
            return new BlockState
            {
                Id = (uint)BlockId.IronDoor,
                MetaValue = (((((uint)facing) * 2 + (uint)half) * 2 + (uint)hinge) * 2 + (uint)open) * 2 + (uint)powered,
            };
        }

        public static BlockState OakPressurePlate(OakPressurePlatePoweredType powered)
        {
            return new BlockState
            {
                Id = (uint)BlockId.OakPressurePlate,
                MetaValue = (uint)powered,
            };
        }

        public static BlockState SprucePressurePlate(SprucePressurePlatePoweredType powered)
        {
            return new BlockState
            {
                Id = (uint)BlockId.SprucePressurePlate,
                MetaValue = (uint)powered,
            };
        }

        public static BlockState BirchPressurePlate(BirchPressurePlatePoweredType powered)
        {
            return new BlockState
            {
                Id = (uint)BlockId.BirchPressurePlate,
                MetaValue = (uint)powered,
            };
        }

        public static BlockState JunglePressurePlate(JunglePressurePlatePoweredType powered)
        {
            return new BlockState
            {
                Id = (uint)BlockId.JunglePressurePlate,
                MetaValue = (uint)powered,
            };
        }

        public static BlockState AcaciaPressurePlate(AcaciaPressurePlatePoweredType powered)
        {
            return new BlockState
            {
                Id = (uint)BlockId.AcaciaPressurePlate,
                MetaValue = (uint)powered,
            };
        }

        public static BlockState DarkOakPressurePlate(DarkOakPressurePlatePoweredType powered)
        {
            return new BlockState
            {
                Id = (uint)BlockId.DarkOakPressurePlate,
                MetaValue = (uint)powered,
            };
        }

        public static BlockState RedstoneOre(RedstoneOreLitType lit = RedstoneOreLitType.True)
        {
            return new BlockState
            {
                Id = (uint)BlockId.RedstoneOre,
                MetaValue = (uint)lit,
            };
        }

        public static BlockState RedstoneTorch(RedstoneTorchLitType lit)
        {
            return new BlockState
            {
                Id = (uint)BlockId.RedstoneTorch,
                MetaValue = (uint)lit,
            };
        }

        public static BlockState RedstoneWallTorch(RedstoneWallTorchFacingType facing, RedstoneWallTorchLitType lit)
        {
            return new BlockState
            {
                Id = (uint)BlockId.RedstoneWallTorch,
                MetaValue = ((uint)facing) * 2 + (uint)lit,
            };
        }

        public static BlockState StoneButton(StoneButtonFaceType face, StoneButtonFacingType facing, StoneButtonPoweredType powered)
        {
            return new BlockState
            {
                Id = (uint)BlockId.StoneButton,
                MetaValue = (((uint)face) * 4 + (uint)facing) * 2 + (uint)powered,
            };
        }

        public static BlockState Snow(SnowLayersType layers)
        {
            return new BlockState
            {
                Id = (uint)BlockId.Snow,
                MetaValue = (uint)layers,
            };
        }

        public static BlockState Ice()
        {
            return new BlockState
            {
                Id = (uint)BlockId.Ice,
                MetaValue = 0,
            };
        }

        public static BlockState SnowBlock()
        {
            return new BlockState
            {
                Id = (uint)BlockId.SnowBlock,
                MetaValue = 0,
            };
        }

        public static BlockState Cactus(CactusAgeType age)
        {
            return new BlockState
            {
                Id = (uint)BlockId.Cactus,
                MetaValue = (uint)age,
            };
        }

        public static BlockState Clay()
        {
            return new BlockState
            {
                Id = (uint)BlockId.Clay,
                MetaValue = 0,
            };
        }

        public static BlockState SugarCane(SugarCaneAgeType age)
        {
            return new BlockState
            {
                Id = (uint)BlockId.SugarCane,
                MetaValue = (uint)age,
            };
        }

        public static BlockState Jukebox(JukeboxHasRecordType hasRecord)
        {
            return new BlockState
            {
                Id = (uint)BlockId.Jukebox,
                MetaValue = (uint)hasRecord,
            };
        }

        public static BlockState OakFence(OakFenceEastType east, OakFenceNorthType north, OakFenceSouthType south, OakFenceWaterloggedType waterlogged, OakFenceWestType west)
        {
            return new BlockState
            {
                Id = (uint)BlockId.OakFence,
                MetaValue = (((((uint)east) * 2 + (uint)north) * 2 + (uint)south) * 2 + (uint)waterlogged) * 2 + (uint)west,
            };
        }

        public static BlockState Pumpkin()
        {
            return new BlockState
            {
                Id = (uint)BlockId.Pumpkin,
                MetaValue = 0,
            };
        }

        public static BlockState Netherrack()
        {
            return new BlockState
            {
                Id = (uint)BlockId.Netherrack,
                MetaValue = 0,
            };
        }

        public static BlockState SoulSand()
        {
            return new BlockState
            {
                Id = (uint)BlockId.SoulSand,
                MetaValue = 0,
            };
        }

        public static BlockState Glowstone()
        {
            return new BlockState
            {
                Id = (uint)BlockId.Glowstone,
                MetaValue = 0,
            };
        }

        public static BlockState NetherPortal(NetherPortalAxisType axis)
        {
            return new BlockState
            {
                Id = (uint)BlockId.NetherPortal,
                MetaValue = (uint)axis,
            };
        }

        public static BlockState CarvedPumpkin(CarvedPumpkinFacingType facing)
        {
            return new BlockState
            {
                Id = (uint)BlockId.CarvedPumpkin,
                MetaValue = (uint)facing,
            };
        }

        public static BlockState JackOLantern(JackOLanternFacingType facing)
        {
            return new BlockState
            {
                Id = (uint)BlockId.JackOLantern,
                MetaValue = (uint)facing,
            };
        }

        public static BlockState Cake(CakeBitesType bites)
        {
            return new BlockState
            {
                Id = (uint)BlockId.Cake,
                MetaValue = (uint)bites,
            };
        }

        public static BlockState Repeater(RepeaterDelayType delay, RepeaterFacingType facing, RepeaterLockedType locked, RepeaterPoweredType powered)
        {
            return new BlockState
            {
                Id = (uint)BlockId.Repeater,
                MetaValue = ((((uint)delay) * 4 + (uint)facing) * 2 + (uint)locked) * 2 + (uint)powered,
            };
        }

        public static BlockState WhiteStainedGlass()
        {
            return new BlockState
            {
                Id = (uint)BlockId.WhiteStainedGlass,
                MetaValue = 0,
            };
        }

        public static BlockState OrangeStainedGlass()
        {
            return new BlockState
            {
                Id = (uint)BlockId.OrangeStainedGlass,
                MetaValue = 0,
            };
        }

        public static BlockState MagentaStainedGlass()
        {
            return new BlockState
            {
                Id = (uint)BlockId.MagentaStainedGlass,
                MetaValue = 0,
            };
        }

        public static BlockState LightBlueStainedGlass()
        {
            return new BlockState
            {
                Id = (uint)BlockId.LightBlueStainedGlass,
                MetaValue = 0,
            };
        }

        public static BlockState YellowStainedGlass()
        {
            return new BlockState
            {
                Id = (uint)BlockId.YellowStainedGlass,
                MetaValue = 0,
            };
        }

        public static BlockState LimeStainedGlass()
        {
            return new BlockState
            {
                Id = (uint)BlockId.LimeStainedGlass,
                MetaValue = 0,
            };
        }

        public static BlockState PinkStainedGlass()
        {
            return new BlockState
            {
                Id = (uint)BlockId.PinkStainedGlass,
                MetaValue = 0,
            };
        }

        public static BlockState GrayStainedGlass()
        {
            return new BlockState
            {
                Id = (uint)BlockId.GrayStainedGlass,
                MetaValue = 0,
            };
        }

        public static BlockState LightGrayStainedGlass()
        {
            return new BlockState
            {
                Id = (uint)BlockId.LightGrayStainedGlass,
                MetaValue = 0,
            };
        }

        public static BlockState CyanStainedGlass()
        {
            return new BlockState
            {
                Id = (uint)BlockId.CyanStainedGlass,
                MetaValue = 0,
            };
        }

        public static BlockState PurpleStainedGlass()
        {
            return new BlockState
            {
                Id = (uint)BlockId.PurpleStainedGlass,
                MetaValue = 0,
            };
        }

        public static BlockState BlueStainedGlass()
        {
            return new BlockState
            {
                Id = (uint)BlockId.BlueStainedGlass,
                MetaValue = 0,
            };
        }

        public static BlockState BrownStainedGlass()
        {
            return new BlockState
            {
                Id = (uint)BlockId.BrownStainedGlass,
                MetaValue = 0,
            };
        }

        public static BlockState GreenStainedGlass()
        {
            return new BlockState
            {
                Id = (uint)BlockId.GreenStainedGlass,
                MetaValue = 0,
            };
        }

        public static BlockState RedStainedGlass()
        {
            return new BlockState
            {
                Id = (uint)BlockId.RedStainedGlass,
                MetaValue = 0,
            };
        }

        public static BlockState BlackStainedGlass()
        {
            return new BlockState
            {
                Id = (uint)BlockId.BlackStainedGlass,
                MetaValue = 0,
            };
        }

        public static BlockState OakTrapdoor(OakTrapdoorFacingType facing, OakTrapdoorHalfType half, OakTrapdoorOpenType open, OakTrapdoorPoweredType powered, OakTrapdoorWaterloggedType waterlogged)
        {
            return new BlockState
            {
                Id = (uint)BlockId.OakTrapdoor,
                MetaValue = (((((uint)facing) * 2 + (uint)half) * 2 + (uint)open) * 2 + (uint)powered) * 2 + (uint)waterlogged,
            };
        }

        public static BlockState SpruceTrapdoor(SpruceTrapdoorFacingType facing, SpruceTrapdoorHalfType half, SpruceTrapdoorOpenType open, SpruceTrapdoorPoweredType powered, SpruceTrapdoorWaterloggedType waterlogged)
        {
            return new BlockState
            {
                Id = (uint)BlockId.SpruceTrapdoor,
                MetaValue = (((((uint)facing) * 2 + (uint)half) * 2 + (uint)open) * 2 + (uint)powered) * 2 + (uint)waterlogged,
            };
        }

        public static BlockState BirchTrapdoor(BirchTrapdoorFacingType facing, BirchTrapdoorHalfType half, BirchTrapdoorOpenType open, BirchTrapdoorPoweredType powered, BirchTrapdoorWaterloggedType waterlogged)
        {
            return new BlockState
            {
                Id = (uint)BlockId.BirchTrapdoor,
                MetaValue = (((((uint)facing) * 2 + (uint)half) * 2 + (uint)open) * 2 + (uint)powered) * 2 + (uint)waterlogged,
            };
        }

        public static BlockState JungleTrapdoor(JungleTrapdoorFacingType facing, JungleTrapdoorHalfType half, JungleTrapdoorOpenType open, JungleTrapdoorPoweredType powered, JungleTrapdoorWaterloggedType waterlogged)
        {
            return new BlockState
            {
                Id = (uint)BlockId.JungleTrapdoor,
                MetaValue = (((((uint)facing) * 2 + (uint)half) * 2 + (uint)open) * 2 + (uint)powered) * 2 + (uint)waterlogged,
            };
        }

        public static BlockState AcaciaTrapdoor(AcaciaTrapdoorFacingType facing, AcaciaTrapdoorHalfType half, AcaciaTrapdoorOpenType open, AcaciaTrapdoorPoweredType powered, AcaciaTrapdoorWaterloggedType waterlogged)
        {
            return new BlockState
            {
                Id = (uint)BlockId.AcaciaTrapdoor,
                MetaValue = (((((uint)facing) * 2 + (uint)half) * 2 + (uint)open) * 2 + (uint)powered) * 2 + (uint)waterlogged,
            };
        }

        public static BlockState DarkOakTrapdoor(DarkOakTrapdoorFacingType facing, DarkOakTrapdoorHalfType half, DarkOakTrapdoorOpenType open, DarkOakTrapdoorPoweredType powered, DarkOakTrapdoorWaterloggedType waterlogged)
        {
            return new BlockState
            {
                Id = (uint)BlockId.DarkOakTrapdoor,
                MetaValue = (((((uint)facing) * 2 + (uint)half) * 2 + (uint)open) * 2 + (uint)powered) * 2 + (uint)waterlogged,
            };
        }

        public static BlockState StoneBricks()
        {
            return new BlockState
            {
                Id = (uint)BlockId.StoneBricks,
                MetaValue = 0,
            };
        }

        public static BlockState MossyStoneBricks()
        {
            return new BlockState
            {
                Id = (uint)BlockId.MossyStoneBricks,
                MetaValue = 0,
            };
        }

        public static BlockState CrackedStoneBricks()
        {
            return new BlockState
            {
                Id = (uint)BlockId.CrackedStoneBricks,
                MetaValue = 0,
            };
        }

        public static BlockState ChiseledStoneBricks()
        {
            return new BlockState
            {
                Id = (uint)BlockId.ChiseledStoneBricks,
                MetaValue = 0,
            };
        }

        public static BlockState InfestedStone()
        {
            return new BlockState
            {
                Id = (uint)BlockId.InfestedStone,
                MetaValue = 0,
            };
        }

        public static BlockState InfestedCobblestone()
        {
            return new BlockState
            {
                Id = (uint)BlockId.InfestedCobblestone,
                MetaValue = 0,
            };
        }

        public static BlockState InfestedStoneBricks()
        {
            return new BlockState
            {
                Id = (uint)BlockId.InfestedStoneBricks,
                MetaValue = 0,
            };
        }

        public static BlockState InfestedMossyStoneBricks()
        {
            return new BlockState
            {
                Id = (uint)BlockId.InfestedMossyStoneBricks,
                MetaValue = 0,
            };
        }

        public static BlockState InfestedCrackedStoneBricks()
        {
            return new BlockState
            {
                Id = (uint)BlockId.InfestedCrackedStoneBricks,
                MetaValue = 0,
            };
        }

        public static BlockState InfestedChiseledStoneBricks()
        {
            return new BlockState
            {
                Id = (uint)BlockId.InfestedChiseledStoneBricks,
                MetaValue = 0,
            };
        }

        public static BlockState BrownMushroomBlock(BrownMushroomBlockDownType down, BrownMushroomBlockEastType east, BrownMushroomBlockNorthType north, BrownMushroomBlockSouthType south, BrownMushroomBlockUpType up, BrownMushroomBlockWestType west)
        {
            return new BlockState
            {
                Id = (uint)BlockId.BrownMushroomBlock,
                MetaValue = ((((((uint)down) * 2 + (uint)east) * 2 + (uint)north) * 2 + (uint)south) * 2 + (uint)up) * 2 + (uint)west,
            };
        }

        public static BlockState RedMushroomBlock(RedMushroomBlockDownType down, RedMushroomBlockEastType east, RedMushroomBlockNorthType north, RedMushroomBlockSouthType south, RedMushroomBlockUpType up, RedMushroomBlockWestType west)
        {
            return new BlockState
            {
                Id = (uint)BlockId.RedMushroomBlock,
                MetaValue = ((((((uint)down) * 2 + (uint)east) * 2 + (uint)north) * 2 + (uint)south) * 2 + (uint)up) * 2 + (uint)west,
            };
        }

        public static BlockState MushroomStem(MushroomStemDownType down, MushroomStemEastType east, MushroomStemNorthType north, MushroomStemSouthType south, MushroomStemUpType up, MushroomStemWestType west)
        {
            return new BlockState
            {
                Id = (uint)BlockId.MushroomStem,
                MetaValue = ((((((uint)down) * 2 + (uint)east) * 2 + (uint)north) * 2 + (uint)south) * 2 + (uint)up) * 2 + (uint)west,
            };
        }

        public static BlockState IronBars(IronBarsEastType east, IronBarsNorthType north, IronBarsSouthType south, IronBarsWaterloggedType waterlogged, IronBarsWestType west)
        {
            return new BlockState
            {
                Id = (uint)BlockId.IronBars,
                MetaValue = (((((uint)east) * 2 + (uint)north) * 2 + (uint)south) * 2 + (uint)waterlogged) * 2 + (uint)west,
            };
        }

        public static BlockState GlassPane(GlassPaneEastType east, GlassPaneNorthType north, GlassPaneSouthType south, GlassPaneWaterloggedType waterlogged, GlassPaneWestType west)
        {
            return new BlockState
            {
                Id = (uint)BlockId.GlassPane,
                MetaValue = (((((uint)east) * 2 + (uint)north) * 2 + (uint)south) * 2 + (uint)waterlogged) * 2 + (uint)west,
            };
        }

        public static BlockState Melon()
        {
            return new BlockState
            {
                Id = (uint)BlockId.Melon,
                MetaValue = 0,
            };
        }

        public static BlockState AttachedPumpkinStem(AttachedPumpkinStemFacingType facing)
        {
            return new BlockState
            {
                Id = (uint)BlockId.AttachedPumpkinStem,
                MetaValue = (uint)facing,
            };
        }

        public static BlockState AttachedMelonStem(AttachedMelonStemFacingType facing)
        {
            return new BlockState
            {
                Id = (uint)BlockId.AttachedMelonStem,
                MetaValue = (uint)facing,
            };
        }

        public static BlockState PumpkinStem(PumpkinStemAgeType age)
        {
            return new BlockState
            {
                Id = (uint)BlockId.PumpkinStem,
                MetaValue = (uint)age,
            };
        }

        public static BlockState MelonStem(MelonStemAgeType age)
        {
            return new BlockState
            {
                Id = (uint)BlockId.MelonStem,
                MetaValue = (uint)age,
            };
        }

        public static BlockState Vine(VineType vineType)
        {
            return new BlockState
            {
                Id = (uint)BlockId.Vine,
                MetaValue = (((((uint)vineType.East) * 2 + (uint)vineType.North) * 2 + (uint)vineType.South) * 2 + (uint)vineType.Up) * 2 + (uint)vineType.West,
            };
        }

        public static BlockState Vine(VineEastType east, VineNorthType north, VineSouthType south, VineUpType up, VineWestType west)
        {
            return new BlockState
            {
                Id = (uint)BlockId.Vine,
                MetaValue = (((((uint)east) * 2 + (uint)north) * 2 + (uint)south) * 2 + (uint)up) * 2 + (uint)west,
            };
        }

        public static BlockState OakFenceGate(OakFenceGateFacingType facing, OakFenceGateInWallType inWall, OakFenceGateOpenType open, OakFenceGatePoweredType powered)
        {
            return new BlockState
            {
                Id = (uint)BlockId.OakFenceGate,
                MetaValue = ((((uint)facing) * 2 + (uint)inWall) * 2 + (uint)open) * 2 + (uint)powered,
            };
        }

        public static BlockState BrickStairs(BrickStairsFacingType facing, BrickStairsHalfType half, BrickStairsShapeType shape, BrickStairsWaterloggedType waterlogged)
        {
            return new BlockState
            {
                Id = (uint)BlockId.BrickStairs,
                MetaValue = ((((uint)facing) * 2 + (uint)half) * 5 + (uint)shape) * 2 + (uint)waterlogged,
            };
        }

        public static BlockState StoneBrickStairs(StoneBrickStairsFacingType facing, StoneBrickStairsHalfType half, StoneBrickStairsShapeType shape, StoneBrickStairsWaterloggedType waterlogged)
        {
            return new BlockState
            {
                Id = (uint)BlockId.StoneBrickStairs,
                MetaValue = ((((uint)facing) * 2 + (uint)half) * 5 + (uint)shape) * 2 + (uint)waterlogged,
            };
        }

        public static BlockState Mycelium(MyceliumSnowyType snowy)
        {
            return new BlockState
            {
                Id = (uint)BlockId.Mycelium,
                MetaValue = (uint)snowy,
            };
        }

        public static BlockState LilyPad()
        {
            return new BlockState
            {
                Id = (uint)BlockId.LilyPad,
                MetaValue = 0,
            };
        }

        public static BlockState NetherBricks()
        {
            return new BlockState
            {
                Id = (uint)BlockId.NetherBricks,
                MetaValue = 0,
            };
        }

        public static BlockState NetherBrickFence(NetherBrickFenceEastType east, NetherBrickFenceNorthType north, NetherBrickFenceSouthType south, NetherBrickFenceWaterloggedType waterlogged, NetherBrickFenceWestType west)
        {
            return new BlockState
            {
                Id = (uint)BlockId.NetherBrickFence,
                MetaValue = (((((uint)east) * 2 + (uint)north) * 2 + (uint)south) * 2 + (uint)waterlogged) * 2 + (uint)west,
            };
        }

        public static BlockState NetherBrickStairs(NetherBrickStairsFacingType facing, NetherBrickStairsHalfType half, NetherBrickStairsShapeType shape, NetherBrickStairsWaterloggedType waterlogged)
        {
            return new BlockState
            {
                Id = (uint)BlockId.NetherBrickStairs,
                MetaValue = ((((uint)facing) * 2 + (uint)half) * 5 + (uint)shape) * 2 + (uint)waterlogged,
            };
        }

        public static BlockState NetherWart(NetherWartAgeType age)
        {
            return new BlockState
            {
                Id = (uint)BlockId.NetherWart,
                MetaValue = (uint)age,
            };
        }

        public static BlockState EnchantingTable()
        {
            return new BlockState
            {
                Id = (uint)BlockId.EnchantingTable,
                MetaValue = 0,
            };
        }

        public static BlockState BrewingStand(BrewingStandHasBottle0Type hasBottle0, BrewingStandHasBottle1Type hasBottle1, BrewingStandHasBottle2Type hasBottle2)
        {
            return new BlockState
            {
                Id = (uint)BlockId.BrewingStand,
                MetaValue = (((uint)hasBottle0) * 2 + (uint)hasBottle1) * 2 + (uint)hasBottle2,
            };
        }

        public static BlockState Cauldron(CauldronLevelType level)
        {
            return new BlockState
            {
                Id = (uint)BlockId.Cauldron,
                MetaValue = (uint)level,
            };
        }

        public static BlockState EndPortal()
        {
            return new BlockState
            {
                Id = (uint)BlockId.EndPortal,
                MetaValue = 0,
            };
        }

        public static BlockState EndPortalFrame(EndPortalFrameEyeType eye, EndPortalFrameFacingType facing)
        {
            return new BlockState
            {
                Id = (uint)BlockId.EndPortalFrame,
                MetaValue = ((uint)eye) * 4 + (uint)facing,
            };
        }

        public static BlockState EndStone()
        {
            return new BlockState
            {
                Id = (uint)BlockId.EndStone,
                MetaValue = 0,
            };
        }

        public static BlockState DragonEgg()
        {
            return new BlockState
            {
                Id = (uint)BlockId.DragonEgg,
                MetaValue = 0,
            };
        }

        public static BlockState RedstoneLamp(RedstoneLampLitType lit)
        {
            return new BlockState
            {
                Id = (uint)BlockId.RedstoneLamp,
                MetaValue = (uint)lit,
            };
        }

        public static BlockState Cocoa(CocoaAgeType age, CocoaFacingType facing)
        {
            return new BlockState
            {
                Id = (uint)BlockId.Cocoa,
                MetaValue = ((uint)age) * 4 + (uint)facing,
            };
        }

        public static BlockState SandstoneStairs(SandstoneStairsFacingType facing, SandstoneStairsHalfType half, SandstoneStairsShapeType shape, SandstoneStairsWaterloggedType waterlogged)
        {
            return new BlockState
            {
                Id = (uint)BlockId.SandstoneStairs,
                MetaValue = ((((uint)facing) * 2 + (uint)half) * 5 + (uint)shape) * 2 + (uint)waterlogged,
            };
        }

        public static BlockState EmeraldOre()
        {
            return new BlockState
            {
                Id = (uint)BlockId.EmeraldOre,
                MetaValue = 0,
            };
        }

        public static BlockState EnderChest(EnderChestFacingType facing, EnderChestWaterloggedType waterlogged)
        {
            return new BlockState
            {
                Id = (uint)BlockId.EnderChest,
                MetaValue = ((uint)facing) * 2 + (uint)waterlogged,
            };
        }

        public static BlockState TripwireHook(TripwireHookAttachedType attached, TripwireHookFacingType facing, TripwireHookPoweredType powered)
        {
            return new BlockState
            {
                Id = (uint)BlockId.TripwireHook,
                MetaValue = (((uint)attached) * 4 + (uint)facing) * 2 + (uint)powered,
            };
        }

        public static BlockState Tripwire(TripwireAttachedType attached, TripwireDisarmedType disarmed, TripwireEastType east, TripwireNorthType north, TripwirePoweredType powered, TripwireSouthType south, TripwireWestType west)
        {
            return new BlockState
            {
                Id = (uint)BlockId.Tripwire,
                MetaValue = (((((((uint)attached) * 2 + (uint)disarmed) * 2 + (uint)east) * 2 + (uint)north) * 2 + (uint)powered) * 2 + (uint)south) * 2 + (uint)west,
            };
        }

        public static BlockState EmeraldBlock()
        {
            return new BlockState
            {
                Id = (uint)BlockId.EmeraldBlock,
                MetaValue = 0,
            };
        }

        public static BlockState SpruceStairs(SpruceStairsFacingType facing, SpruceStairsHalfType half, SpruceStairsShapeType shape, SpruceStairsWaterloggedType waterlogged)
        {
            return new BlockState
            {
                Id = (uint)BlockId.SpruceStairs,
                MetaValue = ((((uint)facing) * 2 + (uint)half) * 5 + (uint)shape) * 2 + (uint)waterlogged,
            };
        }

        public static BlockState BirchStairs(BirchStairsFacingType facing, BirchStairsHalfType half, BirchStairsShapeType shape, BirchStairsWaterloggedType waterlogged)
        {
            return new BlockState
            {
                Id = (uint)BlockId.BirchStairs,
                MetaValue = ((((uint)facing) * 2 + (uint)half) * 5 + (uint)shape) * 2 + (uint)waterlogged,
            };
        }

        public static BlockState JungleStairs(JungleStairsFacingType facing, JungleStairsHalfType half, JungleStairsShapeType shape, JungleStairsWaterloggedType waterlogged)
        {
            return new BlockState
            {
                Id = (uint)BlockId.JungleStairs,
                MetaValue = ((((uint)facing) * 2 + (uint)half) * 5 + (uint)shape) * 2 + (uint)waterlogged,
            };
        }

        public static BlockState CommandBlock(CommandBlockConditionalType conditional, CommandBlockFacingType facing)
        {
            return new BlockState
            {
                Id = (uint)BlockId.CommandBlock,
                MetaValue = ((uint)conditional) * 6 + (uint)facing,
            };
        }

        public static BlockState Beacon()
        {
            return new BlockState
            {
                Id = (uint)BlockId.Beacon,
                MetaValue = 0,
            };
        }

        public static BlockState CobblestoneWall(CobblestoneWallEastType east, CobblestoneWallNorthType north, CobblestoneWallSouthType south, CobblestoneWallUpType up, CobblestoneWallWaterloggedType waterlogged, CobblestoneWallWestType west)
        {
            return new BlockState
            {
                Id = (uint)BlockId.CobblestoneWall,
                MetaValue = ((((((uint)east) * 2 + (uint)north) * 2 + (uint)south) * 2 + (uint)up) * 2 + (uint)waterlogged) * 2 + (uint)west,
            };
        }

        public static BlockState MossyCobblestoneWall(MossyCobblestoneWallEastType east, MossyCobblestoneWallNorthType north, MossyCobblestoneWallSouthType south, MossyCobblestoneWallUpType up, MossyCobblestoneWallWaterloggedType waterlogged, MossyCobblestoneWallWestType west)
        {
            return new BlockState
            {
                Id = (uint)BlockId.MossyCobblestoneWall,
                MetaValue = ((((((uint)east) * 2 + (uint)north) * 2 + (uint)south) * 2 + (uint)up) * 2 + (uint)waterlogged) * 2 + (uint)west,
            };
        }

        public static BlockState FlowerPot()
        {
            return new BlockState
            {
                Id = (uint)BlockId.FlowerPot,
                MetaValue = 0,
            };
        }

        public static BlockState PottedOakSapling()
        {
            return new BlockState
            {
                Id = (uint)BlockId.PottedOakSapling,
                MetaValue = 0,
            };
        }

        public static BlockState PottedSpruceSapling()
        {
            return new BlockState
            {
                Id = (uint)BlockId.PottedSpruceSapling,
                MetaValue = 0,
            };
        }

        public static BlockState PottedBirchSapling()
        {
            return new BlockState
            {
                Id = (uint)BlockId.PottedBirchSapling,
                MetaValue = 0,
            };
        }

        public static BlockState PottedJungleSapling()
        {
            return new BlockState
            {
                Id = (uint)BlockId.PottedJungleSapling,
                MetaValue = 0,
            };
        }

        public static BlockState PottedAcaciaSapling()
        {
            return new BlockState
            {
                Id = (uint)BlockId.PottedAcaciaSapling,
                MetaValue = 0,
            };
        }

        public static BlockState PottedDarkOakSapling()
        {
            return new BlockState
            {
                Id = (uint)BlockId.PottedDarkOakSapling,
                MetaValue = 0,
            };
        }

        public static BlockState PottedFern()
        {
            return new BlockState
            {
                Id = (uint)BlockId.PottedFern,
                MetaValue = 0,
            };
        }

        public static BlockState PottedDandelion()
        {
            return new BlockState
            {
                Id = (uint)BlockId.PottedDandelion,
                MetaValue = 0,
            };
        }

        public static BlockState PottedPoppy()
        {
            return new BlockState
            {
                Id = (uint)BlockId.PottedPoppy,
                MetaValue = 0,
            };
        }

        public static BlockState PottedBlueOrchid()
        {
            return new BlockState
            {
                Id = (uint)BlockId.PottedBlueOrchid,
                MetaValue = 0,
            };
        }

        public static BlockState PottedAllium()
        {
            return new BlockState
            {
                Id = (uint)BlockId.PottedAllium,
                MetaValue = 0,
            };
        }

        public static BlockState PottedAzureBluet()
        {
            return new BlockState
            {
                Id = (uint)BlockId.PottedAzureBluet,
                MetaValue = 0,
            };
        }

        public static BlockState PottedRedTulip()
        {
            return new BlockState
            {
                Id = (uint)BlockId.PottedRedTulip,
                MetaValue = 0,
            };
        }

        public static BlockState PottedOrangeTulip()
        {
            return new BlockState
            {
                Id = (uint)BlockId.PottedOrangeTulip,
                MetaValue = 0,
            };
        }

        public static BlockState PottedWhiteTulip()
        {
            return new BlockState
            {
                Id = (uint)BlockId.PottedWhiteTulip,
                MetaValue = 0,
            };
        }

        public static BlockState PottedPinkTulip()
        {
            return new BlockState
            {
                Id = (uint)BlockId.PottedPinkTulip,
                MetaValue = 0,
            };
        }

        public static BlockState PottedOxeyeDaisy()
        {
            return new BlockState
            {
                Id = (uint)BlockId.PottedOxeyeDaisy,
                MetaValue = 0,
            };
        }

        public static BlockState PottedCornflower()
        {
            return new BlockState
            {
                Id = (uint)BlockId.PottedCornflower,
                MetaValue = 0,
            };
        }

        public static BlockState PottedLilyOfTheValley()
        {
            return new BlockState
            {
                Id = (uint)BlockId.PottedLilyOfTheValley,
                MetaValue = 0,
            };
        }

        public static BlockState PottedWitherRose()
        {
            return new BlockState
            {
                Id = (uint)BlockId.PottedWitherRose,
                MetaValue = 0,
            };
        }

        public static BlockState PottedRedMushroom()
        {
            return new BlockState
            {
                Id = (uint)BlockId.PottedRedMushroom,
                MetaValue = 0,
            };
        }

        public static BlockState PottedBrownMushroom()
        {
            return new BlockState
            {
                Id = (uint)BlockId.PottedBrownMushroom,
                MetaValue = 0,
            };
        }

        public static BlockState PottedDeadBush()
        {
            return new BlockState
            {
                Id = (uint)BlockId.PottedDeadBush,
                MetaValue = 0,
            };
        }

        public static BlockState PottedCactus()
        {
            return new BlockState
            {
                Id = (uint)BlockId.PottedCactus,
                MetaValue = 0,
            };
        }

        public static BlockState Carrots(CarrotsAgeType age)
        {
            return new BlockState
            {
                Id = (uint)BlockId.Carrots,
                MetaValue = (uint)age,
            };
        }

        public static BlockState Potatoes(PotatoesAgeType age)
        {
            return new BlockState
            {
                Id = (uint)BlockId.Potatoes,
                MetaValue = (uint)age,
            };
        }

        public static BlockState OakButton(OakButtonFaceType face, OakButtonFacingType facing, OakButtonPoweredType powered)
        {
            return new BlockState
            {
                Id = (uint)BlockId.OakButton,
                MetaValue = (((uint)face) * 4 + (uint)facing) * 2 + (uint)powered,
            };
        }

        public static BlockState SpruceButton(SpruceButtonFaceType face, SpruceButtonFacingType facing, SpruceButtonPoweredType powered)
        {
            return new BlockState
            {
                Id = (uint)BlockId.SpruceButton,
                MetaValue = (((uint)face) * 4 + (uint)facing) * 2 + (uint)powered,
            };
        }

        public static BlockState BirchButton(BirchButtonFaceType face, BirchButtonFacingType facing, BirchButtonPoweredType powered)
        {
            return new BlockState
            {
                Id = (uint)BlockId.BirchButton,
                MetaValue = (((uint)face) * 4 + (uint)facing) * 2 + (uint)powered,
            };
        }

        public static BlockState JungleButton(JungleButtonFaceType face, JungleButtonFacingType facing, JungleButtonPoweredType powered)
        {
            return new BlockState
            {
                Id = (uint)BlockId.JungleButton,
                MetaValue = (((uint)face) * 4 + (uint)facing) * 2 + (uint)powered,
            };
        }

        public static BlockState AcaciaButton(AcaciaButtonFaceType face, AcaciaButtonFacingType facing, AcaciaButtonPoweredType powered)
        {
            return new BlockState
            {
                Id = (uint)BlockId.AcaciaButton,
                MetaValue = (((uint)face) * 4 + (uint)facing) * 2 + (uint)powered,
            };
        }

        public static BlockState DarkOakButton(DarkOakButtonFaceType face, DarkOakButtonFacingType facing, DarkOakButtonPoweredType powered)
        {
            return new BlockState
            {
                Id = (uint)BlockId.DarkOakButton,
                MetaValue = (((uint)face) * 4 + (uint)facing) * 2 + (uint)powered,
            };
        }

        public static BlockState SkeletonSkull(SkeletonSkullRotationType rotation)
        {
            return new BlockState
            {
                Id = (uint)BlockId.SkeletonSkull,
                MetaValue = (uint)rotation,
            };
        }

        public static BlockState SkeletonWallSkull(SkeletonWallSkullFacingType facing)
        {
            return new BlockState
            {
                Id = (uint)BlockId.SkeletonWallSkull,
                MetaValue = (uint)facing,
            };
        }

        public static BlockState WitherSkeletonSkull(WitherSkeletonSkullRotationType rotation)
        {
            return new BlockState
            {
                Id = (uint)BlockId.WitherSkeletonSkull,
                MetaValue = (uint)rotation,
            };
        }

        public static BlockState WitherSkeletonWallSkull(WitherSkeletonWallSkullFacingType facing)
        {
            return new BlockState
            {
                Id = (uint)BlockId.WitherSkeletonWallSkull,
                MetaValue = (uint)facing,
            };
        }

        public static BlockState ZombieHead(ZombieHeadRotationType rotation)
        {
            return new BlockState
            {
                Id = (uint)BlockId.ZombieHead,
                MetaValue = (uint)rotation,
            };
        }

        public static BlockState ZombieWallHead(ZombieWallHeadFacingType facing)
        {
            return new BlockState
            {
                Id = (uint)BlockId.ZombieWallHead,
                MetaValue = (uint)facing,
            };
        }

        public static BlockState PlayerHead(PlayerHeadRotationType rotation)
        {
            return new BlockState
            {
                Id = (uint)BlockId.PlayerHead,
                MetaValue = (uint)rotation,
            };
        }

        public static BlockState PlayerWallHead(PlayerWallHeadFacingType facing)
        {
            return new BlockState
            {
                Id = (uint)BlockId.PlayerWallHead,
                MetaValue = (uint)facing,
            };
        }

        public static BlockState CreeperHead(CreeperHeadRotationType rotation)
        {
            return new BlockState
            {
                Id = (uint)BlockId.CreeperHead,
                MetaValue = (uint)rotation,
            };
        }

        public static BlockState CreeperWallHead(CreeperWallHeadFacingType facing)
        {
            return new BlockState
            {
                Id = (uint)BlockId.CreeperWallHead,
                MetaValue = (uint)facing,
            };
        }

        public static BlockState DragonHead(DragonHeadRotationType rotation)
        {
            return new BlockState
            {
                Id = (uint)BlockId.DragonHead,
                MetaValue = (uint)rotation,
            };
        }

        public static BlockState DragonWallHead(DragonWallHeadFacingType facing)
        {
            return new BlockState
            {
                Id = (uint)BlockId.DragonWallHead,
                MetaValue = (uint)facing,
            };
        }

        public static BlockState Anvil(AnvilFacingType facing)
        {
            return new BlockState
            {
                Id = (uint)BlockId.Anvil,
                MetaValue = (uint)facing,
            };
        }

        public static BlockState ChippedAnvil(ChippedAnvilFacingType facing)
        {
            return new BlockState
            {
                Id = (uint)BlockId.ChippedAnvil,
                MetaValue = (uint)facing,
            };
        }

        public static BlockState DamagedAnvil(DamagedAnvilFacingType facing)
        {
            return new BlockState
            {
                Id = (uint)BlockId.DamagedAnvil,
                MetaValue = (uint)facing,
            };
        }

        public static BlockState TrappedChest(TrappedChestFacingType facing, TrappedChestTypeType type, TrappedChestWaterloggedType waterlogged)
        {
            return new BlockState
            {
                Id = (uint)BlockId.TrappedChest,
                MetaValue = (((uint)facing) * 3 + (uint)type) * 2 + (uint)waterlogged,
            };
        }

        public static BlockState LightWeightedPressurePlate(LightWeightedPressurePlatePowerType power)
        {
            return new BlockState
            {
                Id = (uint)BlockId.LightWeightedPressurePlate,
                MetaValue = (uint)power,
            };
        }

        public static BlockState HeavyWeightedPressurePlate(HeavyWeightedPressurePlatePowerType power)
        {
            return new BlockState
            {
                Id = (uint)BlockId.HeavyWeightedPressurePlate,
                MetaValue = (uint)power,
            };
        }

        public static BlockState Comparator(ComparatorFacingType facing, ComparatorModeType mode, ComparatorPoweredType powered)
        {
            return new BlockState
            {
                Id = (uint)BlockId.Comparator,
                MetaValue = (((uint)facing) * 2 + (uint)mode) * 2 + (uint)powered,
            };
        }

        public static BlockState DaylightDetector(DaylightDetectorInvertedType inverted, DaylightDetectorPowerType power)
        {
            return new BlockState
            {
                Id = (uint)BlockId.DaylightDetector,
                MetaValue = ((uint)inverted) * 16 + (uint)power,
            };
        }

        public static BlockState RedstoneBlock()
        {
            return new BlockState
            {
                Id = (uint)BlockId.RedstoneBlock,
                MetaValue = 0,
            };
        }

        public static BlockState NetherQuartzOre()
        {
            return new BlockState
            {
                Id = (uint)BlockId.NetherQuartzOre,
                MetaValue = 0,
            };
        }

        public static BlockState Hopper(HopperEnabledType enabled, HopperFacingType facing)
        {
            return new BlockState
            {
                Id = (uint)BlockId.Hopper,
                MetaValue = ((uint)enabled) * 5 + (uint)facing,
            };
        }

        public static BlockState QuartzBlock()
        {
            return new BlockState
            {
                Id = (uint)BlockId.QuartzBlock,
                MetaValue = 0,
            };
        }

        public static BlockState ChiseledQuartzBlock()
        {
            return new BlockState
            {
                Id = (uint)BlockId.ChiseledQuartzBlock,
                MetaValue = 0,
            };
        }

        public static BlockState QuartzPillar(QuartzPillarAxisType axis)
        {
            return new BlockState
            {
                Id = (uint)BlockId.QuartzPillar,
                MetaValue = (uint)axis,
            };
        }

        public static BlockState QuartzStairs(QuartzStairsFacingType facing, QuartzStairsHalfType half, QuartzStairsShapeType shape, QuartzStairsWaterloggedType waterlogged)
        {
            return new BlockState
            {
                Id = (uint)BlockId.QuartzStairs,
                MetaValue = ((((uint)facing) * 2 + (uint)half) * 5 + (uint)shape) * 2 + (uint)waterlogged,
            };
        }

        public static BlockState ActivatorRail(ActivatorRailPoweredType powered, ActivatorRailShapeType shape)
        {
            return new BlockState
            {
                Id = (uint)BlockId.ActivatorRail,
                MetaValue = ((uint)powered) * 6 + (uint)shape,
            };
        }

        public static BlockState Dropper(DropperFacingType facing, DropperTriggeredType triggered)
        {
            return new BlockState
            {
                Id = (uint)BlockId.Dropper,
                MetaValue = ((uint)facing) * 2 + (uint)triggered,
            };
        }

        public static BlockState WhiteTerracotta()
        {
            return new BlockState
            {
                Id = (uint)BlockId.WhiteTerracotta,
                MetaValue = 0,
            };
        }

        public static BlockState OrangeTerracotta()
        {
            return new BlockState
            {
                Id = (uint)BlockId.OrangeTerracotta,
                MetaValue = 0,
            };
        }

        public static BlockState MagentaTerracotta()
        {
            return new BlockState
            {
                Id = (uint)BlockId.MagentaTerracotta,
                MetaValue = 0,
            };
        }

        public static BlockState LightBlueTerracotta()
        {
            return new BlockState
            {
                Id = (uint)BlockId.LightBlueTerracotta,
                MetaValue = 0,
            };
        }

        public static BlockState YellowTerracotta()
        {
            return new BlockState
            {
                Id = (uint)BlockId.YellowTerracotta,
                MetaValue = 0,
            };
        }

        public static BlockState LimeTerracotta()
        {
            return new BlockState
            {
                Id = (uint)BlockId.LimeTerracotta,
                MetaValue = 0,
            };
        }

        public static BlockState PinkTerracotta()
        {
            return new BlockState
            {
                Id = (uint)BlockId.PinkTerracotta,
                MetaValue = 0,
            };
        }

        public static BlockState GrayTerracotta()
        {
            return new BlockState
            {
                Id = (uint)BlockId.GrayTerracotta,
                MetaValue = 0,
            };
        }

        public static BlockState LightGrayTerracotta()
        {
            return new BlockState
            {
                Id = (uint)BlockId.LightGrayTerracotta,
                MetaValue = 0,
            };
        }

        public static BlockState CyanTerracotta()
        {
            return new BlockState
            {
                Id = (uint)BlockId.CyanTerracotta,
                MetaValue = 0,
            };
        }

        public static BlockState PurpleTerracotta()
        {
            return new BlockState
            {
                Id = (uint)BlockId.PurpleTerracotta,
                MetaValue = 0,
            };
        }

        public static BlockState BlueTerracotta()
        {
            return new BlockState
            {
                Id = (uint)BlockId.BlueTerracotta,
                MetaValue = 0,
            };
        }

        public static BlockState BrownTerracotta()
        {
            return new BlockState
            {
                Id = (uint)BlockId.BrownTerracotta,
                MetaValue = 0,
            };
        }

        public static BlockState GreenTerracotta()
        {
            return new BlockState
            {
                Id = (uint)BlockId.GreenTerracotta,
                MetaValue = 0,
            };
        }

        public static BlockState RedTerracotta()
        {
            return new BlockState
            {
                Id = (uint)BlockId.RedTerracotta,
                MetaValue = 0,
            };
        }

        public static BlockState BlackTerracotta()
        {
            return new BlockState
            {
                Id = (uint)BlockId.BlackTerracotta,
                MetaValue = 0,
            };
        }

        public static BlockState WhiteStainedGlassPane(WhiteStainedGlassPaneEastType east, WhiteStainedGlassPaneNorthType north, WhiteStainedGlassPaneSouthType south, WhiteStainedGlassPaneWaterloggedType waterlogged, WhiteStainedGlassPaneWestType west)
        {
            return new BlockState
            {
                Id = (uint)BlockId.WhiteStainedGlassPane,
                MetaValue = (((((uint)east) * 2 + (uint)north) * 2 + (uint)south) * 2 + (uint)waterlogged) * 2 + (uint)west,
            };
        }

        public static BlockState OrangeStainedGlassPane(OrangeStainedGlassPaneEastType east, OrangeStainedGlassPaneNorthType north, OrangeStainedGlassPaneSouthType south, OrangeStainedGlassPaneWaterloggedType waterlogged, OrangeStainedGlassPaneWestType west)
        {
            return new BlockState
            {
                Id = (uint)BlockId.OrangeStainedGlassPane,
                MetaValue = (((((uint)east) * 2 + (uint)north) * 2 + (uint)south) * 2 + (uint)waterlogged) * 2 + (uint)west,
            };
        }

        public static BlockState MagentaStainedGlassPane(MagentaStainedGlassPaneEastType east, MagentaStainedGlassPaneNorthType north, MagentaStainedGlassPaneSouthType south, MagentaStainedGlassPaneWaterloggedType waterlogged, MagentaStainedGlassPaneWestType west)
        {
            return new BlockState
            {
                Id = (uint)BlockId.MagentaStainedGlassPane,
                MetaValue = (((((uint)east) * 2 + (uint)north) * 2 + (uint)south) * 2 + (uint)waterlogged) * 2 + (uint)west,
            };
        }

        public static BlockState LightBlueStainedGlassPane(LightBlueStainedGlassPaneEastType east, LightBlueStainedGlassPaneNorthType north, LightBlueStainedGlassPaneSouthType south, LightBlueStainedGlassPaneWaterloggedType waterlogged, LightBlueStainedGlassPaneWestType west)
        {
            return new BlockState
            {
                Id = (uint)BlockId.LightBlueStainedGlassPane,
                MetaValue = (((((uint)east) * 2 + (uint)north) * 2 + (uint)south) * 2 + (uint)waterlogged) * 2 + (uint)west,
            };
        }

        public static BlockState YellowStainedGlassPane(YellowStainedGlassPaneEastType east, YellowStainedGlassPaneNorthType north, YellowStainedGlassPaneSouthType south, YellowStainedGlassPaneWaterloggedType waterlogged, YellowStainedGlassPaneWestType west)
        {
            return new BlockState
            {
                Id = (uint)BlockId.YellowStainedGlassPane,
                MetaValue = (((((uint)east) * 2 + (uint)north) * 2 + (uint)south) * 2 + (uint)waterlogged) * 2 + (uint)west,
            };
        }

        public static BlockState LimeStainedGlassPane(LimeStainedGlassPaneEastType east, LimeStainedGlassPaneNorthType north, LimeStainedGlassPaneSouthType south, LimeStainedGlassPaneWaterloggedType waterlogged, LimeStainedGlassPaneWestType west)
        {
            return new BlockState
            {
                Id = (uint)BlockId.LimeStainedGlassPane,
                MetaValue = (((((uint)east) * 2 + (uint)north) * 2 + (uint)south) * 2 + (uint)waterlogged) * 2 + (uint)west,
            };
        }

        public static BlockState PinkStainedGlassPane(PinkStainedGlassPaneEastType east, PinkStainedGlassPaneNorthType north, PinkStainedGlassPaneSouthType south, PinkStainedGlassPaneWaterloggedType waterlogged, PinkStainedGlassPaneWestType west)
        {
            return new BlockState
            {
                Id = (uint)BlockId.PinkStainedGlassPane,
                MetaValue = (((((uint)east) * 2 + (uint)north) * 2 + (uint)south) * 2 + (uint)waterlogged) * 2 + (uint)west,
            };
        }

        public static BlockState GrayStainedGlassPane(GrayStainedGlassPaneEastType east, GrayStainedGlassPaneNorthType north, GrayStainedGlassPaneSouthType south, GrayStainedGlassPaneWaterloggedType waterlogged, GrayStainedGlassPaneWestType west)
        {
            return new BlockState
            {
                Id = (uint)BlockId.GrayStainedGlassPane,
                MetaValue = (((((uint)east) * 2 + (uint)north) * 2 + (uint)south) * 2 + (uint)waterlogged) * 2 + (uint)west,
            };
        }

        public static BlockState LightGrayStainedGlassPane(LightGrayStainedGlassPaneEastType east, LightGrayStainedGlassPaneNorthType north, LightGrayStainedGlassPaneSouthType south, LightGrayStainedGlassPaneWaterloggedType waterlogged, LightGrayStainedGlassPaneWestType west)
        {
            return new BlockState
            {
                Id = (uint)BlockId.LightGrayStainedGlassPane,
                MetaValue = (((((uint)east) * 2 + (uint)north) * 2 + (uint)south) * 2 + (uint)waterlogged) * 2 + (uint)west,
            };
        }

        public static BlockState CyanStainedGlassPane(CyanStainedGlassPaneEastType east, CyanStainedGlassPaneNorthType north, CyanStainedGlassPaneSouthType south, CyanStainedGlassPaneWaterloggedType waterlogged, CyanStainedGlassPaneWestType west)
        {
            return new BlockState
            {
                Id = (uint)BlockId.CyanStainedGlassPane,
                MetaValue = (((((uint)east) * 2 + (uint)north) * 2 + (uint)south) * 2 + (uint)waterlogged) * 2 + (uint)west,
            };
        }

        public static BlockState PurpleStainedGlassPane(PurpleStainedGlassPaneEastType east, PurpleStainedGlassPaneNorthType north, PurpleStainedGlassPaneSouthType south, PurpleStainedGlassPaneWaterloggedType waterlogged, PurpleStainedGlassPaneWestType west)
        {
            return new BlockState
            {
                Id = (uint)BlockId.PurpleStainedGlassPane,
                MetaValue = (((((uint)east) * 2 + (uint)north) * 2 + (uint)south) * 2 + (uint)waterlogged) * 2 + (uint)west,
            };
        }

        public static BlockState BlueStainedGlassPane(BlueStainedGlassPaneEastType east, BlueStainedGlassPaneNorthType north, BlueStainedGlassPaneSouthType south, BlueStainedGlassPaneWaterloggedType waterlogged, BlueStainedGlassPaneWestType west)
        {
            return new BlockState
            {
                Id = (uint)BlockId.BlueStainedGlassPane,
                MetaValue = (((((uint)east) * 2 + (uint)north) * 2 + (uint)south) * 2 + (uint)waterlogged) * 2 + (uint)west,
            };
        }

        public static BlockState BrownStainedGlassPane(BrownStainedGlassPaneEastType east, BrownStainedGlassPaneNorthType north, BrownStainedGlassPaneSouthType south, BrownStainedGlassPaneWaterloggedType waterlogged, BrownStainedGlassPaneWestType west)
        {
            return new BlockState
            {
                Id = (uint)BlockId.BrownStainedGlassPane,
                MetaValue = (((((uint)east) * 2 + (uint)north) * 2 + (uint)south) * 2 + (uint)waterlogged) * 2 + (uint)west,
            };
        }

        public static BlockState GreenStainedGlassPane(GreenStainedGlassPaneEastType east, GreenStainedGlassPaneNorthType north, GreenStainedGlassPaneSouthType south, GreenStainedGlassPaneWaterloggedType waterlogged, GreenStainedGlassPaneWestType west)
        {
            return new BlockState
            {
                Id = (uint)BlockId.GreenStainedGlassPane,
                MetaValue = (((((uint)east) * 2 + (uint)north) * 2 + (uint)south) * 2 + (uint)waterlogged) * 2 + (uint)west,
            };
        }

        public static BlockState RedStainedGlassPane(RedStainedGlassPaneEastType east, RedStainedGlassPaneNorthType north, RedStainedGlassPaneSouthType south, RedStainedGlassPaneWaterloggedType waterlogged, RedStainedGlassPaneWestType west)
        {
            return new BlockState
            {
                Id = (uint)BlockId.RedStainedGlassPane,
                MetaValue = (((((uint)east) * 2 + (uint)north) * 2 + (uint)south) * 2 + (uint)waterlogged) * 2 + (uint)west,
            };
        }

        public static BlockState BlackStainedGlassPane(BlackStainedGlassPaneEastType east, BlackStainedGlassPaneNorthType north, BlackStainedGlassPaneSouthType south, BlackStainedGlassPaneWaterloggedType waterlogged, BlackStainedGlassPaneWestType west)
        {
            return new BlockState
            {
                Id = (uint)BlockId.BlackStainedGlassPane,
                MetaValue = (((((uint)east) * 2 + (uint)north) * 2 + (uint)south) * 2 + (uint)waterlogged) * 2 + (uint)west,
            };
        }

        public static BlockState AcaciaStairs(AcaciaStairsFacingType facing, AcaciaStairsHalfType half, AcaciaStairsShapeType shape, AcaciaStairsWaterloggedType waterlogged)
        {
            return new BlockState
            {
                Id = (uint)BlockId.AcaciaStairs,
                MetaValue = ((((uint)facing) * 2 + (uint)half) * 5 + (uint)shape) * 2 + (uint)waterlogged,
            };
        }

        public static BlockState DarkOakStairs(DarkOakStairsFacingType facing, DarkOakStairsHalfType half, DarkOakStairsShapeType shape, DarkOakStairsWaterloggedType waterlogged)
        {
            return new BlockState
            {
                Id = (uint)BlockId.DarkOakStairs,
                MetaValue = ((((uint)facing) * 2 + (uint)half) * 5 + (uint)shape) * 2 + (uint)waterlogged,
            };
        }

        public static BlockState SlimeBlock()
        {
            return new BlockState
            {
                Id = (uint)BlockId.SlimeBlock,
                MetaValue = 0,
            };
        }

        public static BlockState Barrier()
        {
            return new BlockState
            {
                Id = (uint)BlockId.Barrier,
                MetaValue = 0,
            };
        }

        public static BlockState IronTrapdoor(IronTrapdoorFacingType facing, IronTrapdoorHalfType half, IronTrapdoorOpenType open, IronTrapdoorPoweredType powered, IronTrapdoorWaterloggedType waterlogged)
        {
            return new BlockState
            {
                Id = (uint)BlockId.IronTrapdoor,
                MetaValue = (((((uint)facing) * 2 + (uint)half) * 2 + (uint)open) * 2 + (uint)powered) * 2 + (uint)waterlogged,
            };
        }

        public static BlockState Prismarine()
        {
            return new BlockState
            {
                Id = (uint)BlockId.Prismarine,
                MetaValue = 0,
            };
        }

        public static BlockState PrismarineBricks()
        {
            return new BlockState
            {
                Id = (uint)BlockId.PrismarineBricks,
                MetaValue = 0,
            };
        }

        public static BlockState DarkPrismarine()
        {
            return new BlockState
            {
                Id = (uint)BlockId.DarkPrismarine,
                MetaValue = 0,
            };
        }

        public static BlockState PrismarineStairs(PrismarineStairsFacingType facing, PrismarineStairsHalfType half, PrismarineStairsShapeType shape, PrismarineStairsWaterloggedType waterlogged)
        {
            return new BlockState
            {
                Id = (uint)BlockId.PrismarineStairs,
                MetaValue = ((((uint)facing) * 2 + (uint)half) * 5 + (uint)shape) * 2 + (uint)waterlogged,
            };
        }

        public static BlockState PrismarineBrickStairs(PrismarineBrickStairsFacingType facing, PrismarineBrickStairsHalfType half, PrismarineBrickStairsShapeType shape, PrismarineBrickStairsWaterloggedType waterlogged)
        {
            return new BlockState
            {
                Id = (uint)BlockId.PrismarineBrickStairs,
                MetaValue = ((((uint)facing) * 2 + (uint)half) * 5 + (uint)shape) * 2 + (uint)waterlogged,
            };
        }

        public static BlockState DarkPrismarineStairs(DarkPrismarineStairsFacingType facing, DarkPrismarineStairsHalfType half, DarkPrismarineStairsShapeType shape, DarkPrismarineStairsWaterloggedType waterlogged)
        {
            return new BlockState
            {
                Id = (uint)BlockId.DarkPrismarineStairs,
                MetaValue = ((((uint)facing) * 2 + (uint)half) * 5 + (uint)shape) * 2 + (uint)waterlogged,
            };
        }

        public static BlockState PrismarineSlab(PrismarineSlabTypeType type, PrismarineSlabWaterloggedType waterlogged)
        {
            return new BlockState
            {
                Id = (uint)BlockId.PrismarineSlab,
                MetaValue = ((uint)type) * 2 + (uint)waterlogged,
            };
        }

        public static BlockState PrismarineBrickSlab(PrismarineBrickSlabTypeType type, PrismarineBrickSlabWaterloggedType waterlogged)
        {
            return new BlockState
            {
                Id = (uint)BlockId.PrismarineBrickSlab,
                MetaValue = ((uint)type) * 2 + (uint)waterlogged,
            };
        }

        public static BlockState DarkPrismarineSlab(DarkPrismarineSlabTypeType type, DarkPrismarineSlabWaterloggedType waterlogged)
        {
            return new BlockState
            {
                Id = (uint)BlockId.DarkPrismarineSlab,
                MetaValue = ((uint)type) * 2 + (uint)waterlogged,
            };
        }

        public static BlockState SeaLantern()
        {
            return new BlockState
            {
                Id = (uint)BlockId.SeaLantern,
                MetaValue = 0,
            };
        }

        public static BlockState HayBlock(HayBlockAxisType axis)
        {
            return new BlockState
            {
                Id = (uint)BlockId.HayBlock,
                MetaValue = (uint)axis,
            };
        }

        public static BlockState WhiteCarpet()
        {
            return new BlockState
            {
                Id = (uint)BlockId.WhiteCarpet,
                MetaValue = 0,
            };
        }

        public static BlockState OrangeCarpet()
        {
            return new BlockState
            {
                Id = (uint)BlockId.OrangeCarpet,
                MetaValue = 0,
            };
        }

        public static BlockState MagentaCarpet()
        {
            return new BlockState
            {
                Id = (uint)BlockId.MagentaCarpet,
                MetaValue = 0,
            };
        }

        public static BlockState LightBlueCarpet()
        {
            return new BlockState
            {
                Id = (uint)BlockId.LightBlueCarpet,
                MetaValue = 0,
            };
        }

        public static BlockState YellowCarpet()
        {
            return new BlockState
            {
                Id = (uint)BlockId.YellowCarpet,
                MetaValue = 0,
            };
        }

        public static BlockState LimeCarpet()
        {
            return new BlockState
            {
                Id = (uint)BlockId.LimeCarpet,
                MetaValue = 0,
            };
        }

        public static BlockState PinkCarpet()
        {
            return new BlockState
            {
                Id = (uint)BlockId.PinkCarpet,
                MetaValue = 0,
            };
        }

        public static BlockState GrayCarpet()
        {
            return new BlockState
            {
                Id = (uint)BlockId.GrayCarpet,
                MetaValue = 0,
            };
        }

        public static BlockState LightGrayCarpet()
        {
            return new BlockState
            {
                Id = (uint)BlockId.LightGrayCarpet,
                MetaValue = 0,
            };
        }

        public static BlockState CyanCarpet()
        {
            return new BlockState
            {
                Id = (uint)BlockId.CyanCarpet,
                MetaValue = 0,
            };
        }

        public static BlockState PurpleCarpet()
        {
            return new BlockState
            {
                Id = (uint)BlockId.PurpleCarpet,
                MetaValue = 0,
            };
        }

        public static BlockState BlueCarpet()
        {
            return new BlockState
            {
                Id = (uint)BlockId.BlueCarpet,
                MetaValue = 0,
            };
        }

        public static BlockState BrownCarpet()
        {
            return new BlockState
            {
                Id = (uint)BlockId.BrownCarpet,
                MetaValue = 0,
            };
        }

        public static BlockState GreenCarpet()
        {
            return new BlockState
            {
                Id = (uint)BlockId.GreenCarpet,
                MetaValue = 0,
            };
        }

        public static BlockState RedCarpet()
        {
            return new BlockState
            {
                Id = (uint)BlockId.RedCarpet,
                MetaValue = 0,
            };
        }

        public static BlockState BlackCarpet()
        {
            return new BlockState
            {
                Id = (uint)BlockId.BlackCarpet,
                MetaValue = 0,
            };
        }

        public static BlockState Terracotta()
        {
            return new BlockState
            {
                Id = (uint)BlockId.Terracotta,
                MetaValue = 0,
            };
        }

        public static BlockState CoalBlock()
        {
            return new BlockState
            {
                Id = (uint)BlockId.CoalBlock,
                MetaValue = 0,
            };
        }

        public static BlockState PackedIce()
        {
            return new BlockState
            {
                Id = (uint)BlockId.PackedIce,
                MetaValue = 0,
            };
        }

        public static BlockState Sunflower(SunflowerHalfType half)
        {
            return new BlockState
            {
                Id = (uint)BlockId.Sunflower,
                MetaValue = (uint)half,
            };
        }

        public static BlockState Lilac(LilacHalfType half)
        {
            return new BlockState
            {
                Id = (uint)BlockId.Lilac,
                MetaValue = (uint)half,
            };
        }

        public static BlockState RoseBush(RoseBushHalfType half)
        {
            return new BlockState
            {
                Id = (uint)BlockId.RoseBush,
                MetaValue = (uint)half,
            };
        }

        public static BlockState Peony(PeonyHalfType half)
        {
            return new BlockState
            {
                Id = (uint)BlockId.Peony,
                MetaValue = (uint)half,
            };
        }

        public static BlockState TallGrass(TallGrassHalfType half)
        {
            return new BlockState
            {
                Id = (uint)BlockId.TallGrass,
                MetaValue = (uint)half,
            };
        }

        public static BlockState LargeFern(LargeFernHalfType half)
        {
            return new BlockState
            {
                Id = (uint)BlockId.LargeFern,
                MetaValue = (uint)half,
            };
        }

        public static BlockState WhiteBanner(WhiteBannerRotationType rotation)
        {
            return new BlockState
            {
                Id = (uint)BlockId.WhiteBanner,
                MetaValue = (uint)rotation,
            };
        }

        public static BlockState OrangeBanner(OrangeBannerRotationType rotation)
        {
            return new BlockState
            {
                Id = (uint)BlockId.OrangeBanner,
                MetaValue = (uint)rotation,
            };
        }

        public static BlockState MagentaBanner(MagentaBannerRotationType rotation)
        {
            return new BlockState
            {
                Id = (uint)BlockId.MagentaBanner,
                MetaValue = (uint)rotation,
            };
        }

        public static BlockState LightBlueBanner(LightBlueBannerRotationType rotation)
        {
            return new BlockState
            {
                Id = (uint)BlockId.LightBlueBanner,
                MetaValue = (uint)rotation,
            };
        }

        public static BlockState YellowBanner(YellowBannerRotationType rotation)
        {
            return new BlockState
            {
                Id = (uint)BlockId.YellowBanner,
                MetaValue = (uint)rotation,
            };
        }

        public static BlockState LimeBanner(LimeBannerRotationType rotation)
        {
            return new BlockState
            {
                Id = (uint)BlockId.LimeBanner,
                MetaValue = (uint)rotation,
            };
        }

        public static BlockState PinkBanner(PinkBannerRotationType rotation)
        {
            return new BlockState
            {
                Id = (uint)BlockId.PinkBanner,
                MetaValue = (uint)rotation,
            };
        }

        public static BlockState GrayBanner(GrayBannerRotationType rotation)
        {
            return new BlockState
            {
                Id = (uint)BlockId.GrayBanner,
                MetaValue = (uint)rotation,
            };
        }

        public static BlockState LightGrayBanner(LightGrayBannerRotationType rotation)
        {
            return new BlockState
            {
                Id = (uint)BlockId.LightGrayBanner,
                MetaValue = (uint)rotation,
            };
        }

        public static BlockState CyanBanner(CyanBannerRotationType rotation)
        {
            return new BlockState
            {
                Id = (uint)BlockId.CyanBanner,
                MetaValue = (uint)rotation,
            };
        }

        public static BlockState PurpleBanner(PurpleBannerRotationType rotation)
        {
            return new BlockState
            {
                Id = (uint)BlockId.PurpleBanner,
                MetaValue = (uint)rotation,
            };
        }

        public static BlockState BlueBanner(BlueBannerRotationType rotation)
        {
            return new BlockState
            {
                Id = (uint)BlockId.BlueBanner,
                MetaValue = (uint)rotation,
            };
        }

        public static BlockState BrownBanner(BrownBannerRotationType rotation)
        {
            return new BlockState
            {
                Id = (uint)BlockId.BrownBanner,
                MetaValue = (uint)rotation,
            };
        }

        public static BlockState GreenBanner(GreenBannerRotationType rotation)
        {
            return new BlockState
            {
                Id = (uint)BlockId.GreenBanner,
                MetaValue = (uint)rotation,
            };
        }

        public static BlockState RedBanner(RedBannerRotationType rotation)
        {
            return new BlockState
            {
                Id = (uint)BlockId.RedBanner,
                MetaValue = (uint)rotation,
            };
        }

        public static BlockState BlackBanner(BlackBannerRotationType rotation)
        {
            return new BlockState
            {
                Id = (uint)BlockId.BlackBanner,
                MetaValue = (uint)rotation,
            };
        }

        public static BlockState WhiteWallBanner(WhiteWallBannerFacingType facing)
        {
            return new BlockState
            {
                Id = (uint)BlockId.WhiteWallBanner,
                MetaValue = (uint)facing,
            };
        }

        public static BlockState OrangeWallBanner(OrangeWallBannerFacingType facing)
        {
            return new BlockState
            {
                Id = (uint)BlockId.OrangeWallBanner,
                MetaValue = (uint)facing,
            };
        }

        public static BlockState MagentaWallBanner(MagentaWallBannerFacingType facing)
        {
            return new BlockState
            {
                Id = (uint)BlockId.MagentaWallBanner,
                MetaValue = (uint)facing,
            };
        }

        public static BlockState LightBlueWallBanner(LightBlueWallBannerFacingType facing)
        {
            return new BlockState
            {
                Id = (uint)BlockId.LightBlueWallBanner,
                MetaValue = (uint)facing,
            };
        }

        public static BlockState YellowWallBanner(YellowWallBannerFacingType facing)
        {
            return new BlockState
            {
                Id = (uint)BlockId.YellowWallBanner,
                MetaValue = (uint)facing,
            };
        }

        public static BlockState LimeWallBanner(LimeWallBannerFacingType facing)
        {
            return new BlockState
            {
                Id = (uint)BlockId.LimeWallBanner,
                MetaValue = (uint)facing,
            };
        }

        public static BlockState PinkWallBanner(PinkWallBannerFacingType facing)
        {
            return new BlockState
            {
                Id = (uint)BlockId.PinkWallBanner,
                MetaValue = (uint)facing,
            };
        }

        public static BlockState GrayWallBanner(GrayWallBannerFacingType facing)
        {
            return new BlockState
            {
                Id = (uint)BlockId.GrayWallBanner,
                MetaValue = (uint)facing,
            };
        }

        public static BlockState LightGrayWallBanner(LightGrayWallBannerFacingType facing)
        {
            return new BlockState
            {
                Id = (uint)BlockId.LightGrayWallBanner,
                MetaValue = (uint)facing,
            };
        }

        public static BlockState CyanWallBanner(CyanWallBannerFacingType facing)
        {
            return new BlockState
            {
                Id = (uint)BlockId.CyanWallBanner,
                MetaValue = (uint)facing,
            };
        }

        public static BlockState PurpleWallBanner(PurpleWallBannerFacingType facing)
        {
            return new BlockState
            {
                Id = (uint)BlockId.PurpleWallBanner,
                MetaValue = (uint)facing,
            };
        }

        public static BlockState BlueWallBanner(BlueWallBannerFacingType facing)
        {
            return new BlockState
            {
                Id = (uint)BlockId.BlueWallBanner,
                MetaValue = (uint)facing,
            };
        }

        public static BlockState BrownWallBanner(BrownWallBannerFacingType facing)
        {
            return new BlockState
            {
                Id = (uint)BlockId.BrownWallBanner,
                MetaValue = (uint)facing,
            };
        }

        public static BlockState GreenWallBanner(GreenWallBannerFacingType facing)
        {
            return new BlockState
            {
                Id = (uint)BlockId.GreenWallBanner,
                MetaValue = (uint)facing,
            };
        }

        public static BlockState RedWallBanner(RedWallBannerFacingType facing)
        {
            return new BlockState
            {
                Id = (uint)BlockId.RedWallBanner,
                MetaValue = (uint)facing,
            };
        }

        public static BlockState BlackWallBanner(BlackWallBannerFacingType facing)
        {
            return new BlockState
            {
                Id = (uint)BlockId.BlackWallBanner,
                MetaValue = (uint)facing,
            };
        }

        public static BlockState RedSandstone()
        {
            return new BlockState
            {
                Id = (uint)BlockId.RedSandstone,
                MetaValue = 0,
            };
        }

        public static BlockState ChiseledRedSandstone()
        {
            return new BlockState
            {
                Id = (uint)BlockId.ChiseledRedSandstone,
                MetaValue = 0,
            };
        }

        public static BlockState CutRedSandstone()
        {
            return new BlockState
            {
                Id = (uint)BlockId.CutRedSandstone,
                MetaValue = 0,
            };
        }

        public static BlockState RedSandstoneStairs(RedSandstoneStairsFacingType facing, RedSandstoneStairsHalfType half, RedSandstoneStairsShapeType shape, RedSandstoneStairsWaterloggedType waterlogged)
        {
            return new BlockState
            {
                Id = (uint)BlockId.RedSandstoneStairs,
                MetaValue = ((((uint)facing) * 2 + (uint)half) * 5 + (uint)shape) * 2 + (uint)waterlogged,
            };
        }

        public static BlockState OakSlab(OakSlabTypeType type, OakSlabWaterloggedType waterlogged)
        {
            return new BlockState
            {
                Id = (uint)BlockId.OakSlab,
                MetaValue = ((uint)type) * 2 + (uint)waterlogged,
            };
        }

        public static BlockState SpruceSlab(SpruceSlabTypeType type, SpruceSlabWaterloggedType waterlogged)
        {
            return new BlockState
            {
                Id = (uint)BlockId.SpruceSlab,
                MetaValue = ((uint)type) * 2 + (uint)waterlogged,
            };
        }

        public static BlockState BirchSlab(BirchSlabTypeType type, BirchSlabWaterloggedType waterlogged)
        {
            return new BlockState
            {
                Id = (uint)BlockId.BirchSlab,
                MetaValue = ((uint)type) * 2 + (uint)waterlogged,
            };
        }

        public static BlockState JungleSlab(JungleSlabTypeType type, JungleSlabWaterloggedType waterlogged)
        {
            return new BlockState
            {
                Id = (uint)BlockId.JungleSlab,
                MetaValue = ((uint)type) * 2 + (uint)waterlogged,
            };
        }

        public static BlockState AcaciaSlab(AcaciaSlabTypeType type, AcaciaSlabWaterloggedType waterlogged)
        {
            return new BlockState
            {
                Id = (uint)BlockId.AcaciaSlab,
                MetaValue = ((uint)type) * 2 + (uint)waterlogged,
            };
        }

        public static BlockState DarkOakSlab(DarkOakSlabTypeType type, DarkOakSlabWaterloggedType waterlogged)
        {
            return new BlockState
            {
                Id = (uint)BlockId.DarkOakSlab,
                MetaValue = ((uint)type) * 2 + (uint)waterlogged,
            };
        }

        public static BlockState StoneSlab(StoneSlabTypeType type, StoneSlabWaterloggedType waterlogged)
        {
            return new BlockState
            {
                Id = (uint)BlockId.StoneSlab,
                MetaValue = ((uint)type) * 2 + (uint)waterlogged,
            };
        }

        public static BlockState SmoothStoneSlab(SmoothStoneSlabTypeType type, SmoothStoneSlabWaterloggedType waterlogged)
        {
            return new BlockState
            {
                Id = (uint)BlockId.SmoothStoneSlab,
                MetaValue = ((uint)type) * 2 + (uint)waterlogged,
            };
        }

        public static BlockState SandstoneSlab(SandstoneSlabTypeType type, SandstoneSlabWaterloggedType waterlogged)
        {
            return new BlockState
            {
                Id = (uint)BlockId.SandstoneSlab,
                MetaValue = ((uint)type) * 2 + (uint)waterlogged,
            };
        }

        public static BlockState CutSandstoneSlab(CutSandstoneSlabTypeType type, CutSandstoneSlabWaterloggedType waterlogged)
        {
            return new BlockState
            {
                Id = (uint)BlockId.CutSandstoneSlab,
                MetaValue = ((uint)type) * 2 + (uint)waterlogged,
            };
        }

        public static BlockState PetrifiedOakSlab(PetrifiedOakSlabTypeType type, PetrifiedOakSlabWaterloggedType waterlogged)
        {
            return new BlockState
            {
                Id = (uint)BlockId.PetrifiedOakSlab,
                MetaValue = ((uint)type) * 2 + (uint)waterlogged,
            };
        }

        public static BlockState CobblestoneSlab(CobblestoneSlabTypeType type, CobblestoneSlabWaterloggedType waterlogged)
        {
            return new BlockState
            {
                Id = (uint)BlockId.CobblestoneSlab,
                MetaValue = ((uint)type) * 2 + (uint)waterlogged,
            };
        }

        public static BlockState BrickSlab(BrickSlabTypeType type, BrickSlabWaterloggedType waterlogged)
        {
            return new BlockState
            {
                Id = (uint)BlockId.BrickSlab,
                MetaValue = ((uint)type) * 2 + (uint)waterlogged,
            };
        }

        public static BlockState StoneBrickSlab(StoneBrickSlabTypeType type, StoneBrickSlabWaterloggedType waterlogged)
        {
            return new BlockState
            {
                Id = (uint)BlockId.StoneBrickSlab,
                MetaValue = ((uint)type) * 2 + (uint)waterlogged,
            };
        }

        public static BlockState NetherBrickSlab(NetherBrickSlabTypeType type, NetherBrickSlabWaterloggedType waterlogged)
        {
            return new BlockState
            {
                Id = (uint)BlockId.NetherBrickSlab,
                MetaValue = ((uint)type) * 2 + (uint)waterlogged,
            };
        }

        public static BlockState QuartzSlab(QuartzSlabTypeType type, QuartzSlabWaterloggedType waterlogged)
        {
            return new BlockState
            {
                Id = (uint)BlockId.QuartzSlab,
                MetaValue = ((uint)type) * 2 + (uint)waterlogged,
            };
        }

        public static BlockState RedSandstoneSlab(RedSandstoneSlabTypeType type, RedSandstoneSlabWaterloggedType waterlogged)
        {
            return new BlockState
            {
                Id = (uint)BlockId.RedSandstoneSlab,
                MetaValue = ((uint)type) * 2 + (uint)waterlogged,
            };
        }

        public static BlockState CutRedSandstoneSlab(CutRedSandstoneSlabTypeType type, CutRedSandstoneSlabWaterloggedType waterlogged)
        {
            return new BlockState
            {
                Id = (uint)BlockId.CutRedSandstoneSlab,
                MetaValue = ((uint)type) * 2 + (uint)waterlogged,
            };
        }

        public static BlockState PurpurSlab(PurpurSlabTypeType type, PurpurSlabWaterloggedType waterlogged)
        {
            return new BlockState
            {
                Id = (uint)BlockId.PurpurSlab,
                MetaValue = ((uint)type) * 2 + (uint)waterlogged,
            };
        }

        public static BlockState SmoothStone()
        {
            return new BlockState
            {
                Id = (uint)BlockId.SmoothStone,
                MetaValue = 0,
            };
        }

        public static BlockState SmoothSandstone()
        {
            return new BlockState
            {
                Id = (uint)BlockId.SmoothSandstone,
                MetaValue = 0,
            };
        }

        public static BlockState SmoothQuartz()
        {
            return new BlockState
            {
                Id = (uint)BlockId.SmoothQuartz,
                MetaValue = 0,
            };
        }

        public static BlockState SmoothRedSandstone()
        {
            return new BlockState
            {
                Id = (uint)BlockId.SmoothRedSandstone,
                MetaValue = 0,
            };
        }

        public static BlockState SpruceFenceGate(SpruceFenceGateFacingType facing, SpruceFenceGateInWallType inWall, SpruceFenceGateOpenType open, SpruceFenceGatePoweredType powered)
        {
            return new BlockState
            {
                Id = (uint)BlockId.SpruceFenceGate,
                MetaValue = ((((uint)facing) * 2 + (uint)inWall) * 2 + (uint)open) * 2 + (uint)powered,
            };
        }

        public static BlockState BirchFenceGate(BirchFenceGateFacingType facing, BirchFenceGateInWallType inWall, BirchFenceGateOpenType open, BirchFenceGatePoweredType powered)
        {
            return new BlockState
            {
                Id = (uint)BlockId.BirchFenceGate,
                MetaValue = ((((uint)facing) * 2 + (uint)inWall) * 2 + (uint)open) * 2 + (uint)powered,
            };
        }

        public static BlockState JungleFenceGate(JungleFenceGateFacingType facing, JungleFenceGateInWallType inWall, JungleFenceGateOpenType open, JungleFenceGatePoweredType powered)
        {
            return new BlockState
            {
                Id = (uint)BlockId.JungleFenceGate,
                MetaValue = ((((uint)facing) * 2 + (uint)inWall) * 2 + (uint)open) * 2 + (uint)powered,
            };
        }

        public static BlockState AcaciaFenceGate(AcaciaFenceGateFacingType facing, AcaciaFenceGateInWallType inWall, AcaciaFenceGateOpenType open, AcaciaFenceGatePoweredType powered)
        {
            return new BlockState
            {
                Id = (uint)BlockId.AcaciaFenceGate,
                MetaValue = ((((uint)facing) * 2 + (uint)inWall) * 2 + (uint)open) * 2 + (uint)powered,
            };
        }

        public static BlockState DarkOakFenceGate(DarkOakFenceGateFacingType facing, DarkOakFenceGateInWallType inWall, DarkOakFenceGateOpenType open, DarkOakFenceGatePoweredType powered)
        {
            return new BlockState
            {
                Id = (uint)BlockId.DarkOakFenceGate,
                MetaValue = ((((uint)facing) * 2 + (uint)inWall) * 2 + (uint)open) * 2 + (uint)powered,
            };
        }

        public static BlockState SpruceFence(SpruceFenceEastType east, SpruceFenceNorthType north, SpruceFenceSouthType south, SpruceFenceWaterloggedType waterlogged, SpruceFenceWestType west)
        {
            return new BlockState
            {
                Id = (uint)BlockId.SpruceFence,
                MetaValue = (((((uint)east) * 2 + (uint)north) * 2 + (uint)south) * 2 + (uint)waterlogged) * 2 + (uint)west,
            };
        }

        public static BlockState BirchFence(BirchFenceEastType east, BirchFenceNorthType north, BirchFenceSouthType south, BirchFenceWaterloggedType waterlogged, BirchFenceWestType west)
        {
            return new BlockState
            {
                Id = (uint)BlockId.BirchFence,
                MetaValue = (((((uint)east) * 2 + (uint)north) * 2 + (uint)south) * 2 + (uint)waterlogged) * 2 + (uint)west,
            };
        }

        public static BlockState JungleFence(JungleFenceEastType east, JungleFenceNorthType north, JungleFenceSouthType south, JungleFenceWaterloggedType waterlogged, JungleFenceWestType west)
        {
            return new BlockState
            {
                Id = (uint)BlockId.JungleFence,
                MetaValue = (((((uint)east) * 2 + (uint)north) * 2 + (uint)south) * 2 + (uint)waterlogged) * 2 + (uint)west,
            };
        }

        public static BlockState AcaciaFence(AcaciaFenceEastType east, AcaciaFenceNorthType north, AcaciaFenceSouthType south, AcaciaFenceWaterloggedType waterlogged, AcaciaFenceWestType west)
        {
            return new BlockState
            {
                Id = (uint)BlockId.AcaciaFence,
                MetaValue = (((((uint)east) * 2 + (uint)north) * 2 + (uint)south) * 2 + (uint)waterlogged) * 2 + (uint)west,
            };
        }

        public static BlockState DarkOakFence(DarkOakFenceEastType east, DarkOakFenceNorthType north, DarkOakFenceSouthType south, DarkOakFenceWaterloggedType waterlogged, DarkOakFenceWestType west)
        {
            return new BlockState
            {
                Id = (uint)BlockId.DarkOakFence,
                MetaValue = (((((uint)east) * 2 + (uint)north) * 2 + (uint)south) * 2 + (uint)waterlogged) * 2 + (uint)west,
            };
        }

        public static BlockState SpruceDoor(SpruceDoorFacingType facing, SpruceDoorHalfType half, SpruceDoorHingeType hinge, SpruceDoorOpenType open, SpruceDoorPoweredType powered)
        {
            return new BlockState
            {
                Id = (uint)BlockId.SpruceDoor,
                MetaValue = (((((uint)facing) * 2 + (uint)half) * 2 + (uint)hinge) * 2 + (uint)open) * 2 + (uint)powered,
            };
        }

        public static BlockState BirchDoor(BirchDoorFacingType facing, BirchDoorHalfType half, BirchDoorHingeType hinge, BirchDoorOpenType open, BirchDoorPoweredType powered)
        {
            return new BlockState
            {
                Id = (uint)BlockId.BirchDoor,
                MetaValue = (((((uint)facing) * 2 + (uint)half) * 2 + (uint)hinge) * 2 + (uint)open) * 2 + (uint)powered,
            };
        }

        public static BlockState JungleDoor(JungleDoorFacingType facing, JungleDoorHalfType half, JungleDoorHingeType hinge, JungleDoorOpenType open, JungleDoorPoweredType powered)
        {
            return new BlockState
            {
                Id = (uint)BlockId.JungleDoor,
                MetaValue = (((((uint)facing) * 2 + (uint)half) * 2 + (uint)hinge) * 2 + (uint)open) * 2 + (uint)powered,
            };
        }

        public static BlockState AcaciaDoor(AcaciaDoorFacingType facing, AcaciaDoorHalfType half, AcaciaDoorHingeType hinge, AcaciaDoorOpenType open, AcaciaDoorPoweredType powered)
        {
            return new BlockState
            {
                Id = (uint)BlockId.AcaciaDoor,
                MetaValue = (((((uint)facing) * 2 + (uint)half) * 2 + (uint)hinge) * 2 + (uint)open) * 2 + (uint)powered,
            };
        }

        public static BlockState DarkOakDoor(DarkOakDoorFacingType facing, DarkOakDoorHalfType half, DarkOakDoorHingeType hinge, DarkOakDoorOpenType open, DarkOakDoorPoweredType powered)
        {
            return new BlockState
            {
                Id = (uint)BlockId.DarkOakDoor,
                MetaValue = (((((uint)facing) * 2 + (uint)half) * 2 + (uint)hinge) * 2 + (uint)open) * 2 + (uint)powered,
            };
        }

        public static BlockState EndRod(EndRodFacingType facing)
        {
            return new BlockState
            {
                Id = (uint)BlockId.EndRod,
                MetaValue = (uint)facing,
            };
        }

        public static BlockState ChorusPlant(ChorusPlantDownType down, ChorusPlantEastType east, ChorusPlantNorthType north, ChorusPlantSouthType south, ChorusPlantUpType up, ChorusPlantWestType west)
        {
            return new BlockState
            {
                Id = (uint)BlockId.ChorusPlant,
                MetaValue = ((((((uint)down) * 2 + (uint)east) * 2 + (uint)north) * 2 + (uint)south) * 2 + (uint)up) * 2 + (uint)west,
            };
        }

        public static BlockState ChorusFlower(ChorusFlowerAgeType age)
        {
            return new BlockState
            {
                Id = (uint)BlockId.ChorusFlower,
                MetaValue = (uint)age,
            };
        }

        public static BlockState PurpurBlock()
        {
            return new BlockState
            {
                Id = (uint)BlockId.PurpurBlock,
                MetaValue = 0,
            };
        }

        public static BlockState PurpurPillar(PurpurPillarAxisType axis)
        {
            return new BlockState
            {
                Id = (uint)BlockId.PurpurPillar,
                MetaValue = (uint)axis,
            };
        }

        public static BlockState PurpurStairs(PurpurStairsFacingType facing, PurpurStairsHalfType half, PurpurStairsShapeType shape, PurpurStairsWaterloggedType waterlogged)
        {
            return new BlockState
            {
                Id = (uint)BlockId.PurpurStairs,
                MetaValue = ((((uint)facing) * 2 + (uint)half) * 5 + (uint)shape) * 2 + (uint)waterlogged,
            };
        }

        public static BlockState EndStoneBricks()
        {
            return new BlockState
            {
                Id = (uint)BlockId.EndStoneBricks,
                MetaValue = 0,
            };
        }

        public static BlockState Beetroots(BeetrootsAgeType age)
        {
            return new BlockState
            {
                Id = (uint)BlockId.Beetroots,
                MetaValue = (uint)age,
            };
        }

        public static BlockState GrassPath()
        {
            return new BlockState
            {
                Id = (uint)BlockId.GrassPath,
                MetaValue = 0,
            };
        }

        public static BlockState EndGateway()
        {
            return new BlockState
            {
                Id = (uint)BlockId.EndGateway,
                MetaValue = 0,
            };
        }

        public static BlockState RepeatingCommandBlock(RepeatingCommandBlockConditionalType conditional, RepeatingCommandBlockFacingType facing)
        {
            return new BlockState
            {
                Id = (uint)BlockId.RepeatingCommandBlock,
                MetaValue = ((uint)conditional) * 6 + (uint)facing,
            };
        }

        public static BlockState ChainCommandBlock(ChainCommandBlockConditionalType conditional, ChainCommandBlockFacingType facing)
        {
            return new BlockState
            {
                Id = (uint)BlockId.ChainCommandBlock,
                MetaValue = ((uint)conditional) * 6 + (uint)facing,
            };
        }

        public static BlockState FrostedIce(FrostedIceAgeType age)
        {
            return new BlockState
            {
                Id = (uint)BlockId.FrostedIce,
                MetaValue = (uint)age,
            };
        }

        public static BlockState MagmaBlock()
        {
            return new BlockState
            {
                Id = (uint)BlockId.MagmaBlock,
                MetaValue = 0,
            };
        }

        public static BlockState NetherWartBlock()
        {
            return new BlockState
            {
                Id = (uint)BlockId.NetherWartBlock,
                MetaValue = 0,
            };
        }

        public static BlockState RedNetherBricks()
        {
            return new BlockState
            {
                Id = (uint)BlockId.RedNetherBricks,
                MetaValue = 0,
            };
        }

        public static BlockState BoneBlock(BoneBlockAxisType axis)
        {
            return new BlockState
            {
                Id = (uint)BlockId.BoneBlock,
                MetaValue = (uint)axis,
            };
        }

        public static BlockState StructureVoid()
        {
            return new BlockState
            {
                Id = (uint)BlockId.StructureVoid,
                MetaValue = 0,
            };
        }

        public static BlockState Observer(ObserverFacingType facing, ObserverPoweredType powered)
        {
            return new BlockState
            {
                Id = (uint)BlockId.Observer,
                MetaValue = ((uint)facing) * 2 + (uint)powered,
            };
        }

        public static BlockState ShulkerBox(ShulkerBoxFacingType facing)
        {
            return new BlockState
            {
                Id = (uint)BlockId.ShulkerBox,
                MetaValue = (uint)facing,
            };
        }

        public static BlockState WhiteShulkerBox(WhiteShulkerBoxFacingType facing)
        {
            return new BlockState
            {
                Id = (uint)BlockId.WhiteShulkerBox,
                MetaValue = (uint)facing,
            };
        }

        public static BlockState OrangeShulkerBox(OrangeShulkerBoxFacingType facing)
        {
            return new BlockState
            {
                Id = (uint)BlockId.OrangeShulkerBox,
                MetaValue = (uint)facing,
            };
        }

        public static BlockState MagentaShulkerBox(MagentaShulkerBoxFacingType facing)
        {
            return new BlockState
            {
                Id = (uint)BlockId.MagentaShulkerBox,
                MetaValue = (uint)facing,
            };
        }

        public static BlockState LightBlueShulkerBox(LightBlueShulkerBoxFacingType facing)
        {
            return new BlockState
            {
                Id = (uint)BlockId.LightBlueShulkerBox,
                MetaValue = (uint)facing,
            };
        }

        public static BlockState YellowShulkerBox(YellowShulkerBoxFacingType facing)
        {
            return new BlockState
            {
                Id = (uint)BlockId.YellowShulkerBox,
                MetaValue = (uint)facing,
            };
        }

        public static BlockState LimeShulkerBox(LimeShulkerBoxFacingType facing)
        {
            return new BlockState
            {
                Id = (uint)BlockId.LimeShulkerBox,
                MetaValue = (uint)facing,
            };
        }

        public static BlockState PinkShulkerBox(PinkShulkerBoxFacingType facing)
        {
            return new BlockState
            {
                Id = (uint)BlockId.PinkShulkerBox,
                MetaValue = (uint)facing,
            };
        }

        public static BlockState GrayShulkerBox(GrayShulkerBoxFacingType facing)
        {
            return new BlockState
            {
                Id = (uint)BlockId.GrayShulkerBox,
                MetaValue = (uint)facing,
            };
        }

        public static BlockState LightGrayShulkerBox(LightGrayShulkerBoxFacingType facing)
        {
            return new BlockState
            {
                Id = (uint)BlockId.LightGrayShulkerBox,
                MetaValue = (uint)facing,
            };
        }

        public static BlockState CyanShulkerBox(CyanShulkerBoxFacingType facing)
        {
            return new BlockState
            {
                Id = (uint)BlockId.CyanShulkerBox,
                MetaValue = (uint)facing,
            };
        }

        public static BlockState PurpleShulkerBox(PurpleShulkerBoxFacingType facing)
        {
            return new BlockState
            {
                Id = (uint)BlockId.PurpleShulkerBox,
                MetaValue = (uint)facing,
            };
        }

        public static BlockState BlueShulkerBox(BlueShulkerBoxFacingType facing)
        {
            return new BlockState
            {
                Id = (uint)BlockId.BlueShulkerBox,
                MetaValue = (uint)facing,
            };
        }

        public static BlockState BrownShulkerBox(BrownShulkerBoxFacingType facing)
        {
            return new BlockState
            {
                Id = (uint)BlockId.BrownShulkerBox,
                MetaValue = (uint)facing,
            };
        }

        public static BlockState GreenShulkerBox(GreenShulkerBoxFacingType facing)
        {
            return new BlockState
            {
                Id = (uint)BlockId.GreenShulkerBox,
                MetaValue = (uint)facing,
            };
        }

        public static BlockState RedShulkerBox(RedShulkerBoxFacingType facing)
        {
            return new BlockState
            {
                Id = (uint)BlockId.RedShulkerBox,
                MetaValue = (uint)facing,
            };
        }

        public static BlockState BlackShulkerBox(BlackShulkerBoxFacingType facing)
        {
            return new BlockState
            {
                Id = (uint)BlockId.BlackShulkerBox,
                MetaValue = (uint)facing,
            };
        }

        public static BlockState WhiteGlazedTerracotta(WhiteGlazedTerracottaFacingType facing)
        {
            return new BlockState
            {
                Id = (uint)BlockId.WhiteGlazedTerracotta,
                MetaValue = (uint)facing,
            };
        }

        public static BlockState OrangeGlazedTerracotta(OrangeGlazedTerracottaFacingType facing)
        {
            return new BlockState
            {
                Id = (uint)BlockId.OrangeGlazedTerracotta,
                MetaValue = (uint)facing,
            };
        }

        public static BlockState MagentaGlazedTerracotta(MagentaGlazedTerracottaFacingType facing)
        {
            return new BlockState
            {
                Id = (uint)BlockId.MagentaGlazedTerracotta,
                MetaValue = (uint)facing,
            };
        }

        public static BlockState LightBlueGlazedTerracotta(LightBlueGlazedTerracottaFacingType facing)
        {
            return new BlockState
            {
                Id = (uint)BlockId.LightBlueGlazedTerracotta,
                MetaValue = (uint)facing,
            };
        }

        public static BlockState YellowGlazedTerracotta(YellowGlazedTerracottaFacingType facing)
        {
            return new BlockState
            {
                Id = (uint)BlockId.YellowGlazedTerracotta,
                MetaValue = (uint)facing,
            };
        }

        public static BlockState LimeGlazedTerracotta(LimeGlazedTerracottaFacingType facing)
        {
            return new BlockState
            {
                Id = (uint)BlockId.LimeGlazedTerracotta,
                MetaValue = (uint)facing,
            };
        }

        public static BlockState PinkGlazedTerracotta(PinkGlazedTerracottaFacingType facing)
        {
            return new BlockState
            {
                Id = (uint)BlockId.PinkGlazedTerracotta,
                MetaValue = (uint)facing,
            };
        }

        public static BlockState GrayGlazedTerracotta(GrayGlazedTerracottaFacingType facing)
        {
            return new BlockState
            {
                Id = (uint)BlockId.GrayGlazedTerracotta,
                MetaValue = (uint)facing,
            };
        }

        public static BlockState LightGrayGlazedTerracotta(LightGrayGlazedTerracottaFacingType facing)
        {
            return new BlockState
            {
                Id = (uint)BlockId.LightGrayGlazedTerracotta,
                MetaValue = (uint)facing,
            };
        }

        public static BlockState CyanGlazedTerracotta(CyanGlazedTerracottaFacingType facing)
        {
            return new BlockState
            {
                Id = (uint)BlockId.CyanGlazedTerracotta,
                MetaValue = (uint)facing,
            };
        }

        public static BlockState PurpleGlazedTerracotta(PurpleGlazedTerracottaFacingType facing)
        {
            return new BlockState
            {
                Id = (uint)BlockId.PurpleGlazedTerracotta,
                MetaValue = (uint)facing,
            };
        }

        public static BlockState BlueGlazedTerracotta(BlueGlazedTerracottaFacingType facing)
        {
            return new BlockState
            {
                Id = (uint)BlockId.BlueGlazedTerracotta,
                MetaValue = (uint)facing,
            };
        }

        public static BlockState BrownGlazedTerracotta(BrownGlazedTerracottaFacingType facing)
        {
            return new BlockState
            {
                Id = (uint)BlockId.BrownGlazedTerracotta,
                MetaValue = (uint)facing,
            };
        }

        public static BlockState GreenGlazedTerracotta(GreenGlazedTerracottaFacingType facing)
        {
            return new BlockState
            {
                Id = (uint)BlockId.GreenGlazedTerracotta,
                MetaValue = (uint)facing,
            };
        }

        public static BlockState RedGlazedTerracotta(RedGlazedTerracottaFacingType facing)
        {
            return new BlockState
            {
                Id = (uint)BlockId.RedGlazedTerracotta,
                MetaValue = (uint)facing,
            };
        }

        public static BlockState BlackGlazedTerracotta(BlackGlazedTerracottaFacingType facing)
        {
            return new BlockState
            {
                Id = (uint)BlockId.BlackGlazedTerracotta,
                MetaValue = (uint)facing,
            };
        }

        public static BlockState WhiteConcrete()
        {
            return new BlockState
            {
                Id = (uint)BlockId.WhiteConcrete,
                MetaValue = 0,
            };
        }

        public static BlockState OrangeConcrete()
        {
            return new BlockState
            {
                Id = (uint)BlockId.OrangeConcrete,
                MetaValue = 0,
            };
        }

        public static BlockState MagentaConcrete()
        {
            return new BlockState
            {
                Id = (uint)BlockId.MagentaConcrete,
                MetaValue = 0,
            };
        }

        public static BlockState LightBlueConcrete()
        {
            return new BlockState
            {
                Id = (uint)BlockId.LightBlueConcrete,
                MetaValue = 0,
            };
        }

        public static BlockState YellowConcrete()
        {
            return new BlockState
            {
                Id = (uint)BlockId.YellowConcrete,
                MetaValue = 0,
            };
        }

        public static BlockState LimeConcrete()
        {
            return new BlockState
            {
                Id = (uint)BlockId.LimeConcrete,
                MetaValue = 0,
            };
        }

        public static BlockState PinkConcrete()
        {
            return new BlockState
            {
                Id = (uint)BlockId.PinkConcrete,
                MetaValue = 0,
            };
        }

        public static BlockState GrayConcrete()
        {
            return new BlockState
            {
                Id = (uint)BlockId.GrayConcrete,
                MetaValue = 0,
            };
        }

        public static BlockState LightGrayConcrete()
        {
            return new BlockState
            {
                Id = (uint)BlockId.LightGrayConcrete,
                MetaValue = 0,
            };
        }

        public static BlockState CyanConcrete()
        {
            return new BlockState
            {
                Id = (uint)BlockId.CyanConcrete,
                MetaValue = 0,
            };
        }

        public static BlockState PurpleConcrete()
        {
            return new BlockState
            {
                Id = (uint)BlockId.PurpleConcrete,
                MetaValue = 0,
            };
        }

        public static BlockState BlueConcrete()
        {
            return new BlockState
            {
                Id = (uint)BlockId.BlueConcrete,
                MetaValue = 0,
            };
        }

        public static BlockState BrownConcrete()
        {
            return new BlockState
            {
                Id = (uint)BlockId.BrownConcrete,
                MetaValue = 0,
            };
        }

        public static BlockState GreenConcrete()
        {
            return new BlockState
            {
                Id = (uint)BlockId.GreenConcrete,
                MetaValue = 0,
            };
        }

        public static BlockState RedConcrete()
        {
            return new BlockState
            {
                Id = (uint)BlockId.RedConcrete,
                MetaValue = 0,
            };
        }

        public static BlockState BlackConcrete()
        {
            return new BlockState
            {
                Id = (uint)BlockId.BlackConcrete,
                MetaValue = 0,
            };
        }

        public static BlockState WhiteConcretePowder()
        {
            return new BlockState
            {
                Id = (uint)BlockId.WhiteConcretePowder,
                MetaValue = 0,
            };
        }

        public static BlockState OrangeConcretePowder()
        {
            return new BlockState
            {
                Id = (uint)BlockId.OrangeConcretePowder,
                MetaValue = 0,
            };
        }

        public static BlockState MagentaConcretePowder()
        {
            return new BlockState
            {
                Id = (uint)BlockId.MagentaConcretePowder,
                MetaValue = 0,
            };
        }

        public static BlockState LightBlueConcretePowder()
        {
            return new BlockState
            {
                Id = (uint)BlockId.LightBlueConcretePowder,
                MetaValue = 0,
            };
        }

        public static BlockState YellowConcretePowder()
        {
            return new BlockState
            {
                Id = (uint)BlockId.YellowConcretePowder,
                MetaValue = 0,
            };
        }

        public static BlockState LimeConcretePowder()
        {
            return new BlockState
            {
                Id = (uint)BlockId.LimeConcretePowder,
                MetaValue = 0,
            };
        }

        public static BlockState PinkConcretePowder()
        {
            return new BlockState
            {
                Id = (uint)BlockId.PinkConcretePowder,
                MetaValue = 0,
            };
        }

        public static BlockState GrayConcretePowder()
        {
            return new BlockState
            {
                Id = (uint)BlockId.GrayConcretePowder,
                MetaValue = 0,
            };
        }

        public static BlockState LightGrayConcretePowder()
        {
            return new BlockState
            {
                Id = (uint)BlockId.LightGrayConcretePowder,
                MetaValue = 0,
            };
        }

        public static BlockState CyanConcretePowder()
        {
            return new BlockState
            {
                Id = (uint)BlockId.CyanConcretePowder,
                MetaValue = 0,
            };
        }

        public static BlockState PurpleConcretePowder()
        {
            return new BlockState
            {
                Id = (uint)BlockId.PurpleConcretePowder,
                MetaValue = 0,
            };
        }

        public static BlockState BlueConcretePowder()
        {
            return new BlockState
            {
                Id = (uint)BlockId.BlueConcretePowder,
                MetaValue = 0,
            };
        }

        public static BlockState BrownConcretePowder()
        {
            return new BlockState
            {
                Id = (uint)BlockId.BrownConcretePowder,
                MetaValue = 0,
            };
        }

        public static BlockState GreenConcretePowder()
        {
            return new BlockState
            {
                Id = (uint)BlockId.GreenConcretePowder,
                MetaValue = 0,
            };
        }

        public static BlockState RedConcretePowder()
        {
            return new BlockState
            {
                Id = (uint)BlockId.RedConcretePowder,
                MetaValue = 0,
            };
        }

        public static BlockState BlackConcretePowder()
        {
            return new BlockState
            {
                Id = (uint)BlockId.BlackConcretePowder,
                MetaValue = 0,
            };
        }

        public static BlockState Kelp(KelpAgeType age)
        {
            return new BlockState
            {
                Id = (uint)BlockId.Kelp,
                MetaValue = (uint)age,
            };
        }

        public static BlockState KelpPlant()
        {
            return new BlockState
            {
                Id = (uint)BlockId.KelpPlant,
                MetaValue = 0,
            };
        }

        public static BlockState DriedKelpBlock()
        {
            return new BlockState
            {
                Id = (uint)BlockId.DriedKelpBlock,
                MetaValue = 0,
            };
        }

        public static BlockState TurtleEgg(TurtleEggEggsType eggs, TurtleEggHatchType hatch)
        {
            return new BlockState
            {
                Id = (uint)BlockId.TurtleEgg,
                MetaValue = ((uint)eggs) * 3 + (uint)hatch,
            };
        }

        public static BlockState DeadTubeCoralBlock()
        {
            return new BlockState
            {
                Id = (uint)BlockId.DeadTubeCoralBlock,
                MetaValue = 0,
            };
        }

        public static BlockState DeadBrainCoralBlock()
        {
            return new BlockState
            {
                Id = (uint)BlockId.DeadBrainCoralBlock,
                MetaValue = 0,
            };
        }

        public static BlockState DeadBubbleCoralBlock()
        {
            return new BlockState
            {
                Id = (uint)BlockId.DeadBubbleCoralBlock,
                MetaValue = 0,
            };
        }

        public static BlockState DeadFireCoralBlock()
        {
            return new BlockState
            {
                Id = (uint)BlockId.DeadFireCoralBlock,
                MetaValue = 0,
            };
        }

        public static BlockState DeadHornCoralBlock()
        {
            return new BlockState
            {
                Id = (uint)BlockId.DeadHornCoralBlock,
                MetaValue = 0,
            };
        }

        public static BlockState TubeCoralBlock()
        {
            return new BlockState
            {
                Id = (uint)BlockId.TubeCoralBlock,
                MetaValue = 0,
            };
        }

        public static BlockState BrainCoralBlock()
        {
            return new BlockState
            {
                Id = (uint)BlockId.BrainCoralBlock,
                MetaValue = 0,
            };
        }

        public static BlockState BubbleCoralBlock()
        {
            return new BlockState
            {
                Id = (uint)BlockId.BubbleCoralBlock,
                MetaValue = 0,
            };
        }

        public static BlockState FireCoralBlock()
        {
            return new BlockState
            {
                Id = (uint)BlockId.FireCoralBlock,
                MetaValue = 0,
            };
        }

        public static BlockState HornCoralBlock()
        {
            return new BlockState
            {
                Id = (uint)BlockId.HornCoralBlock,
                MetaValue = 0,
            };
        }

        public static BlockState DeadTubeCoral(DeadTubeCoralWaterloggedType waterlogged)
        {
            return new BlockState
            {
                Id = (uint)BlockId.DeadTubeCoral,
                MetaValue = (uint)waterlogged,
            };
        }

        public static BlockState DeadBrainCoral(DeadBrainCoralWaterloggedType waterlogged)
        {
            return new BlockState
            {
                Id = (uint)BlockId.DeadBrainCoral,
                MetaValue = (uint)waterlogged,
            };
        }

        public static BlockState DeadBubbleCoral(DeadBubbleCoralWaterloggedType waterlogged)
        {
            return new BlockState
            {
                Id = (uint)BlockId.DeadBubbleCoral,
                MetaValue = (uint)waterlogged,
            };
        }

        public static BlockState DeadFireCoral(DeadFireCoralWaterloggedType waterlogged)
        {
            return new BlockState
            {
                Id = (uint)BlockId.DeadFireCoral,
                MetaValue = (uint)waterlogged,
            };
        }

        public static BlockState DeadHornCoral(DeadHornCoralWaterloggedType waterlogged)
        {
            return new BlockState
            {
                Id = (uint)BlockId.DeadHornCoral,
                MetaValue = (uint)waterlogged,
            };
        }

        public static BlockState TubeCoral(TubeCoralWaterloggedType waterlogged)
        {
            return new BlockState
            {
                Id = (uint)BlockId.TubeCoral,
                MetaValue = (uint)waterlogged,
            };
        }

        public static BlockState BrainCoral(BrainCoralWaterloggedType waterlogged)
        {
            return new BlockState
            {
                Id = (uint)BlockId.BrainCoral,
                MetaValue = (uint)waterlogged,
            };
        }

        public static BlockState BubbleCoral(BubbleCoralWaterloggedType waterlogged)
        {
            return new BlockState
            {
                Id = (uint)BlockId.BubbleCoral,
                MetaValue = (uint)waterlogged,
            };
        }

        public static BlockState FireCoral(FireCoralWaterloggedType waterlogged)
        {
            return new BlockState
            {
                Id = (uint)BlockId.FireCoral,
                MetaValue = (uint)waterlogged,
            };
        }

        public static BlockState HornCoral(HornCoralWaterloggedType waterlogged)
        {
            return new BlockState
            {
                Id = (uint)BlockId.HornCoral,
                MetaValue = (uint)waterlogged,
            };
        }

        public static BlockState DeadTubeCoralFan(DeadTubeCoralFanWaterloggedType waterlogged)
        {
            return new BlockState
            {
                Id = (uint)BlockId.DeadTubeCoralFan,
                MetaValue = (uint)waterlogged,
            };
        }

        public static BlockState DeadBrainCoralFan(DeadBrainCoralFanWaterloggedType waterlogged)
        {
            return new BlockState
            {
                Id = (uint)BlockId.DeadBrainCoralFan,
                MetaValue = (uint)waterlogged,
            };
        }

        public static BlockState DeadBubbleCoralFan(DeadBubbleCoralFanWaterloggedType waterlogged)
        {
            return new BlockState
            {
                Id = (uint)BlockId.DeadBubbleCoralFan,
                MetaValue = (uint)waterlogged,
            };
        }

        public static BlockState DeadFireCoralFan(DeadFireCoralFanWaterloggedType waterlogged)
        {
            return new BlockState
            {
                Id = (uint)BlockId.DeadFireCoralFan,
                MetaValue = (uint)waterlogged,
            };
        }

        public static BlockState DeadHornCoralFan(DeadHornCoralFanWaterloggedType waterlogged)
        {
            return new BlockState
            {
                Id = (uint)BlockId.DeadHornCoralFan,
                MetaValue = (uint)waterlogged,
            };
        }

        public static BlockState TubeCoralFan(TubeCoralFanWaterloggedType waterlogged)
        {
            return new BlockState
            {
                Id = (uint)BlockId.TubeCoralFan,
                MetaValue = (uint)waterlogged,
            };
        }

        public static BlockState BrainCoralFan(BrainCoralFanWaterloggedType waterlogged)
        {
            return new BlockState
            {
                Id = (uint)BlockId.BrainCoralFan,
                MetaValue = (uint)waterlogged,
            };
        }

        public static BlockState BubbleCoralFan(BubbleCoralFanWaterloggedType waterlogged)
        {
            return new BlockState
            {
                Id = (uint)BlockId.BubbleCoralFan,
                MetaValue = (uint)waterlogged,
            };
        }

        public static BlockState FireCoralFan(FireCoralFanWaterloggedType waterlogged)
        {
            return new BlockState
            {
                Id = (uint)BlockId.FireCoralFan,
                MetaValue = (uint)waterlogged,
            };
        }

        public static BlockState HornCoralFan(HornCoralFanWaterloggedType waterlogged)
        {
            return new BlockState
            {
                Id = (uint)BlockId.HornCoralFan,
                MetaValue = (uint)waterlogged,
            };
        }

        public static BlockState DeadTubeCoralWallFan(DeadTubeCoralWallFanFacingType facing, DeadTubeCoralWallFanWaterloggedType waterlogged)
        {
            return new BlockState
            {
                Id = (uint)BlockId.DeadTubeCoralWallFan,
                MetaValue = ((uint)facing) * 2 + (uint)waterlogged,
            };
        }

        public static BlockState DeadBrainCoralWallFan(DeadBrainCoralWallFanFacingType facing, DeadBrainCoralWallFanWaterloggedType waterlogged)
        {
            return new BlockState
            {
                Id = (uint)BlockId.DeadBrainCoralWallFan,
                MetaValue = ((uint)facing) * 2 + (uint)waterlogged,
            };
        }

        public static BlockState DeadBubbleCoralWallFan(DeadBubbleCoralWallFanFacingType facing, DeadBubbleCoralWallFanWaterloggedType waterlogged)
        {
            return new BlockState
            {
                Id = (uint)BlockId.DeadBubbleCoralWallFan,
                MetaValue = ((uint)facing) * 2 + (uint)waterlogged,
            };
        }

        public static BlockState DeadFireCoralWallFan(DeadFireCoralWallFanFacingType facing, DeadFireCoralWallFanWaterloggedType waterlogged)
        {
            return new BlockState
            {
                Id = (uint)BlockId.DeadFireCoralWallFan,
                MetaValue = ((uint)facing) * 2 + (uint)waterlogged,
            };
        }

        public static BlockState DeadHornCoralWallFan(DeadHornCoralWallFanFacingType facing, DeadHornCoralWallFanWaterloggedType waterlogged)
        {
            return new BlockState
            {
                Id = (uint)BlockId.DeadHornCoralWallFan,
                MetaValue = ((uint)facing) * 2 + (uint)waterlogged,
            };
        }

        public static BlockState TubeCoralWallFan(TubeCoralWallFanFacingType facing, TubeCoralWallFanWaterloggedType waterlogged)
        {
            return new BlockState
            {
                Id = (uint)BlockId.TubeCoralWallFan,
                MetaValue = ((uint)facing) * 2 + (uint)waterlogged,
            };
        }

        public static BlockState BrainCoralWallFan(BrainCoralWallFanFacingType facing, BrainCoralWallFanWaterloggedType waterlogged)
        {
            return new BlockState
            {
                Id = (uint)BlockId.BrainCoralWallFan,
                MetaValue = ((uint)facing) * 2 + (uint)waterlogged,
            };
        }

        public static BlockState BubbleCoralWallFan(BubbleCoralWallFanFacingType facing, BubbleCoralWallFanWaterloggedType waterlogged)
        {
            return new BlockState
            {
                Id = (uint)BlockId.BubbleCoralWallFan,
                MetaValue = ((uint)facing) * 2 + (uint)waterlogged,
            };
        }

        public static BlockState FireCoralWallFan(FireCoralWallFanFacingType facing, FireCoralWallFanWaterloggedType waterlogged)
        {
            return new BlockState
            {
                Id = (uint)BlockId.FireCoralWallFan,
                MetaValue = ((uint)facing) * 2 + (uint)waterlogged,
            };
        }

        public static BlockState HornCoralWallFan(HornCoralWallFanFacingType facing, HornCoralWallFanWaterloggedType waterlogged)
        {
            return new BlockState
            {
                Id = (uint)BlockId.HornCoralWallFan,
                MetaValue = ((uint)facing) * 2 + (uint)waterlogged,
            };
        }

        public static BlockState SeaPickle(SeaPicklePicklesType pickles, SeaPickleWaterloggedType waterlogged)
        {
            return new BlockState
            {
                Id = (uint)BlockId.SeaPickle,
                MetaValue = ((uint)pickles) * 2 + (uint)waterlogged,
            };
        }

        public static BlockState BlueIce()
        {
            return new BlockState
            {
                Id = (uint)BlockId.BlueIce,
                MetaValue = 0,
            };
        }

        public static BlockState Conduit(ConduitWaterloggedType waterlogged)
        {
            return new BlockState
            {
                Id = (uint)BlockId.Conduit,
                MetaValue = (uint)waterlogged,
            };
        }

        public static BlockState BambooSapling()
        {
            return new BlockState
            {
                Id = (uint)BlockId.BambooSapling,
                MetaValue = 0,
            };
        }

        public static BlockState Bamboo(BambooAgeType age, BambooLeavesType leaves, BambooStageType stage)
        {
            return new BlockState
            {
                Id = (uint)BlockId.Bamboo,
                MetaValue = (((uint)age) * 3 + (uint)leaves) * 2 + (uint)stage,
            };
        }

        public static BlockState PottedBamboo()
        {
            return new BlockState
            {
                Id = (uint)BlockId.PottedBamboo,
                MetaValue = 0,
            };
        }

        public static BlockState VoidAir()
        {
            return new BlockState
            {
                Id = (uint)BlockId.VoidAir,
                MetaValue = 0,
            };
        }

        public static BlockState CaveAir()
        {
            return new BlockState
            {
                Id = (uint)BlockId.CaveAir,
                MetaValue = 0,
            };
        }

        public static BlockState BubbleColumn(BubbleColumnDragType drag)
        {
            return new BlockState
            {
                Id = (uint)BlockId.BubbleColumn,
                MetaValue = (uint)drag,
            };
        }

        public static BlockState PolishedGraniteStairs(PolishedGraniteStairsFacingType facing, PolishedGraniteStairsHalfType half, PolishedGraniteStairsShapeType shape, PolishedGraniteStairsWaterloggedType waterlogged)
        {
            return new BlockState
            {
                Id = (uint)BlockId.PolishedGraniteStairs,
                MetaValue = ((((uint)facing) * 2 + (uint)half) * 5 + (uint)shape) * 2 + (uint)waterlogged,
            };
        }

        public static BlockState SmoothRedSandstoneStairs(SmoothRedSandstoneStairsFacingType facing, SmoothRedSandstoneStairsHalfType half, SmoothRedSandstoneStairsShapeType shape, SmoothRedSandstoneStairsWaterloggedType waterlogged)
        {
            return new BlockState
            {
                Id = (uint)BlockId.SmoothRedSandstoneStairs,
                MetaValue = ((((uint)facing) * 2 + (uint)half) * 5 + (uint)shape) * 2 + (uint)waterlogged,
            };
        }

        public static BlockState MossyStoneBrickStairs(MossyStoneBrickStairsFacingType facing, MossyStoneBrickStairsHalfType half, MossyStoneBrickStairsShapeType shape, MossyStoneBrickStairsWaterloggedType waterlogged)
        {
            return new BlockState
            {
                Id = (uint)BlockId.MossyStoneBrickStairs,
                MetaValue = ((((uint)facing) * 2 + (uint)half) * 5 + (uint)shape) * 2 + (uint)waterlogged,
            };
        }

        public static BlockState PolishedDioriteStairs(PolishedDioriteStairsFacingType facing, PolishedDioriteStairsHalfType half, PolishedDioriteStairsShapeType shape, PolishedDioriteStairsWaterloggedType waterlogged)
        {
            return new BlockState
            {
                Id = (uint)BlockId.PolishedDioriteStairs,
                MetaValue = ((((uint)facing) * 2 + (uint)half) * 5 + (uint)shape) * 2 + (uint)waterlogged,
            };
        }

        public static BlockState MossyCobblestoneStairs(MossyCobblestoneStairsFacingType facing, MossyCobblestoneStairsHalfType half, MossyCobblestoneStairsShapeType shape, MossyCobblestoneStairsWaterloggedType waterlogged)
        {
            return new BlockState
            {
                Id = (uint)BlockId.MossyCobblestoneStairs,
                MetaValue = ((((uint)facing) * 2 + (uint)half) * 5 + (uint)shape) * 2 + (uint)waterlogged,
            };
        }

        public static BlockState EndStoneBrickStairs(EndStoneBrickStairsFacingType facing, EndStoneBrickStairsHalfType half, EndStoneBrickStairsShapeType shape, EndStoneBrickStairsWaterloggedType waterlogged)
        {
            return new BlockState
            {
                Id = (uint)BlockId.EndStoneBrickStairs,
                MetaValue = ((((uint)facing) * 2 + (uint)half) * 5 + (uint)shape) * 2 + (uint)waterlogged,
            };
        }

        public static BlockState StoneStairs(StoneStairsFacingType facing, StoneStairsHalfType half, StoneStairsShapeType shape, StoneStairsWaterloggedType waterlogged)
        {
            return new BlockState
            {
                Id = (uint)BlockId.StoneStairs,
                MetaValue = ((((uint)facing) * 2 + (uint)half) * 5 + (uint)shape) * 2 + (uint)waterlogged,
            };
        }

        public static BlockState SmoothSandstoneStairs(SmoothSandstoneStairsFacingType facing, SmoothSandstoneStairsHalfType half, SmoothSandstoneStairsShapeType shape, SmoothSandstoneStairsWaterloggedType waterlogged)
        {
            return new BlockState
            {
                Id = (uint)BlockId.SmoothSandstoneStairs,
                MetaValue = ((((uint)facing) * 2 + (uint)half) * 5 + (uint)shape) * 2 + (uint)waterlogged,
            };
        }

        public static BlockState SmoothQuartzStairs(SmoothQuartzStairsFacingType facing, SmoothQuartzStairsHalfType half, SmoothQuartzStairsShapeType shape, SmoothQuartzStairsWaterloggedType waterlogged)
        {
            return new BlockState
            {
                Id = (uint)BlockId.SmoothQuartzStairs,
                MetaValue = ((((uint)facing) * 2 + (uint)half) * 5 + (uint)shape) * 2 + (uint)waterlogged,
            };
        }

        public static BlockState GraniteStairs(GraniteStairsFacingType facing, GraniteStairsHalfType half, GraniteStairsShapeType shape, GraniteStairsWaterloggedType waterlogged)
        {
            return new BlockState
            {
                Id = (uint)BlockId.GraniteStairs,
                MetaValue = ((((uint)facing) * 2 + (uint)half) * 5 + (uint)shape) * 2 + (uint)waterlogged,
            };
        }

        public static BlockState AndesiteStairs(AndesiteStairsFacingType facing, AndesiteStairsHalfType half, AndesiteStairsShapeType shape, AndesiteStairsWaterloggedType waterlogged)
        {
            return new BlockState
            {
                Id = (uint)BlockId.AndesiteStairs,
                MetaValue = ((((uint)facing) * 2 + (uint)half) * 5 + (uint)shape) * 2 + (uint)waterlogged,
            };
        }

        public static BlockState RedNetherBrickStairs(RedNetherBrickStairsFacingType facing, RedNetherBrickStairsHalfType half, RedNetherBrickStairsShapeType shape, RedNetherBrickStairsWaterloggedType waterlogged)
        {
            return new BlockState
            {
                Id = (uint)BlockId.RedNetherBrickStairs,
                MetaValue = ((((uint)facing) * 2 + (uint)half) * 5 + (uint)shape) * 2 + (uint)waterlogged,
            };
        }

        public static BlockState PolishedAndesiteStairs(PolishedAndesiteStairsFacingType facing, PolishedAndesiteStairsHalfType half, PolishedAndesiteStairsShapeType shape, PolishedAndesiteStairsWaterloggedType waterlogged)
        {
            return new BlockState
            {
                Id = (uint)BlockId.PolishedAndesiteStairs,
                MetaValue = ((((uint)facing) * 2 + (uint)half) * 5 + (uint)shape) * 2 + (uint)waterlogged,
            };
        }

        public static BlockState DioriteStairs(DioriteStairsFacingType facing, DioriteStairsHalfType half, DioriteStairsShapeType shape, DioriteStairsWaterloggedType waterlogged)
        {
            return new BlockState
            {
                Id = (uint)BlockId.DioriteStairs,
                MetaValue = ((((uint)facing) * 2 + (uint)half) * 5 + (uint)shape) * 2 + (uint)waterlogged,
            };
        }

        public static BlockState PolishedGraniteSlab(PolishedGraniteSlabTypeType type, PolishedGraniteSlabWaterloggedType waterlogged)
        {
            return new BlockState
            {
                Id = (uint)BlockId.PolishedGraniteSlab,
                MetaValue = ((uint)type) * 2 + (uint)waterlogged,
            };
        }

        public static BlockState SmoothRedSandstoneSlab(SmoothRedSandstoneSlabTypeType type, SmoothRedSandstoneSlabWaterloggedType waterlogged)
        {
            return new BlockState
            {
                Id = (uint)BlockId.SmoothRedSandstoneSlab,
                MetaValue = ((uint)type) * 2 + (uint)waterlogged,
            };
        }

        public static BlockState MossyStoneBrickSlab(MossyStoneBrickSlabTypeType type, MossyStoneBrickSlabWaterloggedType waterlogged)
        {
            return new BlockState
            {
                Id = (uint)BlockId.MossyStoneBrickSlab,
                MetaValue = ((uint)type) * 2 + (uint)waterlogged,
            };
        }

        public static BlockState PolishedDioriteSlab(PolishedDioriteSlabTypeType type, PolishedDioriteSlabWaterloggedType waterlogged)
        {
            return new BlockState
            {
                Id = (uint)BlockId.PolishedDioriteSlab,
                MetaValue = ((uint)type) * 2 + (uint)waterlogged,
            };
        }

        public static BlockState MossyCobblestoneSlab(MossyCobblestoneSlabTypeType type, MossyCobblestoneSlabWaterloggedType waterlogged)
        {
            return new BlockState
            {
                Id = (uint)BlockId.MossyCobblestoneSlab,
                MetaValue = ((uint)type) * 2 + (uint)waterlogged,
            };
        }

        public static BlockState EndStoneBrickSlab(EndStoneBrickSlabTypeType type, EndStoneBrickSlabWaterloggedType waterlogged)
        {
            return new BlockState
            {
                Id = (uint)BlockId.EndStoneBrickSlab,
                MetaValue = ((uint)type) * 2 + (uint)waterlogged,
            };
        }

        public static BlockState SmoothSandstoneSlab(SmoothSandstoneSlabTypeType type, SmoothSandstoneSlabWaterloggedType waterlogged)
        {
            return new BlockState
            {
                Id = (uint)BlockId.SmoothSandstoneSlab,
                MetaValue = ((uint)type) * 2 + (uint)waterlogged,
            };
        }

        public static BlockState SmoothQuartzSlab(SmoothQuartzSlabTypeType type, SmoothQuartzSlabWaterloggedType waterlogged)
        {
            return new BlockState
            {
                Id = (uint)BlockId.SmoothQuartzSlab,
                MetaValue = ((uint)type) * 2 + (uint)waterlogged,
            };
        }

        public static BlockState GraniteSlab(GraniteSlabTypeType type, GraniteSlabWaterloggedType waterlogged)
        {
            return new BlockState
            {
                Id = (uint)BlockId.GraniteSlab,
                MetaValue = ((uint)type) * 2 + (uint)waterlogged,
            };
        }

        public static BlockState AndesiteSlab(AndesiteSlabTypeType type, AndesiteSlabWaterloggedType waterlogged)
        {
            return new BlockState
            {
                Id = (uint)BlockId.AndesiteSlab,
                MetaValue = ((uint)type) * 2 + (uint)waterlogged,
            };
        }

        public static BlockState RedNetherBrickSlab(RedNetherBrickSlabTypeType type, RedNetherBrickSlabWaterloggedType waterlogged)
        {
            return new BlockState
            {
                Id = (uint)BlockId.RedNetherBrickSlab,
                MetaValue = ((uint)type) * 2 + (uint)waterlogged,
            };
        }

        public static BlockState PolishedAndesiteSlab(PolishedAndesiteSlabTypeType type, PolishedAndesiteSlabWaterloggedType waterlogged)
        {
            return new BlockState
            {
                Id = (uint)BlockId.PolishedAndesiteSlab,
                MetaValue = ((uint)type) * 2 + (uint)waterlogged,
            };
        }

        public static BlockState DioriteSlab(DioriteSlabTypeType type, DioriteSlabWaterloggedType waterlogged)
        {
            return new BlockState
            {
                Id = (uint)BlockId.DioriteSlab,
                MetaValue = ((uint)type) * 2 + (uint)waterlogged,
            };
        }

        public static BlockState BrickWall(BrickWallEastType east, BrickWallNorthType north, BrickWallSouthType south, BrickWallUpType up, BrickWallWaterloggedType waterlogged, BrickWallWestType west)
        {
            return new BlockState
            {
                Id = (uint)BlockId.BrickWall,
                MetaValue = ((((((uint)east) * 2 + (uint)north) * 2 + (uint)south) * 2 + (uint)up) * 2 + (uint)waterlogged) * 2 + (uint)west,
            };
        }

        public static BlockState PrismarineWall(PrismarineWallEastType east, PrismarineWallNorthType north, PrismarineWallSouthType south, PrismarineWallUpType up, PrismarineWallWaterloggedType waterlogged, PrismarineWallWestType west)
        {
            return new BlockState
            {
                Id = (uint)BlockId.PrismarineWall,
                MetaValue = ((((((uint)east) * 2 + (uint)north) * 2 + (uint)south) * 2 + (uint)up) * 2 + (uint)waterlogged) * 2 + (uint)west,
            };
        }

        public static BlockState RedSandstoneWall(RedSandstoneWallEastType east, RedSandstoneWallNorthType north, RedSandstoneWallSouthType south, RedSandstoneWallUpType up, RedSandstoneWallWaterloggedType waterlogged, RedSandstoneWallWestType west)
        {
            return new BlockState
            {
                Id = (uint)BlockId.RedSandstoneWall,
                MetaValue = ((((((uint)east) * 2 + (uint)north) * 2 + (uint)south) * 2 + (uint)up) * 2 + (uint)waterlogged) * 2 + (uint)west,
            };
        }

        public static BlockState MossyStoneBrickWall(MossyStoneBrickWallEastType east, MossyStoneBrickWallNorthType north, MossyStoneBrickWallSouthType south, MossyStoneBrickWallUpType up, MossyStoneBrickWallWaterloggedType waterlogged, MossyStoneBrickWallWestType west)
        {
            return new BlockState
            {
                Id = (uint)BlockId.MossyStoneBrickWall,
                MetaValue = ((((((uint)east) * 2 + (uint)north) * 2 + (uint)south) * 2 + (uint)up) * 2 + (uint)waterlogged) * 2 + (uint)west,
            };
        }

        public static BlockState GraniteWall(GraniteWallEastType east, GraniteWallNorthType north, GraniteWallSouthType south, GraniteWallUpType up, GraniteWallWaterloggedType waterlogged, GraniteWallWestType west)
        {
            return new BlockState
            {
                Id = (uint)BlockId.GraniteWall,
                MetaValue = ((((((uint)east) * 2 + (uint)north) * 2 + (uint)south) * 2 + (uint)up) * 2 + (uint)waterlogged) * 2 + (uint)west,
            };
        }

        public static BlockState StoneBrickWall(StoneBrickWallEastType east, StoneBrickWallNorthType north, StoneBrickWallSouthType south, StoneBrickWallUpType up, StoneBrickWallWaterloggedType waterlogged, StoneBrickWallWestType west)
        {
            return new BlockState
            {
                Id = (uint)BlockId.StoneBrickWall,
                MetaValue = ((((((uint)east) * 2 + (uint)north) * 2 + (uint)south) * 2 + (uint)up) * 2 + (uint)waterlogged) * 2 + (uint)west,
            };
        }

        public static BlockState NetherBrickWall(NetherBrickWallEastType east, NetherBrickWallNorthType north, NetherBrickWallSouthType south, NetherBrickWallUpType up, NetherBrickWallWaterloggedType waterlogged, NetherBrickWallWestType west)
        {
            return new BlockState
            {
                Id = (uint)BlockId.NetherBrickWall,
                MetaValue = ((((((uint)east) * 2 + (uint)north) * 2 + (uint)south) * 2 + (uint)up) * 2 + (uint)waterlogged) * 2 + (uint)west,
            };
        }

        public static BlockState AndesiteWall(AndesiteWallEastType east, AndesiteWallNorthType north, AndesiteWallSouthType south, AndesiteWallUpType up, AndesiteWallWaterloggedType waterlogged, AndesiteWallWestType west)
        {
            return new BlockState
            {
                Id = (uint)BlockId.AndesiteWall,
                MetaValue = ((((((uint)east) * 2 + (uint)north) * 2 + (uint)south) * 2 + (uint)up) * 2 + (uint)waterlogged) * 2 + (uint)west,
            };
        }

        public static BlockState RedNetherBrickWall(RedNetherBrickWallEastType east, RedNetherBrickWallNorthType north, RedNetherBrickWallSouthType south, RedNetherBrickWallUpType up, RedNetherBrickWallWaterloggedType waterlogged, RedNetherBrickWallWestType west)
        {
            return new BlockState
            {
                Id = (uint)BlockId.RedNetherBrickWall,
                MetaValue = ((((((uint)east) * 2 + (uint)north) * 2 + (uint)south) * 2 + (uint)up) * 2 + (uint)waterlogged) * 2 + (uint)west,
            };
        }

        public static BlockState SandstoneWall(SandstoneWallEastType east, SandstoneWallNorthType north, SandstoneWallSouthType south, SandstoneWallUpType up, SandstoneWallWaterloggedType waterlogged, SandstoneWallWestType west)
        {
            return new BlockState
            {
                Id = (uint)BlockId.SandstoneWall,
                MetaValue = ((((((uint)east) * 2 + (uint)north) * 2 + (uint)south) * 2 + (uint)up) * 2 + (uint)waterlogged) * 2 + (uint)west,
            };
        }

        public static BlockState EndStoneBrickWall(EndStoneBrickWallEastType east, EndStoneBrickWallNorthType north, EndStoneBrickWallSouthType south, EndStoneBrickWallUpType up, EndStoneBrickWallWaterloggedType waterlogged, EndStoneBrickWallWestType west)
        {
            return new BlockState
            {
                Id = (uint)BlockId.EndStoneBrickWall,
                MetaValue = ((((((uint)east) * 2 + (uint)north) * 2 + (uint)south) * 2 + (uint)up) * 2 + (uint)waterlogged) * 2 + (uint)west,
            };
        }

        public static BlockState DioriteWall(DioriteWallEastType east, DioriteWallNorthType north, DioriteWallSouthType south, DioriteWallUpType up, DioriteWallWaterloggedType waterlogged, DioriteWallWestType west)
        {
            return new BlockState
            {
                Id = (uint)BlockId.DioriteWall,
                MetaValue = ((((((uint)east) * 2 + (uint)north) * 2 + (uint)south) * 2 + (uint)up) * 2 + (uint)waterlogged) * 2 + (uint)west,
            };
        }

        public static BlockState Scaffolding(ScaffoldingBottomType bottom, ScaffoldingDistanceType distance, ScaffoldingWaterloggedType waterlogged)
        {
            return new BlockState
            {
                Id = (uint)BlockId.Scaffolding,
                MetaValue = (((uint)bottom) * 8 + (uint)distance) * 2 + (uint)waterlogged,
            };
        }

        public static BlockState Loom(LoomFacingType facing)
        {
            return new BlockState
            {
                Id = (uint)BlockId.Loom,
                MetaValue = (uint)facing,
            };
        }

        public static BlockState Barrel(BarrelFacingType facing, BarrelOpenType open)
        {
            return new BlockState
            {
                Id = (uint)BlockId.Barrel,
                MetaValue = ((uint)facing) * 2 + (uint)open,
            };
        }

        public static BlockState Smoker(SmokerFacingType facing, SmokerLitType lit)
        {
            return new BlockState
            {
                Id = (uint)BlockId.Smoker,
                MetaValue = ((uint)facing) * 2 + (uint)lit,
            };
        }

        public static BlockState BlastFurnace(BlastFurnaceFacingType facing, BlastFurnaceLitType lit)
        {
            return new BlockState
            {
                Id = (uint)BlockId.BlastFurnace,
                MetaValue = ((uint)facing) * 2 + (uint)lit,
            };
        }

        public static BlockState CartographyTable()
        {
            return new BlockState
            {
                Id = (uint)BlockId.CartographyTable,
                MetaValue = 0,
            };
        }

        public static BlockState FletchingTable()
        {
            return new BlockState
            {
                Id = (uint)BlockId.FletchingTable,
                MetaValue = 0,
            };
        }

        public static BlockState Grindstone(GrindstoneFaceType face, GrindstoneFacingType facing)
        {
            return new BlockState
            {
                Id = (uint)BlockId.Grindstone,
                MetaValue = ((uint)face) * 4 + (uint)facing,
            };
        }

        public static BlockState Lectern(LecternFacingType facing, LecternHasBookType hasBook, LecternPoweredType powered)
        {
            return new BlockState
            {
                Id = (uint)BlockId.Lectern,
                MetaValue = (((uint)facing) * 2 + (uint)hasBook) * 2 + (uint)powered,
            };
        }

        public static BlockState SmithingTable()
        {
            return new BlockState
            {
                Id = (uint)BlockId.SmithingTable,
                MetaValue = 0,
            };
        }

        public static BlockState Stonecutter(StonecutterFacingType facing)
        {
            return new BlockState
            {
                Id = (uint)BlockId.Stonecutter,
                MetaValue = (uint)facing,
            };
        }

        public static BlockState Bell(BellAttachmentType attachment, BellFacingType facing, BellPoweredType powered)
        {
            return new BlockState
            {
                Id = (uint)BlockId.Bell,
                MetaValue = (((uint)attachment) * 4 + (uint)facing) * 2 + (uint)powered,
            };
        }

        public static BlockState Lantern(LanternHangingType hanging)
        {
            return new BlockState
            {
                Id = (uint)BlockId.Lantern,
                MetaValue = (uint)hanging,
            };
        }

        public static BlockState Campfire(CampfireFacingType facing, CampfireLitType lit, CampfireSignalFireType signalFire, CampfireWaterloggedType waterlogged)
        {
            return new BlockState
            {
                Id = (uint)BlockId.Campfire,
                MetaValue = ((((uint)facing) * 2 + (uint)lit) * 2 + (uint)signalFire) * 2 + (uint)waterlogged,
            };
        }

        public static BlockState SweetBerryBush(SweetBerryBushAgeType age)
        {
            return new BlockState
            {
                Id = (uint)BlockId.SweetBerryBush,
                MetaValue = (uint)age,
            };
        }

        public static BlockState StructureBlock(StructureBlockModeType mode)
        {
            return new BlockState
            {
                Id = (uint)BlockId.StructureBlock,
                MetaValue = (uint)mode,
            };
        }

        public static BlockState Jigsaw(JigsawFacingType facing)
        {
            return new BlockState
            {
                Id = (uint)BlockId.Jigsaw,
                MetaValue = (uint)facing,
            };
        }

        public static BlockState Composter(ComposterLevelType level)
        {
            return new BlockState
            {
                Id = (uint)BlockId.Composter,
                MetaValue = (uint)level,
            };
        }

        public static BlockState BeeNest(BeeNestFacingType facing, BeeNestHoneyLevelType honeyLevel)
        {
            return new BlockState
            {
                Id = (uint)BlockId.BeeNest,
                MetaValue = ((uint)facing) * 6 + (uint)honeyLevel,
            };
        }

        public static BlockState Beehive(BeehiveFacingType facing, BeehiveHoneyLevelType honeyLevel)
        {
            return new BlockState
            {
                Id = (uint)BlockId.Beehive,
                MetaValue = ((uint)facing) * 6 + (uint)honeyLevel,
            };
        }

        public static BlockState HoneyBlock()
        {
            return new BlockState
            {
                Id = (uint)BlockId.HoneyBlock,
                MetaValue = 0,
            };
        }

        public static BlockState HoneycombBlock()
        {
            return new BlockState
            {
                Id = (uint)BlockId.HoneycombBlock,
                MetaValue = 0,
            };
        }
    }
}