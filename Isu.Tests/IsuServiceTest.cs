using System.Linq;
using Isu.Models;
using Isu.Services;
using Isu.Tools;
using NUnit.Framework;

namespace Isu.Tests
{
    public class Tests
    {


        [Test]
        public void AddStudentToGroup_StudentHasGroupAndGroupContainsStudent() 
        {
            var isuService = new IsuService();

            var studentGroupToAdd = new Group("M3207", new CourseNumber(2), 7);

            var studentToAdd = new Student("Kisssusha")
            {
                StudentGroup = studentGroupToAdd,
            };
            
            isuService.AddGroup("M3207");
            isuService.AddStudent(studentGroupToAdd, "Kisssusha");

            Student student = isuService.FindStudents("M3207").Single();

            if (student.Name != studentToAdd.Name || student.StudentGroup.GroupName != studentGroupToAdd.GroupName)
                Assert.Fail();
        }

        [Test]
        public void ReachMaxStudentPerGroup_ThrowException()
        {
            Assert.Catch<IsuException>(() =>
            {
                var isuService = new IsuService();
                
                isuService.AddGroup("M3607");
                
                var studentGroupExpected = new Group("M3607", new CourseNumber(2), 7);

                for (int i = 0; i < 27; i++)
                {
                    isuService.AddStudent(studentGroupExpected, $"Kisssusha{i}");
                }
                
            });
        }

        [Test]
        public void CreateGroupWithInvalidName_ThrowException() 
        {
            Assert.Catch<IsuException>(() =>
            {
                var isuService = new IsuService();
                isuService.AddGroup("M3207M");
            });
        }

        [Test]
        public void TransferStudentToAnotherGroup_GroupChanged()
        {
            var isuService = new IsuService();
            
            isuService.AddGroup("M3207");
            isuService.AddGroup("M3203");
            isuService.AddStudent(isuService.FindGroup("M3203"), "Kisssusha");
            isuService.ChangeStudentGroup(isuService.FindStudent("Kisssusha"), isuService.FindGroup("M3207"));
            
            if(isuService.FindStudent("Kisssusha").StudentGroup.GroupName == "M3203")
                Assert.Fail();
        }
    }
}