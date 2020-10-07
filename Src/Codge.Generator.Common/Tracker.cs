using Common.Logging;
using System.Collections.Generic;

namespace Codge.Generator.Common
{
    public class Tracker
    {
        private IList<string> _filesUdated = new List<string>();
        public IEnumerable<string> FilesUpdated => _filesUdated;

        private IList<string> _filesSkipped = new List<string>();
        public IEnumerable<string> FilesSkipped => _filesSkipped;

        public ILog Logger { get; }

        public Tracker(ILog logger)
        {
            Logger = logger;
        }

        public void OnFileUpdated(string path)
        {
            _filesUdated.Add(path);
            Logger.Debug(m => m("Updating file [{0}]", path));
        }

        public void OnFileSkipped(string path)
        {
            _filesSkipped.Add(path);
            Logger.Debug(m => m("Skipping update [{0}]", path));
        }
    }
}
