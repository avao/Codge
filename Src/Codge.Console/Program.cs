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


namespace Codge.Generator.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            ILog logger = LogManager.GetLogger("");
            var model = LoadModel(args[0]);//D:\work\CodeGen\TestModel.xml

            ProcessTemplates(LoadConfig(@"../../../Generated/CS"),
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


        static Model LoadModel(string path)
        {
            System.Console.WriteLine("Loading model [" + path + "]");
            var reader = XmlReader.Create(path);
            var model = Codge.Generator.Presentations.Xml.ModelLoader.Load(reader);

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
