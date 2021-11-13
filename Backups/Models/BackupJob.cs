using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Backups.Services;
using Backups.Tools;

namespace Backups.Models
{
    public class BackupJob
    {
        private List<RestorePoint> _restorePoints;
        private Repository _repository;

        public BackupJob(JobObjects jobObjectBackup, string path)
        {
            if (jobObjectBackup == null) throw new BackupsException("Invalid jobObjectBackup");
            JobObjectBackup = jobObjectBackup;
            _restorePoints = new List<RestorePoint>();
            if (path == null) throw new BackupsException("Invalid way");
            WayOfBackupckup = path;
            _repository = new Repository(path);
        }

        public ReadOnlyCollection<RestorePoint> RestorePoints => _restorePoints.AsReadOnly();

        public JobObjects JobObjectBackup { get; }
        public string WayOfBackupckup { get; }

        public void StartBackup(string algorithmName)
        {
            IAlgorithmic algo = algorithmName switch
            {
                "SingleStorage" => new SingleAlgo(),
                "SplitStorage" => new SplitAlgo(),
                _ => throw new BackupsException("Unsupported algorithmName", new ArgumentOutOfRangeException(nameof(algorithmName)))
            };

            var restorePoint2 = new RestorePoint(algo.StartAlgorithmic(JobObjectBackup, _repository));
            _restorePoints.Add(restorePoint2);
        }
    }
}