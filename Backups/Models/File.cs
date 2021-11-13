using System;
using System.Linq;
using Backups.Tools;

namespace Backups.Models
{
    public class File
    {
        public File(string pathOfFile, long sizeFile)
        {
            if (pathOfFile == null) throw new BackupsException("Invalid wayOfFile");
            PathOfFile = pathOfFile;
            if (sizeFile < 0) throw new BackupsException("Invalid sizeFile");
            SizeFile = sizeFile;
            Name = PathOfFile.Split("\\").Last();
        }

        public string PathOfFile { get; }
        public long SizeFile { get; }
        public string Name { get; }
    }
}