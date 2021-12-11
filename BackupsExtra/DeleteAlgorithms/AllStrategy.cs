using System.Collections.Generic;
using System.Linq;
using Backups.Entities;
using BackupsExtra.Services;

namespace BackupsExtra.DeleteAlgorithms
{
    public class AllStrategy : IDeleteAlgorithm
    {
        public List<RestorePoint> Execute(List<List<RestorePoint>> listsOfRpToDelete)
        {
            if (listsOfRpToDelete.Count == 1)
            {
                return listsOfRpToDelete.First();
            }

            var result = listsOfRpToDelete[0].Intersect(listsOfRpToDelete[1]).ToList();
            for (int i = 2; i < listsOfRpToDelete.Count - 1; i++)
            {
                result = result.Intersect(listsOfRpToDelete[i]).ToList();
            }

            return result;
        }
    }
}