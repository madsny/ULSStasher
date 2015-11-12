namespace ULSStasher.Tests.TestData
{
    internal class TestLogLine
    {
        private static int _globalcounter = 0;

        public string Time;
        public string Process;
        public string Tid;
        public string Area;
        public string Category;
        public string EventId;
        public string Level;
        public string Message;
        public string Correlation;

        public TestLogLine(bool isStartOfMultiline = false, bool isMiddleOfMultiline = false, bool isEndOfMultiline = false)
        {
            var counter = _globalcounter++;
            Time = "08/14/2015 11:27:06.16" + ((isMiddleOfMultiline || isEndOfMultiline) ? "*" : "");
            Process = "Process" + counter;
            Tid = "Tid" + counter;
            Area = "Areal" + counter;
            Category = "Category" + counter;
            EventId = "EventId" + counter;
            Level = "Level" + counter;
            Message = "Message" + counter;
            Correlation = "CorrelationId" + counter;

            if (isStartOfMultiline || isMiddleOfMultiline)
                Message = Message + "...";
            if (isMiddleOfMultiline || isEndOfMultiline)
                Message = "..." + Message;
        }

        public override string ToString()
        {
            return string.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}", 
                Time, Process, Tid, Area, Category, EventId, Level, Message, Correlation);
        }

        public static void ResetCounter()
        {
            _globalcounter = 0;
        }

        public const string HeaderLine =
            @"Timestamp              	Process                                 	TID   	Area                          	Category                      	EventID	Level     	Message 	Correlation";
    }
}
