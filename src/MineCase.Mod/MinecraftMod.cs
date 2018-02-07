using MineCase.Mod.common.eventhandler;
using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase.Mod
{
    public class MinecraftMod
    {
        /**
         * The core Forge EventBusses, all events for Forge will be fired on these,
         * you should use this to register all your listeners.
         * This replaces every register*Handler() function in the old version of Forge.
         * TERRAIN_GEN_BUS for terrain gen events
         * ORE_GEN_BUS for ore gen events
         * EVENT_BUS for everything else
         */
        public static readonly EventBus EVENT_BUS = new EventBus();
        public static readonly EventBus TERRAIN_GEN_BUS = new EventBus();
        public static readonly EventBus ORE_GEN_BUS = new EventBus();
        // public static readonly string MC_VERSION = Loader.MC_VERSION;

        static MinecraftMod()
        {
            EVENT_BUS = new EventBus();
            TERRAIN_GEN_BUS = new EventBus();
            ORE_GEN_BUS = new EventBus();
        }
    }
}
