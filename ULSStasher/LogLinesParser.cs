using System.Collections;
using System.Collections.Generic;

namespace ULSStasher
{
    internal class LogLinesParser
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
                if (logLine.IsFirstOfMultipart || logLine.IsNotMultipart)
                {
                    if (elementCreator.HasAnything)
                    {
                        _lineprovider.SetUsedLineNumber(elementCreator.GetLastLineNumber());
                        yield return elementCreator.Create();
                        elementCreator = new LogElementCreator();
                    }
                }
                elementCreator.Push(logLine);
            }
            if (elementCreator.HasEnough)
            {
                _lineprovider.SetUsedLineNumber(elementCreator.GetLastLineNumber());
                    yield return elementCreator.Create();
            }
                
        }
    }
}