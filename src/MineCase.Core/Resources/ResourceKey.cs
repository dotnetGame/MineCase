using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineCase.Resources
{
    public class ResourceKey<T>
    {
        private ResourceLocation _registryName;
        private ResourceLocation _location;

        public ResourceKey(ResourceLocation name, ResourceLocation location)
        {
            _registryName = name;
            _location = location;
        }
    }
}
