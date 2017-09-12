using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MineCase.Algorithm;
using MineCase.Server.Game.Entities;
using MineCase.Server.Game.Windows;
using MineCase.Server.Game.Windows.SlotAreas;
using MineCase.Server.World;
using Orleans.Concurrency;

namespace MineCase.Server.Game.BlockEntities
{
    [Reentrant]
    internal class FurnaceBlockEntity : BlockEntityGrain, IFurnaceBlockEntity
    {
        private Slot[] _slots;

        private bool _isCooking;
        private FindFurnaceRecipeResult _currentRecipe;
        private int _fuelLeft;
        private int _maxFuelTime;
        private int _cookProgress;
        private int _maxProgress;

        public override Task OnActivateAsync()
        {
            _slots = Enumerable.Repeat(Slot.Empty, FurnaceSlotArea.FurnaceSlotsCount).ToArray();
            _currentRecipe = null;
            _isCooking = false;
            _fuelLeft = 0;
            _maxFuelTime = 0;
            _cookProgress = 0;
            _maxFuelTime = 200;
            return base.OnActivateAsync();
        }

        public Task<Slot> GetSlot(int slotIndex)
        {
            return Task.FromResult(_slots[slotIndex]);
        }

        public async Task SetSlot(int slotIndex, Slot item)
        {
            _slots[slotIndex] = item;
            if (_currentRecipe == null)
                await UpdateRecipe();
        }

        private async Task UpdateRecipe()
        {
            var recipe = await GrainFactory.GetGrain<IFurnaceRecipes>(0).FindRecipe(_slots[0], _slots[1]);
            if (recipe != null)
            {
                if (_slots[2].IsEmpty || _slots[2].CanStack(recipe.Recipe.Output))
                {
                    _currentRecipe = recipe;
                    return;
                }
            }

            _currentRecipe = null;
        }

        private IFurnaceWindow _furnaceWindow;

        public async Task UseBy(IPlayer player)
        {
            if (_furnaceWindow == null)
            {
                _furnaceWindow = GrainFactory.GetGrain<IFurnaceWindow>(Guid.NewGuid());
                await _furnaceWindow.SetEntity(this);
            }

            await player.OpenWindow(_furnaceWindow);
        }

        public override async Task OnCreated()
        {
            var chunkPos = Position.ToChunkWorldPos();
            var tracker = GrainFactory.GetGrain<IChunkTrackingHub>(World.MakeChunkTrackingHubKey(chunkPos.X, chunkPos.Z));
            await tracker.Subscribe(this);
        }

        public override async Task Destroy()
        {
            var chunkPos = Position.ToChunkWorldPos();
            var tracker = GrainFactory.GetGrain<IChunkTrackingHub>(World.MakeChunkTrackingHubKey(chunkPos.X, chunkPos.Z));
            await tracker.Unsubscribe(this);
        }

        public async Task OnGameTick(TimeSpan deltaTime)
        {
            if (_currentRecipe != null)
            {
                if (!_isCooking)
                    await StartCooking();
                if (_cookProgress == 0)
                    await TakeIngredient();
                if (_fuelLeft == 0)
                    await TakeFuel();
                if (_cookProgress == _maxProgress)
                {
                    if (_slots[2].IsEmpty)
                        _slots[2] = _currentRecipe.Recipe.Output;
                    else
                        _slots[2].ItemCount += _currentRecipe.Recipe.Output.ItemCount;
                    if (_furnaceWindow != null)
                        await _furnaceWindow.BroadcastSlotChanged(2, _slots[2]);
                    _cookProgress = 0;
                    await UpdateRecipe();
                }

                if (_currentRecipe != null)
                {
                    _cookProgress++;
                    _fuelLeft--;
                }
            }
            else if (_isCooking)
            {
                await StopCooking();
            }

            if (_furnaceWindow != null)
                await _furnaceWindow.OnGameTick(deltaTime);
        }

        private async Task TakeFuel()
        {
            _slots[1].ItemCount -= _currentRecipe.Fuel.Slot.ItemCount;
            _slots[1].MakeEmptyIfZero();
            _maxFuelTime = _fuelLeft = _currentRecipe.Fuel.Time;
            if (_furnaceWindow != null)
                await _furnaceWindow.BroadcastSlotChanged(0, _slots[1]);
        }

        private async Task TakeIngredient()
        {
            _slots[0].ItemCount -= _currentRecipe.Recipe.Input.ItemCount;
            _slots[0].MakeEmptyIfZero();
            _cookProgress = 0;
            _maxProgress = _currentRecipe.Recipe.Time;
            if (_furnaceWindow != null)
                await _furnaceWindow.BroadcastSlotChanged(0, _slots[0]);
        }

        private async Task StartCooking()
        {
            _isCooking = true;
            var facing = (FacingDirectionType)(await World.GetBlockState(GrainFactory, Position)).MetaValue;
            await World.SetBlockState(GrainFactory, Position, BlockStates.BurningFurnace(facing));
        }

        private async Task StopCooking()
        {
            _isCooking = false;
            var facing = (FacingDirectionType)(await World.GetBlockState(GrainFactory, Position)).MetaValue;
            await World.SetBlockState(GrainFactory, Position, BlockStates.Furnace(facing));
        }

        public Task<(int fuelLeft, int maxFuelTime, int cookProgress, int maxProgress)> GetCookingState()
        {
            return Task.FromResult((_fuelLeft, _maxFuelTime, _cookProgress, _maxProgress));
        }
    }
}
