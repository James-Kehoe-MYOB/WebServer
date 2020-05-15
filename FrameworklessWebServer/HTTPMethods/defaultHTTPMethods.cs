using System.Net;

namespace FrameworklessWebServer {
    public class defaultHTTPMethods : HTTPMethods {
        public void Get(HttpListenerContext context) {
            ContextOperations.Write("Welcome to the server! To get the list of names, head to http://localhost:8080/names", context.Response);
        }

        public void Post(HttpListenerContext context) {
            ContextOperations.Write("Welcome to the server! To get the list of names, head to http://localhost:8080/names", context.Response);
        }

        public void Put(HttpListenerContext context) {
            ContextOperations.Write("Welcome to the server! To get the list of names, head to http://localhost:8080/names", context.Response);
        }

        public void Patch(HttpListenerContext context) {
            ContextOperations.Write("Welcome to the server! To get the list of names, head to http://localhost:8080/names", context.Response);
        }

        public void Delete(HttpListenerContext context) {
            ContextOperations.Write("Welcome to the server! To get the list of names, head to http://localhost:8080/names", context.Response);
        }
    }
}