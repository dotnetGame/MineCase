using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using MineCase.Client.Messages;
using MineCase.Client.Network;
using MineCase.Client.Network.Handshaking;
using MineCase.Client.Network.Login;
using MineCase.Client.Network.Status;
using MineCase.Engine;
using Newtonsoft.Json;
using UnityEngine;

namespace MineCase.Client
{
    /// <summary>
    /// 服务器管理器
    /// </summary>
    public class ServerManager : SmartBehaviour
    {
        private Dictionary<int, SessionScope> _sessionScopes;

        public Material TerrainMaterial;

        protected override void Awake()
        {
            base.Awake();
            _sessionScopes = new Dictionary<int, SessionScope>();
        }

        private void Start()
        {
            LoadTextures();
            ConnectServer(0, false);
        }

        private async void ConnectServer(int id, bool status = true)
        {
            var session = new SessionScope
            {
                Name = id.ToString(),
                ServerAddress = "gs.sunnycase.moe",
                ServerPort = 25565
            };
            var scope = ServiceProvider.BeginLifetimeScope(c =>
            {
                c.RegisterInstance(session);
            });
            session.ServiceProvider = scope;
            _sessionScopes[id] = session;
            var clientSession = scope.Resolve<ClientSession>();
            var handler = status ? (EventHandler)scope.Resolve<StatusFlow>().OnClientSessionConnected : scope.Resolve<LoginFlow>().OnClientSessionConnected;
            clientSession.Connected += handler;
            await clientSession.Startup(default);
            _sessionScopes.Remove(id);
        }

        private class StatusFlow
        {
            private readonly IHandshakingPacketGenerator _handshakingPacket;
            private readonly IStatusPacketGenerator _statusPacket;

            public StatusFlow(IHandshakingPacketGenerator handshakingPacket, IStatusPacketGenerator statusPacket)
            {
                _handshakingPacket = handshakingPacket;
                _statusPacket = statusPacket;
            }

            public async void OnClientSessionConnected(object sender, EventArgs e)
            {
                var random = new System.Random();
                var clientSession = (ClientSession)sender;
                Debug.Log($"Connected: {clientSession.SessionScope.Name}");
                await _handshakingPacket.Handshake(MineCase.Protocol.Protocol.Version, clientSession.SessionScope.ServerAddress, (ushort)clientSession.SessionScope.ServerPort, 1);
                Debug.Log("Handshaked");

                Debug.Log(JsonConvert.DeserializeObject<dynamic>(await _statusPacket.Request()).description.text);

                var stopwatch = new System.Diagnostics.Stopwatch();
                stopwatch.Restart();
                await _statusPacket.Ping(random.Next());
                stopwatch.Stop();
                Debug.Log("Ping: " + stopwatch.ElapsedMilliseconds + "ms");
            }
        }

        private class LoginFlow
        {
            private readonly IHandshakingPacketGenerator _handshakingPacket;
            private readonly ILoginPacketGenerator _loginPacket;
            private readonly IEventAggregator _eventAggregator;

            public LoginFlow(IHandshakingPacketGenerator handshakingPacket, ILoginPacketGenerator loginPacket, IEventAggregator eventAggregator)
            {
                _handshakingPacket = handshakingPacket;
                _loginPacket = loginPacket;
                _eventAggregator = eventAggregator;
            }

            public async void OnClientSessionConnected(object sender, EventArgs e)
            {
                var random = new System.Random();
                var clientSession = (ClientSession)sender;
                Debug.Log($"Connected: {clientSession.SessionScope.Name}");
                await _handshakingPacket.Handshake(MineCase.Protocol.Protocol.Version, clientSession.SessionScope.ServerAddress, (ushort)clientSession.SessionScope.ServerPort, 2);
                Debug.Log("Handshaked");

                switch (await _loginPacket.LoginStart("chino"))
                {
                    case LoginStartDisconnectResponse disconnect:
                        Debug.Log(disconnect.Reason);
                        break;
                    case LoginStartSuccessResponse success:
                        Debug.Log("Login Success: " + success.User.UserName + ", " + success.User.UUID);
                        _eventAggregator.PublishOnMainThread(new UserLoggedInMessage
                        {
                            SessionScope = clientSession.SessionScope,
                            User = success.User
                        });
                        break;
                }
            }
        }

        private static readonly string[] _textureNames = new[]
        {
            "stone",
            "grass_side",
            "grass_top",
            "dirt",
            "water_overlay",
            "planks_oak",
            "log_oak",
            "leaves_oak"
        };

        public static Dictionary<string, Vector2Int> TextureOffsets;

        public static Texture2D Terrain;
        public static Vector2Int TerrainSize;

        private void LoadTextures()
        {
            TextureOffsets = new Dictionary<string, Vector2Int>();

            const int maxWidth = 1024;
            var height = (int)Math.Ceiling((float)_textureNames.Length / (maxWidth / 16)) * 16;
            var texture = new Texture2D(maxWidth, height, TextureFormat.ARGB32, true)
            {
                filterMode = FilterMode.Point
            };

            Debug.Log("Height: " + height);

            int x = 0, y = 0;
            foreach (var texName in _textureNames)
            {
                var tex = Resources.Load<Texture2D>("textures/blocks/" + texName);
                texture.SetPixels(x, y, 16, 16, tex.GetPixels());
                TextureOffsets.Add(texName, new Vector2Int(x, y));
                x += 16;

                if (x >= maxWidth)
                {
                    y += 16;
                    x = 0;
                }
            }

            texture.Apply();

            Terrain = texture;
            TerrainSize = new Vector2Int(texture.width, texture.height);
            TerrainMaterial.mainTexture = texture;
        }

        private void OnGUI()
        {
            GUI.DrawTexture(new Rect(0, 0, 1024, 16), Terrain);
        }

        internal class Module : Autofac.Module
        {
            protected override void Load(ContainerBuilder builder)
            {
                builder.RegisterType<StatusFlow>();
                builder.RegisterType<LoginFlow>();
            }
        }
    }
}
