using System.Collections.Generic;
using IsuExtra.Tools;

namespace IsuExtra.Models
{
    public class Course
    {
        private List<Group> _groups;
        public Course(CourseNumber course)
        {
            CourseName = course;
            _groups = new List<Group>();
        }

        public IEnumerable<Group> Groups => _groups;
        public CourseNumber CourseName { get; }
        public Group AddGrouptInCourse(string groupName, CourseNumber courseNumber, uint number)
        {
            var group = new Group(groupName, courseNumber, number);
            _groups.Add(group);
            return group;
        }
    }
}