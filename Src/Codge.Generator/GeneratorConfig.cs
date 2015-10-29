using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Codge.DataModel;
using Codge.Generator.StringTemplateTasks;

namespace Codge.Generator
{
    public class GeneratorConfig
    {
        public string BaseDir { get; private set; }
        public ITaskFactory TaskFactory{ get; private set; }
        public IModelBehaviour ModelBehaviour { get; private set; }

        public GeneratorConfig(string baseDir, ITaskFactory taskFactory, IModelBehaviour modelBehaviour)
        {
            BaseDir = Path.GetFullPath(baseDir);
            TaskFactory = taskFactory;
            ModelBehaviour = modelBehaviour;
        }

    }
}
