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