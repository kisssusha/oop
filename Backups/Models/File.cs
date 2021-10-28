using System;
using Backups.Tools;

namespace Backups.Models
{
    public class File
    {
        public File(string wayOfFile, long sizeFile)
        {
            if (wayOfFile == null) throw new BackupsException("Invalid wayOfFile");
            WayOfFile = wayOfFile;
            if (sizeFile < 0) throw new BackupsException("Invalid sizeFile");
            SizeFile = sizeFile;
            Name = WayOfFile.Split("\\")[WayOfFile.Split("\\").Length - 1];
        }

        public string WayOfFile { get; }
        public long SizeFile { get; }
        public string Name { get; }
    }
}