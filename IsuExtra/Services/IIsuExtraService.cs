using System;
using System.Collections.Generic;
using IsuExtra.Models;

namespace IsuExtra.Services
{
    public interface IIsuExtraService
    {
        Faculty AddFaculty(string name);
        Group AddGroup(string name);
        Student AddStudent(Group group, string name);

        Student GetStudent(Guid id);
        Student FindStudent(string name);
        List<Student> FindStudents(string groupName);
        List<Student> FindStudents(CourseNumber courseNumber);
        Group FindGroup(string groupName);
        List<Group> FindGroups(CourseNumber courseNumber);

        void ChangeStudentGroup(Student student, Group newGroup);
    }
}