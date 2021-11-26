using BackupsExtra.Models;
using BackupsExtra.Models.ClearAlgo;
using NUnit.Framework;

namespace BackupsExtra.Tests
{
    public class BackupsExtraTest
    {
        [Test]

        public void ClearAlgoTest()
        {
            var file1 = new File("\\path\\file1", 45);
            var file2 = new File("\\path\\file2", 46);
            var file3 = new File("\\path\\file3", 46);
            var file4= new File("\\path\\file4", 46);

            var backup = new BackupJob("\\path\\BackupFolder\\", "ToConsole", true);
            backup.AddBackupJobObjects(file1);
            backup.AddBackupJobObjects(file2);
            backup.AddBackupJobObjects(file3);
            backup.AddBackupJobObjects(file4);
            RestorePoint restorePoint1 = backup.StartBackup("SingleStorage");
            RestorePoint restorePoint2 = backup.StartBackup("SplitStorage");
            RestorePoint restorePoint3 = backup.StartBackup("SplitStorage");
            RestorePoint restorePoint4 = backup.StartBackup("SplitStorage");
            var countClear = new CountLimitClear(backup);
            countClear.AddLimit(2);
            countClear.Clear();
            Assert.AreEqual(backup.RestorePoints.Count , 2);
        }
        
        [Test]

        public void MergeAlgoOption3Test()
        {
            var file1 = new File("\\path\\file1", 45);
            var file2 = new File("\\path\\file2", 46);
            
            var backup = new BackupJob("\\path\\BackupFolder\\", "ToConsole", true);
            backup.AddBackupJobObjects(file1);
            backup.AddBackupJobObjects(file2);
            RestorePoint restorePoint1 = backup.StartBackup("SplitStorage");
            RestorePoint restorePoint2 = backup.StartBackup("SingleStorage");
            backup.Merge(restorePoint1, restorePoint2, "option3");
            
            Assert.AreEqual(backup.RestorePoints.Count , 1);
        }
        [Test]

        public void MergeAlgoOption1Test()
        {
            var file1 = new File("\\path\\file1", 45);
            var file2 = new File("\\path\\file2", 46);
            
            var backup = new BackupJob("\\path\\BackupFolder\\", "ToConsole", true);
            backup.AddBackupJobObjects(file1);
            backup.AddBackupJobObjects(file2);
            RestorePoint restorePoint1 = backup.StartBackup("SplitStorage");
            
            var file3 = new File("\\path\\file2", 46);
            
            backup.AddBackupJobObjects(file3);
            RestorePoint restorePoint2 = backup.StartBackup("SplitStorage");
            
            backup.Merge(restorePoint1, restorePoint2, "option1");
            Assert.AreEqual(restorePoint1.AccessRestorePoint.Count, 0);
            Assert.AreEqual(restorePoint2.AccessRestorePoint.Count, 3);
        }
        [Test]

        public void MergeAlgoOption2Test()
        {
            var file1 = new File("\\path\\file1", 45);
            var file2 = new File("\\path\\file2", 46);
            
            var backup = new BackupJob("\\path\\BackupFolder\\", "ToConsole", true);
            
            RestorePoint restorePoint2 = backup.StartBackup("SplitStorage");
            backup.AddBackupJobObjects(file1);
            backup.AddBackupJobObjects(file2);
            RestorePoint restorePoint1 = backup.StartBackup("SplitStorage");

            backup.Merge(restorePoint1, restorePoint2, "option2");
            
            Assert.AreEqual(restorePoint1.AccessRestorePoint.Count, 2);
            Assert.AreEqual(restorePoint2.AccessRestorePoint.Count, 2);
        }
        
    }
}