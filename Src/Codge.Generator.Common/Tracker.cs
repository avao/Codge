using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace Codge.Generator.Common
{
    public class Tracker
    {
        private List<string> _filesUdated = new List<string>();
        public IReadOnlyCollection<string> FilesUpdated => _filesUdated;

        private List<string> _filesSkipped = new List<string>();
        public IReadOnlyCollection<string> FilesSkipped => _filesSkipped;

        private ILogger _logger;

        public Tracker(ILogger logger)
        {
            _logger = logger;
        }

        public void OnFileUpdated(string path)
        {
            _filesUdated.Add(path);
            _logger.LogDebug("Updating file [{path}]", path);
        }

        public void OnFileSkipped(string path)
        {
            _filesSkipped.Add(path);
            _logger.LogDebug("Skipping update [{path}]", path);
        }
    }
}
