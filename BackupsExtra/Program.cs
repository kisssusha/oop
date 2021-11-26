using BackupsExtra.Models;

namespace BackupsExtra
{
    internal class Program
    {
        private static void Main()
        {
            var file1 = new File("\\path\\file1", 45);
            var file2 = new File("\\path\\file2", 46);

            var backup = new BackupJob("\\path\\BackupFolder\\", "ToConsole", true);
            backup.AddBackupJobObjects(file1);
            backup.AddBackupJobObjects(file2);
            RestorePoint restorePoint1 = backup.StartBackup("SingleStorage");
            RestorePoint restorePoint2 = backup.StartBackup("SplitStorage");
            backup.Recovery(restorePoint1, "to original location", null);
        }
    }
}
