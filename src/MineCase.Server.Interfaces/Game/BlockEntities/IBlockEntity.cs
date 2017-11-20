using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Engine;
using MineCase.Server.World;
using MineCase.World;
using Orleans;

namespace MineCase.Server.Game.BlockEntities
{
    public interface IBlockEntity : IDependencyObject, IGrainWithGuidKey
    {
        Task<IWorld> GetWorld();

        Task<BlockWorldPos> GetPosition();
    }

    [AttributeUsage(AttributeTargets.Interface, Inherited = false, AllowMultiple = true)]
    public sealed class BlockEntityAttribute : Attribute
    {
        public BlockId BlockId { get; }

        public BlockEntityAttribute(BlockId blockId)
        {
            BlockId = blockId;
        }
    }
}
