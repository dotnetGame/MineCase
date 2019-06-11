// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Buffers;
using System.Runtime;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

#pragma warning disable

namespace System.IO.Pipelines
{   
    internal static class ReadWriteExtensions
    {
        /// <summary>
        /// Reverses a primitive value - performs an endianness swap
        /// </summary> 
        private static unsafe T Reverse<T>(T value) where T : struct
        {
            // note: relying on JIT goodness here!
            if (typeof(T) == typeof(byte) || typeof(T) == typeof(sbyte))
            {
                return value;
            }
            else if (typeof(T) == typeof(ushort) || typeof(T) == typeof(short))
            {
                ushort val = 0;
                Unsafe.Write(&val, value);
                val = (ushort)((val >> 8) | (val << 8));
                return Unsafe.Read<T>(&val);
            }
            else if (typeof(T) == typeof(uint) || typeof(T) == typeof(int)
                || typeof(T) == typeof(float))
            {
                uint val = 0;
                Unsafe.Write(&val, value);
                val = (val << 24)
                    | ((val & 0xFF00) << 8)
                    | ((val & 0xFF0000) >> 8)
                    | (val >> 24);
                return Unsafe.Read<T>(&val);
            }
            else if (typeof(T) == typeof(ulong) || typeof(T) == typeof(long)
                || typeof(T) == typeof(double))
            {
                ulong val = 0;
                Unsafe.Write(&val, value);
                val = (val << 56)
                    | ((val & 0xFF00) << 40)
                    | ((val & 0xFF0000) << 24)
                    | ((val & 0xFF000000) << 8)
                    | ((val & 0xFF00000000) >> 8)
                    | ((val & 0xFF0000000000) >> 24)
                    | ((val & 0xFF000000000000) >> 40)
                    | (val >> 56);
                return Unsafe.Read<T>(&val);
            }
            else
            {
                // default implementation
                int len = Unsafe.SizeOf<T>();
                var val = stackalloc byte[len];
                Unsafe.Write(val, value);
                int to = len >> 1, dest = len - 1;
                for (int i = 0; i < to; i++)
                {
                    var tmp = val[i];
                    val[i] = val[dest];
                    val[dest--] = tmp;
                }
                return Unsafe.Read<T>(val);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ReadBigEndian<T>(this ReadOnlySpan<byte> buffer) where T : struct
            => BitConverter.IsLittleEndian ? Reverse(ReadMachineEndian<T>(buffer)) : ReadMachineEndian<T>(buffer);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ReadLittleEndian<T>(this ReadOnlySpan<byte> buffer) where T : struct
            => BitConverter.IsLittleEndian ? ReadMachineEndian<T>(buffer) : Reverse(ReadMachineEndian<T>(buffer));


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteBigEndian<T>(this Span<byte> buffer, T value) where T : struct
        {
            if (BitConverter.IsLittleEndian)
                value = Reverse(value);
            WriteMachineEndian(buffer, ref value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteLittleEndian<T>(this Span<byte> buffer, T value) where T : struct
        {
            if (!BitConverter.IsLittleEndian)
                value = Reverse(value);
            WriteMachineEndian(buffer, ref value);
        }

        // BinaryPrimitives
        public static void WriteMachineEndian<T>(this Span<byte> buffer, ref T value)
        where T : struct
        {
            Unsafe.WriteUnaligned<T>(ref MemoryMarshal.GetReference(buffer), value);
        }


        public static bool TryWriteMachineEndian<T>(this Span<byte> buffer, ref T value)
            where T : struct
        {
            if (Unsafe.SizeOf<T>() > (uint)buffer.Length)
            {
                return false;
            }
            Unsafe.WriteUnaligned<T>(ref MemoryMarshal.GetReference(buffer), value);
            return true;
        }


        public static T ReadMachineEndian<T>(this ReadOnlySpan<byte> buffer)
            where T : struct
        {
            return Unsafe.ReadUnaligned<T>(ref MemoryMarshal.GetReference(buffer));
        }


        public static bool TryReadMachineEndian<T>(this ReadOnlySpan<byte> buffer, out T value)
            where T : struct
        {
            if (Unsafe.SizeOf<T>() > (uint)buffer.Length)
            {
                value = default;
                return false;
            }
            value = Unsafe.ReadUnaligned<T>(ref MemoryMarshal.GetReference(buffer));
            return true;
        }
    }
}
