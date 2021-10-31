using System;
using System.IO;
using Backups.Services;

namespace Backups.Entities
{
    public class BackupJob
    {
        private JobObject _jobObject;
        private ISaveStrategy _saveStrategy;
        private Repository _repository;
        private string _jobPath;

        public BackupJob(string jobPath, ISaveStrategy saveStrategy, Repository repository)
        {
            _jobPath = jobPath;
            _saveStrategy = saveStrategy;
            _jobObject = new JobObject();
            _repository = repository;
            repository.SetJob(this);
            Directory.CreateDirectory(_jobPath);
        }

        public JobObject JobObject
        {
            get => _jobObject;
        }

        public string JobPath
        {
            get => _jobPath;
        }

        public void SetSaveAlgorithm(ISaveStrategy saveStrategy)
        {
            if (saveStrategy == null)
            {
                throw new ArgumentNullException();
            }

            _saveStrategy = saveStrategy;
        }

        public RestorePoint Save()
        {
            return _saveStrategy.Execute(_jobObject, _repository, DateTime.Now);
        }
    }
}
