namespace Isu
{
    public class Student
    {
        private string _name;
        private int _id;
        private string _groupName;

        public Student(string groupName, string name, int id)
        {
            _groupName = groupName;
            _name = name;
            _id = id;
        }

        public int GetId() { return _id; }
        public string GetName() { return _name; }
        public string GetGroupName() { return _groupName; }
        internal void ChangeStudentGroup(string newGroupName) { _groupName = newGroupName; }
    }
}