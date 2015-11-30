﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Codge.BasicModel.Templates.CS.Templates;
using Codge.Generator;
using Codge.Generator.Tasks;
using Common.Logging;
using Codge.DataModel;

namespace Codge.BasicModel.Templates.CS
{
    public class TaskFactory : ITaskFactory
    {
        public ILog Logger { get; private set; }

        public TaskFactory(ILog logger)
        {
            Logger = logger;
        }

        public IEnumerable<ITask<Model>> CreateTasksForModel(Model model, IModelBehaviour modelBehaviour)
        {
            return new ITask<Model>[] { 
                new OutputTask<Model>(new ProjectUpdater(model), Logger),
                new OutputTask<Model>(new Registrar(model, modelBehaviour), Logger)
            };
        }

        public IEnumerable<ITask<Namespace>> CreateTasksForNamespace(Namespace model, IModelBehaviour modelBehaviour)
        {
            return new ITask<Namespace>[] { };
        }

        public IEnumerable<ITask<TypeBase>> CreateTasksForType(TypeBase type, IModelBehaviour modelBehaviour)
        {
            return new[] {
                new OutputTask<TypeBase>(new Codge.BasicModel.Templates.CS.Templates.Types.Composite(type, modelBehaviour), Logger),
                new OutputTask<TypeBase>(new Codge.BasicModel.Templates.CS.Templates.Types.Primitive(type, modelBehaviour), Logger),
                new OutputTask<TypeBase>(new Codge.BasicModel.Templates.CS.Templates.Types.Enum(type, modelBehaviour), Logger),
                new OutputTask<TypeBase>(new Codge.BasicModel.Templates.CS.Templates.XmlSerialisers.Composite(type, modelBehaviour), Logger)
                //new OutputTask<TypeBase>(new BasicModel.Templates.CS.Templates.PofSerialisers.Composite(type), Logger)
            };
        }
    }
}