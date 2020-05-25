using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FrameworklessWebServer.DataAccess;
using Newtonsoft.Json;

namespace FrameworklessWebServer.HTTPMethods {
    public class NameHttpMethods : IHttpMethods {
        public async Task Get(HttpListenerContext context) {
            await Task.Run(() => {
                if (JsonHandler.Students.Count > 0) {
                    var studentsOrdered = JsonHandler.Students.OrderBy(m => m.ID);
                    var studentList = studentsOrdered.Aggregate("The students are: \n",
                        (current, student) => current + $"No. {student.ID} - {student.Name}, Age {student.Age}\n");
                    ContextOperations.Write(studentList, context.Response);
                }
                else {
                    ContextOperations.Write("No data found!", context.Response);
                }
            });
        }

        public async Task Post(HttpListenerContext context) {
            await Task.Run(() => {
                var student = GetStudentFromRequestBody(context.Request);
                            
                var time = DateTime.Now;
                var timeHours = time.ToString("hh:mmtt");
                var timeDate = time.ToString("dd MMMM yyyy");
                if (!JsonHandler.Students.Exists(m => m.ID == student.ID)) {
                    JsonHandler.Students.Add(student);
                    JsonHandler.UpdateData();
                    var report = $"{student.Name} added to student list at {timeHours} on {timeDate}";
                    ContextOperations.Write(report, context.Response);
                }
                else {
                    ContextOperations.Write("A student with that ID already exists, please use a different ID", context.Response);
                }
            });
            
        }

        public async Task Put(HttpListenerContext context) {
            var defaultMethods = new DefaultHttpMethods();
            await defaultMethods.Put(context);
        }

        public async Task Patch(HttpListenerContext context) {
            var defaultMethods = new DefaultHttpMethods();
            await defaultMethods.Patch(context);
        }

        public async Task Delete(HttpListenerContext context) {
            await Task.Run(() => {
                var student = GetStudentFromRequestBody(context.Request);
                            
                var search = JsonHandler.Students.Find(m => m.ID == student.ID);
                if (JsonHandler.Students.Exists(m => m.ID == search.ID)) {
                    JsonHandler.Students.Remove(search);
                    ContextOperations.Write($"Student No. {search.ID} - '{search.Name}' has been deleted", context.Response);
                    JsonHandler.UpdateData();
                }
                else {
                    ContextOperations.Write($"'{student.ID}' did not match any item in student list", context.Response);
                }
            });
        }
        
        private static Student GetStudentFromRequestBody(HttpListenerRequest request) {
            var input = request.InputStream;
            var reader = new StreamReader(input, request.ContentEncoding);
            var deleteName = reader.ReadToEnd();
            var dName = JsonConvert.DeserializeObject<Student>(deleteName);
            return dName;
        }
    }
}