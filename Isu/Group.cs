using System.Collections.Generic;
using System.Linq;
using Isu.Tools;
namespace Isu
{
    public class Group
    {
        private const int _maximumCapacity = 25;
        private const string _prefix = "M3";
        private List<Student> _listOfStudents;
        private string _name;
        private CourseNumber _courseNumber;

        public Group(string name)
        {
            if (name == null)
                throw new IsuException("Received null at Group's constructor!");
            _listOfStudents = new List<Student>();

            if ($"{name[0]}{name[1]}" != _prefix)
                throw new IsuException("Wrong group prefix!");

            _courseNumber = new CourseNumber(name[2] - '0');
            _name = name;
        }

        public string GetGroupName() => _name;

        public void AddStudent(Student student)
        {
            if (student == null)
                throw new IsuException($"Trying to add non-existent student! Group - {_name}");

            if (_listOfStudents.Count >= _maximumCapacity)
                throw new IsuException($"Reached maximum group capacity! Group - {_name}");
            _listOfStudents.Add(student);
        }

        public Student GetStudent(int id)
        {
            foreach (Student student in _listOfStudents)
            {
                if (student.GetId() == id)
                    return student;
            }

            return null;
        }

        public Student GetStudent(string name)
        {
            foreach (Student student in _listOfStudents)
            {
                if (student.GetName() == name)
                    return student;
            }

            return null;
        }

        public CourseNumber GetCourseNumber() => _courseNumber;

        internal void RemoveStudent(int id)
        {
            // Student removeStudent = null;
            /*foreach (Student student in _listOfStudents)
            {
                if (id == student.GetId())
                    removeStudent = student;
            }*/
            IEnumerable<Student> removeStudent = from student in _listOfStudents
                where student.GetId() == id
                select student;

            if (!removeStudent.Any())
                throw new IsuException($"There no such student! Id = {id}");
            _listOfStudents.Remove(removeStudent.First());
        }

        internal List<Student> GetStudentsList() => _listOfStudents;
    }
}
