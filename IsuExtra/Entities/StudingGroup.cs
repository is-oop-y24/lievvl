using System.Collections.Generic;
using System.Linq;
using Isu;
using Isu.Tools;

namespace IsuExtra.Entities
{
    public class StudingGroup
    {
        private Group _group;
        private List<Lecture> _groupSchedule;

        public StudingGroup(Group group)
        {
            _group = group;
            _groupSchedule = new List<Lecture>();
        }

        public string GroupName
        {
            get
            {
                return _group.GetGroupName();
            }
        }

        public IReadOnlyList<Lecture> Schedule => _groupSchedule;

        internal void AddLecture(Lecture lecture)
        {
            bool isTimeIntersects = _groupSchedule.Any(lect => lect.TimeOfLecture.IsTimeIntersects(lecture.TimeOfLecture));
            if (isTimeIntersects)
            {
                throw new IsuException("Time intersects!");
            }

            _groupSchedule.Add(lecture);
        }
    }
}
