using System.Net;

namespace FrameworklessWebServer {
    public static class ContextOperations {
        
        public static void Write(string msg, HttpListenerResponse response) {
            var buffer = System.Text.Encoding.UTF8.GetBytes(msg);
            response.ContentLength64 = buffer.Length;
            response.OutputStream.Write(buffer, 0, buffer.Length);
        }
        
    }
}