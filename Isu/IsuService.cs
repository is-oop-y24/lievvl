using System.Collections.Generic;
using System.Linq;
using Isu.Services;
using Isu.Tools;
namespace Isu
{
    public class IsuService : IIsuService
    {
        private List<Group> _listOfGroups;
        private int _nextId;

        public IsuService()
        {
            _listOfGroups = new List<Group>();
            _nextId = 100000;
        }

        public Group AddGroup(string name)
        {
            if (name == null)
                throw new IsuException("Received null at IsuService's method: AddGroup");
            _listOfGroups.Add(new Group(name));
            return _listOfGroups[_listOfGroups.Count - 1];
        }

        public Student AddStudent(Group group, string name)
        {
            if (group == null)
                throw new IsuException("Received null at IsuService's method: AddStudent");
            var newStudent = new Student(group.GetGroupName(), name, _nextId);
            group.AddStudent(newStudent);
            _nextId++;
            return newStudent;
        }

        public Student GetStudent(int id)
        {
            foreach (Group group in _listOfGroups)
            {
                Student searchedStudent = group.GetStudent(id);
                if (searchedStudent != null)
                    return searchedStudent;
            }

            return null;
        }

        public Student FindStudent(string name)
        {
            foreach (Group group in _listOfGroups)
            {
                Student searchedStudent = group.GetStudent(name);
                if (searchedStudent != null)
                    return searchedStudent;
            }

            return null;
        }

        public List<Student> FindStudents(CourseNumber courseNumber)
        {
            var listOfStudents = new List<Student>();
            foreach (Group group in _listOfGroups)
            {
                if (group.GetCourseNumber().GetCourseNumber() == courseNumber.GetCourseNumber())
                {
                    foreach (Student student in group.GetStudentsList())
                        listOfStudents.Add(student);
                }
            }

            if (listOfStudents.Count == 0)
                return null;
            return listOfStudents;
        }

        public List<Student> FindStudents(string groupName)
        {
            /*foreach (Group group in _listOfGroups)
            {
                if (group.GetGroupName() == groupName)
                    return group.GetStudentsList();
            }*/
            IEnumerable<List<Student>> studentsList = from gr in _listOfGroups
                where gr.GetGroupName() == groupName
                select gr.GetStudentsList();
            if (!studentsList.Any())
                return null;
            return studentsList.First();
        }

        public Group FindGroup(string groupName)
        {
            /*foreach (Group group in _listOfGroups)
            {
                if (group.GetGroupName() == groupName)
                    return group;
            }*/

            IEnumerable<Group> group = from gr in _listOfGroups
                where gr.GetGroupName() == groupName
                select gr;
            if (!group.Any())
                return null;
            return group.First();
        }

        public List<Group> FindGroups(CourseNumber courseNumber)
        {
            var listOfGroups = new List<Group>();
            foreach (Group group in _listOfGroups)
            {
                if (group.GetCourseNumber().GetCourseNumber() == courseNumber.GetCourseNumber())
                    listOfGroups.Add(group);
            }

            return listOfGroups;
        }

        public void ChangeStudentGroup(Student student, Group newGroup)
        {
            if (newGroup.GetGroupName() == student.GetGroupName())
                throw new IsuException("Trying to transfer student to same group!");
            foreach (Group oldGroup in _listOfGroups)
            {
                if (oldGroup.GetGroupName() == student.GetGroupName())
                    oldGroup.RemoveStudent(student.GetId());
            }

            student.ChangeStudentGroup(newGroup.GetGroupName());
            newGroup.AddStudent(student);
        }
    }
}