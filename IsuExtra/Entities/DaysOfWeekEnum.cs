using System;

namespace IsuExtra.Entities
{
    public class DaysOfWeekEnum
    {
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

        public bool IsDefined(string value)
        {
            return Enum.IsDefined(typeof(DaysOfWeek), value);
        }
    }
}