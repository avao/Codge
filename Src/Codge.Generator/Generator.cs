using Codge.DataModel;
using Codge.Generator.Common;
using Codge.Models.Common;
using Microsoft.Extensions.Logging;

namespace Codge.Generator
{
    public class Generator
    {
        public GeneratorConfig Config { get; }
        public Context Context { get; }

        private readonly ILogger _logger;

        public Generator(GeneratorConfig config, ILogger logger)
        {
            Config = config;
            Context = new Context(config.BaseDir, logger, new OutputPathMapper());//TODO inject tracker, mapper
            _logger = logger;
        }

        public void Generate(Model model)
        {
            _logger.LogInformation("Starting generation for model baseDir=[{baseDir}]", Config.BaseDir);
            ProcessNamespace(model.Namespace);

            foreach (var task in Config.TaskFactory.CreateTasksForModel(model))
            {
                task.Execute(Context);
            }
        }


        private void ProcessNamespace(Namespace ns)
        {
            foreach (var task in Config.TaskFactory.CreateTasksForNamespace(ns))
            {
                task.Execute(Context);
            }

            foreach (var typeDescriptor in ns.Types)
            {
                ProcessType(typeDescriptor);
            }

            foreach (var namespaceDescriptor in ns.Namespaces)
            {
                ProcessNamespace(namespaceDescriptor);
            }
        }

        private void ProcessType(TypeBase type)
        {
            foreach (var task in Config.TaskFactory.CreateTasksForType(type))
            {
                task.Execute(Context);
            }
        }
    }
}
