using System;
using Backups.Tools;

namespace Backups.Models
{
    public class Storage
    {
        public Storage(string pathOfFile, long size)
        {
            if (pathOfFile == null) throw new BackupsException("Invalid wayOfFile");
            PathOfFile = pathOfFile;
            if (size < 0) throw new BackupsException("Invalid size");
            Size = size;
            FileTime = DateTime.Now;
        }

        public string PathOfFile { get; }
        public long Size { get; }
        public DateTime FileTime { get; }
    }
}