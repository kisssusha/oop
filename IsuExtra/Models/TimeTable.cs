using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using IsuExtra.Tools;

namespace IsuExtra.Models
{
    public class TimeTable
    {
        private int _duringOfLesson = 90;
        private List<Lesson> _timeTable;

        public TimeTable()
        {
            _timeTable = new List<Lesson>(7);
        }

        public IEnumerable<Lesson> Lessons => _timeTable;

        public void AddLesson(Lesson les)
        {
            if (les == null) throw new ArgumentNullException(nameof(les));

            if (_timeTable.All(lesson => ((lesson.TimeOfLesson - _duringOfLesson > 0) &&
                                          (lesson.TimeOfLesson + _duringOfLesson <= les.TimeOfLesson ||
                                           les.TimeOfLesson + _duringOfLesson <= lesson.TimeOfLesson))))
            {
                if (les.TimeOfLesson < 9990)
                {
                    _timeTable.Add(les);
                }
                else
                {
                    throw new IsuExtraException(" Unable to add item");
                }
            }
            else
            {
                throw new IsuExtraException(" Unable to add item");
            }
        }

        public void GetInfo()
        {
            foreach (Lesson les in _timeTable)
            {
                Console.WriteLine(les.NameOfLesson);
            }
        }
    }
}