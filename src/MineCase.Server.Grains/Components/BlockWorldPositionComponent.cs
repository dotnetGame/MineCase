using MineCase.Engine;
using MineCase.World;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MineCase.Server.Components
{
    internal class BlockWorldPositionComponent : Component
    {
        public static readonly DependencyProperty<BlockWorldPos> BlockWorldPositionProperty =
            DependencyProperty.Register<BlockWorldPos>("BlockWorldPosition", typeof(BlockWorldPositionComponent));

        public BlockWorldPos BlockWorldPosition => AttachedObject.GetValue(BlockWorldPositionProperty);

        public BlockWorldPositionComponent(string name = "blockWorldPosition")
            : base(name)
        {
        }

        public Task SetBlockWorldPosition(BlockWorldPos value) =>
            AttachedObject.SetLocalValue(BlockWorldPositionProperty, value);
    }
}
