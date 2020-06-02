using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FrameworklessWebServer.DataAccess;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FrameworklessWebServer {
    class EntryPoint {
        static async Task Main(string[] args) {
            const int port = 8080;
        
            var Listener = new HttpListener { Prefixes = { $"http://+:{port}/" } };
            
            var ws = new ServerOperations(Listener);
            
            //var db = new DynamoHandler();
            //await db.Update(new Student("3", "John", 17));
            //Console.WriteLine(Guid.NewGuid().ToString());
            //var student = await db.GetStudentByID(3);
            //Console.WriteLine(student != null ? $"{student.id} - {student.Name}, {student.Age}" : "null student");


            ws.StartWebServer();
            
            Console.ReadKey();
            ws.StopWebServer();
        }
        
        
    }
}