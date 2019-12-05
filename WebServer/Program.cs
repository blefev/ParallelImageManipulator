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
            WebServer ws = new WebServer();
            Console.WriteLine("Starting server...");
            ws.Start();
        }


    }
}
