using System;
using System.Collections.Generic;
using Backups.Entities;
using BackupsExtra.Services;

namespace BackupsExtra.DeleteHandlers
{
    public class TimeHandler : IHandler
    {
        private DateTime _expired;
        private IHandler _nextHandler;

        public TimeHandler()
        {
        }

        public TimeHandler(DateTime expired)
        {
            _expired = expired;
        }

        private DateTime Expired
        {
            get => _expired;
        }

        private IHandler NextHandler
        {
            get => _nextHandler;
        }

        public List<List<RestorePoint>> Execute(IReadOnlyList<RestorePoint> listOfPoints)
        {
            var listOfRpToDelete = new List<RestorePoint>();
            foreach (RestorePoint restorePoint in listOfPoints)
            {
                if ((restorePoint.Date - _expired.Date).Days < 0)
                {
                    listOfRpToDelete.Add(restorePoint);
                }
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