using System.Text.RegularExpressions;
using Isu.Tools;

namespace IsuExtra.Entities
{
    public class LectureTime
    {
        // TimePattern is used to check with Regex, if input match time format
        private const string TimePattern = @"^([0-9]|1[0-9]|2[0-3]):[0-5][0-9]$";
        private string _dayOfWeek;
        private string _beginTime;
        private string _endTime;
        private DaysOfWeekEnum _daysOfWeek;

        public LectureTime(string beginTime, string endTime, string dayOfWeek)
        {
            _daysOfWeek = new DaysOfWeekEnum();
            BeginTime = beginTime;
            EndTime = endTime;
            if (string.CompareOrdinal(BeginTime, EndTime) > 0)
            {
                throw new IsuException("Time of end cannot be lesser than time of begin!");
            }

            DayOfWeek = dayOfWeek;
        }

        public string DayOfWeek
        {
            get => _dayOfWeek;

            internal set
            {
                bool isDefined = _daysOfWeek.IsDefined(value);
                if (!isDefined)
                {
                    throw new IsuException("Trying to set incorrect day!");
                }

                _dayOfWeek = value;
            }
        }

        public string BeginTime
        {
            get => _beginTime;

            private set
            {
                if (!Regex.IsMatch(value, TimePattern))
                {
                    throw new IsuException("Wrong begin time format!");
                }

                if (value.Split(':')[0].Length == 1)
                {
                    value = $"0{value}";
                }

                _beginTime = value;
            }
        }

        public string EndTime
        {
            get => _endTime;

            private set
            {
                if (!Regex.IsMatch(value, TimePattern))
                {
                    throw new IsuException("Wrong end time format!");
                }

                if (value.Split(':')[0].Length == 1)
                {
                    value = $"0{value}";
                }

                _endTime = value;
            }
        }

        public bool IsTimeIntersects(LectureTime lectureTime)
        {
            if (lectureTime.DayOfWeek != DayOfWeek)
            {
                return false;
            }

            int compare1 = string.CompareOrdinal(EndTime, lectureTime.BeginTime);
            int compare2 = string.CompareOrdinal(lectureTime.EndTime, BeginTime);
            return compare1 >= 0 && compare2 >= 0;
        }
    }
}