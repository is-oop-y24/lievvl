using System.Collections.Generic;
using Backups.Entities;

namespace BackupsExtra.Services
{
    public interface IDeleteAlgorithm
    {
        List<RestorePoint> Execute(List<List<RestorePoint>> listsOfRpToDelete);
    }
}