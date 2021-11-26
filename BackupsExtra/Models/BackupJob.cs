using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using BackupsExtra.Models.ClearAlgo;
using BackupsExtra.Services;
using BackupsExtra.Tools;

namespace BackupsExtra.Models
{
    public class BackupJob
    {
        private List<RestorePoint> _restorePoints;
        private Repository _repository;
        private JobObjects _jobObjects;
        private ILogger _logger;

        public BackupJob(string path, string typoOfLoggers, bool turnPrefixForLoggers)
        {
            _logger = typoOfLoggers switch
            {
                "ToFile" => new FileLog(turnPrefixForLoggers),
                "ToConsole" => new ConsoleLog(turnPrefixForLoggers),
                _ => null
            };
            _restorePoints = new List<RestorePoint>();
            WayOfBackup = path ?? throw new BackupsExtraException("Invalid way");
            _jobObjects = new JobObjects();
            _repository = new Repository(path);
            _logger?.Log($"Create repository in {path}");
        }

        public ReadOnlyCollection<RestorePoint> RestorePoints => _restorePoints.AsReadOnly();

        public string WayOfBackup { get; }
        public long Size
        {
            get
            {
                long res = 0;
                foreach (RestorePoint restorePoint in RestorePoints)
                {
                    res += restorePoint.Size;
                }

                return res;
            }
        }

        public void AddBackupJobObjects(File file)
        {
            _jobObjects.AddObjects(file);
            _logger?.Log($"Add file {file} in backup");
        }

        public RestorePoint StartBackup(string algorithmName)
        {
            IAlgorithmic algo = algorithmName switch
            {
                "SingleStorage" => new SingleAlgo(),
                "SplitStorage" => new SplitAlgo(),
                _ => throw new BackupsExtraException("Unsupported algorithmName", new ArgumentOutOfRangeException(nameof(algorithmName)))
            };
            _logger?.Log($"Selected {algorithmName} algorithm");

            var restorePoint = new RestorePoint(algo.StartAlgorithmic(_jobObjects, _repository));
            _restorePoints.Add(restorePoint);
            restorePoint.AddAlgo(algorithmName);
            _logger?.Log($"Add {restorePoint} in backup");

            return restorePoint;
        }

       /* public void CreationClear(string clearAlgorithmName)
        {
            IClear algo = clearAlgorithmName switch
            {
                "CountLimitClear" => new CountLimitClear(),
                "DateLimitClear" => new DateLimitClear(DateTime.Now),
                "HybridClear" => new HybridClear(),
                "HybridOneLimitClear" => new HybridOneLimitClear(),
                _ => throw new BackupsExtraException("Unsupported clearAlgorithmName", new ArgumentOutOfRangeException(nameof(clearAlgorithmName)))
            };
            algo.Clear(this);
        }*/

        public void RemoveRestorePoint(RestorePoint restorePoint)
        {
            _restorePoints.Remove(restorePoint);
            _logger?.Log($"Remove {restorePoint} from backup");
        }

        public void Recovery(RestorePoint restorePoint, string option, string path)
        {
            switch (option)
            {
                case "to original location":
                    _jobObjects.RecoveryFile(restorePoint.UnpackRestorePointOfStorage(WayOfBackup));
                    _logger?.Log($"Unpack Restore Point Of Storage {WayOfBackup} ");
                    break;
                case "to different location":
                    _jobObjects.RecoveryFile(restorePoint.UnpackRestorePointOfStorage(path));
                    _logger?.Log($"Unpack Restore Point Of Storage {path} ");
                    break;
            }

            _logger?.Log($"Selected {option} recovery");
        }

        public void Merge(RestorePoint restorePoint1, RestorePoint restorePoint2, string option)
        {
            switch (option)
            {
                case "option1":
                    if (restorePoint2.AccessRestorePoint.Count != 0)
                    {
                        for (int i = 0; i < restorePoint1.AccessRestorePoint.Count; i++)
                        {
                            restorePoint1.RemoveStorage(restorePoint1.AccessRestorePoint[i]);
                            i--;
                        }
                    }

                    break;
                case "option2":
                    if (restorePoint2.AccessRestorePoint.Count == 0)
                    {
                        foreach (var t in restorePoint1.AccessRestorePoint)
                        {
                            restorePoint2.AddStorage(t);
                        }
                    }

                    break;
                case "option3":
                    if (restorePoint2.Algo == "SingleStorage")
                        RemoveRestorePoint(restorePoint1);
                    break;
            }
        }
    }
}