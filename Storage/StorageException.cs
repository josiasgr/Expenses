using Newtonsoft.Json;
using System;

namespace Storage
{
    public class StorageException : Exception
    {
        public StorageException(LogEvent logData)
            : base(JsonConvert.SerializeObject(logData))
        {
        }
    }
}