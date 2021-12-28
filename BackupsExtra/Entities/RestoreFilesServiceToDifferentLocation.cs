using System;
using System.IO;
using Backups.Entities;
using BackupsExtra.Services;

namespace BackupsExtra.Entities
{
    public class RestoreFilesServiceToDifferentLocation : AbstractRestoreService
    {
        private string _path;

        public RestoreFilesServiceToDifferentLocation()
        {
        }

        public RestoreFilesServiceToDifferentLocation(string path)
        {
            if (!Directory.Exists(path))
            {
                Logger.Log("Received wrong directory path!");
                throw new ArgumentOutOfRangeException();
            }

            _path = path;
        }

        private string Path
        {
            get => _path;
        }

        public override void Restore(RestorePoint restorePoint)
        {
            foreach (Storage storage in restorePoint.Storages)
            {
                RestoreTo(_path, storage.ZipPath);
                Logger.Log($"Restored {storage.Filename} to {_path}");
            }
        }
    }
}