﻿using MineCase.Engine;
using MineCase.Server.Game.Windows;
using MineCase.Server.Network.Play;
using Orleans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineCase.Server.Game.Entities.Components
{
    internal class WindowManagerComponent : Component<PlayerGrain>
    {
        private Dictionary<byte, WindowContext> _windows;

        public WindowManagerComponent(string name = "windowManager")
            : base(name)
        {
        }

        protected override Task OnAttached()
        {
            _windows = new Dictionary<byte, WindowContext>
            {
                { 0, new WindowContext { Window = AttachedObject.GetComponent<InventoryComponent>().GetInventoryWindow() } }
            };
            return base.OnAttached();
        }

        public async Task ClickWindow(byte windowId, short slot, ClickAction clickAction, short actionNumber, Slot clickedItem)
        {
            var window = GetWindow(windowId);
            await window.Window.Click(AttachedObject, slot, clickAction, clickedItem);
            await AttachedObject.GetComponent<ClientboundPacketComponent>().GetGenerator()
                .ConfirmTransaction(windowId, window.ActionNumber++, true);
        }

        public async Task CloseWindow(byte windowId)
        {
            await GetWindow(windowId).Window.Close(AttachedObject);
            if (windowId != 0)
                _windows.Remove(windowId);
        }

        public async Task OpenWindow(IWindow window)
        {
            var id = (from w in _windows
                      where w.Value.Window.GetPrimaryKey() == window.GetPrimaryKey()
                      select (byte?)w.Key).FirstOrDefault();
            if (id == null)
            {
                for (byte i = 1; i <= byte.MaxValue; i++)
                {
                    if (!_windows.ContainsKey(i))
                    {
                        id = i;
                        _windows.Add(i, new WindowContext { Window = window });
                        break;
                    }
                }
            }

            if (id != null)
                await window.OpenWindow(AttachedObject);
        }

        private WindowContext GetWindow(byte windowId)
        {
            return _windows[windowId];
        }

        private class WindowContext
        {
            public IWindow Window;
            public short ActionNumber;
        }
    }
}
