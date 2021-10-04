using Isu.Tools;

namespace Isu
{
    public class Student
    {
        private string _name;
        private int _id;
        private string _groupName;

        public Student(string groupName, string name, int id)
        {
            if (groupName == null || name == null)
                throw new IsuException("Received null at Student's constructor!");
            _groupName = groupName;
            _name = name;
            _id = id;
        }

        public int GetId() => _id;
        public string GetName() => _name;
        public string GetGroupName() => _groupName;
        internal void ChangeStudentGroup(string newGroupName) { _groupName = newGroupName; }
    }
}