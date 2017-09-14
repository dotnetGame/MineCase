using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Orleans;

namespace MineCase.Server.Game.CreatureEntities
{
    public interface ICreatureEntity : IGrainWithStringKey
    {
        Task OnCreated();

        Task Destroy();
    }
}
