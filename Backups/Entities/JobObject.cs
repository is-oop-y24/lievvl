using System.Collections.Generic;
using System.IO;
using System.Linq;
using Backups.Tools;

namespace Backups.Entities
{
    public class JobObject
    {
        private List<string> _filePaths;

        public JobObject()
        {
            _filePaths = new List<string>();
        }

        public JobObject(List<string> filePaths)
        {
            _filePaths = filePaths;
        }

        public List<string> FilePaths
        {
            get => _filePaths;
            set
            {
                _filePaths = value;
            }
        }

        public void AddFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new BackupException("File does not exist!");
            }

            if (IsFileInJob(filePath))
            {
                return;
            }

            _filePaths.Add(filePath);
        }

        public void DeleteFile(string filePath)
        {
            if (!IsFileInJob(filePath))
            {
                throw new BackupException("File isn't at JobObject!");
            }

            _filePaths.Remove(filePath);
        }

        private bool IsFileInJob(string filePath)
        {
            return _filePaths.Any(path => path == filePath);
        }
    }
}
