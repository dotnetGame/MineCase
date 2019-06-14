using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using MineCase.Block;
using MineCase.Server.World;
using MineCase.World;
using Orleans;
using Orleans.Runtime;

namespace MineCase.Server.Game.BlockEntities
{
    public static class BlockEntity
    {
        private static readonly Dictionary<BlockId, MethodInfo> _blockEntityTypes;

        static BlockEntity()
        {
            var genGetGrain = typeof(BlockEntity).GetMethod(nameof(GetBlockEntity), BindingFlags.NonPublic | BindingFlags.Static);

            _blockEntityTypes = (from t in typeof(IBlockEntity).Assembly.DefinedTypes
                                 where t.IsInterface && t.ImplementedInterfaces.Contains(typeof(IBlockEntity))
                                 let attrs = t.GetCustomAttributes<BlockEntityAttribute>()
                                 from attr in attrs
                                 select new
                                 {
                                     BlockId = attr.BlockId,
                                     Method = genGetGrain.MakeGenericMethod(t)
                                 }).ToDictionary(o => o.BlockId, o => o.Method);
        }

        private static IBlockEntity GetBlockEntity<TGrain>(IGrainFactory grainFactory, Guid key)
            where TGrain : IBlockEntity
        {
            return grainFactory.GetGrain<TGrain>(key).Cast<IBlockEntity>();
        }

        public static IBlockEntity Create(IGrainFactory grainFactory, BlockId blockId)
        {
            if (_blockEntityTypes.TryGetValue(blockId, out var method))
            {
                var key = Guid.NewGuid();
                return (IBlockEntity)method.Invoke(null, new object[] { grainFactory, key });
            }

            return null;
        }
    }
}
