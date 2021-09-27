using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineCase.Server.Network
{
    public class ServerStatus
    {
        public static readonly int FAVICONWIDTH = 64;
        public static readonly int FAVICONHEIGHT = 64;
        private ChatComponent _description;
        private ServerStatus.Players _players;
        private ServerStatus.Version _version;
        private string _favicon;

        public ChatComponent GetDescription()
        {
            return _description;
        }

        public void SetDescription(ChatComponent desccription)
        {
            _description = desccription;
        }

        public ServerStatus.Players GetPlayers()
        {
            return _players;
        }

        public void SetPlayers(ServerStatus.Players players)
        {
            _players = players;
        }

        public ServerStatus.Version GetVersion()
        {
            return _version;
        }

        public void SetVersion(ServerStatus.Version version)
        {
            _version = version;
        }

        public void SetFavicon(string favicon)
        {
            _favicon = favicon;
        }

        public string GetFavicon()
        {
            return _favicon;
        }

        public class Players
        {
            private readonly int _maxPlayers;
            private readonly int _numPlayers;
            private GameProfile[] _sample;

            public Players(int maxPlayers, int numPlayers)
            {
                _maxPlayers = maxPlayers;
                _numPlayers = numPlayers;
            }

            public int GetMaxPlayers()
            {
                return _maxPlayers;
            }

            public int GetNumPlayers()
            {
                return _numPlayers;
            }

            public GameProfile[] GetSample()
            {
                return _sample;
            }

            public void SetSample(GameProfile[] profile)
            {
                _sample = profile;
            }
        }

        public class Version
        {
            private readonly string _name;
            private readonly int _protocol;

            public Version(string p_134965_, int p_134966_)
            {
                _name = p_134965_;
                _protocol = p_134966_;
            }

            public string GetName()
            {
                return _name;
            }

            public int GetProtocol()
            {
                return _protocol;
            }
        }
    }
}
