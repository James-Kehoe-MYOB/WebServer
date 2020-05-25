using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace FrameworklessWebServer.HTTPMethods {
    public class DefaultHttpMethods : IHttpMethods {
        public async Task Get(HttpListenerContext context) {
            await Task.Run(() => ContextOperations.Write("Welcome to the server! To get the list of students, head to http://localhost:8080/names", context.Response));
        }

        public async Task Post(HttpListenerContext context) {
            await Task.Run(() => ContextOperations.Write("Welcome to the server! To get the list of students, head to http://localhost:8080/names", context.Response));
        }

        public async Task Put(HttpListenerContext context) {
            await Task.Run(() => ContextOperations.Write("Welcome to the server! To get the list of students, head to http://localhost:8080/names", context.Response));
        }

        public async Task Patch(HttpListenerContext context) {
            await Task.Run(() => ContextOperations.Write("Welcome to the server! To get the list of students, head to http://localhost:8080/names", context.Response));
        }

        public async Task Delete(HttpListenerContext context) {
            await Task.Run(() => ContextOperations.Write("Welcome to the server! To get the list of students, head to http://localhost:8080/names", context.Response));
        }
    }
}