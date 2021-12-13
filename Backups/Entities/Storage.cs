namespace Backups.Entities
{
    public class Storage
    {
        private string _zipPath;
        private string _filename;
        private string _originalFilePath;

        public Storage(string zipPath, string filename, string originalFilePath)
        {
            _zipPath = zipPath;
            _filename = filename;
            _originalFilePath = originalFilePath;
        }

        public string ZipPath
        {
            get => _zipPath;
            set
            {
                _zipPath = value;
            }
        }

        public string Filename
        {
            get => _filename;
            set
            {
                _filename = value;
            }
        }

        public string OriginalFilePath
        {
            get => _originalFilePath;
            set
            {
                _originalFilePath = value;
            }
        }
    }
}
