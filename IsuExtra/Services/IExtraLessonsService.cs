using System.Collections.Generic;
using IsuExtra.Models;

namespace IsuExtra.Services
{
    public interface IExtraLessonsService
    {
        IEnumerable<IsuExtraStream> GetStreams(ExtraLesson extraLesson);
        IEnumerable<Student> GetStudentsWithoutExtraLesson();
        IEnumerable<Student> GetStudentsWithExtraLesson();
        Student AddStudentToIsuExtraStream(Student student, IsuExtraStream isuExtraStream);
        IEnumerable<ExtraLesson> GetExtraLessons();
        void StudentDeleteExtraLesson(Student student, ExtraLesson extraLesson);
        IEnumerable<Student> GetStudents(ExtraLesson extraLesson);
    }
}