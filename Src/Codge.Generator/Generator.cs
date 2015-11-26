using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using Codge.DataModel;

namespace Codge.Generator
{
    public class Generator
    {
        public GeneratorConfig Config { get; private set; }
        public Context Context { get; private set; }
        public IModelBehaviour ModelBehaviour { get; private set; }
        public ILog Logger { get; private set; }


        public Generator(GeneratorConfig config, ILog logger)
        {
            Config = config;
            Context = new Context(config.BaseDir, logger, config.ModelBehaviour, new OutpuPathMapper());//TODO inject mapper
            Logger = logger;
        }

        public void Generate(Model model)
        {
            Logger.Info(m => m("Starting generation for model baseDir=[{0}]", Config.BaseDir));
            foreach (var task in Config.TaskFactory.CreateTasksForModel(model, Config.ModelBehaviour))
            {
                if (task.IsApplicable())
                {
                    task.Execute(Context);
                }
            }

            ProcessNamespace(model.Namespace);
        }


        void ProcessNamespace(Namespace ns)
        {

            foreach (var task in Config.TaskFactory.CreateTasksForNamespace(ns, Config.ModelBehaviour))
            {
                if (task.IsApplicable())
                {
                    task.Execute(Context);
                }
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

        void ProcessType(TypeBase type)
        {

            foreach (var task in Config.TaskFactory.CreateTasksForType(type, Config.ModelBehaviour))
            {
                if (task.IsApplicable())
                {
                    task.Execute(Context);
                }
            }
        }

        private string GetAbsolutePath(string relativePath)
        {
            return Path.Combine(Config.BaseDir, relativePath);
        }
                        

    }
}
