using Isu.Tools;

namespace Isu.Models
{
    public class Group
    {
        public Group(string groupName, CourseNumber courseNumber, uint number)
        {
            if (string.IsNullOrEmpty(groupName))
            {
                throw new IsuException($"Invalid groupname - {groupName}");
            }

            GroupName = groupName;
            Number = number;
            CourseNumber = courseNumber;
        }

        public string GroupName { get; set; }
        public CourseNumber CourseNumber { get; set; }
        public uint Number { get; set; }
    }
}
