using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MineCase.Server.Game.Entities
{
    public interface ICollectable : IEntity
    {
        Task CollectBy(IPlayer player);

        Task Register();
    }
}
