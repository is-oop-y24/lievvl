using System;
using Backups.Entities;

namespace Backups.Services
{
    public interface ISaveStrategy
    {
        RestorePoint Execute(JobObject jobObject, AbstractRepository repository, DateTime date);
    }
}
