using System;
using System.IO;
using System.Linq;
using System.Net;
using FrameworklessWebServer.DataAccess;
using Newtonsoft.Json;

namespace FrameworklessWebServer {
    public class nameHTTPMethods : HTTPMethods {
        public void Get(HttpListenerContext context) {
            if (JsonHandler.People.Count > 0) {
                var peopleOrdered = JsonHandler.People.OrderBy(m => m.ID);
                var namelist = peopleOrdered.Aggregate("The people are: \n", (current, person) => current + $"No. {person.ID} - {person.Name}, Age {person.Age}\n");
                ContextOperations.Write(namelist, context.Response);
            }
            else {
                ContextOperations.Write("No data found!", context.Response);
            }
        }

        public void Post(HttpListenerContext context) {
            var person = GetPersonFromRequestBody(context.Request);
            
            var time = DateTime.Now;
            var timeHours = time.ToString("hh:mmtt");
            var timeDate = time.ToString("dd MMMM yyyy");
            if (!JsonHandler.People.Exists(m => m.ID == person.ID)) {
                JsonHandler.People.Add(person);
                JsonHandler.UpdateData();
                var report = $"{person.Name} added to person list at {timeHours} on {timeDate}";
                ContextOperations.Write(report, context.Response);
            }
            else {
                ContextOperations.Write("A person with that ID already exists, please use a different ID", context.Response);
            }
        }

        public void Put(HttpListenerContext context) {
            throw new System.NotImplementedException();
        }

        public void Patch(HttpListenerContext context) {
            throw new System.NotImplementedException();
        }

        public void Delete(HttpListenerContext context) {
            var person = GetPersonFromRequestBody(context.Request);
            
            var search = JsonHandler.People.Find(m => m.ID == person.ID);
            if (JsonHandler.People.Exists(m => m.ID == search.ID)) {
                JsonHandler.People.Remove(search);
                ContextOperations.Write($"Person No. {search.ID} - '{search.Name}' has been deleted", context.Response);
                JsonHandler.UpdateData();
            }
            else {
                ContextOperations.Write($"'{person.ID}' did not match any item in person list", context.Response);
            }
        }
        
        private static Person GetPersonFromRequestBody(HttpListenerRequest request) {
            var input = request.InputStream;
            var reader = new StreamReader(input, request.ContentEncoding);
            var deleteName = reader.ReadToEnd();
            var dName = JsonConvert.DeserializeObject<Person>(deleteName);
            return dName;
        }
    }
}