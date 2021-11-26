using System.Collections.Generic;
using System.Collections.ObjectModel;
using BackupsExtra.Services;
using BackupsExtra.Tools;

namespace BackupsExtra.Models
{
    public class HybridOneLimitClear : IClear
    {
        private List<IClear> _clearAlgo;

        public HybridOneLimitClear(BackupJob backupJob)
        {
            _clearAlgo = new List<IClear>();
            BackupJobInClear = backupJob;
        }

        public BackupJob BackupJobInClear { get; }

        public ReadOnlyCollection<IClear> ClearAlgo => _clearAlgo.AsReadOnly();

        public void AddClearAlgoInHybrid(IClear clear)
        {
            _clearAlgo.Add(clear);
        }

        public bool IsLimitExceeded()
        {
            foreach (IClear algorithm in ClearAlgo)
            {
                if (algorithm.IsLimitExceeded()) return true;
            }

            return false;
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