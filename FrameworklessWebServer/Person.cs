namespace FrameworklessWebServer {
    public class Person {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }

        public Person(int id, string name, int age) {
            ID = id;
            Age = age;
            Name = name;
        }
    }
}