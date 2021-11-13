using System.Collections.Generic;
using System.Linq;
using Backups.Services;
using Backups.Tools;

namespace Backups.Models
{
    public class SplitAlgo : IAlgorithmic
    {
        public List<Archive> StartAlgorithmic(JobObjects jobObjects, Repository repository)
        {
            if (jobObjects == null) throw new BackupsException("Invalid jobObjects");
            repository.Counter++;
            string pathOfArchive = $"{repository.PathOfRepository}\\Archive_{repository.Counter}";
            foreach (File file in jobObjects.Objects)
            {
                var archive = new Archive(pathOfArchive, jobObjects.SizeFiles);
                var storage = new Storage($"{pathOfArchive}\\{file.Name}_{repository.Counter}", file.SizeFile);
                archive.AddStorage(storage);
                repository.AddArchive(archive);
            }

            return repository.Archives.ToList();
        }
    }
}