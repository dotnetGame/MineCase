using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MineCase.Block;
using MineCase.World;
using MineCase.World.Plants;
using Newtonsoft.Json;
using Orleans;
using Orleans.Concurrency;

namespace MineCase.Server.World.Decoration.Plants
{
    [StatelessWorker]
    public class JungleGeneratorGrain : HugeTreeGeneratorGrain, IJungleGenerator
    {
        public JungleGeneratorGrain(ILoggerFactory loggerFactory)
            : base(loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<JungleGeneratorGrain>();
        }

        public async override Task OnActivateAsync()
        {
            await base.OnActivateAsync();
            _wood = BlockStates.JungleLog();
            _leaves = BlockStates.JungleLeaves(JungleLeavesDistanceType.Distance1, JungleLeavesPersistentType.False);
        }

        public override async Task GenerateSingle(IWorld world, ChunkWorldPos chunkWorldPos, BlockWorldPos pos)
        {
            int seed = await world.GetSeed();
            int treeSeed = pos.X ^ pos.Z ^ seed;
            Random rand = new Random(treeSeed);
            await GenerateImpl(world, chunkWorldPos, pos, rand);
        }

        protected async Task<bool> GenerateImpl(IWorld world, ChunkWorldPos chunkWorldPos, BlockWorldPos pos, Random random)
        {
            int height = GetHeight(random);

            if (!await EnsureGrowable(world, random, pos, height))
            {
                return false;
            }
            else
            {
                await CreateCrown(world, pos.Up(height), 2);
                for (int h = pos.Y + height - 2 - random.Next(4); h > pos.Y + height / 2; h -= 2 + random.Next(4))
                {
                    float branchDirection = (float)random.NextDouble() * ((float)Math.PI * 2F);
                    int branchTopX = pos.X + (int)(0.5F + Math.Cos(branchDirection) * 4.0F); // range(0.5,4.5)
                    int branchTopZ = pos.Z + (int)(0.5F + Math.Sin(branchDirection) * 4.0F); // range(0.5,4.5)

                    for (int len = 0; len < 5; ++len)
                    {
                        // gen next branch node
                        branchTopX = pos.X + (int)(1.5F + Math.Cos(branchDirection) * (float)len); // range(1.5,len + 1.5)
                        branchTopZ = pos.Z + (int)(1.5F + Math.Sin(branchDirection) * (float)len); // range(1.5,len + 1.5)
                        await world.SetBlockStateUnsafe(this.GrainFactory, new BlockWorldPos(branchTopX, h - 3 + len / 2, branchTopZ), _wood);
                    }

                    // now, generate the top leaves layer
                    int layerHeight = 1 + random.Next(2);
                    int layerTop = h;

                    for (int layerY = h - layerHeight; layerY <= layerTop; ++layerY)
                    {
                        int width = layerY - layerTop;
                        await GrowLeavesLayer(world, new BlockWorldPos(branchTopX, layerY, branchTopZ), 1 - width);
                    }
                }

                // generate wood
                for (int y = 0; y < height; ++y)
                {
                    BlockWorldPos blockpos = pos.Up(y);

                    if (await IsAirLeaves(world, blockpos))
                    {
                        await world.SetBlockStateUnsafe(this.GrainFactory, blockpos, _wood);

                        if (y > 0)
                        {
                            await PlaceVine(world, random, blockpos.West(), new VineType { East = VineEastType.True });
                            await PlaceVine(world, random, blockpos.North(), new VineType { South = VineSouthType.True });
                        }
                    }

                    if (y < height - 1)
                    {
                        BlockWorldPos blockpos1 = blockpos.East();

                        if (await IsAirLeaves(world, blockpos1))
                        {
                            await world.SetBlockStateUnsafe(this.GrainFactory, blockpos1, _wood);

                            if (y > 0)
                            {
                                await PlaceVine(world, random, blockpos1.East(), new VineType { West = VineWestType.True });
                                await PlaceVine(world, random, blockpos1.North(), new VineType { South = VineSouthType.True });
                            }
                        }

                        BlockWorldPos blockpos2 = blockpos.South().East();

                        if (await IsAirLeaves(world, blockpos2))
                        {
                            await world.SetBlockStateUnsafe(this.GrainFactory, blockpos2, _wood);

                            if (y > 0)
                            {
                                await PlaceVine(world, random, blockpos2.East(), new VineType { West = VineWestType.True });
                                await PlaceVine(world, random, blockpos2.South(), new VineType { North = VineNorthType.True });
                            }
                        }

                        BlockWorldPos blockpos3 = blockpos.South();

                        if (await IsAirLeaves(world, blockpos3))
                        {
                            await world.SetBlockStateUnsafe(this.GrainFactory, blockpos3, _wood);

                            if (y > 0)
                            {
                                await PlaceVine(world, random, blockpos3.West(), new VineType { East = VineEastType.True });
                                await PlaceVine(world, random, blockpos3.South(), new VineType { North = VineNorthType.True });
                            }
                        }
                    }
                }

                return true;
            }
        }

        private async Task CreateCrown(IWorld world, BlockWorldPos pos, int width)
        {
            int crownHeight = 2;

            for (int yOffset = -crownHeight; yOffset <= 0; ++yOffset)
            {
                await GrowLeavesLayerStrict(world, pos.Up(yOffset), width + 1 - yOffset);
            }
        }

        private async Task PlaceVine(IWorld world, Random random, BlockWorldPos pos, VineType vineType)
        {
            var block = await world.GetBlockStateUnsafe(this.GrainFactory, pos);
            if (random.Next(3) > 0 && block.IsAir())
            {
                await world.SetBlockStateUnsafe(this.GrainFactory, pos, BlockStates.Vine(vineType.East, vineType.North, vineType.South, vineType.Up, vineType.West));
            }
        }
    }
}
