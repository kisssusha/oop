using System;
using System.Linq;
using IsuExtra.Models;
using IsuExtra.Services;
using IsuEXtra.Services;
using NUnit.Framework;
using Student = IsuExtra.Models.Student;

namespace IsuExtra.Tests
{
    public class ExtraLessonTest
    {
        private ExtraLessonsService _extraLessonsService;
        private IsuExtraService _isuService;

        [SetUp]
        public void Setup()
        {
            _isuService = new IsuExtraService();
            _isuService.AddFaculty("M3");
            _isuService.AddFaculty("W");
            
            Group group1 = _isuService.AddGroup("M3207");
            
            _isuService.AddStudent(group1, "kisssusha");
            _isuService.AddStudent(group1, "Denis");

            Group group2 = _isuService.AddGroup("W3207");
            _isuService.AddStudent(group2, "Grisha");
            
            var time1 = new TimeTable();
            var time2 = new TimeTable();
            
            var les1 = new Lesson("OOP", group1, new Teacher("TT"), 300, "497l");
            var les2 = new Lesson("high math", group1, new Teacher("tt"), 500, "497");
            var les3 = new Lesson("OC", group2, new Teacher("TT"), 300, "497l");
            var les4 = new Lesson("IZO", group2, new Teacher("tt"), 500, "497l");
            
            time1.AddLesson(les1);
            time1.AddLesson(les2);
            time2.AddLesson(les3);
            time2.AddLesson(les4);

            _extraLessonsService = new ExtraLessonsService(_isuService);
        }

        [Test]
        public void AddEXtraLessonTest()
        {
            Faculty faculty = _isuService.Faculties.Keys.First(w => w.NameOfFaculty == "M3");
            faculty.AddExtraLesson(new ExtraLesson("Analize"));
            Assert.IsNotNull(_extraLessonsService.GetExtraLessons().FirstOrDefault(o => o.NameExtraLesson == "Analize"));
        }

        [Test]
        public void AddStudentToIsuExtraStreamTest()
        {
            Faculty faculty = _isuService.Faculties.Keys.First(w => w.NameOfFaculty == "M3");
            ExtraLesson extraLesson = faculty.AddExtraLesson(new ExtraLesson("Analize"));
            IsuExtraStream isuExtraStream = extraLesson.AddExtraStream(new IsuExtraStream("ANL", new Teacher("TT"), 700, "57879l"));
            Student student = _isuService.FindStudent("kisssusha");
           
            _extraLessonsService.AddStudentToIsuExtraStream(student, isuExtraStream);
            
            Assert.IsTrue(_extraLessonsService.GetStudents(extraLesson).Contains(student));
        }

        [Test]
        public void DeleteExtraLessonStudentTest()
        {
            Faculty faculty = _isuService.Faculties.Keys.First(w => w.NameOfFaculty == "M3");
            ExtraLesson extraLesson = faculty.AddExtraLesson(new ExtraLesson("Analize"));
            IsuExtraStream isuExtraStream = extraLesson.AddExtraStream(new IsuExtraStream("ANL", new Teacher("jnbj"), 500, "57879b"));
            Student student = _isuService.FindStudent("Grisha");
            _extraLessonsService.AddStudentToIsuExtraStream(student, isuExtraStream);
            _extraLessonsService.StudentDeleteExtraLesson(student, extraLesson);
            if (_extraLessonsService.GetStudents(extraLesson).Contains(student))
                Assert.Fail();
        }

        [Test]
        public void GetStudentsWithoutExtraLesson()
        {
            Faculty faculty = _isuService.Faculties.Keys.First(w => w.NameOfFaculty == "M3");
            ExtraLesson extraLesson = faculty.AddExtraLesson(new ExtraLesson("Analize"));
            IsuExtraStream isuExtraStream = extraLesson.AddExtraStream(new IsuExtraStream("ANL", new Teacher("jnbj"), 500, "57879g"));
            Student student1 = _isuService.FindStudent("kisssusha");
            Student student2 = _isuService.FindStudent("Grisha");
            
            _extraLessonsService.AddStudentToIsuExtraStream(student2, isuExtraStream);
            
            Assert.IsFalse(_extraLessonsService.GetStudentsWithoutExtraLesson().Contains(student2));
        }

        [Test]

        public void GetStudentsWithExtraLesson()
        {
            Faculty faculty = _isuService.Faculties.Keys.First(w => w.NameOfFaculty == "M3");
            ExtraLesson extraLesson = faculty.AddExtraLesson(new ExtraLesson("Analize"));
            IsuExtraStream isuExtraStream = extraLesson.AddExtraStream(new IsuExtraStream("ANL", new Teacher("jnbj"), 500, "57879g")); 
            Student student1 = _isuService.FindStudent("kisssusha");
            Student student2 = _isuService.FindStudent("Grisha");
            _extraLessonsService.AddStudentToIsuExtraStream(student1, isuExtraStream);
            _extraLessonsService.AddStudentToIsuExtraStream(student2, isuExtraStream);
            
            Assert.IsFalse(_extraLessonsService.GetStudentsWithoutExtraLesson().Contains(student2));
            Assert.IsFalse(_extraLessonsService.GetStudentsWithoutExtraLesson().Contains(student1));
        }

        [Test]

        public void GetIsuExtraStreamTest()
        {
            Faculty faculty = _isuService.Faculties.Keys.First(w => w.NameOfFaculty == "M3");
            ExtraLesson extraLesson = faculty.AddExtraLesson(new ExtraLesson("Analize"));
            IsuExtraStream isuExtraStream = extraLesson.AddExtraStream(new IsuExtraStream("ANL", new Teacher("jnbj"), 500, "57879g"));

            Assert.IsTrue(_extraLessonsService.GetStreams(extraLesson).Contains(isuExtraStream));
        }
    }
}