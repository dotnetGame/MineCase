using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using MineCase.Server.Game.Blocks;
using MineCase.Server.Game.Entities;
using MineCase.Server.Game.Windows;
using MineCase.Server.Network.Play;
using MineCase.Server.World;
using Orleans;

namespace MineCase.Server.Game.Items
{
    public abstract class ItemHandler
    {
        public uint ItemId { get; }

        public abstract bool IsUsable { get; }

        public abstract bool IsPlaceable { get; }

        public ItemHandler(uint itemId)
        {
            ItemId = itemId;
        }

        private static readonly Dictionary<uint, Type> _itemHandlerTypes;
        private static readonly ConcurrentDictionary<uint, ItemHandler> _itemHandlers = new ConcurrentDictionary<uint, ItemHandler>();
        private static readonly ItemHandler _defaultItemHandler = new DefaultItemHandler();

        static ItemHandler()
        {
            _itemHandlerTypes = (from t in typeof(ItemHandler).Assembly.DefinedTypes
                                 where !t.IsAbstract && t.IsSubclassOf(typeof(ItemHandler))
                                 let attrs = t.GetCustomAttributes<ItemHandlerAttribute>()
                                 from attr in attrs
                                 select new
                                 {
                                     ItemId = attr.ItemId,
                                     Type = t
                                 }).ToDictionary(o => o.ItemId, o => o.Type.AsType());
        }

        public static ItemHandler Create(uint itemId)
        {
            if (_itemHandlerTypes.TryGetValue(itemId, out var type))
                return _itemHandlers.GetOrAdd(itemId, k => (ItemHandler)Activator.CreateInstance(type, k));
            return _defaultItemHandler;
        }

        public virtual async Task<bool> PlaceBy(IPlayer player, IGrainFactory grainFactory, IWorld world, BlockWorldPos position, IInventoryWindow inventoryWindow, int slotIndex, PlayerDiggingFace face, Vector3 cursorPosition)
        {
            if (IsPlaceable)
            {
                AddFace(ref position, face);
                var blockState = await world.GetBlockState(grainFactory, position);
                if ((BlockId)blockState.Id == BlockId.Air)
                {
                    var slot = await inventoryWindow.GetSlot(player, slotIndex);
                    if (!slot.IsEmpty)
                    {
                        var newState = await ConvertToBlock(player, grainFactory, world, position, slot);
                        var blockHandler = BlockHandler.Create((BlockId)newState.Id);
                        if (await blockHandler.CanBeAt(position, grainFactory, world))
                        {
                            var chunk = position.GetChunk();
                            await world.SetBlockState(grainFactory, position, newState);

                            slot.ItemCount--;
                            slot.MakeEmptyIfZero();
                            await inventoryWindow.SetSlot(player, slotIndex, slot);

                            await blockHandler.OnPlaced(player, grainFactory, world, position, newState);
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        protected virtual Task<BlockState> ConvertToBlock(IPlayer player, IGrainFactory grainFactory, IWorld world, BlockWorldPos position, Slot slot)
        {
            return Task.FromResult(new BlockState { Id = (uint)slot.BlockId, MetaValue = (uint)slot.ItemDamage });
        }

        private void AddFace(ref BlockWorldPos location, PlayerDiggingFace face, bool inverse = false)
        {
            switch (face)
            {
                case PlayerDiggingFace.Bottom:
                    location.Y--;
                    break;
                case PlayerDiggingFace.Top:
                    location.Y++;
                    break;
                case PlayerDiggingFace.North:
                    location.Z--;
                    break;
                case PlayerDiggingFace.South:
                    location.Z++;
                    break;
                case PlayerDiggingFace.West:
                    location.X--;
                    break;
                case PlayerDiggingFace.East:
                    location.X++;
                    break;
                case PlayerDiggingFace.Special:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(face));
            }
        }
    }

    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    public sealed class ItemHandlerAttribute : Attribute
    {
        public uint ItemId { get; }

        public ItemHandlerAttribute(BlockId itemId)
        {
            ItemId = (uint)itemId;
        }

        public ItemHandlerAttribute(ItemId itemId)
        {
            ItemId = (uint)itemId;
        }
    }
}
