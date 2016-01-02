using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using Codge.Generator.Common;

namespace Codge.Generator.Common
{
    public class Tracker
    {
        private IList<string> _filesUdated = new List<string>();
        public IEnumerable<string> FilesUpdated { get { return _filesUdated; } }

        private IList<string> _filesSkipped = new List<string>();
        public IEnumerable<string> FilesSkipped { get { return _filesSkipped; } }

        public ILog Logger { get; private set; }

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
