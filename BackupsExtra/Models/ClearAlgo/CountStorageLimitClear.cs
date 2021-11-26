using System;
using BackupsExtra.Services;

namespace BackupsExtra.Models.ClearAlgo
{
    [Serializable]
    public class CountStorageLimitClear : IClear
    {
        private int _limitValue;

        public CountStorageLimitClear(RestorePoint restorePoint)
        {
            RestorePointInClear = restorePoint;
        }

        public RestorePoint RestorePointInClear { get; }

        public void AddLimit(int amount)
            {
                _limitValue = amount;
            }

        public bool IsLimitExceeded()
            {
                return _limitValue < RestorePointInClear.AccessRestorePoint.Count;
            }

        public void Clear()
            {
                for (int i = 0; i < RestorePointInClear.AccessRestorePoint.Count; i++)
                {
                    if (IsLimitExceeded())
                    {
                        RestorePointInClear.RemoveStorage(RestorePointInClear.AccessRestorePoint[i]);
                        i--;
                    }
                }
            }
    }
}