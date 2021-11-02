using System;
using System.Collections.Generic;
using System.IO;
using Backups.Services;

namespace Backups.Repositories
{
    public class VirtualRepository : AbstractRepository
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

        public override List<string> Save(List<MemoryStream> listOfArchives, DateTime date)
        {
            var zipPaths = new List<string>();
            for (int i = 0; i < listOfArchives.Count; i++)
            {
                zipPaths.Add(Convert.ToString(GetHashCode()));
            }

            _memoryStreams.Add(listOfArchives);
            return zipPaths;
        }
    }
}
