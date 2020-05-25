namespace FrameworklessWebServer {
    public class Student {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }

        public Student(int id, string name, int age) {
            ID = id;
            Age = age;
            Name = name;
        }
    }
}