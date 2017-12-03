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
using MineCase.Server.Game.Entities.Components;
using MineCase.Server.Game.Windows;
using MineCase.Server.Network.Play;
using MineCase.Server.World;
using MineCase.World;
using Orleans;
using Orleans.Concurrency;

namespace MineCase.Server.Game.Items
{
    public abstract class ItemHandler
    {
        public uint ItemId { get; }

        public abstract bool IsUsable { get; }

        public abstract bool IsPlaceable { get; }

        public virtual byte MaxStackCount => 64;

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

        public virtual async Task<bool> PlaceBy(IEntity entity, IGrainFactory grainFactory, IWorld world, BlockWorldPos position, Slot heldItem, PlayerDiggingFace face, Vector3 cursorPosition)
        {
            if (IsPlaceable)
            {
                AddFace(ref position, face);
                var blockState = await world.GetBlockState(grainFactory, position);
                if ((BlockId)blockState.Id == BlockId.Air)
                {
                    if (!heldItem.IsEmpty)
                    {
                        var newState = await ConvertToBlock(entity, grainFactory, world, position, heldItem);
                        var blockHandler = BlockHandler.Create((BlockId)newState.Id);
                        if (await blockHandler.CanBeAt(position, grainFactory, world))
                        {
                            await world.SetBlockState(grainFactory, position, newState);

                            heldItem.ItemCount--;
                            heldItem.MakeEmptyIfZero();
                            await entity.Tell(new SetHeldItem { Slot = heldItem });

                            await blockHandler.OnPlaced(entity, grainFactory, world, position, newState);
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        protected virtual Task<BlockState> ConvertToBlock(IEntity entity, IGrainFactory grainFactory, IWorld world, BlockWorldPos position, Slot slot)
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

        public async Task<bool> FinishedDigging(IEntity entity, IGrainFactory grainFactory, IWorld world, BlockWorldPos position, BlockState blockState, long usedTick, GameMode gameMode)
        {
            if (!blockState.IsSameId(BlockStates.Bedrock()))
            {
                var newState = BlockStates.Air();
                await world.SetBlockState(grainFactory, position, newState);

                // 产生 Pickup
                if (gameMode.ModeClass != GameMode.Class.Creative)
                {
                    var chunk = position.ToChunkWorldPos();
                    var finder = grainFactory.GetGrain<ICollectableFinder>(world.MakeAddressByPartitionKey(chunk));
                    var blockHandler = BlockHandler.Create((BlockId)blockState.Id);
                    var droppedSlot = blockHandler.DropBlock(ItemId, blockState);
                    if (!droppedSlot.IsEmpty)
                        await finder.SpawnPickup(position + new Vector3(0.5f, 0.5f, 0.5f), new[] { droppedSlot }.AsImmutable());
                }

                return true;
            }

            return false;
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
