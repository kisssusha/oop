using System.Collections.Generic;
using IsuExtra.Tools;

namespace IsuExtra.Models
{
    public class ExtraLesson
    {
        private List<IsuExtraStream> _isuExtraStreams;

        public ExtraLesson(string nameExtraLesson)
        {
            _isuExtraStreams = new List<IsuExtraStream>();
            NameExtraLesson = nameExtraLesson;
        }

        public string NameExtraLesson { get; }

        public IEnumerable<IsuExtraStream> IsuExtraStreams => _isuExtraStreams;

        public IsuExtraStream AddExtraStream(IsuExtraStream isuExtraStream)
        {
            _isuExtraStreams.Add(isuExtraStream);
            return isuExtraStream;
        }
    }
}
