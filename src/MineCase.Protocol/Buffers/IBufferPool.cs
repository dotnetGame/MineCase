using System;
using System.Buffers;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace MineCase.Buffers
{
    public interface IBufferPool<T>
    {
        IBufferPoolScope<T> CreateScope();
    }

    public interface IBufferPoolScope<T> : IDisposable
    {
        ArraySegment<T> Rent(int length);
    }

    public class BufferPool<T> : IBufferPool<T>
    {
        private readonly ArrayPool<T> _arrayPool;
        private readonly ConcurrentBag<BufferPoolScope> _scopes = new ConcurrentBag<BufferPoolScope>();

        public BufferPool(ArrayPool<T> arrayPool)
        {
            _arrayPool = arrayPool;
        }

        public IBufferPoolScope<T> CreateScope()
        {
            if (!_scopes.TryTake(out var scope))
                scope = new BufferPoolScope(this, _arrayPool);
            return scope;
        }

        private void Return(BufferPoolScope scope)
        {
            _scopes.Add(scope);
        }

        private class BufferPoolScope : IBufferPoolScope<T>
        {
            private readonly BufferPool<T> _bufferPool;
            private readonly ArrayPool<T> _arrayPool;
            private readonly ConcurrentBag<T[]> _rents = new ConcurrentBag<T[]>();

            public BufferPoolScope(BufferPool<T> bufferPool, ArrayPool<T> arrayPool)
            {
                _bufferPool = bufferPool;
                _arrayPool = arrayPool;
            }

            public void Dispose()
            {
                while (_rents.TryTake(out var rent))
                    _arrayPool.Return(rent);
                _bufferPool.Return(this);
            }

            public ArraySegment<T> Rent(int length)
            {
                var rent = _arrayPool.Rent(length);
                _rents.Add(rent);
                return new ArraySegment<T>(rent, 0, length);
            }
        }
    }
}
