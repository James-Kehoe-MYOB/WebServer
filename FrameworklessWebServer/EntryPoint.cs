using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using FrameworklessWebServer.DataAccess;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FrameworklessWebServer {
    class EntryPoint {
        static void Main(string[] args) {
            const int port = 8080;
        
            var Listener = new HttpListener { Prefixes = { $"http://+:{port}/" } };
            
            var ws = new WebService(Listener);

            ws.StartWebServer();
            
            Console.ReadKey();
            ws.StopWebServer();
        }
        
        
    }
}