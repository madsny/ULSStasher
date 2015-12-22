using System;
using System.Collections.Generic;

namespace ULSStasher.Files
{
    class ProgressKeeper
    {
        private readonly List<ProgressRecord> _files;
        private ProgressRecord _current;

        public ProgressKeeper()
        {
            //read progress from ES
            _files = new List<ProgressRecord>();
        }

        public ProgressRecord SetCurrentFile(string fullName)
        {
            if (_current != null && !string.Equals(fullName, _current.FileName, StringComparison.InvariantCultureIgnoreCase))
            {
                _current.Done = true;
            }

            _current = _files.Find(f => string.Equals(fullName, f.FileName, StringComparison.InvariantCultureIgnoreCase));
            if (_current == null)
            {
                _current = new ProgressRecord(fullName);
                _files.Add(_current);
            }

            return _current;
        }

        public void SetLineNumber(int lastLineNumber)
        {
            _current.LineNumber = lastLineNumber;
        }

        public void Commit()
        {
            //push to ElasticSearch
        }
    }
}
