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
        Task<T> Create<T>(T obj, bool overwriteIfExists = false);

        Task<T> Read<T>(string id);

        Task<IEnumerable<T>> ReadBy<T>(Func<T, bool> predicate);

        Task<T> Update<T>(T obj);

        Task<bool> Delete<T>(string id);

        Task<bool> Exists<T>(string id);
    }
}