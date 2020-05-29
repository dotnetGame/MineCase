using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using MineCase.Block;
using MineCase.Server.Game.BlockEntities;
using MineCase.Server.Game.Entities;
using MineCase.Server.World;
using MineCase.World;
using Orleans;

namespace MineCase.Server.Game.Blocks
{
    [BlockHandler(BlockId.Chest)]
    public class ChestBlockHandler : BlockHandler
    {
        public override bool IsUsable => true;

        public ChestBlockHandler(BlockId blockId)
            : base(blockId)
        {
        }

        public static readonly (int X, int Z)[] CrossCoords = new[]
        {
            (-1, 0), (0, -1), (1, 0), (0, 1)
        };

        public override async Task<bool> CanBeAt(BlockWorldPos position, IGrainFactory grainFactory, IWorld world)
        {
            bool hasNeighbor = false;
            foreach (var crossCoord in CrossCoords)
            {
                var blockState = await world.GetBlockState(grainFactory, position.X + crossCoord.X, position.Y, position.Z + crossCoord.Z);
                if (blockState.Id != (uint)BlockId) continue;

                if (hasNeighbor) return false;
                hasNeighbor = true;

                // 再检查隔壁的隔壁是不是箱子
                foreach (var crossCoord2 in CrossCoords)
                {
                    var blockState2 = await world.GetBlockState(grainFactory, position.X + crossCoord.X + crossCoord2.X, position.Y, position.Z + crossCoord.Z + crossCoord2.Z);
                    if (blockState2.Id == (uint)BlockId) return false;
                }
            }

            return await base.CanBeAt(position, grainFactory, world);
        }

        public override async Task UseBy(IEntity entity, IGrainFactory grainFactory, IWorld world, BlockWorldPos blockPosition, Vector3 cursorPosition)
        {
            var blockEntity = (await world.GetBlockEntity(grainFactory, blockPosition)).Cast<IChestBlockEntity>();
            await blockEntity.Tell(new UseBy { Entity = entity });
        }

        public override async Task OnNeighborChanged(BlockWorldPos selfPosition, BlockWorldPos neighborPosition, BlockState oldState, BlockState newState, IGrainFactory grainFactory, IWorld world)
        {
            if (oldState.Id == (uint)BlockId.Chest)
            {
                var entity = await world.GetBlockEntity(grainFactory, selfPosition);
                await entity.Tell(NeighborEntityChanged.Empty);
            }

            if (newState.Id == (uint)BlockId.Chest)
            {
                await world.SetBlockState(grainFactory, selfPosition, newState);
                var entity = await world.GetBlockEntity(grainFactory, selfPosition);
                await entity.Tell(new NeighborEntityChanged
                {
                    Entity = await world.GetBlockEntity(grainFactory, neighborPosition)
                });
            }
        }

        public override async Task OnPlaced(IEntity entity, IGrainFactory grainFactory, IWorld world, BlockWorldPos position, BlockState blockState)
        {
            BlockWorldPos? neighborPosition = null;
            foreach (var crossCoord in CrossCoords)
            {
                var neighborPos = new BlockWorldPos(position.X + crossCoord.X, position.Y, position.Z + crossCoord.Z);
                var neighborState = await world.GetBlockState(grainFactory, neighborPos);
                if (neighborState.Id == (uint)BlockId)
                {
                    neighborPosition = neighborPos;
                    break;
                }
            }

            if (neighborPosition.HasValue)
            {
                var blockEntity = await world.GetBlockEntity(grainFactory, position);
                var neightborEntity = await world.GetBlockEntity(grainFactory, neighborPosition.Value);
                await entity.Tell(new NeighborEntityChanged { Entity = neightborEntity });
            }
        }

        public static FacingDirectionType PlayerYawToFacing(float yaw)
        {
            yaw += 90 + 45;  // So its not aligned with axis

            if (yaw > 360.0f)
                yaw -= 360.0f;

            if ((yaw >= 0.0f) && (yaw < 90.0f))
                return FacingDirectionType.FacingWest;
            else if ((yaw >= 180) && (yaw < 270))
                return FacingDirectionType.FacingEast;
            else if ((yaw >= 90) && (yaw < 180))
                return FacingDirectionType.FacingNorth;
            else
                return FacingDirectionType.FacingSouth;
        }
    }
}
