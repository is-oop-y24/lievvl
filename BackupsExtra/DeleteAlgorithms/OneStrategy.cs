using System.Collections.Generic;
using System.Linq;
using Backups.Entities;
using BackupsExtra.Services;

namespace BackupsExtra.DeleteAlgorithms
{
    public class OneStrategy : IDeleteAlgorithm
    {
        public OneStrategy()
        {
        }

        public List<RestorePoint> Execute(List<List<RestorePoint>> listsOfRpToDelete)
        {
            var result = new List<RestorePoint>();
            for (int i = 0; i < listsOfRpToDelete.Count - 1; i++)
            {
                result = result.Union(listsOfRpToDelete[i]).ToList();
            }

            return result;
        }
    }
}