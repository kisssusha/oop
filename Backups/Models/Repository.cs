using System.Collections.Generic;
using System.Collections.ObjectModel;
using Backups.Services;
using Backups.Tools;

namespace Backups.Models
{
    public class Repository
    {
        private List<Archive> _archivesInRepository;

        public Repository(string path)
        {
            if (path == null) throw new BackupsException("Invalid way");
            PathOfRepository = path;
            _archivesInRepository = new List<Archive>();
        }

        public ReadOnlyCollection<Archive> Archives => _archivesInRepository.AsReadOnly();
        public string PathOfRepository { get; }

        public int Counter { get; set; }

        public void AddArchive(Archive archive)
        {
            if (archive == null) throw new BackupsException("Invalid archive");
            _archivesInRepository.Add(archive);
        }
    }
}