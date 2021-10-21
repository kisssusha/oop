using System;
using IsuExtra.Tools;

namespace IsuExtra.Models
{
    public class Teacher
    {
        public Teacher(string name)
        {
            Name = name ?? throw new IsuExtraException("Invalid name");
        }

        public string Name { get; }
    }
}