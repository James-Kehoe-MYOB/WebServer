using System;
using System.Net;
using FrameworklessWebServer.DataAccess;

namespace FrameworklessWebServer {
    public static class RequestHandler {

        public static void HandleRequest(HttpListenerContext context) {
            var request = context.Request;
            var response = context.Response;
            var method = request.HttpMethod;
            var URLsegments = request.Url.Segments;
            HTTPMethods methodList = ParsePath(URLsegments);

            Console.WriteLine($"{method} {request.Url}");
            
            switch (method) {
                case "GET":
                    methodList.Get(context);
                    break;
                case "POST":
                    methodList.Post(context);
                    break;
                case "DELETE":
                    methodList.Delete(context);
                    break;
                case "PUT":
                    methodList.Put(context);
                    break;
                case "PATCH":
                    methodList.Patch(context);
                    break;
                default:
                    throw new Exception();
            }
        }

        private static HTTPMethods ParsePath(string[] URLsegments) {
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
    }
}