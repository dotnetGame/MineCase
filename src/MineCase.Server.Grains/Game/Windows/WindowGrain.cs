using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using MineCase.Server.Game.Entities;
using MineCase.Server.Game.Entities.Components;
using MineCase.Server.Game.Windows.SlotAreas;
using MineCase.Server.Network;
using MineCase.Server.Network.Play;
using MineCase.Server.Persistence;
using MineCase.Server.World;
using Orleans;
using Orleans.Concurrency;

namespace MineCase.Server.Game.Windows
{
    [Reentrant]
    internal abstract class WindowGrain : Grain, IWindow
    {
        protected List<Slot> Slots { get; } = new List<Slot>();

        protected List<SlotArea> SlotAreas { get; } = new List<SlotArea>();

        protected IWorld World { get; private set; }

        protected abstract string WindowType { get; }

        protected abstract Chat Title { get; }

        protected virtual byte? EntityId => null;

        private HashSet<IPlayer> _players;

        public override Task OnActivateAsync()
        {
            _players = new HashSet<IPlayer>();
            return base.OnActivateAsync();
        }

        public Task<uint> GetSlotCount()
        {
            return Task.FromResult((uint)Slots.Count);
        }

        public async Task<IReadOnlyList<Slot>> GetSlots(IPlayer player)
        {
            var slots = new List<Slot>();
            foreach (var slotArea in SlotAreas)
            {
                for (int i = 0; i < slotArea.SlotsCount; i++)
                    slots.Add(await slotArea.GetSlot(player, i));
            }

            return slots;
        }

        private Task<byte> GetWindowId(IPlayer player) =>
            player.Ask(new AskWindowId { Window = this.AsReference<IWindow>() });

        private ClientPlayPacketGenerator GetPlayerPacketGenerator(IPlayer player) =>
            new ClientPlayPacketGenerator(new ForwardToPlayerPacketSink(player, ServiceProvider.GetRequiredService<IPacketPackager>()));

        internal async Task NotifySlotChanged(SlotArea slotArea, IPlayer player, int slotIndex, Slot item)
        {
            await GetPlayerPacketGenerator(player)
                .SetSlot(await GetWindowId(player), (short)LocalSlotIndexToGlobal(slotArea, slotIndex), item);
        }

        internal Task BroadcastSlotChanged(SlotArea slotArea, int slotIndex, Slot item)
        {
            var globalIndex = (short)LocalSlotIndexToGlobal(slotArea, slotIndex);
            async Task SendSetSlot(IPlayer player)
            {
                var id = await GetWindowId(player);
                await GetPlayerPacketGenerator(player).SetSlot(id, globalIndex, item);
            }

            return Task.WhenAll(from p in _players select SendSetSlot(p));
        }

        protected int LocalSlotIndexToGlobal(SlotArea slotArea, int slotIndex)
        {
            for (int i = 0; i < SlotAreas.Count; i++)
            {
                if (SlotAreas[i] == slotArea)
                    break;
                slotIndex += SlotAreas[i].SlotsCount;
            }

            return slotIndex;
        }

        protected (SlotArea slotArea, int slotIndex) GlobalSlotIndexToLocal(int slotIndex)
        {
            for (int i = 0; i < SlotAreas.Count; i++)
            {
                if (slotIndex < SlotAreas[i].SlotsCount)
                    return (SlotAreas[i], slotIndex);
                slotIndex -= SlotAreas[i].SlotsCount;
            }

            throw new ArgumentOutOfRangeException(nameof(slotIndex));
        }

        public virtual Task<Slot> DistributeStack(IPlayer player, Slot item)
        {
            return DistributeStack(player, SlotAreas, item, false);
        }

        protected async Task<Slot> DistributeStack(IPlayer player, IReadOnlyList<SlotArea> slotAreas, Slot item, bool fillFromBack)
        {
            // 先使用已有的 Slot，再使用空 Slot
            for (int pass = 0; pass < 2; pass++)
            {
                foreach (var slotArea in slotAreas)
                {
                    item = await slotArea.DistributeStack(player, item, pass == 1, fillFromBack);
                    if (item.IsEmpty) break;
                }
            }

            return item;
        }

