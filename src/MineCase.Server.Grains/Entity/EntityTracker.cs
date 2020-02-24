using System;
using System.Collections.Generic;
using System.Text;
using MineCase.Server.Entity.Player;
using MineCase.Util.Math;

namespace MineCase.Server.Entity
{
    public class EntityTracker
    {
        private Entity _entity;
        private int _range;
        private SectionPos _pos;
        private HashSet<PlayerEntity> _trackingPlayers = new HashSet<PlayerEntity>();
    }
}
