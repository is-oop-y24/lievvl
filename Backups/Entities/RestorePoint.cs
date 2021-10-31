using System;
using System.Collections.Generic;

namespace Backups.Entities
{
    public class RestorePoint
    {
        private DateTime _dateTime;
        private List<Storage> _storages;

        public RestorePoint(DateTime dateTime, List<Storage> listOfStorages)
        {
            _dateTime = dateTime;
            _storages = listOfStorages;
        }

        public DateTime Date
        {
            get => _dateTime;
        }

        public IReadOnlyList<Storage> Storages
        {
            get => _storages;
        }
    }
}
