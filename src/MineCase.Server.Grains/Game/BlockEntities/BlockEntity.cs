using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using MineCase.Server.World;
using Orleans;
using Orleans.Runtime;

namespace MineCase.Server.Game.BlockEntities
{
    public static class BlockEntity
    {
        private static readonly Dictionary<BlockId, MethodInfo> _blockEntityTypes;

        static BlockEntity()
        {
            _blockEntityTypes = (from t in typeof(IBlockEntity).Assembly.DefinedTypes
                                 where t.IsInterface && t.IsSubclassOf(typeof(IBlockEntity))
                                 let attrs = t.GetCustomAttributes<BlockEntityAttribute>()
                                 from attr in attrs
                                 select new
                                 {
                                     BlockId = attr.BlockId,
                                     Method = typeof(IGrainFactory).GetMethod("GetGrain").MakeGenericMethod(t)
                                 }).ToDictionary(o => o.BlockId, o => o.Method);
        }

        public static IBlockEntity Create(IGrainFactory grainFactory, IWorld world, BlockWorldPos position, BlockId blockId)
        {
            if (_blockEntityTypes.TryGetValue(blockId, out var method))
            {
                var key = world.MakeBlockEntityKey(position);
                return ((IAddressable)method.Invoke(grainFactory, new[] { key })).Cast<IBlockEntity>();
            }

            return null;
        }
    }
}
