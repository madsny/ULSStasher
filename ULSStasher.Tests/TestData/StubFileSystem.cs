using System.Collections.Generic;
using System.IO;
using System.Linq;
using ULSStasher.Files;

namespace ULSStasher.Tests.TestData
{
    class StubFileSystem : IFileSystem
    {
        private readonly Dictionary<FakeFile, IEnumerable<string>> _dic;
        private const string DefaultFileName = "a";

        public StubFileSystem()
        {
            _dic = new Dictionary<FakeFile, IEnumerable<string>>();
            _dic[new FakeFile(DefaultFileName)] = Enumerable.Empty<string>();
        }

        public IEnumerable<string> GetLines(IFile file)
        {
            return _dic[(FakeFile)file];
        }

        public IEnumerable<IFile> GetFiles(string folderPath)
        {
            return _dic.Keys;
        }

        public void AddLines(IEnumerable<string> lines = null, string filename = DefaultFileName)
        {
            var file = new FakeFile(filename);
            if (!_dic.ContainsKey(file))
            {
                _dic[file] = Enumerable.Empty<string>();
            }
            _dic[file] = _dic[file].Concat(lines ?? Enumerable.Empty<string>());
        }
    }
}
