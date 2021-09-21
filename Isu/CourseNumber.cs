using Isu.Tools;
namespace Isu
{
    public class CourseNumber
    {
        private int _numberOfCourse;
        public CourseNumber(int courseNumber)
        {
            if (courseNumber < 1 || courseNumber > 4)
                throw new IsuException("Wrong course number!");
            _numberOfCourse = courseNumber;
        }

        public int GetCourseNumber() { return _numberOfCourse; }
    }
}