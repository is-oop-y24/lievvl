using System;
using System.Collections.Generic;
using System.IO;
using Backups.Entities;
using Backups.Services;

namespace Backups.Repositories
{
    public class VirtualRepository : Repository
    {
        private List<List<MemoryStream>> _memoryStreams;

        public VirtualRepository()
        {
            _memoryStreams = new List<List<MemoryStream>>();
        }

        public IReadOnlyList<List<MemoryStream>> MemoryStreams
        {
            get => _memoryStreams;
        }

        public override RestorePoint Save(List<MemoryStream> listOfArchives, DateTime date)
        {
            var storages = new List<Storage>();
            for (int i = 0; i < listOfArchives.Count; i++)
            {
                storages.Add(new Storage(Convert.ToString(GetHashCode())));
            }

            _memoryStreams.Add(listOfArchives);
            return new RestorePoint(date, storages);
        }
    }
}
