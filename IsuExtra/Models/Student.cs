using System;
using System.Collections.Generic;
using IsuExtra.Tools;

namespace IsuExtra.Models
{
    public class Student
    {
        private readonly TimeTable _timeTable;
        public Student(string name, Group studentGroup)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new IsuExtraException("Invalid student name");
            }

            Id = Guid.NewGuid();
            Name = name;
            StudentGroup = studentGroup;
            _timeTable = new TimeTable();
        }

        public string Name { get; }
        public TimeTable TimeTableLessons => _timeTable;
        public Guid Id { get; }
        public Group StudentGroup { get; set; }
    }
}