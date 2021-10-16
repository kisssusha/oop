using System;
using System.Collections.Generic;
using IsuExtra.Tools;

namespace IsuExtra.Models
{
    public class Faculty
    {
        private List<Course> _courses;
        private List<ExtraLesson> _extraLessons;
        public Faculty(string nameOfFaculty)
        {
            NameOfFaculty = nameOfFaculty;
            _extraLessons = new List<ExtraLesson>();
            _courses = new List<Course>();
        }

        public IEnumerable<Course> Courses => _courses;
        public IEnumerable<ExtraLesson> ExtraLessons => _extraLessons;

        public string NameOfFaculty { get; }

        public Course AddCourse(CourseNumber course)
        {
            if (_courses.Count > 4) throw new IsuExtraException(" The course is overcrowed ");
            var nameCourse = new Course(course);
            _courses.Add(nameCourse);
            return nameCourse;
        }

        public ExtraLesson AddExtraLesson(ExtraLesson extraLesson)
        {
            _extraLessons.Add(extraLesson);
            return extraLesson;
        }
    }
}