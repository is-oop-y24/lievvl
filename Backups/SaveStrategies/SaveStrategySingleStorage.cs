using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using Backups.Entities;
using Backups.Services;

namespace Backups.SaveStrategies
{
    public class SaveStrategySingleStorage : ISaveStrategy
    {
        public SaveStrategySingleStorage() { }

        public RestorePoint Execute(JobObject jobObject, AbstractRepository repository, DateTime date)
        {
            var listOfArchivesAtByte = new List<MemoryStream>();
            var listOfFilenames = new List<string>();
            using var mStream = new MemoryStream();
            using var archive = new ZipArchive(mStream, ZipArchiveMode.Create, true);

            foreach (string filepath in jobObject.FilePaths)
            {
                string filename = filepath.Split("\\")[^1];
                archive.CreateEntryFromFile(filepath, filename);
                listOfFilenames.Add(filename);
            }

            archive.Dispose();
            listOfArchivesAtByte.Add(mStream);
            List<string> zipPaths = repository.Save(listOfArchivesAtByte, date);
            var storages = new List<Storage>();

            for (int i = 0; i < listOfFilenames.Count; i++)
            {
                storages.Add(new Storage(zipPaths[0], listOfFilenames[i], jobObject.FilePaths[i]));
            }

            return new RestorePoint(date, storages);
        }
    }
}
