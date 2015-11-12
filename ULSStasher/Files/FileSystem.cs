using System.Collections.Generic;
using System.IO;

namespace ULSStasher.Files
{
    class FileSystem : IFileSystem
    {
        public IEnumerable<string> GetLines(IFile fileInfo)
        {
            using (var inStream = new FileStream(fileInfo.FullName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
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

        public IEnumerable<IFile> GetFiles(string folderPath)
        {
            yield return new FileInfoWrapper(new FileInfo(@"e:\temp\logs\test.log"));
        }
    }
}
