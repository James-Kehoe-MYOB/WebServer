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
            // var server = new HttpListener();
            // server.Prefixes.Add("http://+:8080/");
            // server.Start();
            // while (true) {
            //     Console.WriteLine("Waiting for EVEN MORE clients...");
            //     var context = server.GetContext(); 
            //     RequestHandler.HandleRequest(context);
            // }
            // server.Stop();  // never reached...
            
            var Port = 8080;
        
            var Listener = new HttpListener { Prefixes = { $"http://+:{Port}/" } };
            
            var ws = new WebService(Listener);

            ws.StartWebServer();
            
            Console.ReadKey();
            ws.StopWebServer();
        }
        
        
    }
}