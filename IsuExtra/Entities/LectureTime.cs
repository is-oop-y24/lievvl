using System;
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

        /// <summary>
        /// Days of week.
        /// </summary>
        public enum DaysOfWeek
        {
            /// <summary>
            /// Represents a monday.
            /// </summary>
            Monday,

            /// <summary>
            /// Represents a tuesday.
            /// </summary>
            Tuesday,

            /// <summary>
            /// Represents a wednesday.
            /// </summary>
            Wednesday,

            /// <summary>
            /// Represents a thursday.
            /// </summary>
            Thursday,

            /// <summary>
            /// Represents a friday.
            /// </summary>
            Friday,

            /// <summary>
            /// Represents a saturday.
            /// </summary>
            Saturday,

            /// <summary>
            /// Thanks resharper!
            /// </summary>
            Sunday,
        }

        public string DayOfWeek
        {
            get => _dayOfWeek;

            internal set
            {
                bool isDefined = Enum.IsDefined(typeof(DaysOfWeek), value);
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