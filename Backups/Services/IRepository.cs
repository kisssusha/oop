using Backups.Services;

namespace Backups.Models
{
    public interface IRepository
    {
        Storage AddStorage(Storage storage);

        RestorePoint CopyFileFromJob(IAlgorithmic algorithmic, JobObjects jobObjects);
    }
}