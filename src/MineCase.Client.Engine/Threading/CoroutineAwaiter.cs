using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace MineCase.Engine.Threading
{
    /// <summary>
    /// 协程等待器构造器
    /// </summary>
    public sealed class CoroutineAwaiterBuilder : MonoBehaviour
    {
        private static readonly KeepAliveLazy<CoroutineAwaiterBuilder> _instance = new KeepAliveLazy<CoroutineAwaiterBuilder>();

        /// <summary>
        /// 获取单例
        /// </summary>
        public static CoroutineAwaiterBuilder Current => _instance.Value;

        internal void Scheduler(IEnumerator enumerator, Action continuation)
        {
            Execute.OnMainThreadAsync(() => StartCoroutine(ExecuteCoroutine(enumerator, continuation)));
        }

        private IEnumerator ExecuteCoroutine(IEnumerator enumerator, Action continuation)
        {
            yield return enumerator;
            continuation();
            Debug.Log("hah");
        }
    }

    /// <summary>
    /// 协程等待器
    /// </summary>
    public struct CoroutineAwaiter<T> : INotifyCompletion
        where T : IEnumerator
    {
        private T _enumerator;

        /// <summary>
        /// 获取是否完成
        /// </summary>
        public bool IsCompleted => false;

        /// <summary>
        /// 获取结果
        /// </summary>
        /// <returns>结果</returns>
        public T GetResult() => _enumerator;

        internal CoroutineAwaiter(T enumerator)
        {
            _enumerator = enumerator;
        }

        /// <inheritdoc/>
        public void OnCompleted(Action continuation)
        {
            CoroutineAwaiterBuilder.Current.Scheduler(_enumerator, continuation);
        }
    }
}
