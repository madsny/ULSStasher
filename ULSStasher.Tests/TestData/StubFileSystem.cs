using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ULSStasher.Tests.TestData
{
    class StubFileSystem : IFileSystem
    {
        public IEnumerable<string> Lines = new string[0];
        public IEnumerable<FileInfo> Files = new[] {new FileInfo("a")}; 

        public IEnumerable<string> GetLines(string filePath)
        {
            return Lines;
        }

        public IEnumerable<FileInfo> GetFiles(string folderPath)
        {
            return Files;
        }
    }
}
