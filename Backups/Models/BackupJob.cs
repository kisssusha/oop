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

        public void StartBackup(string option)
        {
            switch (option)
            {
                case "SingleStorage":
                    IAlgorithmic algo1 = new SingleAlgo();
                    var restorePoint1 = new RestorePoint(algo1.StartAlgorithmic(JobObjectBackup, _repository));
                    _restorePoints.Add(restorePoint1);
                    break;

                case "SplitStorage":
                    IAlgorithmic algo2 = new SplitAlgo();
                    var restorePoint2 = new RestorePoint(algo2.StartAlgorithmic(JobObjectBackup, _repository));
                    _restorePoints.Add(restorePoint2);
                    break;
                default:
                    throw new BackupsException("Unsupported option", new ArgumentOutOfRangeException(nameof(option)));
            }
        }
    }
}