using System.Collections.Generic;

namespace ULSStasher
{
    public class LogLinesParser
    {
        private readonly LineProvider _lineprovider;

        public LogLinesParser(LineProvider lineprovider)
        {
            _lineprovider = lineprovider;
        }

        public IEnumerable<LogElement> GetElements()
        {
            var elementCreator = new LogElementCreator();
            foreach (var logLine in _lineprovider.GetLines())
            {
                if (!logLine.IsValidLine)
                    continue;

                if (elementCreator.HasAnything && (logLine.IsFirstOfMultipart || logLine.IsNotMultipart))
                {
                    _lineprovider.SetLineNumber(elementCreator.GetLastLineNumber());
                    yield return elementCreator.Create();
                    elementCreator = new LogElementCreator();
                }
                elementCreator.Push(logLine);
            }

            if (elementCreator.HasEnough)
            {
                _lineprovider.SetLineNumber(elementCreator.GetLastLineNumber());
                yield return elementCreator.Create();
            }
        }
    }
}