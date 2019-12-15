using System;
using System.Collections.Generic;
using System.Text;
using MineCase.Block;
using MineCase.World;

namespace MineCase.Block
{
    public class BlockStateChange
    {
        public BlockWorldPos Position { get; set; }

        public BlockState State { get; set; }

        public List<BlockState> Condition { get; set; }
    }

    public class BatchBlockChange
    {
        protected List<BlockStateChange> blockChanges;

        public BatchBlockChange()
        {
            blockChanges = new List<BlockStateChange>();
        }

        public BlockState this[int x, int y, int z]
        {
            set
            {
                blockChanges.Add(new BlockStateChange { Position = new BlockWorldPos(x, y, z), State = value, Condition = new List<BlockState>() });
            }
        }

        public void SetIf(BlockWorldPos pos, BlockState state, BlockState ifState)
        {
            blockChanges.Add(new BlockStateChange { Position = pos, State = state, Condition = new List<BlockState> { ifState } } );
        }

        public void SetIf(BlockWorldPos pos, BlockState state, List<BlockState> ifState)
        {
            blockChanges.Add(new BlockStateChange { Position = pos, State = state, Condition = ifState });
        }

        public Dictionary<ChunkWorldPos, List<BlockStateChange>> GetByPartition()
        {
            Dictionary<ChunkWorldPos, List<BlockStateChange>> ret = new Dictionary<ChunkWorldPos, List<BlockStateChange>>();
            foreach (BlockStateChange blockChange in blockChanges)
            {
                var chunkPos = blockChange.Position.ToChunkWorldPos();
                if (ret.ContainsKey(chunkPos))
                {
                    ret[chunkPos].Add(blockChange);
                }
                else
                {
                    ret[chunkPos] = new List<BlockStateChange>();
                }
            }

            return ret;
        }
    }
}
