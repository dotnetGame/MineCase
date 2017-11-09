// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Runtime;
using System.Runtime.CompilerServices;

#pragma warning disable

namespace System.Buffers.Binary
{
    internal static partial class BinaryPrimitives
    {
        /// <summary>
        /// Writes a structure of type T into a span of bytes.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteMachineEndian<T>(Span<byte> buffer, ref T value)
            where T : struct
        {
            Unsafe.WriteUnaligned<T>(ref buffer.DangerousGetPinnableReference(), value);
        }

        /// <summary>
        /// Writes a structure of type T into a span of bytes.
        /// <returns>If the span is too small to contain the type T, return false.</returns>
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryWriteMachineEndian<T>(Span<byte> buffer, ref T value)
            where T : struct
        {
            if (Unsafe.SizeOf<T>() > (uint)buffer.Length)
            {
                return false;
            }
            Unsafe.WriteUnaligned<T>(ref buffer.DangerousGetPinnableReference(), value);
            return true;
        }

        /// <summary>
        /// Reads a structure of type T out of a read-only span of bytes.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ReadMachineEndian<T>(ReadOnlySpan<byte> buffer)
            where T : struct
        {
            return Unsafe.ReadUnaligned<T>(ref buffer.DangerousGetPinnableReference());
        }

        /// <summary>
        /// Reads a structure of type T out of a span of bytes.
        /// <returns>If the span is too small to contain the type T, return false.</returns>
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryReadMachineEndian<T>(ReadOnlySpan<byte> buffer, out T value)
            where T : struct
        {
            if (Unsafe.SizeOf<T>() > (uint)buffer.Length)
            {
                value = default;
                return false;
            }
            value = Unsafe.ReadUnaligned<T>(ref buffer.DangerousGetPinnableReference());
            return true;
        }
    }
}

