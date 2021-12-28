using System.Collections.Generic;
using Backups.Entities;

namespace BackupsExtra.Services
{
    public interface IDeleteMethod
    {
        void Execute(BackupJob job, List<RestorePoint> listOfRpToDelete);
    }
}