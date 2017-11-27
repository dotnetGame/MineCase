using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineCase.Engine
{
    public partial class DependencyObject
    {
        private void Update()
        {
            Tell(Messages.Update.Default);
        }

        private void OnAnimatorIK(int layerIndex)
        {
            Tell(new Messages.OnAnimatorIK { LayerIndex = layerIndex });
        }
    }
}
