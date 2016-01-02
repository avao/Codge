using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Codge.BasicModel.Templates.CS.Templates;
using Codge.Generator;
using Common.Logging;
using Codge.DataModel;
using Codge.Generator.Common;
using Codge.Models.Common;

namespace Codge.BasicModel.Templates.CS
{
    public class TaskFactory : ITaskFactory
    {
        public ILog Logger { get; private set; }
        private ModelBehaviour _modelBehaviour;

        static HashSet<string> _reservedNames = new HashSet<string> { "string", "String", "byte", "Byte", "decimal", "Decimal", "double", "Double", "float", "Float", "int", "Int", "long", "Long", "short", "Short" };

        public TaskFactory(ILog logger)
        {
            Logger = logger;
            _modelBehaviour = new ModelBehaviour(_reservedNames);
        }

        public IEnumerable<ITask<Model>> CreateTasksForModel(Model model)
        {
            return new ITask<Model>[] { 
                new OutputTask<Model>(new Registrar(model, _modelBehaviour), Logger),
                new OutputTask<Model>(new ProjectUpdater(model), Logger)//Should be last to capture all files
            };
        }

        public IEnumerable<ITask<Namespace>> CreateTasksForNamespace(Namespace model)
        {
            return new ITask<Namespace>[] { };
        }

        public IEnumerable<ITask<TypeBase>> CreateTasksForType(TypeBase type)
        {
            return new[] {
                new OutputTask<TypeBase>(new Codge.BasicModel.Templates.CS.Templates.Types.Composite(type, _modelBehaviour), Logger),
                new OutputTask<TypeBase>(new Codge.BasicModel.Templates.CS.Templates.Types.Primitive(type, _modelBehaviour), Logger),
                new OutputTask<TypeBase>(new Codge.BasicModel.Templates.CS.Templates.Types.Enum(type, _modelBehaviour), Logger),
                new OutputTask<TypeBase>(new Codge.BasicModel.Templates.CS.Templates.XmlSerialisers.Composite(type, _modelBehaviour), Logger)
                //new OutputTask<TypeBase>(new BasicModel.Templates.CS.Templates.PofSerialisers.Composite(type), Logger)
            };
        }
    }
}
