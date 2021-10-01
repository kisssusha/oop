using Isu.Tools;

namespace Isu.Models
{
    public class Student
    {
        private static uint _allglobalId = 0;

        public Student(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new IsuException("Invalid student name");
            }

            Id = _allglobalId++;
            Name = name;
        }

        public string Name { get; }
        public uint Id { get; }
        public Group StudentGroup { get; set; }
    }
}