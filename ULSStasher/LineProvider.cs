using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ULSStasher
{
    class LineProvider
    {
        private int _lineNumber;

        public IEnumerable<LogLine> GetLines()
        {
            int index = 0;
            var filename = @"E:\temp\logs\test.log";
            using (var inStream = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (StreamReader reader = new StreamReader(inStream))
                {
                    while (reader.Peek() >= 0)
                    {
                        var rawLine = reader.ReadLine();
                        ++index;
                        if (_lineNumber == 0 || index > _lineNumber)
                        {
                            var line = CreateLogLine(rawLine, index);
                            if (line != null)
                                yield return line;
                        }
                    }
                }
            }
            Console.WriteLine("Done reading {0} up to line {1}", filename, _lineNumber);
        }

        private LogLine CreateLogLine(string line, int linenumber)
        {
            if (line.StartsWith("Timestamp") || string.IsNullOrWhiteSpace(line))
                return null;

            return new LogLine(line, linenumber);
        }

        public void SetUsedLineNumber(int lastLineNumber)
        {
            _lineNumber = lastLineNumber;
        }

        public void Commit()
        {
            Console.WriteLine(_lineNumber);
        }
    }
}
