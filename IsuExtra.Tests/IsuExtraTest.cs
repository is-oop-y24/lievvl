using System.Collections.Generic;
using IsuExtra.Entities;
using NUnit.Framework;
using Isu;
using Isu.Tools;
using System.Linq;
using NUnit.Framework.Internal;

namespace IsuExtra.Tests
{
    public class Tests
    {
        private OgnpManager _ognpManager;
        private Group group1;
        private Group group2;

        private StudingGroup sg1;
        private StudingGroup sg2;

        private Student ognpEnjoyer;
        private Student OOPfan;
        private Student academEnjoyer;
        private Student noname;

        private Course kib;
        private Course matan;
        private Course photonika;
        private Course garbage;

        [SetUp]
        public void Setup()
        {
            _ognpManager = new OgnpManager();
            group1 = _ognpManager.GetIsuService.AddGroup("M3203");
            group2 = _ognpManager.GetIsuService.AddGroup("P0200");
            ognpEnjoyer = _ognpManager.GetIsuService.AddStudent(group1, "ILoveOgnp");
            OOPfan = _ognpManager.GetIsuService.AddStudent(group1, "IUsualStudent");
            academEnjoyer = _ognpManager.GetIsuService.AddStudent(group1, "IHateOgnp");
            noname = _ognpManager.GetIsuService.AddStudent(group2, "IAmNoname");

            sg1 = _ognpManager.AddStudingGroup(group1.GetGroupName());
            sg2 = _ognpManager.AddStudingGroup(group2.GetGroupName());
            _ognpManager.AddLectureToStudingGroup(sg1, new Lecture("OOP", "???", "228", new LectureTime("10:00", "11:30", "Monday")));
            _ognpManager.AddLectureToStudingGroup(sg1, new Lecture("MATAN", "Vozianova", "228", new LectureTime("12:50", "13:10", "Monday")));
            _ognpManager.AddLectureToStudingGroup(sg2, new Lecture("?", "?", "228", new LectureTime("15:20", "16:50", "Sunday")));

            kib = _ognpManager.AddOgnpCourse("Kib", 'O');
            matan = _ognpManager.AddOgnpCourse("Matan", 'M');
            photonika = _ognpManager.AddOgnpCourse("Photonika", 'P');
            garbage = _ognpManager.AddOgnpCourse("Kreatex", 'K');

            _ognpManager.AddStreamToOgnp(kib);
            _ognpManager.AddStreamToOgnp(matan);
            _ognpManager.AddStreamToOgnp(photonika);
            _ognpManager.AddStreamToOgnp(garbage);

            _ognpManager.AddLectureToOgnp(kib, 1, new Lecture("Kib", "Kto-to", "228", new LectureTime("15:20", "18:00", "Wednesday")));
            _ognpManager.AddLectureToOgnp(matan, 1, new Lecture("Matan", "?", "228", new LectureTime("15:20", "16:50", "Sunday")));
            _ognpManager.AddLectureToOgnp(photonika, 1, new Lecture("Photonika", "Kto-to", "228", new LectureTime("15:20", "18:00", "Friday")));
            _ognpManager.AddLectureToOgnp(garbage, 1, new Lecture("Kreatex", "Kto-to", "228", new LectureTime("15:20", "18:00", "Thursday")));

            _ognpManager.SignToOgnp(ognpEnjoyer, kib, 1);
            _ognpManager.SignToOgnp(ognpEnjoyer, photonika, 1);
            _ognpManager.SignToOgnp(OOPfan, kib, 1);
        }

        [Test]
        public void AddToKibIntersectingLecture_CatchException()
        {
            Assert.Catch<IsuException>(() =>
            {
                _ognpManager.AddLectureToOgnp(kib, 1, new Lecture("Kib", "Kto-to", "228", new LectureTime("15:20", "18:00", "Wednesday")));
            });
        }

        [Test]
        public void SignToThreeOgnp_CatchException()
        {
            Assert.Catch<IsuException>(() =>
            {
                _ognpManager.SignToOgnp(ognpEnjoyer, garbage, 1);
            });
        }

        [Test]
        public void SignToOgnpWhereTimeIntersects_CatchException()
        {
            Assert.Catch<IsuException>(() =>
            {
                _ognpManager.SignToOgnp(noname, matan, 1);
            });
        }

        [Test]
        public void SignToOgnpAtSameMegafaculty_CatchException()
        {
            Assert.Catch<IsuException>(() =>
            {
                _ognpManager.SignToOgnp(academEnjoyer, matan, 1);
            });
        }

        [Test]
        public void GetStudentsNotAtAnyOgnpAtGroup1_GetAcademEnjoyer()
        {
            List<Student> students = _ognpManager.GetNotSignedToOgnp(group1);
            Assert.AreEqual(1, students.Count);
            Assert.AreEqual(academEnjoyer.GetId(), students[0].GetId());
        }

        [Test]
        public void UnsignOOPfanFromOgnp_GetStudentsNotAtAnyOgnp()
        {
            _ognpManager.UnsignOgnp(kib, OOPfan);
            List<Student> students = _ognpManager.GetNotSignedToOgnp(group1);
            Assert.AreEqual(2, students.Count);
            Assert.AreNotEqual(null, students.SingleOrDefault(stud => stud.GetId() == OOPfan.GetId()));
            Assert.AreNotEqual(null, students.SingleOrDefault(stud => stud.GetId() == academEnjoyer.GetId()));
        }

    }

}