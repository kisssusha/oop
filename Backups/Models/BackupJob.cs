using System.Collections.Generic;
using System.Collections.ObjectModel;
using Backups.Tools;

namespace Backups.Models
{
    public class BackupJob
    {
        private List<RestorePoint> _restorePoints;
        private Repository _repository;

        public BackupJob(JobObjects jobObjectBackup, string way)
        {
            if (jobObjectBackup == null) throw new BackupsException("Invalid jobObjectBackup");
            JobObjectBackup = jobObjectBackup;
            _restorePoints = new List<RestorePoint>();
            if (way == null) throw new BackupsException("Invalid way");
            WayOfBackup = way;
            _repository = new Repository(way);
        }

        public ReadOnlyCollection<RestorePoint> RestorePoints => _restorePoints.AsReadOnly();

        public JobObjects JobObjectBackup { get; }
        public string WayOfBackup { get; }

        public void StartBackup(string option)
        {
            switch (option)
            {
                case "SingleStorage":
                    var restorePoint1 = new RestorePoint(_repository.CopyFileFromJobToRepoSingleStorage(JobObjectBackup));
                    _restorePoints.Add(restorePoint1);
                    break;

                case "SplitStorage":
                    var restorePoint2 = new RestorePoint(_repository.CopyFileFromJobToRepoSplitStorage(JobObjectBackup));
                    _restorePoints.Add(restorePoint2);
                    break;
            }
        }
    }
}