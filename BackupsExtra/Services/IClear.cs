using BackupsExtra.Models;

namespace BackupsExtra.Services
{
    public interface IClear
    {
        public bool IsRemovable(BackupJob backupJob, RestorePoint restorePoint);
        public void Clear(BackupJob backupJob);
        public bool IsLimitExceeded(BackupJob backupJob);
    }
}