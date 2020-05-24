using System;
using System.Net;
using System.Threading.Tasks;

namespace FrameworklessWebServer {
    public class WebService {
        private HttpListener _listener;

        public WebService(HttpListener Listener) {
            _listener = Listener;
        }
        
        private bool _keepGoing = true;

        public async void StartWebServer() {
            
            _listener.Start();
            while (_keepGoing) {
                Console.WriteLine("Waiting for clients...");
                try {
                    var context = await _listener.GetContextAsync();
                    RequestHandler.HandleRequest(context);
                } catch (Exception e) {
                    if (e is HttpListenerException) return;
                    Console.WriteLine(e.Message);
                }
            }
        }
        
        public void StopWebServer() {
            _keepGoing = false;
            _listener.Stop();
        }
    }
}