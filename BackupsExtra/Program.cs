using Backups.Entities;
using Backups.Repositories;
using Backups.SaveStrategies;
using BackupsExtra.DeleteAlgorithms;
using BackupsExtra.DeleteHandlers;
using BackupsExtra.DeleteMethod;
using BackupsExtra.Entities;
using Newtonsoft.Json;

namespace BackupsExtra
{
    internal class Program
    {
        private static void Main()
        {
            var service = new BackupService();
            var deleteService = new DeleteRestorePointsService();
            deleteService.AttachHandler(new AmountHandler(3));
            deleteService.AttachAlgorithm(new AllStrategy());
            deleteService.AttachMethod(new UsualDeleteMethod());

            var job = new BackupJob("./backupTest", new SaveStrategySingleStorage(), new FileSystemRepository());
            job.JobObject.AddFile("C:\\Users\\Пользователь\\Documents\\GitHub\\lievvl\\1.txt");
            job.JobObject.AddFile("C:\\Users\\Пользователь\\Documents\\GitHub\\lievvl\\2.txt");
            service.ListOfBackupJobs.Add(job);
            job.Save();
            service.Serialize();

            service = JsonConvert.DeserializeObject<BackupService>(BackupService.Deserialize(), new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto,
            });
            service.ToString();
        }
    }
}
