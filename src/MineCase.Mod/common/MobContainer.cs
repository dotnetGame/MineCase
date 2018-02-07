using MineCase.Mod.common.eventhandler;
using MineCase.Mod.common.versioning;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MineCase.Mod.common
{
    

    public abstract class ModContainer
    {
        public enum Disableable
        {
            YES, RESTART, NEVER, DEPENDENCIES
        }

        /**
         * The globally unique modid for this mod
         */
        public abstract string GetModId();

        /**
         * A human readable name
         */

        public abstract string GetName();

        /**
         * A human readable version identifier
         */
        public abstract string GetVersion();

        /**
         * The location on the file system which this mod came from
         */
        public abstract string GetSource();

        /**
         * The metadata for this mod
         */
        public abstract ModMetadata GetMetadata();

        /**
         * Attach this mod to it's metadata from the supplied metadata collection
         */
        public abstract void BindMetadata(MetadataCollection mc);

        /**
         * Set the enabled/disabled state of this mod
         */
        public abstract void SetEnabledState(bool enabled);

        /**
         * A list of the modids that this mod requires loaded prior to loading
         */
        public abstract List<ArtifactVersion> GetRequirements();

        /**
         * A list of modids that should be loaded prior to this one. The special
         * value <strong>*</strong> indicates to load <em>after</em> any other mod.
         */
        public abstract List<ArtifactVersion> GetDependencies();

        /**
         * A list of modids that should be loaded <em>after</em> this one. The
         * special value <strong>*</strong> indicates to load <em>before</em> any
         * other mod.
         */
        public abstract List<ArtifactVersion> GetDependants();

        /**
         * A representative string encapsulating the sorting preferences for this
         * mod
         */
        public abstract string GetSortingRules();

        /**
         * Register the event bus for the mod and the controller for error handling
         * Returns if this bus was successfully registered - disabled mods and other
         * mods that don't need real events should return false and avoid further
         * processing
         *
         * @param bus
         * @param controller
         */
        public abstract bool RegisterBus(EventBus bus, LoadController controller);

        /**
         * Does this mod match the supplied mod
         *
         * @param mod
         */
        public abstract bool Matches(Object mod);

        /**
         * Get the actual mod object
         */
        public abstract Object GetMod();

        public abstract ArtifactVersion GetProcessedVersion();

        public abstract bool IsImmutable();

        public abstract string GetDisplayVersion();

        public abstract VersionRange AcceptableMinecraftVersionRange();

        public abstract Certificate GetSigningCertificate();

        public static readonly Dictionary<string, string> EMPTY_PROPERTIES = ImmutableMap.of();

        public abstract Dictionary<string, string> GetCustomModProperties();

        // public Class<?> GetCustomResourcePackClass();

        public abstract Dictionary<string, string> GetSharedModDescriptor();

        public abstract Disableable CanBeDisabled();

        public abstract string GetGuiClassName();

        public abstract List<string> GetOwnedPackages();

        public abstract bool ShouldLoadInEnvironment();

        public abstract string GetUpdateUrl();

        public abstract void SetClassVersion(int classVersion);

        public abstract int GetClassVersion();
    }
}
