using System.IO;
using System.Linq;
using Backups.Entities;
using Backups.Repositories;
using Backups.SaveStrategies;
using BackupsExtra.DeleteAlgorithms;
using BackupsExtra.DeleteHandlers;
using BackupsExtra.DeleteMethod;
using BackupsExtra.Entities;
using NUnit.Framework;

namespace BackupsExtra.Tests
{
    public class Tests
    {
        private DeleteRestorePointsService deleteService;
        private BackupService service;
        private BackupJob job;

        [SetUp]
        public void Setup()
        {
            File.WriteAllText(".\\1.txt", "file1");
            File.WriteAllText(".\\2.txt", "file2");
            File.WriteAllText(".\\3.txt", "file3");
            service = new BackupService();
            deleteService = new DeleteRestorePointsService();
            deleteService.AttachHandler(new AmountHandler(2));
            deleteService.AttachAlgorithm(new AllStrategy());
            deleteService.AttachMethod(new UsualDeleteMethod());
            job = new BackupJob(".\\testDir", new SaveStrategySplitStorage(), new VirtualRepository());
            job.JobObject.AddFile(".\\1.txt");
            job.JobObject.AddFile(".\\2.txt");
            service.ListOfBackupJobs.Add(job);
        }

        [Test]
        public void Save3Times_ExecuteDeleteService()
        {
            job.Save();
            job.Save();
            job.Save();
            deleteService.Execute(job);
            Assert.AreEqual(2, job.RestorePoints.Count);
        }

        [Test]
        public void Save4Times_SetMerge_ExecuteDeleteService()
        {
            job.Save();
            job.Save();
            job.Save();
            job.Save();
            deleteService.AttachMethod(new MergeDeleteMethod());
            deleteService.Execute(job);
            Assert.AreEqual(3, job.RestorePoints.Count);
        }

        [Test]
        public void SetMerge_Add3txt_Save_Delete3txt_Save3Times_ExecuteDeleteService_CheckIfThereFile()
        {
            deleteService.AttachMethod(new MergeDeleteMethod());
            job.JobObject.AddFile(".\\3.txt");
            job.Save();
            job.JobObject.DeleteFile(".\\3.txt");
            job.Save();
            job.Save();
            job.Save();
            deleteService.Execute(job);
            Assert.AreEqual(3, job.RestorePoints.Count);
            Assert.AreEqual(3, job.RestorePoints.Last().Storages.Count);
        }
    }
}