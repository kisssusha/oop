using System;
using System.Collections.Generic;
using System.Linq;
using BackupsExtra.Services;
using BackupsExtra.Tools;

namespace BackupsExtra.Models
{
    [Serializable]
    public class SplitAlgo : IAlgorithmic
    {
        public List<Storage> StartAlgorithmic(JobObjects jobObjects, Repository repository)
        {
            if (jobObjects == null) throw new BackupsExtraException("Invalid jobObjects");
            if (repository == null) throw new BackupsExtraException("Invalid repository");
            repository.Counter++;
            var listOfStorage = new List<Storage>();
            foreach (File file in jobObjects.Objects)
            {
                string pathOfStorage = $"{repository.PathOfRepository}\\{file.Name}_{repository.Counter}";
                var storage = new Storage(pathOfStorage, file.SizeFile);
                storage.AddFile(file);
                repository.AddStorage(storage);
                listOfStorage.Add(storage);
            }

            return listOfStorage;
        }
    }
}