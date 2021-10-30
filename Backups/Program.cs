using System;
using Backups.Models;
using Backups.Services;

namespace Backups
{
    internal class Program
    {
        private static void Main()
        {
            var file1 = new File("\\path\\file1", 45);
            var file2 = new File("\\path\\file2", 46);

            var jobObjects = new JobObjects();
            jobObjects.AddObjects(file1);
            jobObjects.AddObjects(file2);

            var backup = new BackupJob(jobObjects, "\\path\\");
            backup.StartBackup("SingleStorage");
        }
    }
}
