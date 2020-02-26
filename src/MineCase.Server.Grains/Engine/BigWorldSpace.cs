using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Orleans;

namespace MineCase.Server.Engine
{
    public class BigWorldSpace : Grain, IBigWorldSpace
    {
        private Dictionary<Guid, IBigWorldCellApp> _cells = new Dictionary<Guid, IBigWorldCellApp>();

        public override async Task OnActivateAsync()
        {
            await base.OnActivateAsync();
        }

        protected Task<IBigWorldCellApp> CreateCell()
        {
            Guid cellId = Guid.NewGuid();
            var cell = GrainFactory.GetGrain<IBigWorldCellApp>(cellId);
            _cells.Add(cellId, cell);
            cell.Init();
            return Task.FromResult(cell);
        }

        protected async Task DestroyCell(Guid id)
        {
            if (_cells.ContainsKey(id))
            {
                await _cells[id].Destroy();
                _cells.Remove(id);
            }
        }
    }
}
