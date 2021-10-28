using System;
using System.Collections.Generic;
using Backups.Models;
using Backups.Services;
using NUnit.Framework;

namespace Backups.Tests
{
    public class BackupsTest
    {
        [Test]

        public void Test1()
        {
            var file1 = new File("\\path\\file1", 45);
            var file2 = new File("\\path\\file2", 46);

            var jobObjects = new JobObjects();
            jobObjects.AddObjects(file1);
            jobObjects.AddObjects(file2);

            var backup = new BackupJob(jobObjects, "\\path\\");
            backup.StartBackup("SingleStorage");
            Assert.True(backup.RestorePoints[0].AccessRestorePoint.Count == 1 &&
                         backup.RestorePoints[0].AccessRestorePoint[0].Storages.Count == 2);
        }

        [Test]
        public void Test2()
        {
            var file1 = new File("\\path\\file1", 45);
            var file2 = new File("\\path\\file2", 46);
            
            var jobObjects = new JobObjects();
            jobObjects.AddObjects(file1);
            jobObjects.AddObjects(file2);
            var backup = new BackupJob(jobObjects, "\\path\\");
            backup.StartBackup("SplitStorage");
            
            jobObjects.RemoveObjects(file2);
            backup.StartBackup("SplitStorage");
            
            Assert.True(backup.RestorePoints.Count == 2 && backup.RestorePoints[1].AccessRestorePoint.Count == 3);
        }
    }
}