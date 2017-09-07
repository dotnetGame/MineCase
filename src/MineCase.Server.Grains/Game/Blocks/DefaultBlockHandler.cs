﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase.Server.Game.Blocks
{
    public class DefaultBlockHandler : BlockHandler
    {
        public DefaultBlockHandler()
            : base(0)
        {
        }

        public override bool IsUsable => false;
    }
}
