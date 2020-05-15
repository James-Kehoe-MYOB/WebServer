using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FrameworklessWebServer.DataAccess {
    public static class JsonHandler {
        private static string path = "Data/Students.json";
        public static List<Person> People = InitPeopleData();
        
        
        public static List<Person> InitPeopleData() {
            var responseBody = JObject.Load(new JsonTextReader(new StreamReader(path)));
            var people = JsonConvert.DeserializeObject<List<Person>>(responseBody["people"].ToString());
            return people;
        }
        
        public static void UpdateData() {
            var list =
                new JObject(
                    new JProperty("people",
                        new JArray(
                            from p in People
                            orderby p.ID
                            select new JObject(
                                new JProperty("id", p.ID),
                                new JProperty("name", p.Name),
                                new JProperty("age", p.Age)
                            )
                        )
                    )
                );

            var listString = list.ToString();
            File.WriteAllText(path, listString);
        }
    }
}