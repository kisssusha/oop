using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.InteropServices.WindowsRuntime;
using Backups.Services;
using Backups.Tools;

namespace Backups.Models
{
    public class RestorePoint
    {
        private List<Archive> _archives;
        public RestorePoint(List<Archive> archives)
        {
            Time = DateTime.Now;
            if (archives == null) throw new BackupsException("Invalid archives");
            _archives = archives;
        }

        public DateTime Time { get; }

        public ReadOnlyCollection<Archive> AccessRestorePoint => _archives.AsReadOnly();

        public void AddArchive(Archive archive)
        {
            if (archive == null) throw new BackupsException("Invalid archive");
            _archives.Add(archive);
        }
    }
}