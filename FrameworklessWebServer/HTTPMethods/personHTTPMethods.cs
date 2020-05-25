using System.Net;
using System.Threading.Tasks;
using FrameworklessWebServer.DataAccess;

namespace FrameworklessWebServer.HTTPMethods {
    public class PersonHttpMethods : IHttpMethods {
        public int id { get; set; }

        public PersonHttpMethods(int id) {
            this.id = id;
        }
        
        public async Task Get(HttpListenerContext context) {
            await Task.Run(() => {
                if (JsonHandler.People.Exists(m => m.ID == id)) {
                    var person = JsonHandler.People.Find(m => m.ID == id);
                    ContextOperations.Write("Here is the person you are looking for:\n" + $"No. {person.ID} - {person.Name}, Age {person.Age}\n", context.Response);
                }
                else {
                    ContextOperations.Write($"There is no person with the ID number {id}", context.Response);
                }
            });
        }

        public async Task Post(HttpListenerContext context) {
            var defaultMethods = new DefaultHttpMethods();
            await defaultMethods.Post(context);
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
            var defaultMethods = new DefaultHttpMethods();
            await defaultMethods.Delete(context);
        }
    }
}