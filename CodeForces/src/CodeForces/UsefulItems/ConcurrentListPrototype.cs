using System;
using System.Collections.Generic;
using System.Threading;

namespace UsefulItems
{
    public class ConcurrentList<T>
    {
        private readonly ReaderWriterLockSlim _listLock = new ReaderWriterLockSlim();
        private readonly List<T> _holder = new List<T>();

        public void Add(T elem)
        {
            _listLock.EnterWriteLock();
            try
            {
                _holder.Add(elem);
            }
            finally
            {
                _listLock.ExitWriteLock();
            }
        }
        public void Remove(T elem)
        {
            _listLock.EnterWriteLock();
            try
            {
                _holder.Remove(elem);
            }
            finally
            {
                _listLock.ExitWriteLock();
            }
        }
        public Int32 IndexOf(T elem)
        {
            _listLock.EnterReadLock();
            try
            {
                return _holder.IndexOf(elem);
            }
            finally
            {
                _listLock.ExitReadLock();
            }
        }
    }
}