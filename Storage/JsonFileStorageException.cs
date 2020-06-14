using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;

namespace Storage
{
    public class JsonFileStorageException : Exception
    {
        public JsonFileStorageException(LogEvent logData)
            : base(JsonConvert.SerializeObject(logData))
        {
        }

        public JsonFileStorageException()
        {
        }

        public JsonFileStorageException(string message) : base(message)
        {
        }

        public JsonFileStorageException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected JsonFileStorageException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}