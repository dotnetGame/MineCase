using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MineCase.Server.Game.Entities
{
    public interface ICreature : IEntity, ITickable
    {
        Task Spawn(Guid uuid, Vector3 position, MobType type);

        Task OnCreated();

        Task Destroy();

        Task Move(Vector3 pos);

        Task Look(Vector3 pos);
    }
}
