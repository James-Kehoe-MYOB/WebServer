using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FrameworklessWebServer.DataAccess;
using Newtonsoft.Json;

namespace FrameworklessWebServer.HTTPMethods {
    public class NameHttpMethods : IHttpMethods {

        DynamoHandler _database = new DynamoHandler();
        
        public async Task Get(HttpListenerContext context) {
            await Task.Run(() => {
                var students = _database.GetStudents();
                var studentsOrdered = students.Result.OrderBy(m => m.id);
                var studentList = studentsOrdered.Aggregate("The students are: \n", (current, student) => current + $"No. {student.id} - {student.Name}, Age {student.Age}\n");
                ContextOperations.Write(studentList, context.Response);
            });
        }

        public async Task Post(HttpListenerContext context) {
            await Task.Run(async () => {
                var student = ContextOperations.GetStudentFromRequestBody(context);
                if (student != null) {
                    if (_database.GetStudentByID(student.id).Result == null) {
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
            await Task.Run(async () => {
                var student = ContextOperations.GetStudentFromRequestBody(context);
                
                if (_database.GetStudentByID(student.id).Result != null) {
                    ContextOperations.Write($"Deleting student number {student.id} from student list...", context.Response);
                    await _database.DeleteStudentByID(student.id);
                }
                else {
                    ContextOperations.Write("Could not find student with that ID", context.Response);
                }
            });
        }
        
        
    }
}