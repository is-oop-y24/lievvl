using System.Text.RegularExpressions;
using Isu.Tools;

namespace IsuExtra.Entities
{
    public class LectureTime
    {
        // DayPattern is used to check with Regex, if input match one of week days
        private const string DayPattern = @"^(Monday|Tuesday|Wednesday|Thursday|Friday|Saturday|Sunday)$";

        // TimePattern is used to check with Regex, if input match time format
        private const string TimePattern = @"^([0-9]|1[0-9]|2[0-3]):[0-5][0-9]$";
        private string _dayOfWeek;
        private string _beginTime;
        private string _endTime;

        public LectureTime(string beginTime, string endTime, string dayOfWeek)
        {
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
                if (!Regex.IsMatch(value, DayPattern))
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
            if (compare1 >= 0 && compare2 >= 0)
            {
                return true;
            }

            return false;
        }
    }
}