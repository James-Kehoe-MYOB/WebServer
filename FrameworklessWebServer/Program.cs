using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using FrameworklessWebServer.DataAccess;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FrameworklessWebServer {
    class Program {
        static void Main(string[] args) {
            var server = new HttpListener();
            server.Prefixes.Add("http://+:8080/");
            server.Start();
            while (true) {
                Console.WriteLine("Waiting for EVEN MORE clients...");
                var context = server.GetContext(); 
                RequestHandler.HandleRequest(context);
            }
            server.Stop();  // never reached...
        }
    }
}