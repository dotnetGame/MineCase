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
        None = 0,
        Attached = 1,
        ReadOnly = 2
    }

    public abstract class DependencyProperty : IEquatable<DependencyProperty>
    {
        private static int _nextAvailableGlobalId = 0;
        private static readonly ConcurrentDictionary<FromNameKey, DependencyProperty> _fromNameMaps = new ConcurrentDictionary<FromNameKey, DependencyProperty>();

        public string Name { get; }

        public Type OwnerType { get; }

        public abstract Type PropertyType { get; }

        public bool IsAttached => Flags.HasFlag(DependencyPropertyFlags.Attached);

        public bool IsReadOnly => Flags.HasFlag(DependencyPropertyFlags.ReadOnly);

        internal DependencyPropertyFlags Flags { get; }

        private readonly int _globalId;

        internal DependencyProperty(string name, Type ownerType, DependencyPropertyFlags flags)
        {
            Name = name;
            OwnerType = ownerType;
            Flags = flags;
            AddFromeNameKey(name, ownerType);
            _globalId = Interlocked.Increment(ref _nextAvailableGlobalId);
        }

        public override bool Equals(object obj)
        {
            if (obj is DependencyProperty)
                return Equals((DependencyProperty)obj);
            return false;
        }

        public bool Equals(DependencyProperty other)
        {
            if (other != null) return _globalId == other._globalId;
            return false;
        }

        public override int GetHashCode()
        {
            return _globalId.GetHashCode();
        }

        public static DependencyProperty<T> Register<T>(string name, Type ownerType, PropertyMetadata<T> metadata = null)
        {
            return RegisterIntern(name, ownerType, DependencyPropertyFlags.None, metadata ?? new PropertyMetadata<T>(UnsetValue));
        }

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
                    ownerType = ownerType.GetType().BaseType;
            }

            return property != null ? property : throw new InvalidOperationException($"Property {ownerType.Name}.{name} not found.");
        }

        protected void AddFromeNameKey(string name, Type ownerType)
        {
            if (!_fromNameMaps.TryAdd(new FromNameKey(name, ownerType), this))
                throw new ArgumentException($"Property {ownerType.Name}.{name} is already registered.");
        }

        public static readonly UnsetValueType UnsetValue = default(UnsetValueType);

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

    public sealed class DependencyProperty<T> : DependencyProperty
    {
        private PropertyMetadata<T> _baseMetadata;
        private readonly ConcurrentDictionary<Type, PropertyMetadata<T>> _metadatas = new ConcurrentDictionary<Type, PropertyMetadata<T>>();

        public override Type PropertyType => typeof(T);

        internal DependencyProperty(string name, Type ownerType, DependencyPropertyFlags flags, PropertyMetadata<T> metadata)
            : base(name, ownerType, flags)
        {
            Contract.Assert(metadata != null);

            _baseMetadata = metadata;
        }

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

        public DependencyProperty<T> AddOwner(Type ownerType, PropertyMetadata<T> metadata = null)
        {
            if (ownerType == null)
                throw new ArgumentNullException(nameof(ownerType));

            AddFromeNameKey(Name, ownerType);

            if (metadata != null)
                OverrideMetadata(ownerType, metadata);
            return this;
        }

        public bool TryGetDefaultValue(DependencyObject d, Type type, out T value)
        {
            return GetMetadata(type).TryGetDefaultValue(d, this, out value);
        }

        internal Task RaisePropertyChanged(Type type, object sender, PropertyChangedEventArgs<T> e)
        {
            return GetMetadata(type).RaisePropertyChanged(sender, e);
        }

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

        public bool TryGetNonDefaultValue(DependencyObject d, Type type, out T value)
        {
            return GetMetadata(type).TryGetNonDefaultValue(d, this, out value);
        }
    }
}
