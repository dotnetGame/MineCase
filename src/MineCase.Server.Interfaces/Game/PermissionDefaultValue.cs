using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase.Server.Game
{
    /// <summary>
    /// 权限 (<see cref="Permission"/>) 的默认值
    /// </summary>
    public enum PermissionDefaultValue
    {
        /// <summary>
        /// 对所有人都启用
        /// </summary>
        True,

        /// <summary>
        /// 对 Operator 启用
        /// </summary>
        TrueIfOp,

        /// <summary>
        /// 对所有人都禁用
        /// </summary>
        False
    }
}
