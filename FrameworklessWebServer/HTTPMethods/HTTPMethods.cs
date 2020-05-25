using System.Net;
using System.Threading.Tasks;

namespace FrameworklessWebServer.HTTPMethods {
    public interface IHttpMethods {
        Task Get(HttpListenerContext context);
        
        Task Post(HttpListenerContext context);

        Task Put(HttpListenerContext context);

        Task Patch(HttpListenerContext context);

        Task Delete(HttpListenerContext context);
    }
}