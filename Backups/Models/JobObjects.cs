using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Backups.Tools;

namespace Backups.Models
{
    public class JobObjects
    {
        private List<File> _objects;

        public JobObjects()
        {
            _objects = new List<File>();
        }

        public ReadOnlyCollection<File> Objects => _objects.AsReadOnly();
        public long SizeFiles
        {
            get
            {
                return Objects.Sum(file => file.SizeFile);
            }
        }

        public void AddObjects(File jobObject)
        {
            if (jobObject == null) throw new BackupsException("Invalid jobObject");
            if (_objects.Contains(jobObject)) throw new BackupsException("File already exist");
            _objects.Add(jobObject);
        }

        public void RemoveObjects(File jobObject)
        {
            if (jobObject == null) throw new BackupsException("Invalid jobObject");
            if (_objects.Any(jb => jb.WayOfFile == jobObject.WayOfFile)) _objects.Remove(jobObject);
        }
    }
}