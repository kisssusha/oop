using System.Collections.Generic;
using System.Collections.ObjectModel;
using BackupsExtra.Services;
using BackupsExtra.Tools;

namespace BackupsExtra.Models
{
    public class HybridClear : IClear
    {
        private List<IClear> _clearAlgo;

        public HybridClear()
        {
            _clearAlgo = new List<IClear>();
        }

        public ReadOnlyCollection<IClear> ClearAlgo => _clearAlgo.AsReadOnly();

        public void AddClearAlgoInHybrid(IClear clear)
        {
            _clearAlgo.Add(clear);
        }

        public bool IsLimitExceeded(BackupJob backupJob)
        {
            foreach (IClear algorithm in ClearAlgo)
            {
                if (!algorithm.IsLimitExceeded(backupJob)) return false;
            }

            return true;
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