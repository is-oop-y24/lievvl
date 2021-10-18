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
        private MyItmo myItmo;
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
            myItmo = new MyItmo();
            group1 = myItmo.GetIsuService.AddGroup("M3203");
            group2 = myItmo.GetIsuService.AddGroup("P0200");
            ognpEnjoyer = myItmo.GetIsuService.AddStudent(group1, "ILoveOgnp");
            OOPfan = myItmo.GetIsuService.AddStudent(group1, "IUsualStudent");
            academEnjoyer = myItmo.GetIsuService.AddStudent(group1, "IHateOgnp");
            noname = myItmo.GetIsuService.AddStudent(group2, "IAmNoname");

            sg1 = myItmo.AddStudingGroup(group1.GetGroupName());
            sg2 = myItmo.AddStudingGroup(group2.GetGroupName());
            myItmo.AddLectureToStudingGroup(sg1, new Lecture("OOP", "???", "228", new LectureTime("10:00", "11:30", "Monday")));
            myItmo.AddLectureToStudingGroup(sg1, new Lecture("MATAN", "Vozianova", "228", new LectureTime("12:50", "13:10", "Monday")));
            myItmo.AddLectureToStudingGroup(sg2, new Lecture("?", "?", "228", new LectureTime("15:20", "16:50", "Sunday")));

            kib = myItmo.AddOgnpCourse("Kib", 'O');
            matan = myItmo.AddOgnpCourse("Matan", 'M');
            photonika = myItmo.AddOgnpCourse("Photonika", 'P');
            garbage = myItmo.AddOgnpCourse("Kreatex", 'K');

            myItmo.AddStreamToOgnp(kib);
            myItmo.AddStreamToOgnp(matan);
            myItmo.AddStreamToOgnp(photonika);
            myItmo.AddStreamToOgnp(garbage);

            myItmo.AddLectureToOgnp(kib, 1, new Lecture("Kib", "Kto-to", "228", new LectureTime("15:20", "18:00", "Wednesday")));
            myItmo.AddLectureToOgnp(matan, 1, new Lecture("Matan", "?", "228", new LectureTime("15:20", "16:50", "Sunday")));
            myItmo.AddLectureToOgnp(photonika, 1, new Lecture("Photonika", "Kto-to", "228", new LectureTime("15:20", "18:00", "Friday")));
            myItmo.AddLectureToOgnp(garbage, 1, new Lecture("Kreatex", "Kto-to", "228", new LectureTime("15:20", "18:00", "Thursday")));

            myItmo.SignToOgnp(ognpEnjoyer, kib, 1);
            myItmo.SignToOgnp(ognpEnjoyer, photonika, 1);
            myItmo.SignToOgnp(OOPfan, kib, 1);
        }

        [Test]
        public void AddToKibIntersectingLecture_CatchException()
        {
            Assert.Catch<IsuException>(() =>
            {
                myItmo.AddLectureToOgnp(kib, 1, new Lecture("Kib", "Kto-to", "228", new LectureTime("15:20", "18:00", "Wednesday")));
            });
        }

        [Test]
        public void SignToThreeOgnp_CatchException()
        {
            Assert.Catch<IsuException>(() =>
            {
                myItmo.SignToOgnp(ognpEnjoyer, garbage, 1);
            });
        }

        [Test]
        public void SignToOgnpWhereTimeIntersects_CatchException()
        {
            Assert.Catch<IsuException>(() =>
            {
                myItmo.SignToOgnp(noname, matan, 1);
            });
        }

        [Test]
        public void SignToOgnpAtSameMegafaculty_CatchException()
        {
            Assert.Catch<IsuException>(() =>
            {
                myItmo.SignToOgnp(academEnjoyer, matan, 1);
            });
        }

        [Test]
        public void GetStudentsNotAtAnyOgnpAtGroup1_GetAcademEnjoyer()
        {
            List<Student> students = myItmo.GetNotSignedToOgnp(group1);
            Assert.AreEqual(1, students.Count);
            Assert.AreEqual(academEnjoyer.GetId(), students[0].GetId());
        }

        [Test]
        public void UnsignOOPfanFromOgnp_GetStudentsNotAtAnyOgnp()
        {
            myItmo.UnsignOgnp(kib, OOPfan);
            List<Student> students = myItmo.GetNotSignedToOgnp(group1);
            Assert.AreEqual(2, students.Count);
            Assert.AreNotEqual(null, students.SingleOrDefault(stud => stud.GetId() == OOPfan.GetId()));
            Assert.AreNotEqual(null, students.SingleOrDefault(stud => stud.GetId() == academEnjoyer.GetId()));
        }

    }

}