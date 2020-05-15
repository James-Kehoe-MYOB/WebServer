using System.Net;
using FrameworklessWebServer.DataAccess;

namespace FrameworklessWebServer {
    public class personHTTPMethods : HTTPMethods {
        public int id { get; set; }

        public personHTTPMethods(int id) {
            this.id = id;
        }
        
        public void Get(HttpListenerContext context) {
            if (JsonHandler.People.Exists(m => m.ID == id)) {
                var person = JsonHandler.People.Find(m => m.ID == id);
                ContextOperations.Write("Here is the person you are looking for:\n" + $"No. {person.ID} - {person.Name}, Age {person.Age}\n", context.Response);
            }
            else {
                ContextOperations.Write($"There is no person with the ID number {id}", context.Response);
            }

        }

        public void Post(HttpListenerContext context) {
            throw new System.NotImplementedException();
        }

        public void Put(HttpListenerContext context) {
            throw new System.NotImplementedException();
        }

        public void Patch(HttpListenerContext context) {
            throw new System.NotImplementedException();
        }

        public void Delete(HttpListenerContext context) {
            throw new System.NotImplementedException();
        }
    }
}