using System.Collections.Generic;
using System.IO;

namespace ULSStasher
{
    public interface IFileSystem
    {
        IEnumerable<string> GetLines(string filePath);

        IEnumerable<FileInfo> GetFiles(string folderPath);
    }
}
