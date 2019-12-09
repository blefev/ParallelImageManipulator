using System;
using System.Threading.Tasks;

namespace WebServer
{
    class Program
    {
        static void Main(string[] args)
        {
            SetParallelismToCoreCount();
            string url = "http://localhost:9410/";
            WebServer ws = new WebServer(url, Console.Out);
            Console.WriteLine("Starting server...");
            ws.Start();
        }

        static private void SetParallelismToCoreCount()
        {
            ParallelOptions options = new ParallelOptions();
            options.MaxDegreeOfParallelism = System.Environment.ProcessorCount;
        }
    }
}
