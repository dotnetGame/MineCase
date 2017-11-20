using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MineCase.Engine
{
    public partial class DependencyObject
    {
        public override async Task OnActivateAsync()
        {
            await LoadStateAsync();
            await InitializeComponents();
        }

        public override async Task OnDeactivateAsync()
        {
            await SaveStateAsync();
            await base.OnDeactivateAsync();
        }

        protected virtual Task InitializeComponents()
        {
            return Task.CompletedTask;
        }

        public Task LoadStateAsync()
        {
            return Task.CompletedTask;
        }

        public Task SaveStateAsync()
        {
            return Task.CompletedTask;
        }
    }
}
