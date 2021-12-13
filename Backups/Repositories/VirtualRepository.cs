using System;
using System.Collections.Generic;
using System.IO;
using Backups.Services;

namespace Backups.Repositories
{
    public class VirtualRepository : AbstractRepository
    {
        public VirtualRepository()
        {
            MemoryStreams = new List<List<MemoryStream>>();
        }

        public List<List<MemoryStream>> MemoryStreams
        {
            get;
            set;
        }

        public override List<string> Save(List<MemoryStream> listOfArchives, DateTime date)
        {
            var zipPaths = new List<string>();
            for (int i = 0; i < listOfArchives.Count; i++)
            {
                zipPaths.Add($"{MemoryStreams.Count}:{i}");
            }

            MemoryStreams.Add(listOfArchives);
            return zipPaths;
        }
    }
}
