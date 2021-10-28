namespace Backups.Models
{
        public interface IRepository
        {
                Storage AddStorage(Storage storage);
                RestorePoint CopyFileFromJobToRepoSingleStorage(JobObjects jobObjects);
                RestorePoint CopyFileFromJobToRepoSplitStorage(JobObjects jobObjects);
        }
}