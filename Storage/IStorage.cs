using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Storage
{
    /// <summary>
    /// All objects must have a unique (string) Id property
    /// </summary>
    public interface IStorage
    {
        Task<T> CreateAsync<T>(T obj, bool overwriteIfExists = false);

        Task<T> ReadAsync<T>(string id);

        Task<IEnumerable<T>> ReadByAsync<T>(Func<T, bool> predicate);

        Task<T> UpdateAsync<T>(T obj);

        Task<bool> DeleteAsync<T>(string id);

        Task<bool> ExistsAsync<T>(string id);
    }
}