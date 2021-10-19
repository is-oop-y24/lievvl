using System.Collections.Generic;
using System.Linq;
using Isu;
using Isu.Tools;

namespace IsuExtra.Entities
{
    public class Course
    {
        private string _name;
        private List<CourseStream> _listOfStreams;
        private char _megafacultyOfCourse;
        private int _id;
        private int _nextNumberOfStream;

        public Course(string name, char megafacultyOfCourse, int id)
        {
            _listOfStreams = new List<CourseStream>();
            _id = id;
            _name = name;
            _nextNumberOfStream = 1;
            _megafacultyOfCourse = megafacultyOfCourse;
        }

        public IReadOnlyList<CourseStream> Streams => _listOfStreams.AsReadOnly();

        public int Id => _id;

        public char MegafacultyOfCourse => _megafacultyOfCourse;

        public string Name => _name;

        public CourseStream FindCourseStream(int numberOfStream)
        {
            return _listOfStreams.SingleOrDefault(stream => stream.NumberOfStream == numberOfStream);
        }

        public Student FindStudent(int id)
        {
            Student searchedStudent = null;
            foreach (CourseStream course in _listOfStreams)
            {
                searchedStudent = course.FindStudent(id);
                if (searchedStudent != null)
                    break;
            }

            return searchedStudent;
        }

        internal CourseStream AddCourseStream()
        {
            var stream = new CourseStream(_nextNumberOfStream++);
            _listOfStreams.Add(stream);
            return stream;
        }

        internal void AddLectureToStream(int numberOfStream, Lecture lecture)
        {
            CourseStream stream = _listOfStreams.SingleOrDefault(courseStream => courseStream.NumberOfStream == numberOfStream);
            if (stream == null)
            {
                throw new IsuException("Course doesn't contains that stream!");
            }

            stream.AddLecture(lecture);
        }

        internal void DeleteStudent(Student student)
        {
            if (student == null)
            {
                throw new IsuException("Received null instead of Student!");
            }

            CourseStream stream = _listOfStreams.SingleOrDefault(stream => stream.FindStudent(student.GetId()) != null);
            if (stream == null)
            {
                throw new IsuException("There is no such student!");
            }

            stream.DeleteStudent(student);
        }
    }
}