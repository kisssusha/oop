using System;
using System.Collections.Generic;
using System.Linq;
using Isu.Tools;
using IsuExtra.Models;
using IsuExtra.Services;
using IsuExtra.Tools;

namespace IsuEXtra.Services
{
    public class IsuExtraService : IIsuExtraService
    {
        private const uint MaxCountInGroup = 25;

        private readonly List<Group> _groups;
        private readonly List<Student> _students;
        private readonly Dictionary<Group, List<Student>> _assignments;
        private readonly Dictionary<Faculty, List<Group>> _faculties;

        public IsuExtraService()
        {
            _faculties = new Dictionary<Faculty, List<Group>>();
            _groups = new List<Group>();
            _students = new List<Student>();
            _assignments = new Dictionary<Group, List<Student>>();
        }

        public IReadOnlyDictionary<Faculty, List<Group>> Faculties => _faculties;
        public IEnumerable<Student> Students => _students;

        public Faculty AddFaculty(string name)
        {
            Faculty faculty = new Faculty(name);
            _faculties.Add(faculty, new List<Group>());
            return faculty;
        }

        public Group AddGroup(string name)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));
            if (string.IsNullOrWhiteSpace(name) || name.Length != 5)
                throw new IsuExtraException("Invalid group name");
            if (!uint.TryParse(name.Substring(3, 2), out uint groupNum))
                throw new IsuExtraException("Invalid group number");
            if (!uint.TryParse($"{name[2]}", out uint courseNum))
                throw new IsuExtraException("Invalid course number");
            var group = new Group(name, new CourseNumber(courseNum), groupNum);
            if (_groups.Any(g => g.GroupName == group.GroupName))
                throw new IsuExtraException("This group already exists");
            _groups.Add(group);
            _assignments.Add(group, new List<Student>());
            return group;
        }

        public Student AddStudent(Group @group, string name)
        {
            Group gr = _groups.FirstOrDefault(gr => gr.GroupName == group.GroupName);
            if (_assignments[gr].Count >= MaxCountInGroup)
                throw new IsuException("Group has max count of students");
            var student = new Student(name, gr);
            if (_assignments[gr].Any(st => st.Name == student.Name))
                throw new IsuException("This student already exists");
            student.ChangeGroup(gr);
            _assignments[gr].Add(student);
            _students.Add(student);
            return student;
        }

        public Student GetStudent(Guid id)
        {
            if (_students.All(st => st.Id != id))
                throw new IsuException("The student doesn't exist");
            return _students.Find(s => s.Id == id);
        }

        public Student FindStudent(string name)
        {
            return _students.FirstOrDefault(s => s.Name == name);
        }

        public List<Student> FindStudents(string groupName)
        {
            Group group = _groups.FirstOrDefault(g => g.GroupName == groupName);
            if (group is null)
                throw new IsuException("Invalid group");
            return _assignments[group];
        }

        public List<Student> FindStudents(CourseNumber courseNumber)
        {
            return _assignments
                .Where(g => g.Key.CourseNumber.Value == courseNumber.Value)
                .SelectMany(x => x.Value)
                .ToList();
        }

        public Group FindGroup(string groupName)
        {
            return _groups.FirstOrDefault(g => g.GroupName == groupName);
        }

        public List<Group> FindGroups(CourseNumber courseNumber)
        {
            return _groups.Where(group => group.CourseNumber == courseNumber).ToList();
        }

        public void ChangeStudentGroup(Student student, Group newGroup)
        {
            Group g = _groups.FirstOrDefault(gr => gr.GroupName == newGroup.GroupName);
            Student st = _students.FirstOrDefault(s => s.Name == student.Name);
            if (_groups.Contains(g))
            {
                foreach (Group group in _groups)
                    _assignments[@group].Remove(st);
            }
            else
            {
                throw new IsuException("Group not found");
            }

            if (st == null) return;
            st.ChangeGroup(newGroup);
            _assignments[newGroup].Add(st);
        }
    }
}