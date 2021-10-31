namespace Backups.Entities
{
    public class Storage
    {
        private string _zipPath;

        public Storage(string zipPath)
        {
            _zipPath = zipPath;
        }

        public string ZipPath
        {
            get => _zipPath;
        }
    }
}
