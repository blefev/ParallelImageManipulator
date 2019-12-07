using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;

namespace WebServer
{
    class Program
    {
        static void Main(string[] args)
        {
            SetParallelismToCoreCount();
            WebServer ws = new WebServer();
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
