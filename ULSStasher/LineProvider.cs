using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ULSStasher
{
    public class LineProvider
    {
        private readonly IFileSystem _fileSystem;

        public LineProvider(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
        }


        private int _lineNumber;

        public IEnumerable<LogLine> GetLines()
        {
            var files = _fileSystem.GetFiles(@"e:\temp\logs\");

            foreach (var filename in files)
            {
                int index = 0;
                foreach (var rawLine in _fileSystem.GetLines(filename.FullName))
                {
                    index++;
                    var line = CreateLogLine(rawLine, index);
                    if (line != null)
                        yield return line;

                }
            }
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
