using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Codge.Generator;
using Codge.Generator.StringTemplateTasks;
using Codge.Generator.Tasks;
using Common.Logging;
using Codge.DataModel;
using Codge.DataModel.Descriptors;


namespace Codge.Generator.Console
{
    class Program
    {
        //../../../Generated/TestModel.xml  ../../../Generated/CS
        static void Main(string[] args)
        {
            ILog logger = LogManager.GetLogger("");
            string modelPath = args[0];
            string outputDir = @"../../../Generated/CS";
            if(args.Length >1)//TODO use console args for argument parsing
                outputDir = args[1];
            
            var modelName = "TODO";
            if (args.Length > 2)
                modelName = args[2];


            var model = LoadModel(modelPath, modelName);

            ProcessTemplates(LoadConfig(outputDir),
                             new BasicModel.Templates.CS.TaskFactory(logger),
                             model,
                             logger);


          /*  ProcessTemplates(LoadConfig(@"../../../Generated/CPP"),
                             new BasicModel.Templates.CPP.TaskFactory(logger),
                             model,
                             logger);*/
            
        }

        static void ProcessTemplates(Config config, ITaskFactory taskFactory, Model model, ILog logger)
        {
            var generator = new Codge.Generator.Generator(config, taskFactory, logger);
            generator.Generate(model);
            
            logger.Info("--------------------");
            logger.Info("Files evaluated: " + (generator.Context.Tracker.FilesUpdated.Count() + generator.Context.Tracker.FilesSkipped.Count()));
            logger.Info("Files updated: " + generator.Context.Tracker.FilesUpdated.Count());
            logger.Info("--------------------");
        }


        static Model LoadModel(string path, string modelName)
        {
            System.Console.WriteLine("Loading model [" + path + "]");

            ModelDescriptor model;
            if(path.ToLower().EndsWith(".xsd"))
            {//loader type selection
                var schema = Codge.Generator.Presentations.Xsd.SchemaLoader.Load(path);
                model = Codge.Generator.Presentations.Xsd.ModelLoader.Load(schema, modelName);
            }
            else
            {
                var reader = XmlReader.Create(path);
                model = Codge.Generator.Presentations.Xml.ModelLoader.Load(reader);
            }

            var typeSystem = new TypeSystem();
            var compiler = new ModelCompiler();
            return compiler.Compile(typeSystem, model);
        }


        static Config LoadConfig(string path)
        {
            var config = new Config(path);
                        
            //config.AddTypeTask(new OutputTask<TypeDescriptor>(new TypeTask(CreateTaskInput(@"D:\work\2012\ConsoleApplication1\Composite.stg"))));
            //config.AddTypeTask(new OutputTask<TypeDescriptor>(new TypeTask(CreateTaskInput(@"D:\work\2012\ConsoleApplication1\Primitive.stg"))));

            return config;

        }
    }
}
