using System.Collections.Generic;
using Backups.Entities;
using BackupsExtra.Services;

namespace BackupsExtra.DeleteMethod
{
    public class UsualDeleteMethod : IDeleteMethod
    {
        public void Execute(BackupJob job, List<RestorePoint> listOfRpToDelete)
        {
            foreach (RestorePoint restorePoint in listOfRpToDelete)
            {
                job.DeleteRestorePoint(restorePoint);
            }
        }
    }
}