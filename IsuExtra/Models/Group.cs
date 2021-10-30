using System;
using System.Collections.Generic;
using Isu.Tools;
using IsuExtra.Services;
using IsuExtra.Tools;

namespace IsuExtra.Models
    {
        public class Group
        {
            public Group(string groupName, CourseNumber courseNumber, uint number)
            {
                if (string.IsNullOrEmpty(groupName))
                {
                    throw new IsuExtraException($"Invalid groupname - {groupName}");
                }

                GroupName = groupName;
                Number = number;
                CourseNumber = courseNumber;
            }

            public string GroupName { get; }
            public CourseNumber CourseNumber { get; }
            public uint Number { get; }
        }
    }