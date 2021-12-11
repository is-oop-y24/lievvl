using System.Collections.Generic;
using Backups.Entities;

namespace BackupsExtra.Services
{
    public interface IHandler
    {
        void SetNext(IHandler nextHandler);
        List<List<RestorePoint>> Execute(IReadOnlyList<RestorePoint> listOfPoints);
    }
}