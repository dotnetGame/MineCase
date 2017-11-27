using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MineCase.Engine
{
    [Flags]
    internal enum DependencyPropertyFlags
    {
        /// <summary>
        /// 无
        /// </summary>
        None = 0,

        /// <summary>
        /// 附加属性
        /// </summary>
        Attached = 1,

        /// <summary>
        /// 只读属性
        /// </summary>
        ReadOnly = 2
    }

    /// <summary>
    /// 依赖属性
    /// </summary>
    public abstract class DependencyProperty : IEquatable<DependencyProperty>
    {
        private static int _nextAvailableGlobalId = 0;
        private static readonly ConcurrentDictionary<FromNameKey, DependencyProperty> _fromNameMaps = new ConcurrentDictionary<FromNameKey, DependencyProperty>();
        private static readonly ConcurrentDictionary<string, Type> _ownerTypes = new ConcurrentDictionary<string, Type>();

        /// <summary>
        /// 所有者类型加载器
        /// </summary>
        public static Func<string, Type> OwnerTypeLoader { get; set; } = t => Type.GetType(t);

        /// <summary>
        /// 获取名称
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// 获取所有者类型
        /// </summary>
        public Type OwnerType { get; }

        /// <summary>
        /// 获取属性类型
        /// </summary>
        public abstract Type PropertyType { get; }

        /// <summary>
        /// 获取是否附加属性
        /// </summary>
        public bool IsAttached => Flags.HasFlag(DependencyPropertyFlags.Attached);

        /// <summary>
        /// 获取是否只读属性
        /// </summary>
        public bool IsReadOnly => Flags.HasFlag(DependencyPropertyFlags.ReadOnly);

        internal DependencyPropertyFlags Flags { get; }

        internal abstract IDependencyPropertyHelper Helper { get; }

        private readonly int _globalId;

        internal DependencyProperty(string name, Type ownerType, DependencyPropertyFlags flags)
        {
            Name = name;
            OwnerType = ownerType;
            Flags = flags;
            AddFromeNameKey(name, ownerType);
            _globalId = Interlocked.Increment(ref _nextAvailableGlobalId);
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if (obj is DependencyProperty)
                return Equals((DependencyProperty)obj);
            return false;
        }

        /// <inheritdoc/>
        public bool Equals(DependencyProperty other)
        {
            if (other != null) return _globalId == other._globalId;
            return false;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return _globalId.GetHashCode();
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <typeparam name="T">属性类型</typeparam>
        /// <param name="name">名称</param>
        /// <param name="ownerType">所有者类型</param>
        /// <param name="metadata">元数据</param>
        /// <returns>依赖属性</returns>
        public static DependencyProperty<T> Register<T>(string name, Type ownerType, PropertyMetadata<T> metadata = null)
        {
            return RegisterIntern(name, ownerType, DependencyPropertyFlags.None, metadata ?? new PropertyMetadata<T>(UnsetValue));
        }

        /// <summary>
        /// 注册附加属性
        /// </summary>
        /// <typeparam name="T">属性类型</typeparam>
        /// <param name="name">名称</param>
        /// <param name="ownerType">所有者类型</param>
        /// <param name="metadata">元数据</param>
        /// <returns>依赖属性</returns>
        public static DependencyProperty<T> RegisterAttached<T>(string name, Type ownerType, PropertyMetadata<T> metadata = null)
        {
            return RegisterIntern(name, ownerType, DependencyPropertyFlags.Attached, metadata ?? new PropertyMetadata<T>(UnsetValue));
        }

        private static DependencyProperty<T> RegisterIntern<T>(string name, Type ownerType, DependencyPropertyFlags flag, PropertyMetadata<T> metadata)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));
            if (ownerType == null)
                throw new ArgumentNullException(nameof(ownerType));

            return new DependencyProperty<T>(name, ownerType, flag, metadata ?? new PropertyMetadata<T>(UnsetValue));
        }

        /// <summary>
        /// 从名称获取依赖属性
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="ownerType">所有者类型</param>
        /// <returns>依赖属性</returns>
        public static DependencyProperty FromName(string name, Type ownerType)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));
            if (ownerType == null)
                throw new ArgumentNullException(nameof(ownerType));

            DependencyProperty property = null;
            while (property == null && ownerType != null)
            {
                if (!_fromNameMaps.TryGetValue(new FromNameKey(name, ownerType), out property))
                    ownerType = ownerType.BaseType;
            }

            return property != null ? property : throw new InvalidOperationException($"Property {ownerType.Name}.{name} not found.");
        }

        internal static string OwnerTypeToString(Type type)
        {
            var str = EscapeOwnerTypeString(type);
            if (!_ownerTypes.ContainsKey(str))
                throw new InvalidOperationException($"OwnerType: {type.Name} is not registered.");
            return str;
        }

        internal static Type StringToOwnerType(string str)
        {
            if (!_ownerTypes.TryGetValue(str, out var type))
            {
                var ownerType = UnescapeOwnerTypeString(str);
                System.Runtime.CompilerServices.RuntimeHelpers.RunClassConstructor(ownerType.TypeHandle);
            }

            if (!_ownerTypes.TryGetValue(str, out type))
                throw new ArgumentException($"OwnerType: {str} is not registered.");
            return type;
        }

        private static string EscapeOwnerTypeString(Type type)
        {
            var str = type.FullName;
            return type.ToString().Replace('.', ':');
        }

        private static Type UnescapeOwnerTypeString(string str)
        {
            str = str.Replace(':', '.');
            return OwnerTypeLoader(str);
        }

        /// <summary>
        /// 添加从名称获取
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="ownerType">所有者类型</param>
        protected void AddFromeNameKey(string name, Type ownerType)
        {
            if (!_fromNameMaps.TryAdd(new FromNameKey(name, ownerType), this))
                throw new ArgumentException($"Property {ownerType.Name}.{name} is already registered.");
            else
                _ownerTypes.TryAdd(EscapeOwnerTypeString(ownerType), ownerType);
        }

        /// <summary>
        /// 未设置值
        /// </summary>
        public static readonly UnsetValueType UnsetValue = default(UnsetValueType);

        /// <summary>
        /// 未设置值类型
        /// </summary>
        public struct UnsetValueType
        {
        }

        private struct FromNameKey : IEquatable<FromNameKey>
        {
            public string Name { get; }

            public Type OwnerType { get; }

            private readonly int _hashCode;

            public FromNameKey(string name, Type ownerType)
            {
                Name = name;
                OwnerType = ownerType;
                _hashCode = name.GetHashCode() ^ ownerType.GetHashCode();
            }

            public bool Equals(FromNameKey other)
            {
                return Name == other.Name && OwnerType == other.OwnerType;
            }

            public override int GetHashCode()
            {
                return _hashCode;
            }

            public override bool Equals(object obj)
            {
                if (obj is FromNameKey)
                    return Equals((FromNameKey)obj);
                return false;
            }
        }
    }

    /// <summary>
    /// 依赖属性
    /// </summary>
    /// <typeparam name="T">属性类型</typeparam>
    public sealed class DependencyProperty<T> : DependencyProperty
    {
        private PropertyMetadata<T> _baseMetadata;
        private readonly ConcurrentDictionary<Type, PropertyMetadata<T>> _metadatas = new ConcurrentDictionary<Type, PropertyMetadata<T>>();

        /// <inheritdoc/>
        public override Type PropertyType => typeof(T);

        internal override IDependencyPropertyHelper Helper { get; } = new DependencyPropertyHelper<T>();

        internal DependencyProperty(string name, Type ownerType, DependencyPropertyFlags flags, PropertyMetadata<T> metadata)
            : base(name, ownerType, flags)
        {
            Contract.Assert(metadata != null);

            _baseMetadata = metadata;
        }

        /// <summary>
        /// 重写元数据
        /// </summary>
        /// <param name="type">要重写的目标类型</param>
        /// <param name="metadata">元数据</param>
        public void OverrideMetadata(Type type, PropertyMetadata<T> metadata)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));
            if (metadata == null)
                throw new ArgumentNullException(nameof(metadata));

            bool metadataIsDervied;
            var oldMetadata = GetMetadata(type, out metadataIsDervied);
            metadata = MergeMetadata(metadataIsDervied, oldMetadata, metadata);
            if (type == OwnerType)
            {
                _baseMetadata = metadata;
            }
            else
            {
                _metadatas.AddOrUpdate(type, metadata, (k, old) =>
                {
                    metadata = MergeMetadata(metadataIsDervied, old, metadata);
                    return metadata;
                });
            }
        }

        /// <summary>
        /// 添加所有者
        /// </summary>
        /// <param name="ownerType">所有者类型</param>
        /// <param name="metadata">元数据</param>
        /// <returns>依赖属性</returns>
        public DependencyProperty<T> AddOwner(Type ownerType, PropertyMetadata<T> metadata = null)
        {
            if (ownerType == null)
                throw new ArgumentNullException(nameof(ownerType));

            AddFromeNameKey(Name, ownerType);

            if (metadata != null)
                OverrideMetadata(ownerType, metadata);
            return this;
        }

        /// <summary>
        /// 尝试获取默认值
        /// </summary>
        /// <param name="d">依赖对象</param>
        /// <param name="type">类型</param>
        /// <param name="value">值</param>
        /// <returns>是否获取成功</returns>
        public bool TryGetDefaultValue(DependencyObject d, Type type, out T value)
        {
            return GetMetadata(type).TryGetDefaultValue(d, this, out value);
        }

        internal
