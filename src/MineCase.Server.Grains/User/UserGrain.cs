using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MineCase.Protocol;
using MineCase.Protocol.Login;
using MineCase.Protocol.Play;
using MineCase.Server.Game.Entity;
using MineCase.Server.Network;
using MineCase.Server.World;
using MineCase.World;
using Orleans;

namespace MineCase.Server.User
{
    public class UserGrain : Grain, IUser
    {
        private uint _protocolVersion;

        private bool _online;

        private Guid _sessionId = Guid.Empty;

        private IWorld _world = null;

        private EntityWorldPos _position = new EntityWorldPos { X = 0.0f, Y = 0.0f, Z = 0.0f };

        private HashSet<ChunkWorldPos> _activeChunks = new HashSet<ChunkWorldPos>();

        private IClientboundPacketSink _sink;

        private Player _player;

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

        public Task SetPosition(EntityWorldPos pos)
        {
            _position = pos;

            HashSet<ChunkWorldPos> newChunksView = GetChunksInViewRange(_position.ToChunkWorldPos(), 8);
            var chunksDiff = newChunksView.Except(_activeChunks);
            return SendChunkData(chunksDiff);
        }

        private static HashSet<ChunkWorldPos> GetChunksInViewRange(ChunkWorldPos pos, int range)
        {
            HashSet<ChunkWorldPos> ret = new HashSet<ChunkWorldPos>();
            for (int d = 0; d <= range; d++)
            {
                for (int x = -d; x <= d; x++)
                {
                    var z = d - Math.Abs(x);

                    ret.Add(new ChunkWorldPos(pos.X + x, pos.Z + z));
                    ret.Add(new ChunkWorldPos(pos.X + x, pos.Z - z));
                }
            }

            return ret;
        }

        private async Task SendChunkData(IEnumerable<ChunkWorldPos> changeChunks)
        {
            var sink = GrainFactory.GetGrain<IClientboundPacketSink>(_sessionId);
            foreach (var chunkPos in changeChunks)
            {
                var partitionPos = GetPartitionPos(chunkPos);
                var partitionKey = WorldPartitionGrain.MakeAddressByPartitionKey(_world, partitionPos);
                var partition = GrainFactory.GetGrain<IWorldPartition>(partitionKey);

                // TODO
                var chunkColumn = await partition.GetState(chunkPos);
                await sink.SendPacket(PacketFactory.ChunkDataPacket(chunkColumn, 65535));
            }
        }

        public static ChunkWorldPos GetPartitionPos(ChunkWorldPos pos)
        {
            ChunkWorldPos ret = new ChunkWorldPos { X = WorldPartitionGrain.PartitionSize, Z = WorldPartitionGrain.PartitionSize };
            if (pos.X >= 0)
                ret.X *= pos.X / WorldPartitionGrain.PartitionSize;
            else
                ret.X *= -(((-pos.X - 1) / WorldPartitionGrain.PartitionSize) + 1);

            if (pos.Z >= 0)
                ret.Z *= pos.Z / WorldPartitionGrain.PartitionSize;
            else
                ret.Z *= -(((-pos.Z - 1) / WorldPartitionGrain.PartitionSize) + 1);

            return ret;
        }

        public Task<uint> GetProtocolVersion()
        {
            return Task.FromResult(_protocolVersion);
        }

        public Task SetProtocolVersion(uint version)
        {
            _protocolVersion = version;
            return Task.CompletedTask;
        }

        public Task<IWorld> GetWorld()
        {
            return Task.FromResult(_world);
        }

        public Task SetWorld(IWorld world)
        {
            _world = world;
            return Task.CompletedTask;
        }

        public Task<IClientboundPacketSink> GetClientPacketSink()
        {
            return Task.FromResult(_sink);
        }

        public Task SetClientPacketSink(IClientboundPacketSink sink)
        {
            _sink = sink;
            return Task.CompletedTask;
        }

        public Task<IPlayer> GetPlayer()
        {
            return Task.FromResult((IPlayer)_player);
        }

        public async Task UpdatePlayerList(IReadOnlyList<IPlayer> desc)
        {
            foreach (var player in desc)
            {
                var uuid = await player.GetUUID();
                var name = await player.GetUserName();
                var gamemode = await player.GetGamemode();
                var ping = await player.GetPing();
                await _sink.SendPacket(new PlayerListItem<PlayerListItemAddPlayerAction>
                {
                    Action = 0,
                    NumberOfPlayers = (uint)desc.Count,
                    Players = (from d in desc
                               select new PlayerListItemAddPlayerAction
                               {
                                   UUID = uuid,
                                   Name = name,
                                   NumberOfProperties = 0,
                                   GameMode = gamemode.ToByte(),
                                   Ping = ping,
                                   HasDisplayName = false
                               }).ToArray()
                });
            }
        }

        public async Task RemovePlayerList(IReadOnlyList<IPlayer> desc)
        {
            foreach (var player in desc)
            {
                var uuid = await player.GetUUID();
                await _sink.SendPacket(new PlayerListItem<PlayerListItemRemovePlayerAction>
                {
                    Action = 4,
                    NumberOfPlayers = (uint)desc.Count,
                    Players = (from d in desc
                               select new PlayerListItemRemovePlayerAction
                               {
                                   UUID = uuid
                               }).ToArray()
                });
            }
        }
    }
}
