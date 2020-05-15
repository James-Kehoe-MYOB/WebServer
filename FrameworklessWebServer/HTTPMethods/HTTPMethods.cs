using System.Net;

namespace FrameworklessWebServer {
    public interface HTTPMethods {
        void Get(HttpListenerContext context);

        void Post(HttpListenerContext context);

        void Put(HttpListenerContext context);

        void Patch(HttpListenerContext context);

        void Delete(HttpListenerContext context);
    }
}