using System.Collections.Generic;
using System.Collections.ObjectModel;
using BackupsExtra.Tools;

namespace BackupsExtra.Models
{
    public class Storage
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
        public long Size { get; set; }

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
    }
}