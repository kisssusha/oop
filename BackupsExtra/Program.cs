using BackupsExtra.Models;
using BackupsExtra.Models.ClearAlgo;

namespace BackupsExtra
{
    internal class Program
    {
        private static void Main()
        {
            var file1 = new File("\\path\\file1", 45);
            var file2 = new File("\\path\\file2", 46);
            var file3 = new File("\\path\\file3", 46);
            var file4 = new File("\\path\\file4", 46);

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
        }
    }
}
