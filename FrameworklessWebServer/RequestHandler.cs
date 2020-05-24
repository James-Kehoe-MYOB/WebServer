using System;
using System.Net;
using System.Threading.Tasks;
using FrameworklessWebServer.DataAccess;

namespace FrameworklessWebServer {
    public static class RequestHandler {

        public static async void HandleRequest(HttpListenerContext context) {
            var request = context.Request;
            var response = context.Response;
            var method = request.HttpMethod;
            var URLsegments = request.Url.Segments;
            HTTPMethods methodList = ParsePath(URLsegments);
            var handled = false;
            
            Console.WriteLine($"{method} {request.Url}");

            switch (method) {
                case "GET":
                    await Task.FromResult(0);
                    System.Threading.Thread.Sleep(10000);
                    methodList.Get(context);
                    handled = true;
                    break;
                case "POST":
                    methodList.Post(context);
                    handled = true;
                    break;
                case "DELETE":
                    methodList.Delete(context);
                    handled = true;
                    break;
                case "PUT":
                    methodList.Put(context);
                    handled = true;
                    break;
                case "PATCH":
                    methodList.Patch(context);
                    handled = true;
                    break;
            }

            if (!handled) {
                response.StatusCode = 404;
            }
        }

        private static HTTPMethods ParsePath(string[] URLsegments) {
            if (URLsegments.Length > 1) {
                switch (URLsegments[1].Replace('/', ' ').Trim()) {
                    case "names":
                        switch (URLsegments.Length) {
                            case 2:
                                return new nameHTTPMethods();
                            case 3:
                                if (int.TryParse(URLsegments[2].Replace('/', ' ').Trim(), out var id)) {
                                    return new personHTTPMethods(id);
                                }
                                else {
                                    return new defaultHTTPMethods();
                                }
                            default:
                                return new defaultHTTPMethods();
                        }
                    default:
                        return new defaultHTTPMethods();
                }
            }
            else {
                return new defaultHTTPMethods();
            }
        }
    }
}