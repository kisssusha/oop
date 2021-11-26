using System;
using System.Collections.Generic;
using System.Linq;
using BackupsExtra.Services;
using BackupsExtra.Tools;

namespace BackupsExtra.Models
{
    [Serializable]
    public class SingleAlgo : IAlgorithmic
    {
        public List<Storage> StartAlgorithmic(JobObjects jobObjects, Repository repository)
        {
            if (jobObjects == null) throw new BackupsExtraException("Invalid jobObjects");
            if (repository == null) throw new BackupsExtraException("Invalid repository");
            repository.Counter++;
            string pathOfStorage = $"{repository.PathOfRepository}\\Archive_{repository.Counter}";
            var storage = new Storage(pathOfStorage, jobObjects.SizeFiles);
            foreach (File file in jobObjects.Objects)
            {
                storage.AddFile(file);
            }

            var listOfStorage = new List<Storage> { storage };
            repository.AddStorage(storage);

            return listOfStorage;
        }
    }
}