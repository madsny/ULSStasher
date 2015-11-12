using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ULSStasher.Files;

namespace ULSStasher
{
    public class LineProvider
    {
        private readonly IFileSystem _fileSystem;
        private readonly ProgressKeeper _progressKeeper;

        public LineProvider(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
            _progressKeeper = new ProgressKeeper();
        }

        public IEnumerable<LogLine> GetLines()
        {
            var files = _fileSystem.GetFiles(@"e:\temp\logs\");

            foreach (var fileInfo in files)
            {
                var currentProgress = _progressKeeper.SetCurrentFile(fileInfo.FullName);
                if(currentProgress.Done)
                    continue;
                int index = 0;
                foreach (var rawLine in _fileSystem.GetLines(fileInfo))
                {
                    index++;
                    if(index > currentProgress.LineNumber)
                        yield return new LogLine(rawLine, index, fileInfo.Name);
                }
            }
        }


        public void SetLineNumber(int lastLineNumber)
        {
            _progressKeeper.SetLineNumber(lastLineNumber);
        }

        public void Commit()
        {
            _progressKeeper.Commit();
        }
    }
}
