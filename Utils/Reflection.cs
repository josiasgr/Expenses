using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace Utils
{
    [DebuggerStepThrough]
    public static class Reflection
    {
        public static PropertyInfo GetProperty(this object obj, string propertyName)
        {
            return obj.GetType()
                        .GetProperties()
                        .SingleOrDefault(
                            s => s.Name.Equals(propertyName, StringComparison.InvariantCultureIgnoreCase)
                        );
        }

        public static T GetValue<T>(this object obj, string propertyName)
        {
            return (T)obj.GetProperty(propertyName).GetValue(obj);
        }

        public static bool TryGetValue<T>(this object obj, string propertyName, out T value)
        {
            var property = obj.GetProperty(propertyName);
            if (property == null)
            {
                value = default;
                return false;
            }

            if (property.PropertyType == typeof(T))
            {
                value = (T)property.GetValue(obj);
                return true;
            }

            throw new ArgumentException($"The TryGetValue<T> type '{typeof(T).FullName}' doesn't match with the property '{propertyName}' type '{property.PropertyType.FullName}'");
        }

        public static object SetValue<T>(this object obj, string propertyName, T value)
        {
            obj.GetProperty(propertyName).SetValue(obj, value);
            return obj;
        }
    }
}