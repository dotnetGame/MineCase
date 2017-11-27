﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase.Server
{
    internal class StreamProviders
    {
        public const string JobsProvider = "JobsProvider";

        public const string TransientProvider = "TransientProvider";

        public static class Namespaces
        {
            public const string ChunkSender = "ChunkSender";

            public const string TickEmitter = "TickEmitter";
        }
    }
}
