using System.Collections.Generic;

namespace FrameworklessWebServer.DataAccess {
    public interface IDataHandler {

        void Update(StudentDomain person);

        Student GetStudentByID(int id);

        IEnumerable<Student> GetStudents();

        void DeleteStudentByID(int id);
    }
}