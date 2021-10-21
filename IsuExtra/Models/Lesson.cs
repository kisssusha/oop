using System;
using IsuExtra.Tools;

namespace IsuExtra.Models
{
    public class Lesson
    {
        private const int _finishOfWeek = 9990;
        public Lesson(string nameOfLesson, Group nameGroup, Teacher teacher, int timeOfLesson, string numberAudit)
        {
            NameOfLesson = nameOfLesson ?? throw new IsuExtraException("Invalid nameOfLesson");
            TeacherInLesson = teacher ?? throw new IsuExtraException("Invalid teacher");
            if (timeOfLesson is < 0 or > _finishOfWeek) throw new IsuExtraException("Invalid time");
            TimeOfLesson = timeOfLesson;
            NumberAudit = numberAudit;
            NameGroup = nameGroup ?? throw new IsuExtraException("Invalid nameGroup");
        }

        public string NameOfLesson { get; }
        public Teacher TeacherInLesson { get; }
        public int TimeOfLesson { get; }
        public string NumberAudit { get; }
        public Group NameGroup { get; }
    }
}