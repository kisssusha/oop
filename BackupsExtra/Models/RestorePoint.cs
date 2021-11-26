using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using BackupsExtra.Tools;

namespace BackupsExtra.Models
{
    public class RestorePoint
    {
        private List<Storage> _storages;
        public RestorePoint(List<Storage> archives)
        {
            Time = DateTime.Now;
            if (archives == null) throw new BackupsExtraException("Invalid archives");
            _storages = archives;
        }

        public DateTime Time { get; }
        public long Size
        {
            get
            {
                long res = 0;
                foreach (var archives in AccessRestorePoint)
                {
                    res += archives.Size;
                }

                return res;
            }
        }

        public ReadOnlyCollection<Storage> AccessRestorePoint => _storages.AsReadOnly();

        public void AddArchive(Storage archive)
        {
            if (archive == null) throw new BackupsExtraException("Invalid archive");
            _storages.Add(archive);
        }

        public List<File> UnpackRestorePointOfStorage(string path)
        {
            var listFiles = new List<File>();
            foreach (Storage storage in _storages)
            {
                listFiles.AddRange(storage.Unpack(path));
            }

            return listFiles;
        }
    }
}