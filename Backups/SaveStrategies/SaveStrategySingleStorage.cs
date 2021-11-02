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

        // Maybe somewhere memory leak, idk
        public RestorePoint Execute(JobObject jobObject, AbstractRepository repository, DateTime date)
        {
            var listOfArchivesAtByte = new List<MemoryStream>();
            var listOfFilenames = new List<string>();

            foreach (string filepath in jobObject.FilePaths)
            {
                using var mStream = new MemoryStream();
                using var archive = new ZipArchive(mStream, ZipArchiveMode.Create, true);
                string filename = filepath.Split("\\")[^1];
                archive.CreateEntryFromFile(filepath, filename);
                archive.Dispose();
                listOfArchivesAtByte.Add(mStream);
                listOfFilenames.Add(filename);
            }

            List<string> listOfZipPaths = repository.Save(listOfArchivesAtByte, date);
            var listOfStorages = new List<Storage>();

            for (int i = 0; i < listOfZipPaths.Count; i++)
            {
                listOfStorages.Add(new Storage(listOfZipPaths[i], listOfFilenames[i], jobObject.FilePaths[i]));
            }

            return new RestorePoint(date, listOfStorages);
        }
    }
}
