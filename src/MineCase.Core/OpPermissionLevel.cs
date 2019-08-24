using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase.Server.Game
{
    /// <summary>
    /// 1 - Ops can bypass spawn protection.
    /// 2 - Ops can use all singleplayer cheats commands(except /publish, as it is not on servers; along with /debug) and use command blocks.Command blocks and Realms operators/owners have this level.
    /// 3 - Ops can use most multiplayer-exclusive commands (all except level 4 commands).
    /// 4 - Ops can use all commands including /stop, /save-all, /save-on, and /save-off.
    /// </summary>
    public enum OpPermissionLevel : byte
    {
        BypassSpawnProtection = 1,
        SingleplayerCommands = 2,
        MultiplayerCommands = 3,
        AllCommands = 4
    }
}
