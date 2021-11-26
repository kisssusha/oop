using System;
using BackupsExtra.Services;

namespace BackupsExtra.Models.ClearAlgo
{
    [Serializable]
    public class DateLimitClear : IClear
    {
        private DateTime _limitValue;

        public DateLimitClear(DateTime dateTime, BackupJob backupJob)
        {
            _limitValue = dateTime;
            BackupJobInClear = backupJob;
        }

        public BackupJob BackupJobInClear { get; }

        public bool IsLimitExceeded()
        {
            return _limitValue < BackupJobInClear.RestorePoints[0].Time;
        }

        public void Clear()
        {
            for (int i = 0; i < BackupJobInClear.RestorePoints.Count; i++)
            {
                if (IsLimitExceeded())
                {
                    BackupJobInClear.RemoveRestorePoint(BackupJobInClear.RestorePoints[i]);
                    i--;
                }
            }
        }
    }
}