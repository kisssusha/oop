using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using IsuExtra.Tools;

namespace IsuExtra.Models
{
    public class ExtraLesson
    {
        private List<IsuExtraStream> _isuExtraStreams;

        public ExtraLesson(string nameExtraLesson)
        {
            _isuExtraStreams = new List<IsuExtraStream>();
            NameExtraLesson = nameExtraLesson ?? throw new ArgumentNullException(nameof(nameExtraLesson));
        }

        public string NameExtraLesson { get; }

        public ReadOnlyCollection<IsuExtraStream> IsuExtraStreams => _isuExtraStreams.AsReadOnly();

        public IsuExtraStream AddExtraStream(IsuExtraStream isuExtraStream)
        {
            if (isuExtraStream == null) throw new ArgumentNullException(nameof(isuExtraStream));
            if (_isuExtraStreams.Any(n => n.Name == isuExtraStream.Name))
                throw new IsuExtraException("IsuExtraStream already in use");
            _isuExtraStreams.Add(isuExtraStream);
            return isuExtraStream;
        }
    }
}
