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

        public Task<T> Create(T obj, bool overwriteIfExists = false)
        {
            return _storage.Create(obj, overwriteIfExists);
        }

        public Task<T> Read(string id)
        {
            return _storage.Read<T>(id);
        }

        public Task<IEnumerable<T>> ReadBy(Func<T, bool> predicate)
        {
            return _storage.ReadBy(predicate);
        }

        public Task<T> Update(T obj)
        {
            return _storage.Update(obj);
        }

        public Task<bool> Delete(string id)
        {
            return _storage.Delete<T>(id);
        }
    }
}