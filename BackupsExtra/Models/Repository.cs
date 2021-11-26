using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using BackupsExtra.Services;
using BackupsExtra.Tools;

namespace BackupsExtra.Models
{
    [Serializable]
    public class Repository
    {
        private List<Storage> _archivesInRepository;

        public Repository(string path)
        {
            if (path == null) throw new BackupsExtraException("Invalid way");
            PathOfRepository = path;
            _archivesInRepository = new List<Storage>();
        }

        public ReadOnlyCollection<Storage> Archives => _archivesInRepository.AsReadOnly();
        public string PathOfRepository { get; }

        public int Counter { get; set; }

        public void AddStorage(Storage storage)
        {
            if (storage == null) throw new BackupsExtraException("Invalid archive");
            _archivesInRepository.Add(storage);
        }
    }
}