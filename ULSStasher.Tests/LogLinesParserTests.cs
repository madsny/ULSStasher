using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using ULSStasher.Tests.TestData;

namespace ULSStasher.Tests
{
    [TestFixture]
    public class LogLinesParserTests
    {
        private StubFileSystem _fileSystem;

        [Test]
        public void GetElements_WithNoLines_ReturnsNoElements()
        {
            var parser = CreateParser();

            var actual = parser.GetElements();

            CollectionAssert.IsEmpty(actual);
        }

        [Test]
        public void GetElements_WithOneLineAndHeaderLine_ReturnsOneElement()
        {
            var parser = CreateParser(Lines(TestLogLine.HeaderLine, new TestLogLine()));

            var actual = parser.GetElements();

            Assert.AreEqual(1, actual.Count());
        }

        [Test]
        public void GetElements_WithOneMultilineElement_ReturnsOneElement()
        {
            var parser = CreateParser(Lines(
                new TestLogLine(isStartOfMultiline: true), 
                new TestLogLine(isEndOfMultiline: true)));

            var actual = parser.GetElements();
            Assert.AreEqual(1, actual.Count());
        }

        [Test]
        public void GetElements_WithUnfinishedMultiline_ReturnsNoElements()
        {
            var parser = CreateParser(Lines(new TestLogLine(isStartOfMultiline: true)));

            var actual = parser.GetElements();

            CollectionAssert.IsEmpty(actual);
        }

        [Test]
        public void GetElements_WithUnfinishedMultilineFollowedBySingleLine_ReturnsTwoElements()
        {
            var parser = CreateParser(Lines(
                new TestLogLine(isStartOfMultiline: true),
                new TestLogLine()));

            var actual = parser.GetElements();

            Assert.AreEqual(2, actual.Count());
        }

        [Test]
        public void GetElements_WithMultilineFinishedOnNextRequest_ReturnsResultOnNextRequest()
        {
            var parser = CreateParser(Lines(new TestLogLine(), new TestLogLine(isStartOfMultiline: true)));

            var first = parser.GetElements().Single();

            _fileSystem.AddLines(Lines(new TestLogLine(isEndOfMultiline:true)));

            var second = parser.GetElements().Single();

            Assert.AreEqual("Message0", first.Message);
            Assert.AreEqual("Message1Message2", second.Message);
        }

        [Test]
        public void GetElements_WithMultilineSplitInTwoFiles_ReturnsOneElement()
        {
            var parser = CreateParser();
            _fileSystem.AddLines(Lines(new TestLogLine(isStartOfMultiline:true), new TestLogLine(isMiddleOfMultiline:true)), "fileA");
            _fileSystem.AddLines(Lines(new TestLogLine(isEndOfMultiline:true)), "fileB");

            var actual = parser.GetElements();

            Assert.AreEqual(1, actual.Count());
        }

        [Test]
        public void GetElements_DoesNotReReadFinishedLogFile()
        {
            var parser = CreateParser();
            _fileSystem.AddLines(Lines(new TestLogLine()), "fileA");

            var first = parser.GetElements().Single();

            _fileSystem.AddLines(Lines(new TestLogLine()), "fileB");

            var second = parser.GetElements().Single();

            Assert.AreEqual("Message0", first.Message);
            Assert.AreEqual("Message1", second.Message);
        }

        [SetUp]
        public void Init()
        {
            TestLogLine.ResetCounter();
        }

        private IEnumerable<string> Lines(params object[] lines)
        {
            return lines.Select(line => line.ToString());
        }

        private LogLinesParser CreateParser(IEnumerable<string> lines = null)
        {
            _fileSystem = new StubFileSystem();
            _fileSystem.AddLines(lines);
            var lineProvider = new LineProvider(_fileSystem);
            return new LogLinesParser(lineProvider);
        }
    }
}
