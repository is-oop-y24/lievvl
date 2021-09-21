using System.Collections.Generic;
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
            _listOfGroups.Add(new Group(name));
            return _listOfGroups[_listOfGroups.Count - 1];
        }

        public Student AddStudent(Group group, string name)
        {
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
            foreach (Group group in _listOfGroups)
            {
                if (group.GetGroupName() == groupName)
                    return group.GetStudentsList();
            }

            return null;
        }

        public Group FindGroup(string groupName)
        {
            foreach (Group group in _listOfGroups)
            {
                if (group.GetGroupName() == groupName)
                    return group;
            }

            return null;
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