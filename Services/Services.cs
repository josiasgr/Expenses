using Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services
{
    /// <summary>
    /// Services are meant to read/write over multiple storage.
    /// Read operation ocours only on the first configured storage, all other operations on all storages.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class Services<T>
    {
        private readonly IEnumerable<IStorage> _storage;

        protected Services(
            IStorage[] storage
        )
        {
            _storage = storage;
        }

        public virtual Task<T[]> Create(T obj, bool overwriteIfExists = false)
        {
            return Task.WhenAll(
                    _storage.Select(s => s.Create(obj, overwriteIfExists))
                );
        }

        public virtual Task<T> Read(string id)
        {
            return _storage.First().Read<T>(id);
        }

        public virtual Task<IEnumerable<T>> ReadBy(Func<T, bool> predicate)
        {
            return _storage.First().ReadBy(predicate);
        }

        public virtual Task<T[]> Update(T obj)
        {
            return Task.WhenAll(
                _storage.Select(s => s.Update(obj))
            );
        }

        public virtual Task<bool[]> Delete(string id)
        {
            return Task.WhenAll(
                _storage.Select(s => s.Delete<T>(id))
            );
        }
    }
}