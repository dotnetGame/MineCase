using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Orleans;

namespace MineCase.Server.Interfaces.World
{
    public interface IWorld : IGrainWithStringKey, IGrain
    {
        Task OnTick();
    }
}
