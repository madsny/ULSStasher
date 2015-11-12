using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ULSStasher.Files;

namespace ULSStasher
{
    class Program
    {
        static void Main(string[] args)
        {
            var lineprovider = new LineProvider(new FileSystem());

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
            //string result = @"^(.+?)\-(\d{8})\-(\d{4})\.log$";
            //do
            //{
            //    if (!string.IsNullOrWhiteSpace(result))
            //    {
            //        try
            //        {
            //            var regex = new Regex(result);
            //            var noe = regex.Match("T-DOR-HN-APP-20-20150814-1057.log");
            //            Console.WriteLine("sucess: {0}", noe.Success);
            //            foreach (Capture @group in noe.Captures)
            //            {
            //                Console.WriteLine("{0}: {1}", @group.Value, @group.Index);
            //            }
            //        }
            //        catch (Exception e)
            //        {
            //            Console.WriteLine("error: " + e.Message);
            //        }
            //    }

            //    result = Console.ReadLine();
            //} while (result != "q");
        }
    }
}
