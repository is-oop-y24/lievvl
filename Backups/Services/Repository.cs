using System;
using System.Collections.Generic;
using System.IO;
using Backups.Entities;

namespace Backups.Services
{
    public abstract class Repository
    {
        private BackupJob _backupJob;
        public Repository()
        {
            _backupJob = null;
        }

        public abstract List<string> Save(List<MemoryStream> listOfArchives, DateTime date);

        internal void SetJob(BackupJob job)
        {
            _backupJob = job;
        }

        internal string GetJobPath()
        {
            return _backupJob.JobPath;
        }
    }
}
