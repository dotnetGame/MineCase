using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineCase.Serialization
{
    public enum GenerateSerializerMethods
    {
        Serialize = 0,
        Deserialize = 1,
        Both = 2
    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public sealed class GenerateSerializerAttribute : Attribute
    {
        public GenerateSerializerMethods Methods { get; set; } = GenerateSerializerMethods.Both;
    }
}
