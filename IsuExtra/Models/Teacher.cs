using System;

namespace IsuExtra.Models
{
    public class Teacher
    {
        public Teacher(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public string Name { get; }
    }
}