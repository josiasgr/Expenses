using System;
using System.Collections.Concurrent;
using System.Linq;

namespace Utils
{
    public static class DynamicType
    {
        private static readonly ConcurrentDictionary<string, Type> concurrentDictionary
            = new ConcurrentDictionary<string, Type>();

        public static Type Load(this Type knownType, string typeName)
        {
            if (concurrentDictionary.TryGetValue(typeName, out Type result))
            {
                return result;
            }

            result = knownType
                        .Assembly
                        .GetTypes()
                        .FirstOrDefault(w =>
                            w.FullName.Equals(typeName, StringComparison.InvariantCultureIgnoreCase)
                        );

            if (result != null)
            {
                concurrentDictionary.TryAdd(typeName, result);
            }

            return result;
        }
    }
}