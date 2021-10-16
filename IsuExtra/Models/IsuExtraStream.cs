using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using IsuExtra.Tools;

namespace IsuExtra.Models
{
    public class IsuExtraStream
    {
        private const int _maxCountStudentInGroup = 25;
        private List<Student> _studentsInExtraLesson;

        public IsuExtraStream(string nameExtraStream, Teacher extraTeacher, int timeOfExtraLesson, string numberExtraAudit)
        {
            _studentsInExtraLesson = new List<Student>();
            Name = nameExtraStream ?? throw new ArgumentNullException(nameof(nameExtraStream));
            ExtraTeacher = extraTeacher ?? throw new ArgumentNullException(nameof(extraTeacher));
            if (timeOfExtraLesson < 0) throw new IsuExtraException(" Time is negative");
            TimeOfExtraLesson = timeOfExtraLesson;
            NumberExtraAudit = numberExtraAudit ?? throw new ArgumentNullException(nameof(numberExtraAudit));
            TimeTableLessons = new TimeTable();
        }

        public ReadOnlyCollection<Student> StudentsInExtraLesson => _studentsInExtraLesson.AsReadOnly();
        public TimeTable TimeTableLessons { get; set; }

        public string Name { get; }
        public Teacher ExtraTeacher { get; }
        public int TimeOfExtraLesson { get; }
        public string NumberExtraAudit { get; }

        public Student AddStudentInExtraLesson(Student student)
        {
            if (_studentsInExtraLesson.Count > _maxCountStudentInGroup) throw new IsuExtraException(" The group is overcrowed");
            _studentsInExtraLesson.Add(student);
            return student;
        }
    }
}