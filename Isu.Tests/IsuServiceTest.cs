using Isu.Services;
using Isu.Tools;
using NUnit.Framework;

namespace Isu.Tests
{
    public class Tests
    {
        private IIsuService _isuService;

        [SetUp]
        public void Setup()
        {
            //DONE: implement
            _isuService = new IsuService();
        }

        [Test]
        public void AddStudentToGroup_StudentHasGroupAndGroupContainsStudent()
        {
            Group testGroup = _isuService.AddGroup("M3203");
            int id = _isuService.AddStudent(testGroup, "IvanIvanov").GetId();

            Assert.True(_isuService.FindGroup("M3203").GetStudent("IvanIvanov") != null &&
                _isuService.GetStudent(id).GetGroupName() == "M3203") ;
        }

        [Test]
        public void ReachMaxStudentPerGroup_ThrowException()
        {
            Assert.Catch<IsuException>(() =>
            {
                Group testGroup = _isuService.AddGroup("M3203");
                string testName = "IvanIvanovIvanovich";
                for (int i = 0; i < 25; i++)
                    _isuService.AddStudent(testGroup, testName);
                _isuService.AddStudent(testGroup, testName);
            });
        }

        [Test]
        public void CreateGroupWithInvalidName_ThrowException()
        {
            Assert.Catch<IsuException>(() =>
            {
                _isuService.AddGroup("M3910");
            });
        }

        [Test]
        public void TransferStudentToAnotherGroup_GroupChanged()
        {
            Assert.Catch<IsuException>(() =>
            {
                Group fromGroup = _isuService.AddGroup("M3103");
                Group toGroup = _isuService.AddGroup("M3203");
                Student testStudent = _isuService.AddStudent(fromGroup, "IvanIvanov");
                _isuService.ChangeStudentGroup(testStudent, toGroup);
                Assert.True(toGroup.GetStudent("IvanIvanov") != null &&
                            testStudent.GetGroupName() == "M3203");
                _isuService.ChangeStudentGroup(testStudent, toGroup);
            });
        }
    }
}