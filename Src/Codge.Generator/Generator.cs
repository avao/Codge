using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using Codge.DataModel;
using Codge.Generator.Common;
using Codge.Models.Common;

namespace Codge.Generator
{
    public class Generator
    {
        public GeneratorConfig Config { get; private set; }
        public Context Context { get; private set; }
        public ILog Logger { get; private set; }


        public Generator(GeneratorConfig config, ILog logger)
        {
            Config = config;
            Context = new Context(config.BaseDir, logger, new OutputPathMapper());//TODO inject tracker, mapper
            Logger = logger;
        }

        public void Generate(Model model)
        {
            Logger.Info(m => m("Starting generation for model baseDir=[{0}]", Config.BaseDir));
            ProcessNamespace(model.Namespace);
         
            foreach (var task in Config.TaskFactory.CreateTasksForModel(model))
            {
                task.Execute(Context);
            }
        }


        void ProcessNamespace(Namespace ns)
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

        void ProcessType(TypeBase type)
        {

            foreach (var task in Config.TaskFactory.CreateTasksForType(type))
            {
                task.Execute(Context);
            }
        }

        private string GetAbsolutePath(string relativePath)
        {
            return Path.Combine(Config.BaseDir, relativePath);
        }
                        

    }
}
