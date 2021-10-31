using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using Backups.Entities;
using Backups.Services;

namespace Backups.SaveStrategies
{
    public class SaveStrategySplitStorage : ISaveStrategy
    {
        public SaveStrategySplitStorage() { }

        // Maybe somewhere memory leak, idk
        public RestorePoint Execute(JobObject jobObject, Repository repository, DateTime date)
        {
            var listOfArchivesAtByte = new List<MemoryStream>();
            using var mStream = new MemoryStream();
            using var archive = new ZipArchive(mStream, ZipArchiveMode.Create, true);

            foreach (string filepath in jobObject.FilePaths)
            {
                string filename = filepath.Split("\\")[^1];
                archive.CreateEntryFromFile(filepath, filename);
            }

            listOfArchivesAtByte.Add(mStream);
            return repository.Save(listOfArchivesAtByte, date);
        }
    }
}
