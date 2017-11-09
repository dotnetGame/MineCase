using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MineCase.Client.Game.Blocks
{
    public abstract partial class BlockHandler
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

        public virtual Slot DropBlock(uint itemId, BlockState blockState)
        {
            switch ((BlockId)blockState.Id)
            {
                case BlockId.Air:
                case BlockId.Water:
                    return Slot.Empty;
                default:
                    return new Slot { BlockId = (short)blockState.Id, ItemCount = 1 };
            }
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
