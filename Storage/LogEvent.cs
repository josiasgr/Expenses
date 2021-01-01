using System;
using System.Collections.Generic;

namespace Storage
{
    public class LogEvent
    {
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public string Message { get; set; }
        public IEnumerable<KeyValuePair<string, object>> Data { get; set; }
    }
}