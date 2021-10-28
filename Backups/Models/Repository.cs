using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Backups.Services;
using Backups.Tools;

namespace Backups.Models
{
    public class Repository
    {
        private static int _counter;
        private List<Archive> _archivesInRepository;

        public Repository(string way)
        {
            if (way == null) throw new BackupsException("Invalid way");
            WayOfRepository = way;
            _archivesInRepository = new List<Archive>();
        }

        public ReadOnlyCollection<Archive> Archives => _archivesInRepository.AsReadOnly();
        public string WayOfRepository { get; }

        public void AddStorages(List<Archive> archives)
        {
            _archivesInRepository = archives ?? throw new BackupsException("Invalid archives");
        }

        public List<Archive> CopyFileFromJobToRepoSingleStorage(JobObjects jobObjects)
        {
            if (jobObjects == null) throw new BackupsException("Invalid jobObjects");
            _counter++;
            string wayOfArchive = $"{WayOfRepository}\\Archive_{_counter}";
            var archive = new Archive(wayOfArchive, jobObjects.SizeFiles);
            foreach (File file in jobObjects.Objects)
            {
                var storage = new Storage($"{wayOfArchive}\\{file.Name}_{_counter}", file.SizeFile);
                archive.AddStorage(storage);
            }

            _archivesInRepository.Add(archive);

            return Archives.ToList();
        }

        public List<Archive> CopyFileFromJobToRepoSplitStorage(JobObjects jobObjects)
        {
            if (jobObjects == null) throw new BackupsException("Invalid jobObjects");
            _counter++;
            string wayOfArchive = $"{WayOfRepository}\\Archive_{_counter}";
            foreach (File file in jobObjects.Objects)
            {
                var archive = new Archive(wayOfArchive, jobObjects.SizeFiles);
                var storage = new Storage($"{wayOfArchive}\\{file.Name}_{_counter}", file.SizeFile);
                archive.AddStorage(storage);
                _archivesInRepository.Add(archive);
            }

            return Archives.ToList();
        }
    }
}