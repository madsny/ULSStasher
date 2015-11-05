using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ULSStasher
{
    class Program
    {
        static void Main(string[] args)
        {
            var lineprovider = new LineProvider();

            var logLinesParser = new LogLinesParser(lineprovider);
            string result = "";
            do
            {
                foreach (var element in logLinesParser.GetElements())
                {
                    Console.WriteLine(JsonConvert.SerializeObject(element, Formatting.Indented));
                    lineprovider.Commit();
                }
                result = Console.ReadLine();
            } while (result != "q");
        }
    }
}
