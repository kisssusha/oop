using System;
using System.Linq;
using BackupsExtra.Tools;

namespace BackupsExtra.Models
{
    [Serializable]
    public class File : IEquatable<File>
    {
        public File(string pathOfFile, long sizeFile)
        {
            if (pathOfFile == null) throw new BackupsExtraException("Invalid wayOfFile");
            PathOfFile = pathOfFile;
            if (sizeFile < 0) throw new BackupsExtraException("Invalid sizeFile");
            SizeFile = sizeFile;
            Name = PathOfFile.Split("\\").Last();
        }

        public string PathOfFile { get; private set; }
        public long SizeFile { get; }
        public string Name { get; }

        public File ChangePath(string path)
        {
            PathOfFile = path;
            return this;
        }

        public bool Equals(File other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return PathOfFile == other.PathOfFile && Name == other.Name;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == this.GetType() && Equals((File)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(PathOfFile, Name);
        }
    }
}