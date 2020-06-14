using System;
using System.Collections.Generic;

namespace Storage
{
    public class LogEvent
    {
        public DateTime EventDateTime { get; set; } = DateTime.UtcNow;
        public string EventMessage { get; set; }
        public IEnumerable<KeyValuePair<string, object>> EventData { get; set; }
    }
}