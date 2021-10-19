namespace IsuExtra.Entities
{
    public class Lecture
    {
        private string _name;
        private string _lecturer;
        private string _lectureHall;
        private LectureTime _lectureTime;

        public Lecture(string name, string lecturer, string lectureHall, LectureTime lectureTime)
        {
            _name = name;
            _lecturer = lecturer;
            _lectureHall = lectureHall;
            _lectureTime = lectureTime;
        }

        public LectureTime TimeOfLecture => _lectureTime;
    }
}