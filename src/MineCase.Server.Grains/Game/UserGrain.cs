using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Protocol.Login;
using MineCase.Protocol.Play;
using MineCase.Server.Grains.World;
using MineCase.Server.Interfaces.Game;
using MineCase.Server.Interfaces.World;
using MineCase.Server.Network;
using MineCase.World;
using Orleans;

namespace MineCase.Server.Grains.Game
{
    public class UserGrain : Grain, IUser
    {
        private bool _online = false;

        private Guid _sessionId = Guid.Empty;

        private string _world = "overworld";

        private HashSet<ChunkWorldPos> _activeChunks = new HashSet<ChunkWorldPos>();

        public Task<string> GetName()
        {
            return Task.FromResult(this.GetPrimaryKeyString());
        }

        public async Task Login(Guid sessionId)
        {
            _online = true;
            _sessionId = sessionId;
            var packet = new LoginSuccess { UUID = sessionId.ToString(), Username = this.GetPrimaryKeyString() };
            await GrainFactory.GetGrain<IClientboundPacketSink>(sessionId).SendPacket(packet);
        }

        public Task Logout()
        {
            _online = false;
            GrainFactory.GetGrain<IPacketRouter>(_sessionId).Close().Ignore();
            return Task.CompletedTask;
        }

        public async Task SendChunkData(HashSet<ChunkWorldPos> changeChunks)
        {
            var sink = GrainFactory.GetGrain<IClientboundPacketSink>(_sessionId);
            foreach (var chunkPos in changeChunks)
            {
                var partitionPos = GetPartitionPos(chunkPos);
                var partitionKey = WorldPartitionGrain.MakeAddressByPartitionKey(_world, partitionPos.ToBlockWorldPos());
                var partition = GrainFactory.GetGrain<IWorldPartition>(partitionKey);

                // TODO
                /*
                var chunkColumn = await partition.GetState(chunkPos);
                await sink.SendPacket(new ChunkData
                    {
                        ChunkX = chunkPos.X,
                        ChunkZ = chunkPos.Z,
                        GroundUpContinuous = chunkColumn.Biomes != null,
                        Biomes = chunkColumn.Biomes,
                        PrimaryBitMask = chunkColumn.SectionBitMask,
                        NumberOfBlockEntities = 0,
                        Data = (from c in chunkColumn.Sections
                                where c != null
                                select new Protocol.Play.ChunkSection
                                {
                                    PaletteLength = 0,
                                    BitsPerBlock = c.BitsPerBlock,
                                    SkyLight = c.SkyLight.Storage,
                                    BlockLight = c.BlockLight.Storage,
                                    DataArray = c.Data.Storage
                                }).ToArray()
                    });
                    */
            }
        }

        public static ChunkWorldPos GetPartitionPos(ChunkWorldPos pos)
        {
            // TODO
            return new ChunkWorldPos { X = 0, Z = 0 };
        }
    }
}
