using System;
using System.Threading.Tasks;

namespace MineCase.Server.Game
{
    public class CellEntityRef
    {
        public String SpaceKey { get; set; }

        public String CellKey { get; set; }

        public Guid Id { get; set; }
    }
}
