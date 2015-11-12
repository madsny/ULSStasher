using System;

namespace ULSStasher.Files
{
    public interface IFile
    {
        string FullName { get; }
        string Name { get; }

        DateTime LastWriteTime { get; }
    }
}
