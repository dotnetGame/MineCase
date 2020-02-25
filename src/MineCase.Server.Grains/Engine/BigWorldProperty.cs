using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace MineCase.Server.Engine
{
    public class BigWorldPropertyEntryBase
    {
        public Type ValueType { get; set; }

        public bool HasDefault { get; set; }

        public int Index { get; set; }
    }

    public class BigWorldPropertyEntry<T> : BigWorldPropertyEntryBase
    {
        public T DefaultValue { get; set; }
    }

    public class BigWorldProperty
    {
        private List<object> _values;
        private Dictionary<string, BigWorldPropertyEntryBase> _info;

        public BigWorldProperty()
        {
            _values = new List<object>();
            _info = new Dictionary<string, BigWorldPropertyEntryBase>();
        }

        public BigWorldPropertyEntry<T> Register<T>(string name)
        {
            if (!_info.ContainsKey(name))
            {
                _info[name] = new BigWorldPropertyEntry<T>
                {
                    ValueType = typeof(T),
                    HasDefault = false,
                    Index = _values.Count
                };
                _values.Add(default(T));
                return (BigWorldPropertyEntry<T>)_info[name];
            }
            else
            {
                throw new ArgumentException("This property is already existing。");
            }
        }

        public BigWorldPropertyEntry<T> Register<T>(string name, T defaultValue)
        {
            if (!_info.ContainsKey(name))
            {
                _info[name] = new BigWorldPropertyEntry<T>
                {
                    ValueType = typeof(T),
                    DefaultValue = defaultValue,
                    HasDefault = true,
                    Index = _values.Count
                };
                _values.Add(default(T));
                return (BigWorldPropertyEntry<T>)_info[name];
            }
            else
            {
                throw new ArgumentException("This property is already existing。");
            }
        }

        public T GetValue<T>(string name)
        {
            if (_info.ContainsKey(name))
            {
                var prop = _info[name];
                return (T)_values[prop.Index];
            }
            else
            {
                throw new IndexOutOfRangeException("Property no found.");
            }
        }

        public T GetValue<T>(BigWorldPropertyEntry<T> prop)
        {
            return (T)_values[prop.Index];
        }

        public void SetValue<T>(string name, T value)
        {
            if (_info.ContainsKey(name))
            {
                var prop = _info[name];
                _values[prop.Index] = value;
            }
            else
            {
                throw new IndexOutOfRangeException("Property no found.");
            }
        }

        public void SetValue<T>(BigWorldPropertyEntry<T> prop, T value)
        {
            _values[prop.Index] = value;
        }
    }
}
