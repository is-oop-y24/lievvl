using System;
using System.Collections.Generic;
using System.Linq;
using Backups.Entities;
using BackupsExtra.Services;

namespace BackupsExtra.DeleteMethod
{
    public class MergeDeleteMethod : IDeleteMethod
    {
        public MergeDeleteMethod()
        {
        }

        public void Execute(BackupJob job, List<RestorePoint> listOfRpToDelete)
        {
            List<RestorePoint> listOfRp = listOfRpToDelete;

            while (listOfRp.Count > 1)
            {
                var newIterationList = new List<RestorePoint>();
                for (int i = 0; i < listOfRp.Count - 1; i++)
                {
                    newIterationList.Add(Merge(listOfRp[i], listOfRp[i + 1]));
                }

                listOfRp = newIterationList;
            }

            foreach (RestorePoint rp in listOfRpToDelete)
            {
                job.DeleteRestorePoint(rp);
            }

            job.AddRestorePoint(listOfRp[0]);
        }

        private RestorePoint Merge(RestorePoint rp1, RestorePoint rp2)
        {
            RestorePoint old;
            RestorePoint young;
            if (rp1.Date < rp2.Date)
            {
                old = rp1;
                young = rp2;
            }
            else
            {
                old = rp2;
                young = rp1;
            }

            string youngZipPath = young.Storages.First().ZipPath;
            string oldZipPath = old.Storages.First().ZipPath;
            if (young.Storages.All(storage => storage.ZipPath == youngZipPath)
            || old.Storages.All(storage => storage.ZipPath == oldZipPath))
            {
                return young;
            }

            var storagesOfMergedPoint = new List<Storage>();
            foreach (Storage oldStorage in old.Storages)
            {
                bool foundYounger = false;
                foreach (Storage youngStorage in young.Storages)
                {
                    if (oldStorage.OriginalFilePath == youngStorage.OriginalFilePath)
                    {
                        storagesOfMergedPoint.Add(youngStorage);
                        foundYounger = true;
                        break;
                    }
                }

                if (!foundYounger)
                {
                    storagesOfMergedPoint.Add(oldStorage);
                }
            }

            return new RestorePoint(DateTime.Now, storagesOfMergedPoint);
        }
    }
}