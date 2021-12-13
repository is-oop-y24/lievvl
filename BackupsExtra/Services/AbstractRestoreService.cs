using System;
using System.IO.Compression;
using Backups.Entities;
using BackupsExtra.Loggers;

namespace BackupsExtra.Services
{
    public abstract class AbstractRestoreService
    {
        protected AbstractRestoreService()
        {
            Logger = new UsualLazyLogger();
        }

        public ILogger Logger
        {
            get;
            private set;
        }

        public void AttachLogger(ILogger logger)
        {
            Logger = logger ?? throw new ArgumentNullException();
        }

        public abstract void Restore(RestorePoint restorePoint);

        protected void RestoreTo(string path, string zipPath)
        {
            ZipArchive archive = ZipFile.OpenRead(zipPath);
            archive.ExtractToDirectory(path);
            archive.Dispose();
        }
    }
}