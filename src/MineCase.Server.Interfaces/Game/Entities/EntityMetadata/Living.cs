using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase.Server.Game.Entities.EntityMetadata
{
    public class Living : Entity
    {
        public bool IsHandActive { get; set; }

        public SwingHandState ActiveHand { get; set; }

        public float Health { get; set; }

        public uint PotionEffectColor { get; set; }

        public bool IsPotionEffectAmbient { get; set; }

        public uint NumberOfArrows { get; set; }
    }
}
