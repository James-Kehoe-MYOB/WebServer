using System;
using System.Net;
using FrameworklessWebServer.HTTPMethods;

namespace FrameworklessWebServer.Business_Logic {
    public static class RequestHandler {

        public static async void HandleRequest(HttpListenerContext context) {
            var request = context.Request;
            var response = context.Response;
            var method = request.HttpMethod;
            var urlSegments = request.Url.Segments;
            var methodList = ParsePath(urlSegments);
            var handled = false;

            Console.WriteLine($"{method} {request.Url}");

            switch (method) {
                case "GET":
                    await methodList.Get(context);
                    handled = true;
                    break;
                case "POST":
                    await methodList.Post(context);
                    handled = true;
                    break;
                case "DELETE":
                    await methodList.Delete(context);
                    handled = true;
                    break;
                case "PUT":
                    await methodList.Put(context);
                    handled = true;
                    break;
                case "PATCH":
                    await methodList.Patch(context);
                    handled = true;
                    break;
            }

            if (!handled) {
                response.StatusCode = 404;
            }
        }

        private static IHttpMethods ParsePath(string[] URLsegments) {
            if (URLsegments.Length > 1) {
                switch (URLsegments[1].Replace('/', ' ').Trim()) {
                    case "names":
                        switch (URLsegments.Length) {
                            case 2:
                                return new NameHttpMethods();
                            case 3:
                                if (int.TryParse(URLsegments[2].Replace('/', ' ').Trim(), out var id)) {
                                    return new StudentIdHttpMethods(id.ToString());
                                }
                                else {
                                    return new DefaultHttpMethods();
                                }
                            default:
                                return new DefaultHttpMethods();
                        }
                    default:
                        return new DefaultHttpMethods();
                }
            }
            else {
                return new DefaultHttpMethods();
            }
        }
    }
}