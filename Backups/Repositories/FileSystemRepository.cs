﻿using System;
using System.Collections.Generic;
using System.IO;
using Backups.Services;

namespace Backups.Repositories
{
    public class FileSystemRepository : Repository
    {
        public override List<string> Save(List<MemoryStream> zipArchivesAtByte, DateTime date)
        {
            int i = 0;
            var zipPaths = new List<string>();
            foreach (MemoryStream mStream in zipArchivesAtByte)
            {
                string zipPath = $"{GetJobPath()}\\{date.ToString("yyyyMMddhhmmss")}+{i++}.zip";
                zipPaths.Add(zipPath);
                File.WriteAllBytes(zipPath, mStream.ToArray());
            }

            return zipPaths;
        }
    }
}
