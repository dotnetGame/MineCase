using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Block;
using MineCase.Engine;
using MineCase.Game.Windows;
using MineCase.Server.Components;
using MineCase.Server.Game.Entities.Components;
using MineCase.Server.Game.Windows;
using MineCase.Server.World;
using MineCase.World;

namespace MineCase.Server.Game.BlockEntities.Components
{
    internal class FurnaceComponent : Component<BlockEntityGrain>, IHandle<SetSlot>, IHandle<SpawnBlockEntity>, IHandle<DestroyBlockEntity>, IHandle<UseBy>
    {
        public class FurnaceState
        {
            public bool IsCooking;

            public FurnaceRecipe CurrentRecipe;

            public FurnaceFuel CurrentFuel;

            public int FuelLeft;

            public int MaxFuelTime;

            public int CookProgress;

            public int MaxProgress;
        }

        public static readonly DependencyProperty<FurnaceState> StateProperty =
            DependencyProperty.Register<FurnaceState>(nameof(State), typeof(FurnaceComponent));

        public static readonly DependencyProperty<IFurnaceWindow> FurnaceWindowProperty =
            DependencyProperty.Register<IFurnaceWindow>("FurnaceWindow", typeof(FurnaceComponent));

        public FurnaceState State => AttachedObject.GetValue(StateProperty);

        public IFurnaceWindow FurnaceWindow => AttachedObject.GetValue(FurnaceWindowProperty);

        public FurnaceComponent(string name = "furnace")
            : base(name)
        {
        }

        protected override void OnAttached()
        {
            if (State == null)
            {
                AttachedObject.SetLocalValue(StateProperty, new FurnaceState
                {
                    MaxFuelTime = 200
                });
            }

            Register();
        }

        private bool CanCook()
        {
            var state = State;
            return state.CurrentRecipe != null && (state.FuelLeft > 0 || state.CurrentFuel != null);
        }

        private async Task OnGameTick(object sender, GameTickArgs e)
        {
            var state = State;
            if (CanCook())
            {
                if (!state.IsCooking)
                    await StartCooking();
                if (state.CookProgress == 0)
                    await TakeIngredient();
                if (state.FuelLeft == 0)
                    await TakeFuel();

                if (state.CurrentRecipe != null)
                {
                    state.CookProgress++;
                    state.FuelLeft--;
                    MarkDirty();

                    if (FurnaceWindow != null && e.WorldAge % 10 == 0)
                    {
                        await FurnaceWindow.SetProperty(FurnaceWindowPropertyType.ProgressArrow, (short)state.CookProgress);
                        await FurnaceWindow.SetProperty(FurnaceWindowPropertyType.FireIcon, (short)state.FuelLeft);
                    }
                }

                if (state.CookProgress == state.MaxProgress)
                    await Produce();
            }
            else if (state.IsCooking)
            {
                await StopCooking();
            }
        }

        async Task IHandle<SetSlot>.Handle(SetSlot message)
        {
            var state = State;
            if (state.CurrentRecipe == null && message.Index == 0)
                await UpdateRecipe();
            if (message.Index == 1)
                await UpdateFuel();
            if (!state.IsCooking && CanCook())
                Register();
        }

        private Slot GetSlot(int index) =>
            AttachedObject.GetComponent<SlotContainerComponent>().GetSlot(index);

        private void SetSlot(int index, Slot slot) =>
            AttachedObject.GetComponent<SlotContainerComponent>().SetSlot(index, slot);

        private async Task UpdateFuel()
        {
            State.CurrentFuel = await GrainFactory.GetGrain<IFurnaceRecipes>(0).FindFuel(GetSlot(1));
            MarkDirty();
        }

        private async Task UpdateRecipe()
        {
            var recipe = await GrainFactory.GetGrain<IFurnaceRecipes>(0).FindRecipe(GetSlot(0));
            if (recipe != null)
            {
                if (GetSlot(2).IsEmpty || GetSlot(2).CanStack(recipe.Output))
                {
                    State.CurrentRecipe = recipe;
                    return;
                }
            }

            State.CurrentRecipe = null;
            MarkDirty();
        }

