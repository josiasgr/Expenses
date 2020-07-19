using Storage;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services
{
    public abstract class Services<T>
    {
        private readonly IStorage _storage;

        protected Services(
            IStorage storage
        )
        {
            _storage = storage;
        }

        public virtual Task<T> Create(T obj, bool overwriteIfExists = false)
        {
            return _storage.Create(obj, overwriteIfExists);
        }

        public virtual Task<T> Read(string id)
        {
            return _storage.Read<T>(id);
        }

        public virtual Task<IEnumerable<T>> ReadBy(Func<T, bool> predicate)
        {
            return _storage.ReadBy(predicate);
        }

        public virtual Task<T> Update(T obj)
        {
            return _storage.Update(obj);
        }

        public virtual Task<bool> Delete(string id)
        {
            return _storage.Delete<T>(id);
        }
    }
}