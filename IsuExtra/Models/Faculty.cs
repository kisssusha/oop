using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using IsuExtra.Tools;

namespace IsuExtra.Models
{
    public class Faculty
    {
        private List<Course> _courses;
        private List<ExtraLesson> _extraLessons;
        public Faculty(string nameOfFaculty)
        {
            NameOfFaculty = nameOfFaculty ?? throw new ArgumentNullException(nameof(nameOfFaculty));
            _extraLessons = new List<ExtraLesson>();
            _courses = new List<Course>();
        }

        public ReadOnlyCollection<ExtraLesson> ExtraLessons => _extraLessons.AsReadOnly();
        public string NameOfFaculty { get; }

        public Course AddCourse(CourseNumber course)
        {
            if (course == null) throw new IsuExtraException("Invalid course");
            if (_courses.Count > 4) throw new IsuExtraException("The course is overcrowded");
            var nameCourse = new Course(course);
            _courses.Add(nameCourse);
            return nameCourse;
        }

        public ExtraLesson AddExtraLesson(ExtraLesson extraLesson)
        {
            if (extraLesson == null) throw new IsuExtraException("Invalid extraLesson");
            _extraLessons.Add(extraLesson);
            return extraLesson;
        }
    }
}