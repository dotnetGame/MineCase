using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase.Core.World
{
    public enum WordldTypeId
    {
        Default = 0,
        Flat = 1,
        LargeBiomes = 2,
        Amplified = 3,
        Customized = 4,
        Buffet = 5,
    }

    public class WorldTypes
    {

    }

    public class WorldType
    {
        private readonly int _id;
        private readonly String _name;
        private readonly int _version;
        private bool _isCustomized;
    }
}
