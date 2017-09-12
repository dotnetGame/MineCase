using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MineCase.Server.Game.Entities
{
    public interface IPickup : IEntity, ICollectable
    {
        Task Spawn(Guid uuid, Vector3 position);

        Task SetItem(Slot item);
    }
}