#if ECS_SERVER
        Task
#else
        void
#endif
            RaisePropertyChanged(Type type, object sender, PropertyChangedEventArgs<T> e)
        {
#if ECS_SERVER
            return
#endif
            GetMetadata(type).RaisePropertyChanged(sender, e);
        }

        /// <summary>
        /// 获取元数据
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>元数据</returns>
        public PropertyMetadata<T> GetMetadata(Type type)
        {
            bool metadataIsDervied;
            return GetMetadata(type, out metadataIsDervied);
        }

        private PropertyMetadata<T> GetMetadata(Type type, out bool metadataIsDervied)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));

            var cntType = type;
            if (cntType != OwnerType)
            {
                PropertyMetadata<T> metadata;
                while (cntType != null)
                {
                    if (_metadatas.TryGetValue(cntType, out metadata))
                    {
                        metadataIsDervied = true;
                        return metadata;
                    }

                    cntType = cntType.BaseType;
                }
            }

            metadataIsDervied = OwnerType.IsAssignableFrom(type);
            return _baseMetadata;
        }

        private PropertyMetadata<T> MergeMetadata(bool ownerIsDerived, PropertyMetadata<T> oldMetadata, PropertyMetadata<T> newMetadata)
        {
            if (!oldMetadata.GetType().IsAssignableFrom(newMetadata.GetType()))
                throw new InvalidOperationException("The type of new metadata must be derived from the type of old metadata.");
            newMetadata.Merge(oldMetadata, ownerIsDerived);
            return newMetadata;
        }

        /// <summary>
        /// 尝试获取非默认值
        /// </summary>
        /// <param name="d">依赖对象</param>
        /// <param name="type">类型</param>
        /// <param name="value">值</param>
        /// <returns>是否具有非默认值</returns>
        public bool TryGetNonDefaultValue(DependencyObject d, Type type, out T value)
        {
            return GetMetadata(type).TryGetNonDefaultValue(d, this, out value);
        }
    }
}
