using System;
using ULSStasher.Files;

namespace ULSStasher.Tests.TestData
{
    class FakeFile : IFile
    {
        public FakeFile(string name)
        {
            FullName = name;
            Name = name;
            LastWriteTime = DateTime.Now;
        }

        public string FullName { get; private set; }
        public string Name { get; private set; }
        public DateTime LastWriteTime { get; private set; }


        protected bool Equals(FakeFile other)
        {
            return string.Equals(Name, other.Name);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((FakeFile) obj);
        }

        public override int GetHashCode()
        {
            return (Name != null ? Name.GetHashCode() : 0);
        }
    }
}
