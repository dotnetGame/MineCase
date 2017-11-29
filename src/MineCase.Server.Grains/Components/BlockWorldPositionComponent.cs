using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Engine;
using MineCase.World;

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

        public void SetBlockWorldPosition(BlockWorldPos value) =>
            AttachedObject.SetLocalValue(BlockWorldPositionProperty, value);
    }
}
