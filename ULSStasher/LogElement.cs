using System;

namespace ULSStasher
{
    class LogElement
    {
        public DateTime Date { get; set; }
        public string ProcessName { get; set; }
        public string Tid { get; set; }
        public string Area { get; set; }
        public string Category { get; set; }
        public string EventId { get; set; }
        public string Level { get; set; }
        public string Message { get; set; }
        public string CorrelationId { get; set; }
        public string ProcessId { get; set; }
    }
}