        private async Task Produce()
        {
            var state = State;
            if (GetSlot(2).IsEmpty)
                SetSlot(2, state.CurrentRecipe.Output);
            else
                SetSlot(2, GetSlot(2).AddItemCount(state.CurrentRecipe.Output.ItemCount));
            state.CookProgress = 0;
            MarkDirty();
            if (FurnaceWindow != null)
            {
                await FurnaceWindow.BroadcastSlotChanged(2, GetSlot(2));
                await FurnaceWindow.SetProperty(FurnaceWindowPropertyType.ProgressArrow, (short)state.CookProgress);
            }
        }

        private async Task TakeFuel()
        {
            var state = State;
            var slot = GetSlot(1).AddItemCount(-state.CurrentFuel.Slot.ItemCount);
            slot.MakeEmptyIfZero();
            SetSlot(1, slot);
            state.MaxFuelTime = state.FuelLeft = state.CurrentFuel.Time;
            MarkDirty();
            if (FurnaceWindow != null)
            {
                await FurnaceWindow.BroadcastSlotChanged(1, slot);
                await FurnaceWindow.SetProperty(FurnaceWindowPropertyType.MaximumFuelBurnTime, (short)state.MaxFuelTime);
                await FurnaceWindow.SetProperty(FurnaceWindowPropertyType.FireIcon, (short)state.FuelLeft);
            }

            await UpdateFuel();
        }

        private async Task TakeIngredient()
        {
            var state = State;
            await UpdateRecipe();
            if (state.CurrentRecipe != null)
            {
                var slot = GetSlot(0).AddItemCount(-state.CurrentRecipe.Input.ItemCount);
                slot.MakeEmptyIfZero();
                SetSlot(0, slot);
                state.CookProgress = 0;
                state.MaxProgress = state.CurrentRecipe.Time;
                MarkDirty();
                if (FurnaceWindow != null)
                {
                    await FurnaceWindow.BroadcastSlotChanged(0, slot);
                    await FurnaceWindow.SetProperty(FurnaceWindowPropertyType.ProgressArrow, (short)state.CookProgress);
                    await FurnaceWindow.SetProperty(FurnaceWindowPropertyType.MaximumProgress, (short)state.MaxProgress);
                }
            }
        }

        private async Task StartCooking()
        {
            State.IsCooking = true;
            MarkDirty();
            var meta = (await AttachedObject.World.GetBlockState(GrainFactory, AttachedObject.Position)).MetaValue;
            await AttachedObject.World.SetBlockState(GrainFactory, AttachedObject.Position, new BlockState { Id = (uint)BlockId.BlastFurnace, MetaValue = meta });
        }

        private async Task StopCooking()
        {
            State.IsCooking = false;
            MarkDirty();
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
            State.IsCooking = (await AttachedObject.World.GetBlockState(GrainFactory, AttachedObject.Position))
                .IsId(BlockId.BlastFurnace);
            MarkDirty();
        }

        Task IHandle<DestroyBlockEntity>.Handle(DestroyBlockEntity message)
        {
            State.IsCooking = false;
            MarkDirty();
            Unregister();
            return Task.CompletedTask;
        }

        async Task IHandle<UseBy>.Handle(UseBy message)
        {
            if (FurnaceWindow == null)
                AttachedObject.SetLocalValue(FurnaceWindowProperty, GrainFactory.GetGrain<IFurnaceWindow>(Guid.NewGuid()));

            await FurnaceWindow.SetEntity(AttachedObject);
            await message.Entity.Tell(new OpenWindow { Window = FurnaceWindow });
        }

        private void MarkDirty()
        {
            AttachedObject.ValueStorage.IsDirty = true;
        }
    }
}
