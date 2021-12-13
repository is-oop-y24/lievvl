using System.Collections.Generic;
using Backups.Entities;
using Backups.Tools;
using BackupsExtra.Services;

namespace BackupsExtra.DeleteHandlers
{
    public class AmountHandler : IHandler
    {
        private int _limit;
        private IHandler _nextHandler;

        public AmountHandler()
        {
        }

        public AmountHandler(int limit)
        {
            if (limit <= 0)
            {
                throw new BackupException("Limit < 0!");
            }

            _limit = limit;
        }

        private int Limit
        {
            get => _limit;
        }

        private IHandler NextHandler
        {
            get => _nextHandler;
        }

        public List<List<RestorePoint>> Execute(IReadOnlyList<RestorePoint> listOfPoints)
        {
            var listOfRpToDelete = new List<RestorePoint>();
            int length = listOfPoints.Count;
            for (int i = 0; i < length - _limit; i++)
            {
                listOfRpToDelete.Add(listOfPoints[i]);
            }

            if (_nextHandler != null)
            {
                List<List<RestorePoint>> rp = _nextHandler.Execute(listOfPoints);
                rp.Add(listOfRpToDelete);
                return rp;
            }

            return new List<List<RestorePoint>> { listOfRpToDelete };
        }

        public void SetNext(IHandler nextHandler)
        {
            _nextHandler = nextHandler;
        }
    }
}