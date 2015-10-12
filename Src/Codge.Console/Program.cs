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
using CommandLine;
using Codge.DataModel.Descriptors.Serialisation;


namespace Codge.Generator.Console
{
    class Options
    {
        [Option('m', "model", Required = true, HelpText = "Path to a model file (xml or xsd)")]
        public string Model { get; set; }

        [Option('n', "modelName", Required = false, HelpText = "Name of the model.", DefaultValue="TODO")]
        public string ModelName { get; set; }

        [Option('o', "outputDir", Required = false, HelpText = "Path to a output directory.", DefaultValue = @"../../../Generated/CS")]
        public string OutputDir { get; set; }

        [Option('r', "overrides", Required = false, HelpText = "Path to model descriptor overrides.")]
        public string OverridesPath { get; set; }
    }

    class Program
    {
        //-m "%scriptDir%\Codge.Generator.Test\TestStore\XsdLoader\LoadXsd\Test.xsd" -o "%scriptDir%/Generated/CS_xsd" -n XsdBasedModel
        static void Main(string[] args)
        {
            ILog logger = LogManager.GetLogger("");

            var options = new Options();
            if (CommandLine.Parser.Default.ParseArgumentsStrict(args, options))
            {
                var model = LoadModel(options.Model, options.ModelName, options.OverridesPath);

                ProcessTemplates(LoadConfig(options.OutputDir),
                                 new BasicModel.Templates.CS.TaskFactory(logger),
                                 model,
                                 logger);
            }

            

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


        static Model LoadModel(string path, string modelName, string modelOverridesPath)
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

            if (!string.IsNullOrEmpty(modelOverridesPath))
            {
                ModelDescriptor modelOverrides;
                using (var reader = XmlReader.Create(modelOverridesPath))
                {
                    reader.MoveToContent();
                    modelOverrides = DescriptorXmlReader.Read(reader);
                }

                //TODO apply overrides
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
