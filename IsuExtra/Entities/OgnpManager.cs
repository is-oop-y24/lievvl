using System.Collections.Generic;
using System.Linq;
using Isu;
using Isu.Tools;
using IsuExtra.Services;

namespace IsuExtra.Entities
{
    public class OgnpManager : IOgnpManager
    {
        private IsuService _isuService;
        private List<Course> _listOfOgnpCourses;
        private List<StudingGroup> _listOfStudingGroups;
        private int _nextCourseId;

        public OgnpManager()
        {
            _isuService = new IsuService();
            _listOfOgnpCourses = new List<Course>();
            _listOfStudingGroups = new List<StudingGroup>();
        }

        public IsuService GetIsuService => _isuService;

        public StudingGroup AddStudingGroup(string groupName)
        {
            Group group = _isuService.FindGroup(groupName);
            if (group == null)
            {
                throw new IsuException("IsuService hasn't got that group!");
            }

            var studingGroup = new StudingGroup(group);
            _listOfStudingGroups.Add(studingGroup);
            return studingGroup;
        }

        public void AddLectureToStudingGroup(StudingGroup studingGroup, Lecture lecture)
        {
            studingGroup.AddLecture(lecture);
        }

        public StudingGroup FindStudingGroup(string groupName)
        {
            return _listOfStudingGroups.SingleOrDefault(group => group.GroupName == groupName);
        }

        public Course AddOgnpCourse(string name, char megafacultyOfCourse)
        {
            var course = new Course(name, megafacultyOfCourse, _nextCourseId++);
            _listOfOgnpCourses.Add(course);
            return course;
        }

        public CourseStream AddStreamToOgnp(Course ognp)
        {
            if (ognp == null)
            {
                throw new IsuException("Receive null instead of ognp");
            }

            bool isOgnpAtSystem = _listOfOgnpCourses.Any(course => course.Id == ognp.Id);
            if (!isOgnpAtSystem)
            {
                throw new IsuException("Ognp is not at system");
            }

            return ognp.AddCourseStream();
        }

        public void AddLectureToOgnp(Course course, int numberOfStream, Lecture lecture)
        {
            if (course == null)
            {
                throw new IsuException("Received null instead of Course");
            }

            bool isCourseAtSystem = _listOfOgnpCourses.Any(ognp => ognp.Id == course.Id);
            if (!isCourseAtSystem)
            {
                throw new IsuException("Course not in system!");
            }

            course.AddLectureToStream(numberOfStream, lecture);
        }

        public Course FindOgnp(string name)
        {
            return _listOfOgnpCourses.SingleOrDefault(ognp => ognp.Name == name);
        }

        public void SignToOgnp(Student student, Course course, int numberOfStream)
        {
            if (course == null || student == null)
            {
                throw new IsuException("Received null instead of student/course!");
            }

            bool isCourseAtSystem = _listOfOgnpCourses.Any(ognp => ognp.Id == course.Id);
            if (!isCourseAtSystem)
            {
                throw new IsuException("Course not in system!");
            }

            CourseStream stream = course.FindCourseStream(numberOfStream);
            if (stream == null)
            {
                throw new IsuException("There no such stream!");
            }

            Student isStudentAtCourse = stream.FindStudent(student.GetId());
            if (isStudentAtCourse != null)
            {
                throw new IsuException("Student already assigned to course!");
            }

            if (student.GetGroupName()[0] == course.MegafacultyOfCourse)
            {
                throw new IsuException("Student belongs to same megafaculty as Ognp!");
            }

            var listOfOgnp = _listOfOgnpCourses.Where(ognp => ognp.FindStudent(student.GetId()) != null).ToList();
            if (listOfOgnp.Count >= 2)
            {
                throw new IsuException("Student already assigned to 2 courses!");
            }

            StudingGroup studentGroup = FindStudingGroup(student.GetGroupName());
            bool isTimeIntersects = false;
            foreach (Lecture lecture in studentGroup.Schedule)
            {
                isTimeIntersects = stream.Lectures.Any(lect => lect.TimeOfLecture.IsTimeIntersects(lecture.TimeOfLecture));
                if (isTimeIntersects)
                {
                    throw new IsuException("Time of Studing group and ognp intersects!");
                }
            }

            stream.AddStudent(student);
        }

        public void UnsignOgnp(Course course, Student student)
        {
            if (course == null)
            {
                throw new IsuException("Received null instead of Course");
            }

            bool isCourseAtSystem = _listOfOgnpCourses.Any(ognp => ognp.Id == course.Id);
            if (!isCourseAtSystem)
            {
                throw new IsuException("Course is not at system!");
            }

            course.DeleteStudent(student);
        }

        public List<Student> GetNotSignedToOgnp(Group group)
        {
            StudingGroup studingGroup = _listOfStudingGroups.SingleOrDefault(gr => gr.GroupName == group.GetGroupName());

            if (studingGroup == null)
            {
                throw new IsuException("That group don't have");
            }

            var studentsNotInAnyOgnp = new List<Student>();
            List<Student> listOfStudents = _isuService.FindStudents(group.GetGroupName());
            foreach (Student student in listOfStudents)
            {
                var listOfOgnp = _listOfOgnpCourses.Where(ognp => ognp.FindStudent(student.GetId()) != null).ToList();
                if (listOfOgnp.Count == 0)
                {
                    studentsNotInAnyOgnp.Add(student);
                }
            }

            return studentsNotInAnyOgnp;
        }
    }
}