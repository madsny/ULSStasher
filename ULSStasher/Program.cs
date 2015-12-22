using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Elasticsearch.Net;
using Nest;
using Newtonsoft.Json;
using ULSStasher.Files;

namespace ULSStasher
{
    class Program
    {
        static void Main(string[] args)
        {

            var node = new Uri("http://localhost:9200");

            var settings = new ConnectionSettings(
                node,
                defaultIndex: "ulslogs"
                );
            var client = new ElasticClient(settings);

            client.Map<LogElement>(r => r.MapFromAttributes());
            var lineprovider = new LineProvider(new FileSystem());

            var logLinesParser = new LogLinesParser(lineprovider);
            string result = "";
            do
            {
                foreach (var element in logLinesParser.GetElements())
                {
                    client.Index(element); //bulk insert
                    lineprovider.Commit();
                }
                result = Console.ReadLine();
            } while (result != "q"); //timer with lock

        }
    }
}
