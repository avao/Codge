using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Codge.DataModel;
using Codge.Generator.Common;

namespace Codge.Generator
{
    public class GeneratorConfig
    {
        public string BaseDir { get; private set; }
        public ITaskFactory TaskFactory{ get; private set; }

        public GeneratorConfig(string baseDir, ITaskFactory taskFactory)
        {
            BaseDir = Path.GetFullPath(baseDir);
            TaskFactory = taskFactory;
        }
    }
}
