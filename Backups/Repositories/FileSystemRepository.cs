using System;
using System.Collections.Generic;
using System.IO;
using Backups.Entities;
using Backups.Services;

namespace Backups.Repositories
{
    public class FileSystemRepository : Repository
    {
        public override RestorePoint Save(List<MemoryStream> zipArchivesAtByte, DateTime date)
        {
            int i = 0;
            var storages = new List<Storage>();
            foreach (MemoryStream mStream in zipArchivesAtByte)
            {
                string storagePath = $"{GetJobPath()}\\{date.ToString("yyyyMMddhhmmss")}+{i++}.zip";
                storages.Add(new Storage(storagePath));
                File.WriteAllBytes(storagePath, mStream.ToArray());
            }

            return new RestorePoint(date, storages);
        }
    }
}
