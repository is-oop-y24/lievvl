using Backups.Entities;
using Backups.Repositories;
using Backups.SaveStrategies;

namespace Backups
{
    internal class Program
    {
        private static void Main()
        {
            var job = new BackupJob("C:\\Users\\Пользователь\\Documents\\BackupTest\\testdir", new SaveStrategySingleStorage(), new FileSystemRepository());

            job.JobObject.AddFile("C:\\Users\\Пользователь\\Documents\\BackupTest\\1.txt");
            job.JobObject.AddFile("C:\\Users\\Пользователь\\Documents\\BackupTest\\2.txt");
            job.JobObject.AddFile("C:\\Users\\Пользователь\\Documents\\BackupTest\\3.txt");

            job.Save();
        }
    }
}
