namespace FrameworklessWebServer.DataModels {
    public class Student {
        public string id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }

        public Student(string id, string name, int age) {
            this.id = id;
            Age = age;
            Name = name;
        }
    }
}