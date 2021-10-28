using System;
using Backups.Tools;

namespace Backups.Models
{
    public class Storage
    {
        public Storage(string wayOfFile, long size)
        {
            if (wayOfFile == null) throw new BackupsException("Invalid wayOfFile");
            WayOfFile = wayOfFile;
            if (size < 0) throw new BackupsException("Invalid size");
            Size = size;
            FileTime = DateTime.Now;
        }

        public string WayOfFile { get; }
        public long Size { get; }
        public DateTime FileTime { get; }
    }
}