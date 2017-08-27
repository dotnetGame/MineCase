using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MineCase.Server.Game
{
    /// <summary>
    /// 权限
    /// </summary>
    public class Permission
    {
        /// <summary>
        /// Gets 该权限的名称
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets 该权限的描述，可为 null
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// Gets or sets 该权限的默认值
        /// </summary>
        public PermissionDefaultValue DefaultValue { get; set; }

        private readonly Dictionary<string, Permission> _children;

        public Permission(
            string name,
            string description = null,
            PermissionDefaultValue permissionDefaultValue = PermissionDefaultValue.True,
            IEnumerable<Permission> children = null)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Description = description;
            DefaultValue = permissionDefaultValue;
            _children = children.ToDictionary(
                            p => p?.Name ?? throw new ArgumentException("子权限不得为 null", nameof(children))) ??
                        new Dictionary<string, Permission>();
        }
    }
}
