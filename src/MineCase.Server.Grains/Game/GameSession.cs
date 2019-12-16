using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MineCase.Protocol.Play;
using MineCase.Server.Game.Entity;
using MineCase.Server.Network;
using MineCase.Server.Network.Play;
using MineCase.Server.Settings;
using MineCase.Server.User;
using MineCase.Server.World;
using MineCase.World;
using Orleans;
using Orleans.Concurrency;

namespace MineCase.Server.Game
{
    [Reentrant]
    internal class GameSession : Grain, IGameSession
    {
        private Dictionary<string, IWorld> _worlds = new Dictionary<string, IWorld>();
        private readonly Dictionary<IUser, UserContext> _users = new Dictionary<IUser, UserContext>();

        public uint NextAvailEId { get; set; }

        private ILogger _logger;

        public GameSession(ILogger<GameSession> logger)
        {
            _logger = logger;
        }

        public override async Task OnActivateAsync()
        {
            await base.OnActivateAsync();

            // add default world, TODO: support multi worlds later
            IWorld defaultWorld = await GrainFactory.GetGrain<WorldAccessorGrain>(0).GetDefaultWorld();
            _worlds.Add(defaultWorld.GetPrimaryKeyString(), defaultWorld);
        }

        public async Task JoinGame(IUser user)
        {
            var sink = await user.GetClientPacketSink();
            var settings = await GrainFactory.GetGrain<IServerSettings>(0).GetSettings();

            if (!_users.ContainsKey(user))
            {
                _users[user] = new UserContext
                {
                    Sink = sink,
                    Player = new Player {
                        EntityId = await NewEntityId(),
                        UUID = Guid.NewGuid(),
                        UserName = await user.GetName(),
                        Position = new EntityWorldPos { X = 0, Y = 60, Z = 0 }
                    }
                };
            }

            // get player
            var player = _users[user].Player;

            // add player to partition
            var pos = await player.GetPosition();
            var chunkPos = pos.ToChunkWorldPos();
            var partitionPos = chunkPos.ToPartitionWorldPos();

            var partition = GrainFactory.GetPartitionGrain<IWorldPartition>(await player.GetWorld(), partitionPos);
            // partition.EnterEntity(player);

            // register this user to partition tracking hub
            HashSet<ChunkWorldPos> trackingHubs = new HashSet<ChunkWorldPos>();
            var aoiRange = settings.ViewDistance;
            for (int d = 0; d <= aoiRange; d++)
            {
                for (int x = -d; x <= d; x++)
                {
                    var z = d - Math.Abs(x);

                    trackingHubs.Add(new ChunkWorldPos(chunkPos.X + x, chunkPos.Z + z).ToPartitionWorldPos());
                    trackingHubs.Add(new ChunkWorldPos(chunkPos.X + x, chunkPos.Z - z).ToPartitionWorldPos());
                }
            }

            foreach (var eachPartition in trackingHubs)
            {
                var trackingHub = GrainFactory.GetPartitionGrain<IPartitionTrackingHub>(await player.GetWorld(), eachPartition);
            }

            // await user.Login(sink.GetPrimaryKey());
            /*
            sink.SendPacket(
                new JoinGame
                {
                    EID = (int)await (await user.GetPlayer()).GetEntityId(),
                    GameMode = ToByte(await user.GetGameMode()),
                    Dimension = (int)Dimension.Overworld,
                    Difficulty = (byte)Difficulty.Easy,
                    LevelType = LevelTypes.Default,
                    MaxPlayers = (byte)settings.MaxPlayers,
                    ReducedDebugInfo = false
                }
            );
            */
            // await user.NotifyLoggedIn();
            // await SendWholePlayersList(user);
        }

        public Task LeaveGame(IUser user)
        {
            _users.Remove(user);
            // return BroadcastRemovePlayerFromList(user);
            return Task.CompletedTask;
        }

        private Task<Chat> CreateStandardChatMessage(string name, string message)
        {
            StringComponent nameComponent = new StringComponent(name);
            nameComponent.ClickEvent = new ChatClickEvent(ClickEventType.SuggestCommand, "/msg " + name);
            nameComponent.HoverEvent = new ChatHoverEvent(HoverEventType.ShowEntity, name);
            nameComponent.Insertion = name;

            // construct message
            StringComponent messageComponent = new StringComponent(message);

            // list
            List<ChatComponent> list = new List<ChatComponent>();
            list.Add(nameComponent);
            list.Add(messageComponent);

            Chat jsonData = new Chat(new TranslationComponent("chat.type.text", list));
            return Task.FromResult(jsonData);
        }

        public Task<int> UserNumber()
        {
            return Task.FromResult(_users.Count);
        }

        private async Task BroadcastRemovePlayerFromList(IUser user)
        {
            var players = new List<IPlayer> { await user.GetPlayer() };
            await Task.WhenAll(from u in _users.Keys select u.RemovePlayerList(players));
        }

        public async Task SendWholePlayersList(IUser user)
        {
            var list = await Task.WhenAll(from p in _users.Keys
                                          select p.GetPlayer());

            await user.UpdatePlayerList(list);
        }

        public Task<uint> NewEntityId()
        {
            var id = NextAvailEId++;
            return Task.FromResult(id);
        }

        private class UserContext
        {
            public IClientboundPacketSink Sink { get; set; }

            public IPlayer Player { get; set; }
        }
    }
}
