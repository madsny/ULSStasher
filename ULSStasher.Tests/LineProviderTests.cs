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
        public void GetLines_WithNoLines_YieldsNoLines()
        {
            var lineProvider = CreateLineProvider();

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

        private LineProvider CreateLineProvider(IEnumerable<string> lines = null)
        {
            _fileSystem = new StubFileSystem();
            _fileSystem.AddLines(lines);
            return new LineProvider(_fileSystem);
        }
    }
}
