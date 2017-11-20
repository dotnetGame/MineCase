using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MineCase.Engine.Threading;
using UnityEngine;

/// <summary>
/// 协程扩展
/// </summary>
public static class CoroutineExtensions
{
    /// <summary>
    /// 获取等待器
    /// </summary>
    /// <param name="source">操作</param>
    /// <returns>等待器</returns>
    public static CoroutineAwaiter<T> GetAwaiter<T>(this T source)
        where T : CustomYieldInstruction
    {
        return new CoroutineAwaiter<T>(source);
    }
}