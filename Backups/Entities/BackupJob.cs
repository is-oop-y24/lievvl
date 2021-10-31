using System;
using System.Collections.Generic;
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
        private List<RestorePoint> _listOfRestorePoints;

        public BackupJob(string jobPath, ISaveStrategy saveStrategy, Repository repository)
        {
            _jobPath = jobPath;
            _saveStrategy = saveStrategy;
            _jobObject = new JobObject();
            _repository = repository;
            repository.SetJob(this);
            _listOfRestorePoints = new List<RestorePoint>();
            Directory.CreateDirectory(_jobPath);
        }

        public IReadOnlyList<RestorePoint> RestorePoints
        {
            get => _listOfRestorePoints;
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
            _listOfRestorePoints.Add(_saveStrategy.Execute(_jobObject, _repository, DateTime.Now));
            return _listOfRestorePoints[^1];
        }

        public void DeleteRestorePoint(RestorePoint restorePoint)
        {
            if (restorePoint == null)
            {
                throw new ArgumentNullException();
            }

            _listOfRestorePoints.Remove(restorePoint);
        }
    }
}
