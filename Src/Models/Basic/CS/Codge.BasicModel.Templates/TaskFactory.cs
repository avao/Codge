using Codge.BasicModel.Templates.CS.Templates;
using Codge.DataModel;
using Codge.Generator.Common;
using Codge.Models.Common;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace Codge.BasicModel.Templates.CS
{
    public class TaskFactory : ITaskFactory
    {
        public ILogger Logger { get; }
        private ModelBehaviour _modelBehaviour;

        static HashSet<string> _reservedNames = new HashSet<string> { "string", "String", "byte", "Byte", "decimal", "Decimal", "double", "Double", "float", "Float", "int", "Int", "long", "Long", "short", "Short" };

        public TaskFactory(ILogger logger)
        {
            Logger = logger;
            _modelBehaviour = new ModelBehaviour(_reservedNames);
        }

        public IEnumerable<ITask<Model>> CreateTasksForModel(Model model)
        {
            return new ITask<Model>[] {
                new OutputTask<Model>(new Registrar(model, _modelBehaviour), Logger)
            };
        }

        public IEnumerable<ITask<Namespace>> CreateTasksForNamespace(Namespace model)
        {
            return new ITask<Namespace>[] { };
        }

        public IEnumerable<ITask<TypeBase>> CreateTasksForType(TypeBase type)
        {
            return new[] {
                new OutputTask<TypeBase>(new Templates.Types.Composite(type, _modelBehaviour), Logger),
                new OutputTask<TypeBase>(new Templates.Types.Primitive(type, _modelBehaviour), Logger),
                new OutputTask<TypeBase>(new Templates.Types.Enum(type, _modelBehaviour), Logger),
                new OutputTask<TypeBase>(new Templates.XmlSerialisers.Composite(type, _modelBehaviour), Logger)
            };
        }
    }
}
