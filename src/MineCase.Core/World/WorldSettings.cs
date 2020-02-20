using MineCase.Core.World;
using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase.World
{
    public class WorldSettings
    {
        private readonly long _seed;
        private readonly GameType _gameType;
        private readonly bool _mapFeaturesEnabled;
        private readonly bool _hardcoreEnabled;
        private readonly WorldType _terrainType;
        private bool _commandsAllowed;
        private bool _bonusChestEnabled;
       //  private JsonElement generatorOptions = new JsonObject();
    }
}
