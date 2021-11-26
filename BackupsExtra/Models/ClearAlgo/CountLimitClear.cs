using System;
using System.Linq;
using BackupsExtra.Services;
using BackupsExtra.Tools;

namespace BackupsExtra.Models.ClearAlgo
{
    [Serializable]
    public class CountLimitClear : IClear
    {
        private int _limitValue;

        public CountLimitClear(BackupJob backupJob)
        {
            BackupJobInClear = backupJob;
        }

        public BackupJob BackupJobInClear { get; set; }

        public void AddLimit(int amount)
        {
            _limitValue = amount;
        }

        public bool IsLimitExceeded()
        {
            return _limitValue < BackupJobInClear.RestorePoints.Count;
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