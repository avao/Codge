using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;

namespace Codge.Generator
{
    public class Context
    {
        public Context(string baseDir, ILog logger)
        {
            BaseDir = baseDir;
            Tracker = new Tracker(logger);
        }
        
        public string BaseDir { get; private set; }

        public Tracker Tracker { get; private set; }
    }

    public static class ContextExtensions
    {
        public static string GetAbsolutePath(this Context context, string relativePath)
        {
            return Path.Combine(context.BaseDir, relativePath);
        }
    }
}
