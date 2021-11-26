using System;
using BackupsExtra.Services;
using BackupsExtra.Tools;

namespace BackupsExtra.Models
{
    public class DateLimitClear : IClear
    {
        private DateTime _limitValue;

        public DateLimitClear(DateTime dateTime)
        {
            _limitValue = dateTime;
        }

        public bool IsLimitExceeded(BackupJob backupJob)
        {
            return _limitValue < backupJob.RestorePoints[0].Time;
        }

        public bool IsRemovable(BackupJob backupJob, RestorePoint restorePoint)
        {
            int pos = backupJob.RestorePoints.IndexOf(restorePoint);
            if (backupJob.RestorePoints.Count > 1 && backupJob.RestorePoints[pos + 1] != null)
            {
                return false;
            }

            return true;
        }

        public void Clear(BackupJob backupJob)
        {
            for (int i = 0; i < backupJob.RestorePoints.Count; i++)
            {
                if (IsLimitExceeded(backupJob))
                {
                    if (IsRemovable(backupJob, backupJob.RestorePoints[i]))
                    {
                        backupJob.RemoveRestorePoint(backupJob.RestorePoints[i]);
                        i--;
                        continue;
                    }

                    throw new BackupsExtraException("Try to remove not removable point");
                }
            }
        }
    }
}