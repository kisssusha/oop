using System.Collections.Generic;
using IsuExtra.Tools;

namespace IsuExtra.Models
{
    public class IsuExtraStream
    {
        private List<Student> _studentsInExtraLesson;

        public IsuExtraStream(string nameExtraStream, Teacher extraTeacher, int timeOfExtraLesson, string numberExtraAudit)
        {
            _studentsInExtraLesson = new List<Student>();
            Name = nameExtraStream;
            ExtraTeacher = extraTeacher;
            TimeOfExtraLesson = timeOfExtraLesson;
            NumberExtraAudit = numberExtraAudit;
            TimeTableLessons = new TimeTable();
        }

        public IEnumerable<Student> StudentsInExtraLesson => _studentsInExtraLesson;
        public TimeTable TimeTableLessons { get; set; }

        public string Name { get; }
        public Teacher ExtraTeacher { get; }
        public int TimeOfExtraLesson { get; }
        public string NumberExtraAudit { get; }

        public Student AddStudentInExtraLesson(Student student)
        {
            if (_studentsInExtraLesson.Count > 25) throw new IsuExtraException(" The group is overcrowed");
            _studentsInExtraLesson.Add(student);
            return student;
        }
    }
}