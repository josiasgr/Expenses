using System;
using System.Text.Json;

namespace Storage
{
    public class StorageException : Exception
    {
        public StorageException(LogEvent logData)
            : base(JsonSerializer.Serialize(logData))
        {
        }
    }
}