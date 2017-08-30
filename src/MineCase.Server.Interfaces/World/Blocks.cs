using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase.Server.World
{
    public static class BlockStates
    {
        public static BlockState Air()
        {
            return new BlockState
            {
                Id = (uint)BlockId.Air,
                MetaValue = 0
            };
        }

        public static BlockState Stone(StoneType type = StoneType.Stone)
        {
            return new BlockState
            {
                Id = (uint)BlockId.Stone,
                MetaValue = (uint)type
            };
        }

        public static BlockState GrassBlock()
        {
            return new BlockState
            {
                 Id = (uint)BlockId.GrassBlock,
                 MetaValue = 0
            };
        }

        public static BlockState Dirt(DirtType type = DirtType.Dirt)
        {
            return new BlockState
            {
                Id = (uint)BlockId.Dirt,
                MetaValue = (uint)type
            };
        }

        public static BlockState Cobblestone()
        {
            return new BlockState
            {
                Id = (uint)BlockId.Cobblestone,
                MetaValue = 0
            };
        }

        public static BlockState WoodPlanks(WoodPlanksType type = WoodPlanksType.OakWoodPlanks)
        {
            return new BlockState
            {
                Id = (uint)BlockId.WoodPlanks,
                MetaValue = (uint)type
            };
        }

        public static BlockState Sapling(SaplingsType type = SaplingsType.OakSapling)
        {
            return new BlockState
            {
                Id = (uint)BlockId.Sapling,
                MetaValue = (uint)type
            };
        }

        public static BlockState Bedrock()
        {
            return new BlockState
            {
                Id = (uint)BlockId.Bedrock,
                MetaValue = 0
            };
        }

        public static BlockState Water(FluidType type = FluidType.Falling)
        {
            return new BlockState
            {
                Id = (uint)BlockId.Water,
                MetaValue = (uint)type
            };
        }

        public static BlockState StationaryWater(FluidType type = FluidType.Falling)
        {
            return new BlockState
            {
                Id = (uint)BlockId.StationaryWater,
                MetaValue = (uint)type
            };
        }

        public static BlockState Lava(FluidType type = FluidType.Falling)
        {
            return new BlockState
            {
                Id = (uint)BlockId.Lava,
                MetaValue = (uint)type
            };
        }

        public static BlockState StationaryLava(FluidType type = FluidType.Falling)
        {
            return new BlockState
            {
                Id = (uint)BlockId.StationaryLava,
                MetaValue = 0
            };
        }

        public static BlockState Sand(SandType type = SandType.Sand)
        {
            return new BlockState
            {
                Id = (uint)BlockId.Sand,
                MetaValue = (uint)type
            };
        }

        public static BlockState Gravel()
        {
            return new BlockState
            {
                Id = (uint)BlockId.Gravel,
                MetaValue = 0
            };
        }

        public static BlockState GoldOre()
        {
            return new BlockState
            {
                Id = (uint)BlockId.GoldOre,
                MetaValue = 0
            };
        }

        public static BlockState IronOre()
        {
            return new BlockState
            {
                Id = (uint)BlockId.IronOre,
                MetaValue = 0
            };
        }

        public static BlockState CoalOre()
        {
            return new BlockState
            {
                Id = (uint)BlockId.CoalOre,
                MetaValue = 0
            };
        }

        public static BlockState Wood(WoodType type = WoodType.OakWoodOrAcaciaWood)
        {
            return new BlockState
            {
                Id = (uint)BlockId.Wood,
                MetaValue = (uint)type
            };
        }

        public static BlockState Leaves(LeavesType type = LeavesType.OakLeaves)
        {
            return new BlockState
            {
                Id = (uint)BlockId.Leaves,
                MetaValue = (uint)type
            };
        }

        public static BlockState Sponge(SpongeType type = SpongeType.Sponge)
        {
            return new BlockState
            {
                Id = (uint)BlockId.Sponge,
                MetaValue = (uint)type
            };
        }

        public static BlockState Glass()
        {
            return new BlockState
            {
                Id = (uint)BlockId.Glass,
                MetaValue = 0
            };
        }

        public static BlockState LapisLazuliOre()
        {
            return new BlockState
            {
                Id = (uint)BlockId.LapisLazuliOre,
                MetaValue = 0
            };
        }

        public static BlockState LapisLazuliBlock()
        {
            return new BlockState
            {
                Id = (uint)BlockId.LapisLazuliBlock,
                MetaValue = 0
            };
        }

        public static BlockState Dispenser(DispenserType type = DispenserType.FacingDown)
        {
            return new BlockState
            {
                Id = (uint)BlockId.Dispenser,
                MetaValue = (uint)type
            };
        }

        public static BlockState Sandstone(SandstoneType type = SandstoneType.Sandstone)
        {
            return new BlockState
            {
                Id = (uint)BlockId.Sandstone,
                MetaValue = (uint)type
            };
        }

        public static BlockState NoteBlock()
        {
            return new BlockState
            {
                Id = (uint)BlockId.NoteBlock,
                MetaValue = 0
            };
        }

        public static BlockState Bed(BedType type = BedType.HeadFacingNorth)
        {
            return new BlockState
            {
                Id = (uint)BlockId.Bed,
                MetaValue = (uint)type
            };
        }

        public static BlockState PoweredRail(RailsType type = RailsType.StraightRailConnectNS)
        {
            return new BlockState
            {
                Id = (uint)BlockId.PoweredRail,
                MetaValue = (uint)type
            };
        }

        public static BlockState DetectorRail(RailsType type = RailsType.StraightRailConnectNS)
        {
            return new BlockState
            {
                Id = (uint)BlockId.DetectorRail,
                MetaValue = (uint)type
            };
        }

        public static BlockState StickyPiston(PistionType type = PistionType.Down)
        {
            return new BlockState
            {
                Id = (uint)BlockId.StickyPiston,
                MetaValue = (uint)type
            };
        }

        public static BlockState Cobweb()
        {
            return new BlockState
            {
                Id = (uint)BlockId.Cobweb,
                MetaValue = 0
            };
        }

        public static BlockState Grass(GrassType type = GrassType.Shrub)
        {
            return new BlockState
            {
                Id = (uint)BlockId.Grass,
                MetaValue = (uint)type
            };
        }

        public static BlockState DeadBush()
        {
            return new BlockState
            {
                Id = (uint)BlockId.DeadBush,
                MetaValue = 0
            };
        }

        public static BlockState Piston(PistionType type = PistionType.Down)
        {
            return new BlockState
            {
                Id = (uint)BlockId.Piston,
                MetaValue = (uint)type
            };
        }

        public static BlockState PistonHead(PistionType type = PistionType.Down)
        {
            return new BlockState
            {
                Id = (uint)BlockId.PistonHead,
                MetaValue = (uint)type
            };
        }

        public static BlockState Wool(WoodType type = WoodType.OakWoodOrAcaciaWood)
        {
            return new BlockState
            {
                Id = (uint)BlockId.Wool,
                MetaValue = (uint)type
            };
        }

        public static BlockState BlockMovedByPiston()
        {
            return new BlockState
            {
                Id = (uint)BlockId.BlockMovedByPiston,
                MetaValue = 0
            };
        }

        public static BlockState Dandelion()
        {
            return new BlockState
            {
                Id = (uint)BlockId.Dandelion,
                MetaValue = 0
            };
        }

        public static BlockState Poppy(FlowersType type = FlowersType.Poppy)
        {
            return new BlockState
            {
                Id = (uint)BlockId.Poppy,
                MetaValue = (uint)type
            };
        }

        public static BlockState BrownMushroom()
        {
            return new BlockState
            {
                Id = (uint)BlockId.BrownMushroom,
                MetaValue = 0
            };
        }

        public static BlockState RedMushroom()
        {
            return new BlockState
            {
                Id = (uint)BlockId.RedMushroom,
                MetaValue = 0
            };
        }

        public static BlockState BlockOfGold()
        {
            return new BlockState
            {
                Id = (uint)BlockId.BlockOfGold,
                MetaValue = 0
            };
        }

        public static BlockState BlockOfIron()
        {
            return new BlockState
            {
                Id = (uint)BlockId.BlockOfIron,
                MetaValue = 0
            };
        }

        public static BlockState DoubleStoneSlab(DoubleStoneSlabsType type = DoubleStoneSlabsType.DoubleStoneSlab)
        {
            return new BlockState
            {
                Id = (uint)BlockId.DoubleStoneSlab,
                MetaValue = (uint)type
            };
        }

        public static BlockState StoneSlab(StoneSlabsType type = StoneSlabsType.StoneSlab)
        {
            return new BlockState
            {
                Id = (uint)BlockId.StoneSlab,
                MetaValue = (uint)type
            };
        }

        public static BlockState Bricks()
        {
            return new BlockState
            {
                Id = (uint)BlockId.Bricks,
                MetaValue = 0
            };
        }

        public static BlockState TNT()
        {
            return new BlockState
            {
                Id = (uint)BlockId.TNT,
                MetaValue = 0
            };
        }

        public static BlockState Bookshelf()
        {
            return new BlockState
            {
                Id = (uint)BlockId.Bookshelf,
                MetaValue = 0
            };
        }

        public static BlockState MossStone()
        {
            return new BlockState
            {
                Id = (uint)BlockId.MossStone,
                MetaValue = 0
            };
        }

        public static BlockState Obsidian()
        {
            return new BlockState
            {
                Id = (uint)BlockId.Obsidian,
                MetaValue = 0
            };
        }

        public static BlockState Torch(TorchesType type = TorchesType.FacingUp)
        {
            return new BlockState
            {
                Id = (uint)BlockId.Torch,
                MetaValue = (uint)type
            };
        }

        public static BlockState Fire(FireType type = FireType.Placed)
        {
            return new BlockState
            {
                Id = (uint)BlockId.Fire,
                MetaValue = (uint)type
            };
        }

        public static BlockState MonsterSpawner()
        {
            return new BlockState
            {
                Id = (uint)BlockId.MonsterSpawner,
                MetaValue = 0
            };
        }

        public static BlockState OakWoodStairs(StairsType type = StairsType.East)
        {
            return new BlockState
            {
                Id = (uint)BlockId.OakWoodStairs,
                MetaValue = (uint)type
            };
        }

        public static BlockState Chest(FacingDirectionType type = FacingDirectionType.FacingNorth)
        {
            return new BlockState
            {
                Id = (uint)BlockId.Chest,
                MetaValue = (uint)type
            };
        }

        public static BlockState RedstoneWire(RedstoneWireType type = RedstoneWireType.Level1)
        {
            return new BlockState
            {
                Id = (uint)BlockId.RedstoneWire,
                MetaValue = (uint)type
            };
        }

        public static BlockState DiamondOre()
        {
            return new BlockState
            {
                Id = (uint)BlockId.DiamondOre,
                MetaValue = 0
            };
        }

        public static BlockState BlockOfDiamond()
        {
            return new BlockState
            {
                Id = (uint)BlockId.BlockOfDiamond,
                MetaValue = 0
            };
        }

        public static BlockState CraftingTable()
        {
            return new BlockState
            {
                Id = (uint)BlockId.CraftingTable,
                MetaValue = 0
            };
        }

        public static BlockState Wheat(CropsType type = CropsType.GrowStage1)
        {
            return new BlockState
            {
                Id = (uint)BlockId.Wheat,
                MetaValue = (uint)type
            };
        }

        public static BlockState Farmland(FarmlandType type = FarmlandType.DryLand)
        {
            return new BlockState
            {
                Id = (uint)BlockId.Farmland,
                MetaValue = (uint)type
            };
        }

        public static BlockState Furnace(FacingDirectionType type = FacingDirectionType.FacingNorth)
        {
            return new BlockState
            {
                Id = (uint)BlockId.Furnace,
                MetaValue = (uint)type
            };
        }

        public static BlockState BurningFurnace(FacingDirectionType type = FacingDirectionType.FacingNorth)
        {
            return new BlockState
            {
                Id = (uint)BlockId.BurningFurnace,
                MetaValue = (uint)type
            };
        }

        public static BlockState StandingSign(SignStandingType type = SignStandingType.South)
        {
            return new BlockState
            {
                Id = (uint)BlockId.StandingSign,
                MetaValue = (uint)type
            };
        }

        // TODO: fix
        public static BlockState OakDoor()
        {
            return new BlockState
            {
                Id = (uint)BlockId.OakDoor,
                MetaValue = 0
            };
        }

        public static BlockState Ladder(FacingDirectionType type = FacingDirectionType.FacingNorth)
        {
            return new BlockState
            {
                Id = (uint)BlockId.Ladder,
                MetaValue = (uint)type
            };
        }

        public static BlockState Rail(RailsType type = RailsType.StraightRailConnectNS)
        {
            return new BlockState
            {
                Id = (uint)BlockId.Rail,
                MetaValue = (uint)type
            };
        }

        public static BlockState CobblestoneStairs(StairsType type = StairsType.East)
        {
            return new BlockState
            {
                Id = (uint)BlockId.CobblestoneStairs,
                MetaValue = (uint)type
            };
        }

        public static BlockState WallSign(FacingDirectionType type = FacingDirectionType.FacingNorth)
        {
            return new BlockState
            {
                Id = (uint)BlockId.WallSign,
                MetaValue = (uint)type
            };
        }

        public static BlockState Lever(LeverType type = LeverType.BottomPointsEastWhenOff)
        {
            return new BlockState
            {
                Id = (uint)BlockId.Lever,
                MetaValue = (uint)type
            };
        }

        // TODO: go on
        public static BlockState StonePressurePlate()
        {
            return new BlockState
            {
                Id = (uint)BlockId.StonePressurePlate,
                MetaValue = 0
            };
        }

        public static BlockState IronDoor()
        {
            return new BlockState
            {
                Id = (uint)BlockId.IronDoor,
                MetaValue = 0
            };
        }

        public static BlockState WoodenPressurePlate()
        {
            return new BlockState
            {
                Id = (uint)BlockId.WoodenPressurePlate,
                MetaValue = 0
            };
        }

        public static BlockState RedstoneOre()
        {
            return new BlockState
            {
                Id = (uint)BlockId.RedstoneOre,
                MetaValue = 0
            };
        }

        public static BlockState GlowingRedstoneOre()
        {
            return new BlockState
            {
                Id = (uint)BlockId.GlowingRedstoneOre,
                MetaValue = 0
            };
        }

        public static BlockState RedstoneTorchInactive()
        {
            return new BlockState
            {
                Id = (uint)BlockId.RedstoneTorchInactive,
                MetaValue = 0
            };
        }

        public static BlockState RedstoneTorchActive()
        {
            return new BlockState
            {
                Id = (uint)BlockId.RedstoneTorchActive,
                MetaValue = 0
            };
        }

        public static BlockState StoneButton()
        {
            return new BlockState
            {
                Id = (uint)BlockId.StoneButton,
                MetaValue = 0
            };
        }

        public static BlockState SnowLayer()
        {
            return new BlockState
            {
                Id = (uint)BlockId.SnowLayer,
                MetaValue = 0
            };
        }

        public static BlockState Ice()
        {
            return new BlockState
            {
                Id = (uint)BlockId.Ice,
                MetaValue = 0
            };
        }

        public static BlockState Snow()
        {
            return new BlockState
            {
                Id = (uint)BlockId.Snow,
                MetaValue = 0
            };
        }

        public static BlockState Cactus()
        {
            return new BlockState
            {
                Id = (uint)BlockId.Cactus,
                MetaValue = 0
            };
        }

        public static BlockState Clay()
        {
            return new BlockState
            {
                Id = (uint)BlockId.Clay,
                MetaValue = 0
            };
        }

        public static BlockState SugarCane()
        {
            return new BlockState
            {
                Id = (uint)BlockId.SugarCane,
                MetaValue = 0
            };
        }

        public static BlockState Jukebox()
        {
            return new BlockState
            {
                Id = (uint)BlockId.Jukebox,
                MetaValue = 0
            };
        }

        public static BlockState OakFence()
        {
            return new BlockState
            {
                Id = (uint)BlockId.OakFence,
                MetaValue = 0
            };
        }

        public static BlockState Pumpkin()
        {
            return new BlockState
            {
                Id = (uint)BlockId.Pumpkin,
                MetaValue = 0
            };
        }

        public static BlockState Netherrack()
        {
            return new BlockState
            {
                Id = (uint)BlockId.Netherrack,
                MetaValue = 0
            };
        }

        public static BlockState SoulSand()
        {
            return new BlockState
            {
                Id = (uint)BlockId.SoulSand,
                MetaValue = 0
            };
        }

        public static BlockState Glowstone()
        {
            return new BlockState
            {
                Id = (uint)BlockId.Glowstone,
                MetaValue = 0
            };
        }

        public static BlockState NetherPortal()
        {
            return new BlockState
            {
                Id = (uint)BlockId.NetherPortal,
                MetaValue = 0
            };
        }

        public static BlockState JackLantern()
        {
            return new BlockState
            {
                Id = (uint)BlockId.JackLantern,
                MetaValue = 0
            };
        }

        public static BlockState Cake()
        {
            return new BlockState
            {
                Id = (uint)BlockId.Cake,
                MetaValue = 0
            };
        }

        public static BlockState RedstoneRepeaterInactive()
        {
            return new BlockState
            {
                Id = (uint)BlockId.RedstoneRepeaterInactive,
                MetaValue = 0
            };
        }

        public static BlockState RedstoneRepeaterActive()
        {
            return new BlockState
            {
                Id = (uint)BlockId.RedstoneRepeaterActive,
                MetaValue = 0
            };
        }

        public static BlockState StainedGlass()
        {
            return new BlockState
            {
                Id = (uint)BlockId.StainedGlass,
                MetaValue = 0
            };
        }

        public static BlockState Trapdoor()
        {
            return new BlockState
            {
                Id = (uint)BlockId.Trapdoor,
                MetaValue = 0
            };
        }

        public static BlockState MonsterEgg()
        {
            return new BlockState
            {
                Id = (uint)BlockId.MonsterEgg,
                MetaValue = 0
            };
        }

        public static BlockState StoneBricks()
        {
            return new BlockState
            {
                Id = (uint)BlockId.StoneBricks,
                MetaValue = 0
            };
        }

        public static BlockState BrownMushroomBlock()
        {
            return new BlockState
            {
                Id = (uint)BlockId.BrownMushroomBlock,
                MetaValue = 0
            };
        }

        public static BlockState RedMushroomBlock()
        {
            return new BlockState
            {
                Id = (uint)BlockId.RedMushroomBlock,
                MetaValue = 0
            };
        }

        public static BlockState IronBars()
        {
            return new BlockState
            {
                Id = (uint)BlockId.IronBars,
                MetaValue = 0
            };
        }

        public static BlockState GlassPane()
        {
            return new BlockState
            {
                Id = (uint)BlockId.GlassPane,
                MetaValue = 0
            };
        }

        public static BlockState Melon()
        {
            return new BlockState
            {
                Id = (uint)BlockId.Melon,
                MetaValue = 0
            };
        }

        public static BlockState PumpkinStem()
        {
            return new BlockState
            {
                Id = (uint)BlockId.PumpkinStem,
                MetaValue = 0
            };
        }

        public static BlockState MelonStem()
        {
            return new BlockState
            {
                Id = (uint)BlockId.MelonStem,
                MetaValue = 0
            };
        }

        public static BlockState Vines()
        {
            return new BlockState
            {
                Id = (uint)BlockId.Vines,
                MetaValue = 0
            };
        }

        public static BlockState FenceGate()
        {
            return new BlockState
            {
                Id = (uint)BlockId.FenceGate,
                MetaValue = 0
            };
        }

        public static BlockState BrickStairs()
        {
            return new BlockState
            {
                Id = (uint)BlockId.BrickStairs,
                MetaValue = 0
            };
        }

        public static BlockState StoneBrickStairs()
        {
            return new BlockState
            {
                Id = (uint)BlockId.StoneBrickStairs,
                MetaValue = 0
            };
        }

        public static BlockState Mycelium()
        {
            return new BlockState
            {
                Id = (uint)BlockId.Mycelium,
                MetaValue = 0
            };
        }

        public static BlockState LilyPad()
        {
            return new BlockState
            {
                Id = (uint)BlockId.LilyPad,
                MetaValue = 0
            };
        }

        public static BlockState NetherBrick()
        {
            return new BlockState
            {
                Id = (uint)BlockId.NetherBrick,
                MetaValue = 0
            };
        }

        public static BlockState NetherBrickFence()
        {
            return new BlockState
            {
                Id = (uint)BlockId.NetherBrickFence,
                MetaValue = 0
            };
        }

        public static BlockState NetherBrickStairs()
        {
            return new BlockState
            {
                Id = (uint)BlockId.NetherBrickStairs,
                MetaValue = 0
            };
        }

        public static BlockState NetherWart()
        {
            return new BlockState
            {
                Id = (uint)BlockId.NetherWart,
                MetaValue = 0
            };
        }

        public static BlockState EnchantmentTable()
        {
            return new BlockState
            {
                Id = (uint)BlockId.EnchantmentTable,
                MetaValue = 0
            };
        }

        public static BlockState BrewingStand()
        {
            return new BlockState
            {
                Id = (uint)BlockId.BrewingStand,
                MetaValue = 0
            };
        }

        public static BlockState Cauldron()
        {
            return new BlockState
            {
                Id = (uint)BlockId.Cauldron,
                MetaValue = 0
            };
        }

        public static BlockState EndPortal()
        {
            return new BlockState
            {
                Id = (uint)BlockId.EndPortal,
                MetaValue = 0
            };
        }

        public static BlockState EndPortalFrame()
        {
            return new BlockState
            {
                Id = (uint)BlockId.EndPortalFrame,
                MetaValue = 0
            };
        }

        public static BlockState EndStone()
        {
            return new BlockState
            {
                Id = (uint)BlockId.EndStone,
                MetaValue = 0
            };
        }

        public static BlockState DragonEgg()
        {
            return new BlockState
            {
                Id = (uint)BlockId.DragonEgg,
                MetaValue = 0
            };
        }

        public static BlockState RedstoneLampInactive()
        {
            return new BlockState
            {
                Id = (uint)BlockId.RedstoneLampInactive,
                MetaValue = 0
            };
        }

        public static BlockState RedstoneLampActive()
        {
            return new BlockState
            {
                Id = (uint)BlockId.RedstoneLampActive,
                MetaValue = 0
            };
        }

        public static BlockState DoubleWoodenSlab()
        {
            return new BlockState
            {
                Id = (uint)BlockId.DoubleWoodenSlab,
                MetaValue = 0
            };
        }

        public static BlockState WoodenSlab()
        {
            return new BlockState
            {
                Id = (uint)BlockId.WoodenSlab,
                MetaValue = 0
            };
        }

        public static BlockState Cocoa()
        {
            return new BlockState
            {
                Id = (uint)BlockId.Cocoa,
                MetaValue = 0
            };
        }

        public static BlockState SandstoneStairs()
        {
            return new BlockState
            {
                Id = (uint)BlockId.SandstoneStairs,
                MetaValue = 0
            };
        }

        public static BlockState EmeraldOre()
        {
            return new BlockState
            {
                Id = (uint)BlockId.EmeraldOre,
                MetaValue = 0
            };
        }

        public static BlockState EnderChest()
        {
            return new BlockState
            {
                Id = (uint)BlockId.EnderChest,
                MetaValue = 0
            };
        }

        public static BlockState TripwireHook()
        {
            return new BlockState
            {
                Id = (uint)BlockId.TripwireHook,
                MetaValue = 0
            };
        }

        public static BlockState Tripwire()
        {
            return new BlockState
            {
                Id = (uint)BlockId.Tripwire,
                MetaValue = 0
            };
        }

        public static BlockState BlockOfEmerald()
        {
            return new BlockState
            {
                Id = (uint)BlockId.BlockOfEmerald,
                MetaValue = 0
            };
        }

        public static BlockState SpruceWoodStairs()
        {
            return new BlockState
            {
                Id = (uint)BlockId.SpruceWoodStairs,
                MetaValue = 0
            };
        }

        public static BlockState BirchWoodStairs()
        {
            return new BlockState
            {
                Id = (uint)BlockId.BirchWoodStairs,
                MetaValue = 0
            };
        }

        public static BlockState JungleWoodStairs()
        {
            return new BlockState
            {
                Id = (uint)BlockId.JungleWoodStairs,
                MetaValue = 0
            };
        }

        public static BlockState CommandBlock()
        {
            return new BlockState
            {
                Id = (uint)BlockId.CommandBlock,
                MetaValue = 0
            };
        }

        public static BlockState Beacon()
        {
            return new BlockState
            {
                Id = (uint)BlockId.Beacon,
                MetaValue = 0
            };
        }

        public static BlockState CobblestoneWall()
        {
            return new BlockState
            {
                Id = (uint)BlockId.CobblestoneWall,
                MetaValue = 0
            };
        }

        public static BlockState FlowerPot()
        {
            return new BlockState
            {
                Id = (uint)BlockId.FlowerPot,
                MetaValue = 0
            };
        }

        public static BlockState Carrot()
        {
            return new BlockState
            {
                Id = (uint)BlockId.Carrot,
                MetaValue = 0
            };
        }

        public static BlockState Potato()
        {
            return new BlockState
            {
                Id = (uint)BlockId.Potato,
                MetaValue = 0
            };
        }

        public static BlockState WoodenButton()
        {
            return new BlockState
            {
                Id = (uint)BlockId.WoodenButton,
                MetaValue = 0
            };
        }

        public static BlockState Mobhead()
        {
            return new BlockState
            {
                Id = (uint)BlockId.Mobhead,
                MetaValue = 0
            };
        }

        public static BlockState Anvil()
        {
            return new BlockState
            {
                Id = (uint)BlockId.Anvil,
                MetaValue = 0
            };
        }

        public static BlockState TrappedChest()
        {
            return new BlockState
            {
                Id = (uint)BlockId.TrappedChest,
                MetaValue = 0
            };
        }

        public static BlockState WeightedPressurePlateLight()
        {
            return new BlockState
            {
                Id = (uint)BlockId.WeightedPressurePlateLight,
                MetaValue = 0
            };
        }

        public static BlockState WeightedPressurePlateHeavy()
        {
            return new BlockState
            {
                Id = (uint)BlockId.WeightedPressurePlateHeavy,
                MetaValue = 0
            };
        }

        public static BlockState RedstoneComparator()
        {
            return new BlockState
            {
                Id = (uint)BlockId.RedstoneComparator,
                MetaValue = 0
            };
        }

        public static BlockState RedstoneComparatorDeprecated()
        {
            return new BlockState
            {
                Id = (uint)BlockId.RedstoneComparatorDeprecated,
                MetaValue = 0
            };
        }

        public static BlockState DaylightSensor()
        {
            return new BlockState
            {
                Id = (uint)BlockId.DaylightSensor,
                MetaValue = 0
            };
        }

        public static BlockState BlockOfRedstone()
        {
            return new BlockState
            {
                Id = (uint)BlockId.BlockOfRedstone,
                MetaValue = 0
            };
        }

        public static BlockState NetherQuartzOre()
        {
            return new BlockState
            {
                Id = (uint)BlockId.NetherQuartzOre,
                MetaValue = 0
            };
        }

        public static BlockState Hopper()
        {
            return new BlockState
            {
                Id = (uint)BlockId.Hopper,
                MetaValue = 0
            };
        }

        public static BlockState BlockOfQuartz()
        {
            return new BlockState
            {
                Id = (uint)BlockId.BlockOfQuartz,
                MetaValue = 0
            };
        }

        public static BlockState QuartzStairs()
        {
            return new BlockState
            {
                Id = (uint)BlockId.QuartzStairs,
                MetaValue = 0
            };
        }

        public static BlockState ActivatorRail()
        {
            return new BlockState
            {
                Id = (uint)BlockId.ActivatorRail,
                MetaValue = 0
            };
        }

        public static BlockState Dropper()
        {
            return new BlockState
            {
                Id = (uint)BlockId.Dropper,
                MetaValue = 0
            };
        }

        public static BlockState StainedClay()
        {
            return new BlockState
            {
                Id = (uint)BlockId.StainedClay,
                MetaValue = 0
            };
        }

        public static BlockState StainedGlassPane()
        {
            return new BlockState
            {
                Id = (uint)BlockId.StainedGlassPane,
                MetaValue = 0
            };
        }

        public static BlockState Leaves2()
        {
            return new BlockState
            {
                Id = (uint)BlockId.Leaves2,
                MetaValue = 0
            };
        }

        public static BlockState Wood2()
        {
            return new BlockState
            {
                Id = (uint)BlockId.Wood2,
                MetaValue = 0
            };
        }

        public static BlockState AcaciaWoodStairs()
        {
            return new BlockState
            {
                Id = (uint)BlockId.AcaciaWoodStairs,
                MetaValue = 0
            };
        }

        public static BlockState DarkOakWoodStairs()
        {
            return new BlockState
            {
                Id = (uint)BlockId.DarkOakWoodStairs,
                MetaValue = 0
            };
        }

        public static BlockState SlimeBlock()
        {
            return new BlockState
            {
                Id = (uint)BlockId.SlimeBlock,
                MetaValue = 0
            };
        }

        public static BlockState Barrier()
        {
            return new BlockState
            {
                Id = (uint)BlockId.Barrier,
                MetaValue = 0
            };
        }

        public static BlockState IronTrapdoor()
        {
            return new BlockState
            {
                Id = (uint)BlockId.IronTrapdoor,
                MetaValue = 0
            };
        }

        public static BlockState Prismarine()
        {
            return new BlockState
            {
                Id = (uint)BlockId.Prismarine,
                MetaValue = 0
            };
        }

        public static BlockState SeaLantern()
        {
            return new BlockState
            {
                Id = (uint)BlockId.SeaLantern,
                MetaValue = 0
            };
        }

        public static BlockState HayBale()
        {
            return new BlockState
            {
                Id = (uint)BlockId.HayBale,
                MetaValue = 0
            };
        }

        public static BlockState Carpet()
        {
            return new BlockState
            {
                Id = (uint)BlockId.Carpet,
                MetaValue = 0
            };
        }

        public static BlockState HardenedClay()
        {
            return new BlockState
            {
                Id = (uint)BlockId.HardenedClay,
                MetaValue = 0
            };
        }

        public static BlockState BlockOfCoal()
        {
            return new BlockState
            {
                Id = (uint)BlockId.BlockOfCoal,
                MetaValue = 0
            };
        }

        public static BlockState PackedIce()
        {
            return new BlockState
            {
                Id = (uint)BlockId.PackedIce,
                MetaValue = 0
            };
        }

        public static BlockState LargeFlowers()
        {
            return new BlockState
            {
                Id = (uint)BlockId.LargeFlowers,
                MetaValue = 0
            };
        }

        public static BlockState StandingBanner()
        {
            return new BlockState
            {
                Id = (uint)BlockId.StandingBanner,
                MetaValue = 0
            };
        }

        public static BlockState WallBanner()
        {
            return new BlockState
            {
                Id = (uint)BlockId.WallBanner,
                MetaValue = 0
            };
        }

        public static BlockState InvertedDaylightSensor()
        {
            return new BlockState
            {
                Id = (uint)BlockId.InvertedDaylightSensor,
                MetaValue = 0
            };
        }

        public static BlockState RedSandstone()
        {
            return new BlockState
            {
                Id = (uint)BlockId.RedSandstone,
                MetaValue = 0
            };
        }

        public static BlockState RedSandstoneStairs()
        {
            return new BlockState
            {
                Id = (uint)BlockId.RedSandstoneStairs,
                MetaValue = 0
            };
        }

        public static BlockState DoubleRedSandstoneSlab()
        {
            return new BlockState
            {
                Id = (uint)BlockId.DoubleRedSandstoneSlab,
                MetaValue = 0
            };
        }

        public static BlockState RedSandstoneSlab()
        {
            return new BlockState
            {
                Id = (uint)BlockId.RedSandstoneSlab,
                MetaValue = 0
            };
        }

        public static BlockState SpruceFenceGate()
        {
            return new BlockState
            {
                Id = (uint)BlockId.SpruceFenceGate,
                MetaValue = 0
            };
        }

        public static BlockState BirchFenceGate()
        {
            return new BlockState
            {
                Id = (uint)BlockId.BirchFenceGate,
                MetaValue = 0
            };
        }

        public static BlockState JungleFenceGate()
        {
            return new BlockState
            {
                Id = (uint)BlockId.JungleFenceGate,
                MetaValue = 0
            };
        }

        public static BlockState DarkOakFenceGate()
        {
            return new BlockState
            {
                Id = (uint)BlockId.DarkOakFenceGate,
                MetaValue = 0
            };
        }

        public static BlockState AcaciaFenceGate()
        {
            return new BlockState
            {
                Id = (uint)BlockId.AcaciaFenceGate,
                MetaValue = 0
            };
        }

        public static BlockState SpruceFence()
        {
            return new BlockState
            {
                Id = (uint)BlockId.SpruceFence,
                MetaValue = 0
            };
        }

        public static BlockState BirchFence()
        {
            return new BlockState
            {
                Id = (uint)BlockId.BirchFence,
                MetaValue = 0
            };
        }

        public static BlockState JungleFence()
        {
            return new BlockState
            {
                Id = (uint)BlockId.JungleFence,
                MetaValue = 0
            };
        }

        public static BlockState DarkOakFence()
        {
            return new BlockState
            {
                Id = (uint)BlockId.DarkOakFence,
                MetaValue = 0
            };
        }

        public static BlockState AcaciaFence()
        {
            return new BlockState
            {
                Id = (uint)BlockId.AcaciaFence,
                MetaValue = 0
            };
        }

        public static BlockState SpruceDoor()
        {
            return new BlockState
            {
                Id = (uint)BlockId.SpruceDoor,
                MetaValue = 0
            };
        }

        public static BlockState BirchDoor()
        {
            return new BlockState
            {
                Id = (uint)BlockId.BirchDoor,
                MetaValue = 0
            };
        }

        public static BlockState JungleDoor()
        {
            return new BlockState
            {
                Id = (uint)BlockId.JungleDoor,
                MetaValue = 0
            };
        }

        public static BlockState AcaciaDoor()
        {
            return new BlockState
            {
                Id = (uint)BlockId.AcaciaDoor,
                MetaValue = 0
            };
        }

        public static BlockState DarkOakDoor()
        {
            return new BlockState
            {
                Id = (uint)BlockId.DarkOakDoor,
                MetaValue = 0
            };
        }

        public static BlockState EndRod()
        {
            return new BlockState
            {
                Id = (uint)BlockId.EndRod,
                MetaValue = 0
            };
        }

        public static BlockState ChorusPlant()
        {
            return new BlockState
            {
                Id = (uint)BlockId.ChorusPlant,
                MetaValue = 0
            };
        }

        public static BlockState ChorusFlower()
        {
            return new BlockState
            {
                Id = (uint)BlockId.ChorusFlower,
                MetaValue = 0
            };
        }

        public static BlockState PurpurBlock()
        {
            return new BlockState
            {
                Id = (uint)BlockId.PurpurBlock,
                MetaValue = 0
            };
        }

        public static BlockState PurpurPillar()
        {
            return new BlockState
            {
                Id = (uint)BlockId.PurpurPillar,
                MetaValue = 0
            };
        }

        public static BlockState PurpurStairs()
        {
            return new BlockState
            {
                Id = (uint)BlockId.PurpurStairs,
                MetaValue = 0
            };
        }

        public static BlockState PurpurDoubleSlab()
        {
            return new BlockState
            {
                Id = (uint)BlockId.PurpurDoubleSlab,
                MetaValue = 0
            };
        }

        public static BlockState PurpurSlab()
        {
            return new BlockState
            {
                Id = (uint)BlockId.PurpurSlab,
                MetaValue = 0
            };
        }

        public static BlockState EndStoneBricks()
        {
            return new BlockState
            {
                Id = (uint)BlockId.EndStoneBricks,
                MetaValue = 0
            };
        }

        public static BlockState BeetrootSeeds()
        {
            return new BlockState
            {
                Id = (uint)BlockId.BeetrootSeeds,
                MetaValue = 0
            };
        }

        public static BlockState GrassPath()
        {
            return new BlockState
            {
                Id = (uint)BlockId.GrassPath,
                MetaValue = 0
            };
        }

        public static BlockState EndGateway()
        {
            return new BlockState
            {
                Id = (uint)BlockId.EndGateway,
                MetaValue = 0
            };
        }

        public static BlockState RepeatingCommandBlock()
        {
            return new BlockState
            {
                Id = (uint)BlockId.RepeatingCommandBlock,
                MetaValue = 0
            };
        }

        public static BlockState ChainCommandBlock()
        {
            return new BlockState
            {
                Id = (uint)BlockId.ChainCommandBlock,
                MetaValue = 0
            };
        }

        public static BlockState FrostedIce()
        {
            return new BlockState
            {
                Id = (uint)BlockId.FrostedIce,
                MetaValue = 0
            };
        }

        public static BlockState MagmaBlock()
        {
            return new BlockState
            {
                Id = (uint)BlockId.MagmaBlock,
                MetaValue = 0
            };
        }

        public static BlockState NetherWartBlock()
        {
            return new BlockState
            {
                Id = (uint)BlockId.NetherWartBlock,
                MetaValue = 0
            };
        }

        public static BlockState RedNetherBrick()
        {
            return new BlockState
            {
                Id = (uint)BlockId.RedNetherBrick,
                MetaValue = 0
            };
        }

        public static BlockState BoneBlock()
        {
            return new BlockState
            {
                Id = (uint)BlockId.BoneBlock,
                MetaValue = 0
            };
        }

        public static BlockState StructureVoid()
        {
            return new BlockState
            {
                Id = (uint)BlockId.StructureVoid,
                MetaValue = 0
            };
        }

        public static BlockState Observer()
        {
            return new BlockState
            {
                Id = (uint)BlockId.Observer,
                MetaValue = 0
            };
        }

        public static BlockState WhiteShulkerBox()
        {
            return new BlockState
            {
                Id = (uint)BlockId.WhiteShulkerBox,
                MetaValue = 0
            };
        }

        public static BlockState OrangeShulkerBox()
        {
            return new BlockState
            {
                Id = (uint)BlockId.OrangeShulkerBox,
                MetaValue = 0
            };
        }

        public static BlockState MagentaShulkerBox()
        {
            return new BlockState
            {
                Id = (uint)BlockId.MagentaShulkerBox,
                MetaValue = 0
            };
        }

        public static BlockState LightBlueShulkerBox()
        {
            return new BlockState
            {
                Id = (uint)BlockId.LightBlueShulkerBox,
                MetaValue = 0
            };
        }

        public static BlockState YellowShulkerBox()
        {
            return new BlockState
            {
                Id = (uint)BlockId.YellowShulkerBox,
                MetaValue = 0
            };
        }

        public static BlockState LimeShulkerBox()
        {
            return new BlockState
            {
                Id = (uint)BlockId.LimeShulkerBox,
                MetaValue = 0
            };
        }

        public static BlockState PinkShulkerBox()
        {
            return new BlockState
            {
                Id = (uint)BlockId.PinkShulkerBox,
                MetaValue = 0
            };
        }

        public static BlockState GrayShulkerBox()
        {
            return new BlockState
            {
                Id = (uint)BlockId.GrayShulkerBox,
                MetaValue = 0
            };
        }

        public static BlockState LightGrayShulkerBox()
        {
            return new BlockState
            {
                Id = (uint)BlockId.LightGrayShulkerBox,
                MetaValue = 0
            };
        }

        public static BlockState CyanShulkerBox()
        {
            return new BlockState
            {
                Id = (uint)BlockId.CyanShulkerBox,
                MetaValue = 0
            };
        }

        public static BlockState PurpleShulkerBox()
        {
            return new BlockState
            {
                Id = (uint)BlockId.PurpleShulkerBox,
                MetaValue = 0
            };
        }

        public static BlockState BlueShulkerBox()
        {
            return new BlockState
            {
                Id = (uint)BlockId.BlueShulkerBox,
                MetaValue = 0
            };
        }

        public static BlockState BrownShulkerBox()
        {
            return new BlockState
            {
                Id = (uint)BlockId.BrownShulkerBox,
                MetaValue = 0
            };
        }

        public static BlockState GreenShulkerBox()
        {
            return new BlockState
            {
                Id = (uint)BlockId.GreenShulkerBox,
                MetaValue = 0
            };
        }

        public static BlockState RedShulkerBox()
        {
            return new BlockState
            {
                Id = (uint)BlockId.RedShulkerBox,
                MetaValue = 0
            };
        }

        public static BlockState BlackShulkerBox()
        {
            return new BlockState
            {
                Id = (uint)BlockId.BlackShulkerBox,
                MetaValue = 0
            };
        }

        public static BlockState WhiteGlazedTerracotta()
        {
            return new BlockState
            {
                Id = (uint)BlockId.WhiteGlazedTerracotta,
                MetaValue = 0
            };
        }

        public static BlockState OrangeGlazedTerracotta()
        {
            return new BlockState
            {
                Id = (uint)BlockId.OrangeGlazedTerracotta,
                MetaValue = 0
            };
        }

        public static BlockState MagentaGlazedTerracotta()
        {
            return new BlockState
            {
                Id = (uint)BlockId.MagentaGlazedTerracotta,
                MetaValue = 0
            };
        }

        public static BlockState LightBlueGlazedTerracotta()
        {
            return new BlockState
            {
                Id = (uint)BlockId.LightBlueGlazedTerracotta,
                MetaValue = 0
            };
        }

        public static BlockState YellowGlazedTerracotta()
        {
            return new BlockState
            {
                Id = (uint)BlockId.YellowGlazedTerracotta,
                MetaValue = 0
            };
        }

        public static BlockState LimeGlazedTerracotta()
        {
            return new BlockState
            {
                Id = (uint)BlockId.LimeGlazedTerracotta,
                MetaValue = 0
            };
        }

        public static BlockState PinkGlazedTerracotta()
        {
            return new BlockState
            {
                Id = (uint)BlockId.PinkGlazedTerracotta,
                MetaValue = 0
            };
        }

        public static BlockState GrayGlazedTerracotta()
        {
            return new BlockState
            {
                Id = (uint)BlockId.GrayGlazedTerracotta,
                MetaValue = 0
            };
        }

        public static BlockState LightGrayGlazedTerracotta()
        {
            return new BlockState
            {
                Id = (uint)BlockId.LightGrayGlazedTerracotta,
                MetaValue = 0
            };
        }

        public static BlockState CyanGlazedTerracotta()
        {
            return new BlockState
            {
                Id = (uint)BlockId.CyanGlazedTerracotta,
                MetaValue = 0
            };
        }

        public static BlockState PurpleGlazedTerracotta()
        {
            return new BlockState
            {
                Id = (uint)BlockId.PurpleGlazedTerracotta,
                MetaValue = 0
            };
        }

        public static BlockState BlueGlazedTerracotta()
        {
            return new BlockState
            {
                Id = (uint)BlockId.BlueGlazedTerracotta,
                MetaValue = 0
            };
        }

        public static BlockState BrownGlazedTerracotta()
        {
            return new BlockState
            {
                Id = (uint)BlockId.BrownGlazedTerracotta,
                MetaValue = 0
            };
        }

        public static BlockState GreenGlazedTerracotta()
        {
            return new BlockState
            {
                Id = (uint)BlockId.GreenGlazedTerracotta,
                MetaValue = 0
            };
        }

        public static BlockState RedGlazedTerracotta()
        {
            return new BlockState
            {
                Id = (uint)BlockId.RedGlazedTerracotta,
                MetaValue = 0
            };
        }

        public static BlockState BlackGlazedTerracotta()
        {
            return new BlockState
            {
                Id = (uint)BlockId.BlackGlazedTerracotta,
                MetaValue = 0
            };
        }

        public static BlockState Concrete()
        {
            return new BlockState
            {
                Id = (uint)BlockId.Concrete,
                MetaValue = 0
            };
        }

        public static BlockState ConcretePowder()
        {
            return new BlockState
            {
                Id = (uint)BlockId.ConcretePowder,
                MetaValue = 0
            };
        }

        public static BlockState StructureBlock()
        {
            return new BlockState
            {
                Id = (uint)BlockId.StructureBlock,
                MetaValue = 0
            };
        }
    }
}