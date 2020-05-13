using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FrameworklessWebServer {
    class Program {
        private static string path = "Students.json";
        private static List<Person> _people = InitPeopleData();

        static void Main(string[] args) {
            var server = new HttpListener();
            server.Prefixes.Add("http://+:8080/");
            server.Start();
            while (true) {
                Console.WriteLine("Waiting for EVEN MORE clients...");
                var context = server.GetContext();  // Gets the request
                HandleRequest(context);
                context.Response.StatusCode = 200;
            }
            server.Stop();  // never reached...
        }

        static void HandleRequest(HttpListenerContext context) {
            
            var request = context.Request;
            var response = context.Response;
            var method = request.HttpMethod;
            var URLsegments = request.Url.Segments;

            Console.WriteLine($"{method} {request.Url}");

            if (URLsegments.Length == 2) {
                switch (URLsegments[1].Replace('/', ' ').Trim()) {
                    case "names":
                        switch (method) {
                            case "GET":
                                GetPeople(response);
                                break;
                            case "POST":
                                var addPerson = GetPersonFromRequestBody(request);
                                AddPerson(addPerson, response);
                                break;
                            case "DELETE":
                                var deletePerson = GetPersonFromRequestBody(request);
                                DeletePerson(deletePerson, response);
                                break;
                            default:
                                throw new Exception();
                        }
                        break;
                    default:
                        Write("Welcome to the server! To get the list of names, head to http://localhost:8080/names", response);
                        break;
                }
            }
            else {
                Write("Welcome to the server! To get the list of names, head to http://localhost:8080/names", response);
            }

            
        }

        private static void GetPeople(HttpListenerResponse response) {
            if (_people.Count > 0) {
                var peopleOrdered = _people.OrderBy(m => m.ID);
                var namelist = peopleOrdered.Aggregate("The people are: \n", (current, person) => current + $"No. {person.ID} - {person.Name}, Age {person.Age}\n");
                Write(namelist, response);
            }
            else {
                Write("No data found!", response);
            }
        }
        
        private static void DeletePerson(Person person, HttpListenerResponse response) {
            var search = _people.Find(m => m.ID == person.ID);
            if (_people.Exists(m => m.ID == search.ID)) {
                _people.Remove(search);
                Write($"Person No. {search.ID} - '{search.Name}' has been deleted", response);
                UpdateData();
            }
            else {
                Write($"'{person.ID}' did not match any item in person list", response);
            }
        }

        private static void AddPerson(Person person, HttpListenerResponse response) {
            var time = DateTime.Now;
            var timeHours = time.ToString("hh:mmtt");
            var timeDate = time.ToString("dd MMMM yyyy");
            if (!_people.Exists(m => m.ID == person.ID)) {
                _people.Add(person);
                UpdateData();
                var report = $"{person.Name} added to person list at {timeHours} on {timeDate}";
                Write(report, response);
            }
            else {
                Write("A person with that ID already exists, please use a different ID", response);
            }
        }

        private static Person GetPersonFromRequestBody(HttpListenerRequest request) {
            var input = request.InputStream;
            var reader = new StreamReader(input, request.ContentEncoding);
            var deleteName = reader.ReadToEnd();
            var dName = JsonConvert.DeserializeObject<Person>(deleteName);
            return dName;
        }

        private static void UpdateData() {
            var list =
                new JObject(
                    new JProperty("people",
                        new JArray(
                            from p in _people
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

        private static void Write(string msg, HttpListenerResponse response) {
            var buffer = System.Text.Encoding.UTF8.GetBytes(msg);
            response.ContentLength64 = buffer.Length;
            response.OutputStream.Write(buffer, 0, buffer.Length);
        }

        private static List<Person> InitPeopleData() {
            var responseBody = JObject.Load(new JsonTextReader(new StreamReader(path)));
            var people = JsonConvert.DeserializeObject<List<Person>>(responseBody["people"].ToString());
            return people;
        }
    }
}