        public virtual async Task Click(IPlayer player, int slotIndex, ClickAction clickAction, Slot clickedItem)
        {
            switch (clickAction)
            {
                case ClickAction.LeftMouseClick:
                case ClickAction.RightMouseClick:

                case ClickAction.LeftMouseDragBegin:
                case ClickAction.LeftMouseAddSlot:
                case ClickAction.LeftMouseDragEnd:

                case ClickAction.RightMouseDragBegin:
                case ClickAction.RightMouseAddSlot:
                case ClickAction.RightMouseDragEnd:

                case ClickAction.DoubleClick:
                    var slot = GlobalSlotIndexToLocal(slotIndex);
                    await slot.slotArea.Click(player, SlotAreas, slotIndex, slot.slotIndex, clickAction, clickedItem);
                    break;
                default:
                    break;
            }
        }

        internal Task BroadcastWholeWindow()
        {
            async Task SendWholeWindow(IPlayer player)
            {
                var slots = await GetSlots(player);
                var id = await GetWindowId(player);
                await GetPlayerPacketGenerator(player).WindowItems(id, slots);
            }

            return Task.WhenAll(from p in _players select SendWholeWindow(p));
        }

        public async Task Close(IPlayer player)
        {
            foreach (var slotArea in SlotAreas)
                await slotArea.Close(player);
            _players.Remove(player);
        }

        private byte GetNonInventorySlotsCount()
        {
            byte num = 0;
            foreach (var slotArea in SlotAreas)
            {
                if (slotArea is TemporarySlotArea || slotArea is InventorySlotAreaBase) continue;
                num += (byte)slotArea.SlotsCount;
            }

            return num;
        }

        public virtual async Task OpenWindow(IPlayer player)
        {
            var slots = await GetSlots(player);

            var id = await GetWindowId(player);
            var generator = GetPlayerPacketGenerator(player);
            await generator.OpenWindow(id, WindowType, Title, GetNonInventorySlotsCount(), EntityId);
            await generator.WindowItems(id, slots);
            _players.Add(player);
        }

        public Task<Slot> GetSlot(IPlayer player, int slotIndex)
        {
            var area = GlobalSlotIndexToLocal(slotIndex);
            return area.slotArea.GetSlot(player, area.slotIndex);
        }

        public Task SetSlot(IPlayer player, int slotIndex, Slot item)
        {
            var area = GlobalSlotIndexToLocal(slotIndex);
            return area.slotArea.SetSlot(player, area.slotIndex, item);
        }

        public async Task Destroy()
        {
            async Task SendCloseWindow(IPlayer player)
            {
                var id = await GetWindowId(player);
                await GetPlayerPacketGenerator(player).CloseWindow(id);
            }

            await Task.WhenAll(from p in _players select SendCloseWindow(p));
            await Task.WhenAll(from p in _players select Close(p));
            DeactivateOnIdle();
        }

        public Task BroadcastSlotChanged(int slotIndex, Slot item)
        {
            async Task SendSetSlot(IPlayer player)
            {
                var id = await GetWindowId(player);
                await GetPlayerPacketGenerator(player).SetSlot(id, (short)slotIndex, item);
            }

            return Task.WhenAll(from p in _players select SendSetSlot(p));
        }

        protected Task BroadcastWindowProperty<T>(T property, short value)
            where T : struct
        {
            async Task SendWindowProperty(IPlayer player)
            {
                var id = await GetWindowId(player);
                await GetPlayerPacketGenerator(player).WindowProperty(id, property, value);
            }

            return Task.WhenAll(from p in _players select SendWindowProperty(p));
        }

        protected async Task NotifyWindowProperty<T>(IPlayer player, T property, short value)
            where T : struct
        {
            var id = await GetWindowId(player);
            await GetPlayerPacketGenerator(player).WindowProperty(id, property, value);
        }
    }
}
