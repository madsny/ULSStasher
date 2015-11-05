using System;
using System.Globalization;

namespace ULSStasher
{
    internal class LogLine
    {
        public int Linenumber { get; private set; }
        private readonly string[] _parts;

        public const int ProcessIdx = 1;
        public const int TidIdx = 2;
        public const int AreaIdx = 3;
        public const int CategoryIdx = 4;
        public const int EventIdIdx = 5;
        public const int LevelIdx = 6;
        public const int MessageIdx = 7;
        public const int CorrelationIdx = 8;

        public LogLine(string line, int linenumber)
        {
            Linenumber = linenumber;
            _parts = line.Split('\t');
        }

        public DateTime GetTime()
        {
            var datepart = GetPart(0).Trim('*');
            DateTime parsed;
            if (DateTime.TryParseExact(datepart, "MM/dd/yyyy HH:mm:ss.FFF", CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal, out parsed))
            {
                return parsed;
            };
            return default(DateTime);
        }


        private bool StartsWithContinuation
        {
            get { return GetPart(MessageIdx).StartsWith("..."); }
        }

        public bool EndsWithContinuation
        {
            get { return GetPart(MessageIdx).EndsWith("..."); }
        }

        public bool IsFirstOfMultipart { get { return !StartsWithContinuation && EndsWithContinuation; } }
        public bool IsLastOfMultipart { get { return StartsWithContinuation && !EndsWithContinuation; } }
        public bool IsNotMultipart { get { return !StartsWithContinuation && !EndsWithContinuation; } }

        public string GetEventId() { return GetPart(EventIdIdx); }

        public string GetPart(int index)
        {
            if (_parts.Length > index)
            {
                return _parts[index].Trim();
            }
            return string.Empty;
        }

        public string GetArea()
        {
            return GetPart(AreaIdx);
        }

        public string GetCategory()
        {
            return GetPart(CategoryIdx);
        }

        public string GetCorrelationId()
        {
            return GetPart(CorrelationIdx);
        }

        public string GetLevel()
        {
            return GetPart(LevelIdx);
        }

        public string GetProcess()
        {
            return GetPart(ProcessIdx);
        }

        public string GetTid()
        {
            return GetPart(TidIdx);
        }

        public string GetMessage()
        {
            return GetPart(MessageIdx);
        }
    }
}