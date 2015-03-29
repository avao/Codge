using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BasicModel.Templates.CS.Templates;
using Codge.Generator;
using Codge.Generator.Tasks;
using Common.Logging;
using Codge.DataModel;

namespace BasicModel.Templates.CS
{
    public class TaskFactory : ITaskFactory
    {
        public ILog Logger { get; private set; }

        public TaskFactory(ILog logger)
        {
            Logger = logger;
        }

        public IEnumerable<ITask<Model>> CreateTasksForModel(Model model)
        {
            return new ITask<Model>[] { 
                new OutputTask<Model>(new ProjectUpdater(model), Logger),
                new OutputTask<Model>(new PofConfig(model), Logger)};
        }

        public IEnumerable<ITask<Namespace>> CreateTasksForNamespace(Namespace model)
        {
            return new ITask<Namespace>[] { };
        }

        public IEnumerable<ITask<TypeBase>> CreateTasksForType(TypeBase type)
        {
            return new[] {
                new OutputTask<TypeBase>(new BasicModel.Templates.CS.Templates.Types.Composite(type), Logger),
                new OutputTask<TypeBase>(new BasicModel.Templates.CS.Templates.Types.Primitive(type), Logger),
                new OutputTask<TypeBase>(new BasicModel.Templates.CS.Templates.Types.Enum(type), Logger),
                new OutputTask<TypeBase>(new BasicModel.Templates.CS.Templates.PofSerialisers.Composite(type), Logger)
            };
        }
    }
}
