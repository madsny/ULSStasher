using System;
using System.Collections.Generic;
using System.Linq;

namespace ULSStasher
{
    internal class LogElementCreator
    {
        private readonly List<LogLine> _lines;

        public LogElementCreator()
        {
            _lines = new List<LogLine>();
        }

        public bool HasEnough { get { return HasAnything && !_lines.Last().EndsWithContinuation; } }
        public bool HasAnything { get { return _lines.Count > 0; } }
        public int GetLastLineNumber()
        {
            return _lines.Last().Linenumber;
        }

        public void Push(LogLine logLine)
        {
            _lines.Add(logLine);
        }

        public LogElement Create()
        {
            var firstLine = _lines.First();
            var processnamePid = ParseProcessNameAndPid(firstLine.GetProcess());
            return new LogElement
            {
                Date = firstLine.GetTime(),
                EventId = firstLine.GetEventId(),
                Area = firstLine.GetArea(),
                Category = firstLine.GetCategory(),
                CorrelationId = firstLine.GetCorrelationId(),
                Level = firstLine.GetLevel(),
                ProcessName = processnamePid.Item1,
                ProcessId = processnamePid.Item2,
                Tid = firstLine.GetTid(),
                Message = string.Join("", _lines.Select(l => l.GetMessage().Trim('.', ' ')))
            };
        }

        private Tuple<string,string> ParseProcessNameAndPid(string getProcess)
        {
            var processName = string.Empty;
            var processId = string.Empty;
            var parts = getProcess.Split(new[] {'(', ')'}, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length > 0)
                processName = parts[0];
            if (parts.Length > 1)
                processId = parts[1];
            return new Tuple<string, string>(processName, processId);
        }
    }
}