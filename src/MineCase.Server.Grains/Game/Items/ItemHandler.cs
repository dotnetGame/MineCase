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
        public ItemState Item { get; }

        public abstract bool IsUsable { get; }

        public abstract bool IsPlaceable { get; }

        public virtual byte MaxStackCount => 64;

        public ItemHandler(ItemState item)
        {
            Item = item;
        }

        private static readonly Dictionary<ItemState, Type> _itemHandlerTypes;
        private static readonly ConcurrentDictionary<ItemState, ItemHandler> _itemHandlers = new ConcurrentDictionary<ItemState, ItemHandler>();
        private static readonly ItemHandler _defaultItemHandler = new DefaultItemHandler();

        static ItemHandler()
        {
            _itemHandlerTypes = (from t in typeof(ItemHandler).Assembly.DefinedTypes
                                 where !t.IsAbstract && t.IsSubclassOf(typeof(ItemHandler))
                                 let attrs = t.GetCustomAttributes<ItemHandlerAttribute>()
                                 from attr in attrs
                                 select new
                                 {
                                     Item = attr.Item,
                                     Type = t
                                 }).ToDictionary(o => o.Item, o => o.Type.AsType());
        }

        public static ItemHandler Create(ItemState item)
        {
            if (_itemHandlerTypes.TryGetValue(item, out var type))
                return _itemHandlers.GetOrAdd(item, k => (ItemHandler)Activator.CreateInstance(type, k));

            // return _defaultItemHandler;
            return new DefaultItemHandler(item);
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
                    var droppedSlot = blockHandler.DropBlock(Item, blockState);
                    if (!droppedSlot.IsEmpty)
                    {
                        Random random = new Random();
                        double randomOffsetX = random.NextDouble() * 0.5F + 0.25D;
                        double randomOffsetY = random.NextDouble() * 0.5F + 0.25D;
                        double randomOffsetZ = random.NextDouble() * 0.5F + 0.25D;
                        await finder.SpawnPickup(position + new Vector3((float)randomOffsetX, (float)randomOffsetY, (float)randomOffsetZ), new[] { droppedSlot }.AsImmutable());
                    }
                }

                return true;
            }

            return false;
        }
    }

    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    public sealed class ItemHandlerAttribute : Attribute
    {
        public ItemState Item { get; }

        public ItemHandlerAttribute(BlockId itemId, uint metaValue)
        {
            Item = new ItemState { Id = (uint)itemId, MetaValue = metaValue };
        }

        public ItemHandlerAttribute(ItemId itemId, uint metaValue)
        {
            Item = new ItemState { Id = (uint)itemId, MetaValue = metaValue };
        }
    }
}
