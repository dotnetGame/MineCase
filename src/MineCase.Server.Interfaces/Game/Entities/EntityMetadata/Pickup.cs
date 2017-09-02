using System;
using System.Collections.Generic;
using System.Text;
using MineCase.Formats;

namespace MineCase.Server.Game.Entities.EntityMetadata
{
    public class Pickup : Entity
    {
        public Slot Item { get; set; } = Slot.Empty;
    }
}
