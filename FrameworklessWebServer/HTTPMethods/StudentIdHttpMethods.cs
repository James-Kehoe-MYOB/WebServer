using System;
using System.Net;
using System.Threading.Tasks;
using FrameworklessWebServer.Business_Logic;
using FrameworklessWebServer.DataAccess;

namespace FrameworklessWebServer.HTTPMethods {
    public class StudentIdHttpMethods : IHttpMethods {
        private string id { get; set; }
        private DynamoHandler _database = new DynamoHandler();

        public StudentIdHttpMethods(string id) {
            this.id = id;
        }
        
        public async Task Get(HttpListenerContext context) {
            await Task.Run(() => {
                try {
                    var student = _database.GetStudentByID(id).Result;
                    if (student != null) {
                        ContextOperations.Write("Here is the student you are looking for:\n" + $"No. {student.id} - {student.Name}, Age {student.Age}\n", context.Response);
                    }
                    else {
                        ContextOperations.Write($"There is no student with the id number {id}", context.Response);
                    }
                }
                catch (Exception e) {
                    ContextOperations.Write(e.Message, context.Response);
                }
            });
        }

        public async Task Post(HttpListenerContext context) {
            await Task.Run(async () => {
                var student = ContextOperations.GetStudentFromRequestBody(context);
                if (student != null) {
                    if (_database.GetStudentByID(id).Result == null) {
                        await _database.Update(student); 
                        ContextOperations.Write($"{student.Name} added to student list", context.Response);
                    }
                    else {
                        ContextOperations.Write("A student with that ID already exists", context.Response);
                    }
                }
                else {
                    ContextOperations.Write("Was unable to process request", context.Response);
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

            if (_database.GetStudentByID(id).Result != null) {
                ContextOperations.Write($"Deleting student number {id} from student list...", context.Response);
                await _database.DeleteStudentByID(id);
            }
            else {
                ContextOperations.Write("Could not find student with that ID", context.Response);
            }
        }
    }
}