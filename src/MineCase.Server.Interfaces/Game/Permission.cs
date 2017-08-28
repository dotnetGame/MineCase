using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MineCase.Server.Game
{
    /// <summary>
    /// 权限
    /// </summary>
    public class Permission : IEnumerable<Permission>
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

        /// <summary>
        /// Initializes a new instance of the <see cref="Permission"/> class.
        /// 以指定的名称、描述、默认值及子权限构造 <see cref="Permission"/>
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="description">描述，可为 null</param>
        /// <param name="permissionDefaultValue">默认值</param>
        /// <param name="children">子权限，可为 null，为 null 时表示不存在子权限</param>
        /// <exception cref="ArgumentNullException"><paramref name="name"/> 为 null</exception>
        public Permission(
            string name,
            string description = null,
            PermissionDefaultValue permissionDefaultValue = PermissionDefaultValue.True,
            IEnumerable<Permission> children = null)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Description = description;
            DefaultValue = permissionDefaultValue;
            _children = children?.Where(p => p != null).Distinct().ToDictionary(p => p.Name) ??
                        new Dictionary<string, Permission>();
        }

        /// <see cref="GetChild(string)"/>
        public Permission this[string name] => GetChild(name);

        /// <summary>
        /// 以指定的名称获取子权限
        /// </summary>
        /// <param name="name">要查找的名称</param>
        public Permission GetChild(string name) => _children[name];

        /// <summary>
        /// 判断是否包含指定名称的子权限
        /// </summary>
        /// <param name="name">要判断的名称</param>
        public bool ContainsChild(string name) => _children.ContainsKey(name);

        public IEnumerator<Permission> GetEnumerator()
        {
            return _children.Select(p => p.Value).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
