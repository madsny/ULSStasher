using System.Collections.Generic;
using System.IO;

namespace ULSStasher
{
    class FileSystem : IFileSystem
    {
        public IEnumerable<string> GetLines(string filePath)
        {
            using (var inStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (StreamReader reader = new StreamReader(inStream))
                {
                    while (reader.Peek() >= 0)
                    {
                        yield return reader.ReadLine();
                    }
                }
            }
        }

        public IEnumerable<FileInfo> GetFiles(string folderPath)
        {
            yield return new FileInfo(@"e:\temp\logs\test.log");
        }
    }
}
