using System;
using System.Collections.Generic;
using Backups.Entities;
using Backups.Tools;
using BackupsExtra.Loggers;
using BackupsExtra.Services;

namespace BackupsExtra.Entities
{
    public class DeleteRestorePointsService
    {
        private ILogger _logger;
        private IHandler _deleteHandler;
        private IDeleteAlgorithm _deleteAlgorithm;
        private IDeleteMethod _deleteMethod;

        public DeleteRestorePointsService()
        {
            _logger = new UsualLazyLogger();
        }

        public void AttachLogger(ILogger logger)
        {
            _logger = logger ?? throw new ArgumentNullException();
        }

        public void AttachHandler(IHandler deleteHandler)
        {
            _deleteHandler = deleteHandler ?? throw new ArgumentNullException();
        }

        public void AttachAlgorithm(IDeleteAlgorithm deleteAlgorithm)
        {
            _deleteAlgorithm = deleteAlgorithm ?? throw new ArgumentNullException();
        }

        public void AttachMethod(IDeleteMethod deleteMethod)
        {
            _deleteMethod = deleteMethod ?? throw new ArgumentNullException();
        }

        public void Execute(BackupJob job)
        {
            if (_deleteAlgorithm == null
                || _deleteMethod == null
                || _deleteHandler == null)
            {
                _logger.Log("Haven't attached some DeleteRestorePointsService part!");
                throw new BackupException();
            }

            List<List<RestorePoint>> rpToDelete = _deleteHandler.Execute(job.RestorePoints);
            _deleteMethod.Execute(job, _deleteAlgorithm.Execute(rpToDelete));
        }
    }
}