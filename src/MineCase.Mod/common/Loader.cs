using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase.Mod.common
{
    public class Loader
    {
        // public static readonly String MC_VERSION = net.minecraftforge.common.ForgeVersion.mcVersion;
        /**
         * The singleton instance
         */
        private static Loader instance;
        /**
         * Build information for tracking purposes.
         */
        private static String major;
        private static String minor;
        private static String rev;
        private static String build;
        private static String mccversion;
        private static String mcpversion;

        /**
         * The class loader we load the mods into.
         */
        private ModClassLoader modClassLoader;
        /**
         * The sorted list of mods.
         */
        private List<ModContainer> mods;
        /**
         * A named list of mods
         */
        private Dictionary<String, ModContainer> namedMods;
        /**
         * A reverse dependency graph for mods
         */
        private ListMultimap<String, String> reverseDependencies;
        /**
         * The canonical configuration directory
         */
        private File canonicalConfigDir;
        private File canonicalModsDir;
        private LoadController modController;
        private MinecraftDummyContainer minecraft;
        private MCPDummyContainer mcp;

        private static File minecraftDir;
        private static List<String> injectedContainers;
        private ImmutableMap<String, String> fmlBrandingProperties;
        private File forcedModFile;
        private ModDiscoverer discoverer;
        private ProgressBar progressBar;

        public static Loader GetInstance()
        {
            if (instance == null)
            {
                instance = new Loader();
            }

            return instance;
        }

        public static void injectData(Object[] data)
        {
            major = (String)data[0];
            minor = (String)data[1];
            rev = (String)data[2];
            build = (String)data[3];
            mccversion = (String)data[4];
            mcpversion = (String)data[5];
            minecraftDir = (string)data[6];
            injectedContainers = (List<String>)data[7];
        }

        private Loader()
        {
            modClassLoader = new ModClassLoader(getClass().getClassLoader());
            if (mccversion != null && !mccversion.equals(MC_VERSION))
            {
                throw new LoaderException(String.Format("This version of FML is built for Minecraft %s, we have detected Minecraft %s in your minecraft jar file", mccversion, MC_VERSION));
            }

            minecraft = new MinecraftDummyContainer(MC_VERSION);
            InputStream mcpModInputStream = getClass().getResourceAsStream("/mcpmod.info");
            try
            {
                mcp = new MCPDummyContainer(MetadataCollection.from(mcpModInputStream, "MCP").getMetadataForId("mcp", null));
            }
            finally
            {
                IOUtils.closeQuietly(mcpModInputStream);
            }
        }
    }
}
