using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace MineCase.Server.Engine
{
    public class BigWorldObject
    {
        public BigWorldProperty Properties { get; set; }

        public Dictionary<string, MethodInfo> _methodTable;

        public BigWorldObject()
        {
            Properties = new BigWorldProperty();
            _methodTable = new Dictionary<string, MethodInfo>();
            RegisterAllMethods();
        }

        public void RegisterAllMethods()
        {
            MethodInfo[] methods = GetType().GetMethods();
            foreach (var eachMethod in methods)
            {
                _methodTable.Add(eachMethod.Name, eachMethod);
            }
        }

        public T GetValue<T>(string name)
        {
            return Properties.GetValue<T>(name);
        }

        public T GetValue<T>(BigWorldPropertyEntry<T> prop)
        {
            return Properties.GetValue<T>(prop);
        }

        public void SetValue<T>(string name, T value)
        {
            Properties.SetValue(name, value);
        }

        public void SetValue<T>(BigWorldPropertyEntry<T> property, T value)
        {
            Properties.SetValue(property, value);
        }
    }
}
