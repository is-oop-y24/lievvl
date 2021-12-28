using Backups.Entities;
using BackupsExtra.Services;

namespace BackupsExtra.Entities
{
    public class RestoreFilesServiceToOriginalLocation : AbstractRestoreService
    {
        public RestoreFilesServiceToOriginalLocation()
        {
        }

        public override void Restore(RestorePoint restorePoint)
        {
            foreach (Storage storage in restorePoint.Storages)
            {
                RestoreTo(storage.OriginalFilePath, storage.ZipPath);
                Logger.Log($"Restored {storage.Filename} to {storage.OriginalFilePath}");
            }
        }
    }
}