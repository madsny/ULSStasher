using System;
using System.IO;

namespace ULSStasher.Files
{
    public class FileInfoWrapper : IFile
    {
        private readonly FileInfo _fileInfo;

        public FileInfoWrapper(FileInfo fileInfo)
        {
            _fileInfo = fileInfo;
        }

        public string FullName { get { return _fileInfo.FullName; } }
        public string Name { get { return _fileInfo.Name; } }
        public DateTime LastWriteTime { get {return _fileInfo.LastWriteTime; } }
    }
}
