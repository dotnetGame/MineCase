using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using MineCase.Protocol.Play;
using MineCase.Server.Game.Windows;
using MineCase.Server.Network;
using MineCase.Server.User;
using MineCase.World;
using Orleans.Concurrency;

namespace MineCase.Server.Game.Entities
{
    public interface IPlayer : IEntity
    {
        Task<uint> GetViewDistance();
    }
}
