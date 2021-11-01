using System.IO;
using Backups.Entities;
using Backups.Repositories;
using Backups.SaveStrategies;
using NUnit.Framework;

namespace Backups.Tests
{
    public class Tests
    {
        private BackupJob job = new BackupJob(".\\testDir", new SaveStrategySingleStorage(), new VirtualRepository());
        [SetUp]
        public void Setup()
        {
            File.WriteAllText(".\\1.txt", "file1");
            File.WriteAllText(".\\2.txt", "file2");
            job.JobObject.AddFile(".\\1.txt");
            job.JobObject.AddFile(".\\2.txt");
        }

        [Test]
        public void Test1()
        {
            job.Save();
            job.JobObject.DeleteFile(".\\1.txt");
            job.Save();
            Assert.AreEqual(2, job.RestorePoints.Count);
            int storagesNumber = 0;
            foreach(RestorePoint rp in job.RestorePoints)
            {
                storagesNumber += rp.Storages.Count;
            }
            Assert.AreEqual(3, storagesNumber);
        }
    }
}