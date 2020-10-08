using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace Codge.Generator.Common
{
    public class Tracker
    {
        private IList<string> _filesUdated = new List<string>();
        public IEnumerable<string> FilesUpdated => _filesUdated;

        private IList<string> _filesSkipped = new List<string>();
        public IEnumerable<string> FilesSkipped => _filesSkipped;

        public ILogger Logger { get; }

        public Tracker(ILogger logger)
        {
            Logger = logger;
        }

        public void OnFileUpdated(string path)
        {
            _filesUdated.Add(path);
            Logger.LogDebug("Updating file [{path}]", path);
        }

        public void OnFileSkipped(string path)
        {
            _filesSkipped.Add(path);
            Logger.LogDebug("Skipping update [{path}]", path);
        }
    }
}
