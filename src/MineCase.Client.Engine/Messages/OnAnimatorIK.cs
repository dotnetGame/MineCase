using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineCase.Engine.Messages
{
    /// <summary>
    /// IK
    /// </summary>
    public sealed class OnAnimatorIK : IEntityMessage
    {
        /// <summary>
        /// LyaerIndex
        /// </summary>
        public int LayerIndex { get; set; }
    }
}
