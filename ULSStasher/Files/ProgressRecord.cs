using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ULSStasher.Files
{
    class ProgressRecord
    {
        public string FileName { get; private set; }
        public int LineNumber { get; set; }
        public bool Done { get; set; }

        public ProgressRecord(string fileName, int lineNumber = 0, bool done = false)
        {
            FileName = fileName;
            LineNumber = lineNumber;
            Done = done;
        }
    }
}
