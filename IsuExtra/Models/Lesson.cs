namespace IsuExtra.Models
{
    public class Lesson
    {
        public Lesson(string nameOfLesson, Group nameGroup, Teacher teacher, int timeOfLesson, string numberAudit)
        {
            NameOfLesson = nameOfLesson;
            TeacherInLesson = teacher;
            TimeOfLesson = timeOfLesson;
            NumberAudit = numberAudit;
            NameGroup = nameGroup;
        }

        public string NameOfLesson { get; }
        public Teacher TeacherInLesson { get; }
        public int TimeOfLesson { get; }
        public string NumberAudit { get; }
        public Group NameGroup { get; }
    }
}