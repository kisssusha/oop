using System.Collections.Generic;
using System.Collections.ObjectModel;
using Backups.Models;
using Backups.Tools;

namespace Backups.Services
{
    public class Archive
    {
        private List<Storage> _storages;

        public Archive(string pathOfArchive, long size)
        {
            _storages = new List<Storage>();
            if (pathOfArchive == null) throw new BackupsException("Invalid wayOfArchive");
            PathOfArchive = pathOfArchive;
            Size = size;
        }

        public string PathOfArchive { get; }
        public long Size { get; set; }

        public ReadOnlyCollection<Storage> Storages => _storages.AsReadOnly();

        public void AddStorage(Storage storage)
        {
            if (storage == null) throw new BackupsException("Invalid storage");
            _storages.Add(storage);
        }
    }
}