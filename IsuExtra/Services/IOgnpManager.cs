using System.Collections.Generic;
using Isu;
using IsuExtra.Entities;

namespace IsuExtra.Services
{
    public interface IOgnpManager
    {
        public StudingGroup AddStudingGroup(string groupName);
        public void AddLectureToStudingGroup(StudingGroup studingGroup, Lecture lecture);
        public StudingGroup FindStudingGroup(string groupName);
        public Course AddOgnpCourse(string name, char megafacultyOfCourse);
        public CourseStream AddStreamToOgnp(Course ognp);
        public void AddLectureToOgnp(Course course, int numberOfStream, Lecture lecture);
        public Course FindOgnp(string name);
        public void SignToOgnp(Student student, Course course, int numberOfStream);
        public void UnsignOgnp(Course course, Student student);
        public List<Student> GetNotSignedToOgnp(Group group);
    }
}