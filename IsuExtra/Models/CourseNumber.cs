using IsuExtra.Services;

namespace IsuExtra.Models
{
    public class CourseNumber
    {
        public CourseNumber(uint value)
        {
            Value = value;
        }

        public uint Value { get; }
    }
}