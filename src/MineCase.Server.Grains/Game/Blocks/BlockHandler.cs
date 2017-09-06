using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using MineCase.Server.Game.Entities;
using MineCase.Server.World;
using Orleans;

namespace MineCase.Server.Game.Blocks
{
    public abstract class BlockHandler
    {
        public BlockId BlockId { get; }

        public abstract bool IsUsable { get; }

        public BlockHandler(BlockId blockId)
        {
            BlockId = BlockId;
        }

        private static readonly Dictionary<BlockId, Type> _blockHandlerTypes;
        private static readonly ConcurrentDictionary<BlockId, BlockHandler> _blockHandlers = new ConcurrentDictionary<BlockId, BlockHandler>();

        static BlockHandler()
        {
            var ctorTypes = new[] { typeof(BlockId) };
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
            return null;
        }

        public virtual Task UseBy(IPlayer player, IGrainFactory grainFactory, BlockWorldPos blockPosition, Vector3 cursorPosition)
        {
            return Task.CompletedTask;
        }
    }

    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    public sealed class BlockHandlerAttribute : Attribute
    {
        public BlockId BlockId { get; }

        // This is a positional argument
        public BlockHandlerAttribute(BlockId blockId)
        {
            BlockId = blockId;
        }
    }
}
