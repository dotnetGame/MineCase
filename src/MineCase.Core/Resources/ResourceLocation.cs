using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineCase.Resources
{
    public class ResourceLocation
    {
        public static readonly char NamespaceSeparator = ':';
        public static readonly string DefaultNamespace = "minecraft";
        public static readonly string RealmsNamespace = "realms";

        protected readonly string _namespace;
        protected readonly string _path;

        protected ResourceLocation(string[] parts)
        {
            _namespace = (parts[0] == null || parts[0] == "") ? "minecraft" : parts[0];
            _path = parts[1];
            if (!IsValidNamespace(_namespace))
            {
                throw new ArgumentException("Non [a-z0-9_.-] character in namespace of location: " + _namespace + ":" + _path);
            }
            else if (!IsValidPath(_path))
            {
                 throw new ArgumentException("Non [a-z0-9/._-] character in path of location: " + _namespace + ":" + _path);
            }
        }

        public ResourceLocation(string location)
            : this(Decompose(location, ':'))
        {
        }

        public ResourceLocation(string space, string path)
            : this(new string[] { space, path })
        {
        }

        public override string ToString()
        {
            return _namespace + ":" + _path;
        }

        public static ResourceLocation Of(string space, char path)
        {
            return new ResourceLocation(Decompose(space, path));
        }

        protected static string[] Decompose(string space, char path)
        {
            string[] astring = new string[] { "minecraft", space };
            int i = space.IndexOf(path);
            if (i >= 0)
            {
                astring[1] = space.Substring(i + 1, space.Length);
                if (i >= 1)
                {
                    astring[0] = space.Substring(0, i);
                }
            }

            return astring;
        }

        public static bool IsAllowedInResourceLocation(char c)
        {
            return (c >= '0' && c <= '9')
                || (c >= 'a' && c <= 'z')
                || c == '_' || c == ':' || c == '/' || c == '.' || c == '-';
        }

        public static bool ValidPathChar(char c)
        {
            return c == '_' || c == '-' || (c >= 'a' && c <= 'z') || (c >= '0' && c <= '9') || c == '/' || c == '.';
        }

        private static bool ValidNamespaceChar(char c)
        {
            return c == '_' || c == '-' || (c >= 'a' && c <= 'z') || (c >= '0' && c <= '9') || c == '.';
        }

        private static bool IsValidPath(string path)
        {
            for (int i = 0; i < path.Length; ++i)
            {
                if (!ValidPathChar(path[i]))
                {
                    return false;
                }
            }

            return true;
        }

        private static bool IsValidNamespace(string space)
        {
            for (int i = 0; i < space.Length; ++i)
            {
                if (!ValidNamespaceChar(space[i]))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
