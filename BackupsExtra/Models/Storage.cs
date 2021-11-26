using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using BackupsExtra.Tools;

namespace BackupsExtra.Models
{
    [Serializable]
    public class Storage : IEquatable<Storage>
    {
    private List<File> _files;

    public Storage(string pathOfArchive, long size)
    {
        _files = new List<File>();
        if (pathOfArchive == null) throw new BackupsExtraException("Invalid wayOfArchive");
        PathOfArchive = pathOfArchive;
        Size = size;
    }

    public string PathOfArchive { get; }
    public long Size { get; }

    public ReadOnlyCollection<File> Files => _files.AsReadOnly();

    public void AddFile(File file)
    {
        if (file == null) throw new BackupsExtraException("Invalid storage");
        _files.Add(file);
    }

    public List<File> Unpack(string path)
    {
        var listFile = new List<File>();
        foreach (File file in _files)
        {
            listFile.Add(file.ChangePath(path));
        }

        return listFile;
    }

    public bool Equals(Storage other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Equals(_files, other._files) && PathOfArchive == other.PathOfArchive && Size == other.Size;
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Storage)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_files, PathOfArchive, Size);
    }
    }
}