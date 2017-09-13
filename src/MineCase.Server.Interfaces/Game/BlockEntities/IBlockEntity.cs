using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Server.World;
using Orleans;

namespace MineCase.Server.Game.BlockEntities
{
    public interface IBlockEntity : IGrainWithStringKey
    {
        Task OnCreated();

        Task Destroy();
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

    public static class BlockEntityExtensions
    {
        public static string MakeBlockEntityKey(this IWorld world, BlockWorldPos position)
        {
            return $"{world.GetPrimaryKeyString()},{position.X},{position.Y},{position.Z}";
        }

        public static BlockWorldPos GetBlockEntityPosition(this IBlockEntity blockEntity)
        {
            var key = blockEntity.GetPrimaryKeyString().Split(',');
            return new BlockWorldPos(int.Parse(key[1]), int.Parse(key[2]), int.Parse(key[3]));
        }

        public static (string worldKey, BlockWorldPos position) GetWorldAndBlockEntityPosition(this IBlockEntity blockEntity)
        {
            var key = blockEntity.GetPrimaryKeyString().Split(',');
            return (key[0], new BlockWorldPos(int.Parse(key[1]), int.Parse(key[2]), int.Parse(key[3])));
        }
    }
}
