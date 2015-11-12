using System.Collections.Generic;

namespace ULSStasher.Files
{
    public interface IFileSystem
    {
        IEnumerable<string> GetLines(IFile file);

        IEnumerable<IFile> GetFiles(string folderPath);
    }
}
