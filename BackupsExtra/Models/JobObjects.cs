using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using BackupsExtra.Tools;

namespace BackupsExtra.Models
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
            if (jobObject == null) throw new BackupsExtraException("Invalid jobObject");
            _objects.Add(jobObject);
        }

        public void RemoveObjects(File jobObject)
        {
            if (jobObject == null) throw new BackupsExtraException("Invalid jobObject");
            if (_objects.Any(jb => jb.PathOfFile == jobObject.PathOfFile)) _objects.Remove(jobObject);
        }

        public void RecoveryFile(List<File> files)
        {
            foreach (File file in files.Where(file => !_objects.Contains(file)))
            {
                _objects.Add(file);
            }
        }
    }
}