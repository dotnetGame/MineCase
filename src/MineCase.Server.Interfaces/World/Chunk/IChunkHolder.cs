using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Game.Server;

namespace MineCase.Server.World.Chunk
{
    public interface IChunkHolder : IAddressByPartition
    {
        Task Subscribe(IGameSession gameSession);

        Task Unsubscribe(IGameSession gameSession);

        Task<IChunk> Load();

        Task Save(IChunk chunk);
    }
}
