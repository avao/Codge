using Codge.Generator.Common;
using System.IO;

namespace Codge.Generator
{
    public class GeneratorConfig
    {
        public string BaseDir { get; }
        public ITaskFactory TaskFactory { get; }

        public GeneratorConfig(string baseDir, ITaskFactory taskFactory)
        {
            BaseDir = Path.GetFullPath(baseDir);
            TaskFactory = taskFactory;
        }
    }
}
