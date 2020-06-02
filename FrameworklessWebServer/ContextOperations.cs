using System.IO;
using System.Net;
using Newtonsoft.Json;

namespace FrameworklessWebServer {
    public static class ContextOperations {
        
        public static void Write(string msg, HttpListenerResponse response) {
            var buffer = System.Text.Encoding.UTF8.GetBytes(msg);
            response.ContentLength64 = buffer.Length;
            response.OutputStream.Write(buffer, 0, buffer.Length);
            response.OutputStream.Flush();
            response.OutputStream.Close();
        }
        
        public static Student GetStudentFromRequestBody(HttpListenerContext context) {
            var request = context.Request;
            var response = context.Response;
            var input = request.InputStream;
            if (request.ContentType == "application/json") {
                var reader = new StreamReader(input, request.ContentEncoding);
                var studentData = reader.ReadToEnd();
                var student = JsonConvert.DeserializeObject<Student>(studentData);
                return student;
            }
            else {
                ContextOperations.Write("Incompatible Request Type", response);
                return null;
            }
        }
        
    }
}