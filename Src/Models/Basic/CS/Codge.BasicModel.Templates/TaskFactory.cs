using Codge.BasicModel.Templates.CS.Templates;
using Codge.DataModel;
using Codge.Generator.Common;
using Codge.Models.Common;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace Codge.BasicModel.Templates.CS
{
    public class TaskFactory : ITaskFactory
    {
        public ILogger Logger { get; }
        private readonly ModelBehaviour _modelBehaviour;

        private static readonly HashSet<string> _reservedNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase) {
            "abstract", "as",
            "base", "bool", "break", "byte",
            "case", "catch", "char", "checked", "class", "const", "continue",
            "decimal", "default", "delegate", "do", "double",
            "else", "enum", "event", "explicit", "extern",
            "false", "finally", "fixed", "float", "for", "foreach",
            "goto",
            "if", "implicit", "in", "int", "interface", "internal", "is",
            "lock", "long",
            "namespace", "new", "null",
            "object", "operator", "out", "override",
            "params", "private", "protected", "public",
            "readonly", "ref", "return",
            "sbyte", "sealed", "short", "sizeof", "stackalloc", "static", "string", "struct", "switch",
            "this", "throw", "true", "try", "typeof",
            "uint", "ulong", "unchecked", "unsafe", "ushort", "using",
            "virtual",
            "void", "volatile",
            "while" };

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
