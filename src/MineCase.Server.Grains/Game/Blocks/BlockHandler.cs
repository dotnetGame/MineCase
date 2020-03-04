using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using MineCase.Block;
using MineCase.Item;
using MineCase.Server.Game.Entities;
using MineCase.Server.World;
using MineCase.World;
using Orleans;

namespace MineCase.Server.Game.Blocks
{
    public abstract class BlockHandler
    {
        public BlockId BlockId { get; }

        public abstract bool IsUsable { get; }

        public BlockHandler(BlockId blockId)
        {
            BlockId = blockId;
        }

        private static readonly Dictionary<BlockId, Type> _blockHandlerTypes;
        private static readonly ConcurrentDictionary<BlockId, BlockHandler> _blockHandlers = new ConcurrentDictionary<BlockId, BlockHandler>();
        private static readonly BlockHandler _defaultBlockHandler = new DefaultBlockHandler();

        static BlockHandler()
        {
            _blockHandlerTypes = (from t in typeof(BlockHandler).Assembly.DefinedTypes
                                  where !t.IsAbstract && t.IsSubclassOf(typeof(BlockHandler))
                                  let attrs = t.GetCustomAttributes<BlockHandlerAttribute>()
                                  from attr in attrs
                                  select new
                                  {
                                      BlockId = attr.BlockId,
                                      Type = t
                                  }).ToDictionary(o => o.BlockId, o => o.Type.AsType());
        }

        public static BlockHandler Create(BlockId blockId)
        {
            if (_blockHandlerTypes.TryGetValue(blockId, out var type))
                return _blockHandlers.GetOrAdd(blockId, k => (BlockHandler)Activator.CreateInstance(type, k));
            return _defaultBlockHandler;
        }

        public virtual Task UseBy(IEntity entity, IGrainFactory grainFactory, IWorld world, BlockWorldPos blockPosition, Vector3 cursorPosition)
        {
            return Task.CompletedTask;
        }

        public virtual Task<bool> CanBeAt(BlockWorldPos position, IGrainFactory grainFactory, IWorld world)
        {
            return Task.FromResult(true);
        }

        public virtual Task OnNeighborChanged(BlockWorldPos selfPosition, BlockWorldPos neighborPosition, BlockState oldState, BlockState newState, IGrainFactory grainFactory, IWorld world)
        {
            return Task.CompletedTask;
        }

        public virtual Task OnPlaced(IEntity entity, IGrainFactory grainFactory, IWorld world, BlockWorldPos position, BlockState blockState)
        {
            return Task.CompletedTask;
        }

        public virtual Slot DropBlock(ItemState item, BlockState blockState)
        {
            // TODO: Wait for loot table
            /*
            Block.Block blockObject = Block.Block.FromBlockState(blockState);
            switch ((BlockId)blockState.Id)
            {
                case BlockId.Air:
                case BlockId.Water:
                    return Slot.Empty;
                default:
                    ItemState dropItem = blockObject.BlockBrokenItem(item, false);
                    return new Slot { BlockId = (short)dropItem.Id, ItemCount = 1 };
            }
            */
            return Slot.Empty;
        }
    }

    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    public sealed class BlockHandlerAttribute : Attribute
    {
        public BlockId BlockId { get; }

        public BlockHandlerAttribute(BlockId blockId)
        {
            BlockId = blockId;
        }
    }
}
