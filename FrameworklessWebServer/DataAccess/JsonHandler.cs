using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FrameworklessWebServer.DataAccess {
    public class JsonHandler {
        private static string path = "Data/Students.json";
        public static List<Student> Students = InitStudentData();
        
        
        public static List<Student> InitStudentData() {
            var responseBody = JObject.Load(new JsonTextReader(new StreamReader(path)));
            var students = JsonConvert.DeserializeObject<List<Student>>(responseBody["students"].ToString());
            return students;
        }
        
        public static void UpdateData() {
            var list =
                new JObject(
                    new JProperty("students",
                        new JArray(
                            from p in Students
                            orderby p.id
                            select new JObject(
                                new JProperty("id", p.id),
                                new JProperty("name", p.Name),
                                new JProperty("age", p.Age)
                            )
                        )
                    )
                );

            var listString = list.ToString();
            File.WriteAllText(path, listString);
        }

        public void Update(Student student) {
            throw new System.NotImplementedException();
        }

        public Student GetStudentByID(int id) {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Student> GetStudents() {
            throw new System.NotImplementedException();
        }

        public void DeleteStudentByID(int id) {
            throw new System.NotImplementedException();
        }
    }
}