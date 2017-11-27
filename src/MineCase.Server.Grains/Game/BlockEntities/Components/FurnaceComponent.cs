using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Engine;
using MineCase.Game.Windows;
using MineCase.Server.Components;
using MineCase.Server.Game.Entities.Components;
using MineCase.Server.Game.Windows;
using MineCase.Server.World;

namespace MineCase.Server.Game.BlockEntities.Components
{
    internal class FurnaceComponent : Component<BlockEntityGrain>, IHandle<SetSlot>, IHandle<SpawnBlockEntity>, IHandle<DestroyBlockEntity>, IHandle<UseBy>
    {
        private bool _isCooking;
        private FurnaceRecipe _currentRecipe;
        private FurnaceFuel _currentFuel;
        private int _fuelLeft;
        private int _maxFuelTime;
        private int _cookProgress;
        private int _maxProgress;

        public static readonly DependencyProperty<IFurnaceWindow> FurnaceWindowProperty =
            DependencyProperty.Register<IFurnaceWindow>("FurnaceWindow", typeof(FurnaceComponent));

        public IFurnaceWindow FurnaceWindow => AttachedObject.GetValue(FurnaceWindowProperty);

        public FurnaceComponent(string name = "furnace")
            : base(name)
        {
        }

        protected override Task OnAttached()
        {
            _currentRecipe = null;
            _isCooking = false;
            _fuelLeft = 0;
            _maxFuelTime = 0;
            _cookProgress = 0;
            _maxFuelTime = 200;
            Register();
            return base.OnAttached();
        }

        private bool CanCook() => _currentRecipe != null && (_fuelLeft > 0 || _currentFuel != null);

        private async Task OnGameTick(object sender, (TimeSpan deltaTime, long worldAge) e)
        {
            if (CanCook())
            {
                if (!_isCooking)
                    await StartCooking();
                if (_cookProgress == 0)
                    await TakeIngredient();
                if (_fuelLeft == 0)
                    await TakeFuel();

                if (_currentRecipe != null)
                {
                    _cookProgress++;
                    _fuelLeft--;

                    if (FurnaceWindow != null && e.worldAge % 10 == 0)
                    {
                        await FurnaceWindow.SetProperty(FurnaceWindowPropertyType.ProgressArrow, (short)_cookProgress);
                        await FurnaceWindow.SetProperty(FurnaceWindowPropertyType.FireIcon, (short)_fuelLeft);
                    }
                }

                if (_cookProgress == _maxProgress)
                    await Produce();
            }
            else if (_isCooking)
            {
                await StopCooking();
            }
        }

        async Task IHandle<SetSlot>.Handle(SetSlot message)
        {
            if (_currentRecipe == null && message.Index == 0)
                await UpdateRecipe();
            if (message.Index == 1)
                await UpdateFuel();
            if (!_isCooking && CanCook())
                Register();
        }

        private Slot GetSlot(int index) =>
            AttachedObject.GetComponent<SlotContainerComponent>().GetSlot(index);

        private Task SetSlot(int index, Slot slot) =>
            AttachedObject.GetComponent<SlotContainerComponent>().SetSlot(index, slot);

        private async Task UpdateFuel()
        {
            _currentFuel = await GrainFactory.GetGrain<IFurnaceRecipes>(0).FindFuel(GetSlot(1));
        }

        private async Task UpdateRecipe()
        {
            var recipe = await GrainFactory.GetGrain<IFurnaceRecipes>(0).FindRecipe(GetSlot(0));
            if (recipe != null)
            {
                if (GetSlot(2).IsEmpty || GetSlot(2).CanStack(recipe.Output))
                {
                    _currentRecipe = recipe;
                    return;
                }
            }

            _currentRecipe = null;
        }

        private async Task Produce()
        {
            if (GetSlot(2).IsEmpty)
                await SetSlot(2, _currentRecipe.Output);
            else
                await SetSlot(2, GetSlot(2).AddItemCount(_currentRecipe.Output.ItemCount));
            _cookProgress = 0;
            if (FurnaceWindow != null)
            {
                await FurnaceWindow.BroadcastSlotChanged(2, GetSlot(2));
                await FurnaceWindow.SetProperty(FurnaceWindowPropertyType.ProgressArrow, (short)_cookProgress);
            }
        }

        private async Task TakeFuel()
        {
            var slot = GetSlot(1).AddItemCount(-_currentFuel.Slot.ItemCount);
            slot.MakeEmptyIfZero();
            await SetSlot(1, slot);
            _maxFuelTime = _fuelLeft = _currentFuel.Time;
            if (FurnaceWindow != null)
            {
                await FurnaceWindow.BroadcastSlotChanged(1, slot);
                await FurnaceWindow.SetProperty(FurnaceWindowPropertyType.MaximumFuelBurnTime, (short)_maxFuelTime);
                await FurnaceWindow.SetProperty(FurnaceWindowPropertyType.FireIcon, (short)_fuelLeft);
            }

            await UpdateFuel();
        }

        private async Task TakeIngredient()
        {
            await UpdateRecipe();
            if (_currentRecipe != null)
            {
                var slot = GetSlot(0).AddItemCount(-_currentRecipe.Input.ItemCount);
                slot.MakeEmptyIfZero();
                await SetSlot(0, slot);
                _cookProgress = 0;
                _maxProgress = _currentRecipe.Time;
                if (FurnaceWindow != null)
                {
                    await FurnaceWindow.BroadcastSlotChanged(0, slot);
                    await FurnaceWindow.SetProperty(FurnaceWindowPropertyType.ProgressArrow, (short)_cookProgress);
                    await FurnaceWindow.SetProperty(FurnaceWindowPropertyType.MaximumProgress, (short)_maxProgress);
                }
            }
        }

        private async Task StartCooking()
        {
            _isCooking = true;
            var meta = (await AttachedObject.World.GetBlockState(GrainFactory, AttachedObject.Position)).MetaValue;
            await AttachedObject.World.SetBlockState(GrainFactory, AttachedObject.Position, new BlockState { Id = (uint)BlockId.BurningFurnace, MetaValue = meta });
        }

        private async Task StopCooking()
        {
            _isCooking = false;
            var meta = (await AttachedObject.World.GetBlockState(GrainFactory, AttachedObject.Position)).MetaValue;
            await AttachedObject.World.SetBlockState(GrainFactory, AttachedObject.Position, new BlockState { Id = (uint)BlockId.Furnace, MetaValue = meta });
            Unregister();
        }

        private void Register()
        {
            AttachedObject.GetComponent<GameTickComponent>()
                .Tick += OnGameTick;
        }

        private void Unregister()
        {
            AttachedObject.GetComponent<GameTickComponent>()
                .Tick -= OnGameTick;
        }

        async Task IHandle<SpawnBlockEntity>.Handle(SpawnBlockEntity message)
        {
            _isCooking = (await AttachedObject.World.GetBlockState(GrainFactory, AttachedObject.Position))
                .IsSameId(BlockStates.BurningFurnace());
        }

        Task IHandle<DestroyBlockEntity>.Handle(DestroyBlockEntity message)
        {
            _isCooking = false;
            Unregister();
            return Task.CompletedTask;
        }

        async Task IHandle<UseBy>.Handle(UseBy message)
        {
            if (FurnaceWindow == null)
            {
                await AttachedObject.SetLocalValue(FurnaceWindowProperty, GrainFactory.GetGrain<IFurnaceWindow>(Guid.NewGuid()));
                await FurnaceWindow.SetEntity(AttachedObject);
            }

            await message.Entity.Tell(new OpenWindow { Window = FurnaceWindow });
        }
    }
}
