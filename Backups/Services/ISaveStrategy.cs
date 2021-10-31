using System;
using Backups.Entities;

namespace Backups.Services
{
    public interface ISaveStrategy
    {
        RestorePoint Execute(JobObject jobObject, Repository repository, DateTime date);
    }
}
