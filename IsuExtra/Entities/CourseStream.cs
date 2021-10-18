using System.Collections.Generic;
using System.Linq;
using Isu;
using Isu.Tools;

namespace IsuExtra.Entities
{
    public class CourseStream
    {
        private int _numberOfStream;
        private List<Lecture> _listOfLectures;
        private List<Student> _listOfStudents;

        public CourseStream(int numberOfStream)
        {
            _listOfLectures = new List<Lecture>();
            _listOfStudents = new List<Student>();
            _numberOfStream = numberOfStream;
        }

        public int NumberOfStream => _numberOfStream;

        public IReadOnlyList<Student> Students => _listOfStudents.AsReadOnly();

        public IReadOnlyList<Lecture> Lectures => _listOfLectures.AsReadOnly();

        public Student FindStudent(int id)
        {
            Student searchedStudent = _listOfStudents.SingleOrDefault(student => student.GetId() == id);
            return searchedStudent;
        }

        internal void AddLecture(Lecture lecture)
        {
            bool isTimeIntersects = _listOfLectures.Any(lect => lect.TimeOfLecture.IsTimeIntersects(lecture.TimeOfLecture));
            if (isTimeIntersects)
            {
                throw new IsuException("Time intersects!");
            }

            _listOfLectures.Add(lecture);
        }

        internal void AddStudent(Student student)
        {
            bool isStudentAtStream = _listOfStudents.Any(stud => stud.GetId() == student.GetId());
            if (isStudentAtStream)
            {
                throw new IsuException("Student already assigned to OGNP!");
            }

            _listOfStudents.Add(student);
        }

        internal void DeleteStudent(Student student)
        {
            _listOfStudents.Remove(student);
        }
    }
}