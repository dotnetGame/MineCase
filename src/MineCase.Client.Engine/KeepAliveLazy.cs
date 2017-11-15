using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace MineCase.Engine
{
    /// <summary>
    /// 防止对象被回收的延迟初始化器
    /// </summary>
    /// <typeparam name="T">对象类型</typeparam>
    public sealed class KeepAliveLazy<T> : MonoBehaviour
        where T : MonoBehaviour
    {
        /// <summary>
        /// 获取值
        /// </summary>
        public T Value => _containerObject.Value.Value;

        private readonly Lazy<ValuePair> _containerObject;

        /// <summary>
        /// Initializes a new instance of the <see cref="KeepAliveLazy{T}"/> class.
        /// </summary>
        public KeepAliveLazy()
            : this(false)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="KeepAliveLazy{T}"/> class.
        /// </summary>
        /// <param name="isThreadSafe">是否线程安全</param>
        public KeepAliveLazy(bool isThreadSafe)
        {
            _containerObject = new Lazy<ValuePair>(
                () =>
                {
                    var containerObject = new GameObject(typeof(T).Name, typeof(T));
                    DontDestroyOnLoad(containerObject);
                    return new ValuePair { Container = containerObject, Value = containerObject.GetComponent<T>() };
                }, isThreadSafe);
        }

        private void OnApplicationQuit()
        {
            if (_containerObject.IsValueCreated && _containerObject.Value.Container)
                Destroy(_containerObject.Value.Container);
        }

        private struct ValuePair
        {
            public GameObject Container;

            public T Value;
        }
    }
}
