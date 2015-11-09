using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using ULSStasher.Tests.TestData;

namespace ULSStasher.Tests
{
    [TestFixture]
    public class LineProviderTests
    {
        private StubFileSystem _fileSystem;

        [Test]
        public void GetLines_WithNoLinesAndNoFiles_YieldsNoLines()
        {
            var lineProvider = CreateLineProvider(files: new string[0]);

            var actual = lineProvider.GetLines();

            CollectionAssert.IsEmpty(actual);
        }

        [Test]
        public void GetLines_WithHeaderAndOneLine_YieldsTwoLines()
        {
            var lines = new[] {TestLogLine.HeaderLine, new TestLogLine().ToString()};
            var lineProvider = CreateLineProvider(lines: lines);

            var actual = lineProvider.GetLines();

            Assert.AreEqual(2, actual.Count());
        }

        private LineProvider CreateLineProvider(IEnumerable<string> files = null, IEnumerable<string> lines = null)
        {
            _fileSystem = new StubFileSystem();
            if (files != null)
                _fileSystem.Files = files.Select(f => new FileInfo(f));
            if (lines != null)
                _fileSystem.Lines = lines;
            return new LineProvider(_fileSystem);
        }
    }
}
