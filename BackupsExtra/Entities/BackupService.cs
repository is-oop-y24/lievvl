using System.Collections.Generic;
using System.IO;
using Backups.Entities;
using BackupsExtra.Loggers;
using BackupsExtra.Services;
using Newtonsoft.Json;

namespace BackupsExtra.Entities
{
    public class BackupService
    {
        private const string JsonPath = ".\\backup.json";
        public BackupService()
        {
            ListOfBackupJobs = new List<BackupJob>();
            Logger = new UsualLazyLogger();
        }

        public List<BackupJob> ListOfBackupJobs
        {
            get;
            private set;
        }

        private ILogger Logger
        {
            get;
            set;
        }

        public static string Deserialize()
        {
            using var jsonFile = new FileStream(JsonPath, FileMode.Open);
            byte[] jsonByte = new byte[jsonFile.Length];
            jsonFile.Read(jsonByte, 0, jsonByte.Length);
            string json = System.Text.Encoding.Default.GetString(jsonByte);
            return json;
        }

        public void Serialize()
        {
            string json = JsonConvert.SerializeObject(this, Formatting.Indented, new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.Auto,
            });
            using var jsonFile = new FileStream(JsonPath, FileMode.Create);
            byte[] jsonByte = System.Text.Encoding.Default.GetBytes(json);
            jsonFile.Write(jsonByte);
            Logger.Log("Serialize backup service");
        }
    }
}