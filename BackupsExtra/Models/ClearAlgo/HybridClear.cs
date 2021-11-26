using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using BackupsExtra.Services;

namespace BackupsExtra.Models.ClearAlgo
{
    [Serializable]
    public class HybridClear : IClear
    {
        private List<IClear> _clearAlgo;

        public HybridClear(BackupJob backupJob)
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
                if (!algorithm.IsLimitExceeded()) return false;
            }

            return true;
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