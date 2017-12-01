using System;
using System.Collections.Generic;
using System.Text;
using MineCase.Nbt;

namespace MineCase.Server.Game.Entities.EntityMetadata
{
    public class Player : Living
    {
        public float AdditionalHearts { get; set; }

        public uint Score { get; set; }

        public bool CapeEnabled { get; set; }

        public bool JacketEnabled { get; set; }

        public bool LeftSleeveEnabled { get; set; }

        public bool RightSleeveEnabled { get; set; }

        public bool LeftPantsLegEnabled { get; set; }

        public bool RightPantsLegEnabled { get; set; }

        public bool HatEnabled { get; set; }

        public byte MainHand { get; set; } = 1;

        public NbtFile LeftShoulder { get; set; }

        public NbtFile RightShoulder { get; set; }
    }
}
