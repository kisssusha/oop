using System;
using System.Collections.Generic;
using System.Linq;
using Isu.Services;
using IsuExtra.Models;
using IsuEXtra.Services;

namespace IsuExtra.Services
{
    public class ExtraLessonsService : IExtraLessonsService
    {
        private readonly IsuExtraService _isuExtraService;
        private readonly Dictionary<IsuExtraStream, List<Student>> _assignments;
        public ExtraLessonsService(IsuExtraService isuExtraService)
        {
            _assignments = new Dictionary<IsuExtraStream, List<Student>>();
            _isuExtraService = isuExtraService ?? throw new Exception("Invalid Isu Extra Service");
        }

        public IEnumerable<IsuExtraStream> GetStreams(ExtraLesson extraLesson)
        {
            if (extraLesson == null) throw new Exception("Faluty hasn't the ognpCourse");
            ExtraLesson ex = _isuExtraService.Faculties.Keys
                .SelectMany(faculty => faculty.ExtraLessons)
                .FirstOrDefault(c => c.NameExtraLesson == extraLesson.NameExtraLesson);
            return ex.IsuExtraStreams.ToList();
        }

        public IEnumerable<Student> GetStudentsWithoutExtraLesson()
        {
            return _isuExtraService.Students.Except(_isuExtraService.Faculties.Keys.SelectMany(faculty => faculty.ExtraLessons).SelectMany(course => course.IsuExtraStreams).SelectMany(stream => _assignments[stream]));
        }

        public IEnumerable<Student> GetStudentsWithExtraLesson()
        {
            return _isuExtraService.Students.Except(_isuExtraService.Faculties.Keys.SelectMany(faculty => faculty.ExtraLessons).SelectMany(course => course.IsuExtraStreams).SelectMany(stream => _assignments[stream]));
        }

        public Student AddStudentToIsuExtraStream(Student student, IsuExtraStream isuExtraStream)
        {
            if (student is null)
                throw new Exception("Invalid student reference");
            if (isuExtraStream is null)
                throw new Exception("Invalid stream of extra lesson reference");
            Student stud = _isuExtraService.GetStudent(student.Id);
            IsuExtraStream stream = _isuExtraService.Faculties.Keys.SelectMany(l => l.ExtraLessons).SelectMany(f => f.IsuExtraStreams).FirstOrDefault(str => str.Name == isuExtraStream.Name);

            if (stream is null)
                throw new Exception("Invalid stream");
            if (_assignments.Keys.All(s => s.Name != stream.Name))
                _assignments.Add(stream, new List<Student>());
            if (_assignments[stream].Contains(stud))
                throw new Exception("Stream already has student");

            ExtraLesson isuExtraLesson = _isuExtraService.Faculties.Keys.SelectMany(m => m.ExtraLessons).FirstOrDefault(c => c.IsuExtraStreams.Contains(stream));
            if (GetStudents(isuExtraLesson).Contains(stud))
                throw new Exception("Extra lesson already has the student");
            if (_assignments[stream].Count(s => s.Id == stud.Id) == 2)
                throw new Exception("Student already has two extra lesson");

            foreach (var lesson in stream.TimeTableLessons.Lessons)
                stud.TimeTableLessons.AddLesson(lesson);
            _assignments[isuExtraStream].Add(stud);
            return stud;
        }

        public IEnumerable<ExtraLesson> GetExtraLessons()
        {
            return _isuExtraService.Faculties.Keys.SelectMany(w => w.ExtraLessons);
        }

        public void StudentDeleteExtraLesson(Student student, ExtraLesson extraLesson)
        {
            if (student == null) throw new Exception("This course hasn't the student");
            if (extraLesson == null) throw new Exception("Faculty hasn't this extra lesson");
            ExtraLesson extraCourse = _isuExtraService.Faculties.Keys.SelectMany(faculty => faculty.ExtraLessons).FirstOrDefault(f => f.NameExtraLesson == extraLesson?.NameExtraLesson);
            Student stud = GetStudents(extraCourse).FirstOrDefault(s => s.Id == student.Id);
            IsuExtraStream extraStream = extraCourse.IsuExtraStreams.FirstOrDefault(s => _assignments[s].Contains(student));
            if (extraStream is null)
                throw new Exception("Invalid extra lesson stream");
            _assignments[extraStream].Remove(stud);
        }

        public IEnumerable<Student> GetStudents(ExtraLesson extraLesson)
        {
            if (extraLesson == null) throw new Exception("Faluty hasn't the ognpCourse");
            ExtraLesson ex = _isuExtraService.Faculties.Keys.SelectMany(faculty => faculty.ExtraLessons).FirstOrDefault(c => c.NameExtraLesson == extraLesson.NameExtraLesson);
            return ex.IsuExtraStreams.SelectMany(stream => _assignments[stream]);
        }
    }
}