using Codge.DataModel;
using Codge.DataModel.Framework;
using Codge.Generator.Presentations;
using CommandLine;
using Microsoft.Extensions.Logging;
using Serilog;
using System.Collections.Generic;
using System.Linq;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace Codge.Generator.Console
{
    class Options
    {
        [OptionList('m', "model", Required = true, Separator = ',', HelpText = "Paths to model files, comma separated (xml or xsd)")]
        public IReadOnlyCollection<string> Model { get; set; }

        [Option('n', "modelName", Required = false, HelpText = "Name of the model.", DefaultValue = "TODO")]
        public string ModelName { get; set; }

        [Option('o', "outputDir", Required = false, HelpText = "Path to a output directory.", DefaultValue = @"../../../Generated/CS")]
        public string OutputDir { get; set; }
    }

    class Program
    {
        //-m "%scriptDir%\Codge.Generator.Test\TestStore\XsdLoader\LoadXsd\Test.xsd" -o "%scriptDir%/Generated/CS_xsd" -n XsdBasedModel
        static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                    .Enrich.FromLogContext()
                    .MinimumLevel.Debug()
                    .WriteTo.Console()
                    .CreateLogger();

            var loggerFactory = new LoggerFactory().AddSerilog(Log.Logger);
            var logger = loggerFactory.CreateLogger("");

            var options = new Options();
            if (Parser.Default.ParseArgumentsStrict(args, options))
            {
                var model = LoadModel(options.Model, options.ModelName, logger);

                ProcessTemplates(LoadConfig(options.OutputDir, logger), model, logger);
            }
        }

        static void ProcessTemplates(GeneratorConfig config, Model model, ILogger logger)
        {
            var generator = new Generator(config, logger);
            generator.Generate(model);

            logger.LogInformation("--------------------");
            logger.LogInformation("Files evaluated: " + (generator.Context.Tracker.FilesUpdated.Count() + generator.Context.Tracker.FilesSkipped.Count()));
            logger.LogInformation("Files updated: " + generator.Context.Tracker.FilesUpdated.Count());
            logger.LogInformation("--------------------");
        }


        static Model LoadModel(IReadOnlyCollection<string> paths, string modelName, ILogger logger)
        {
            logger.LogInformation("Loading model [{0}]", string.Join(", ", paths));

            var model = new ModelLoader(logger).LoadModel(paths, modelName);

            var typeSystem = new TypeSystem();
            var compiler = new ModelProcessor(new LoggerFactory());
            return compiler.Compile(typeSystem, model);
        }


        static GeneratorConfig LoadConfig(string path, ILogger logger)
        {
            var config = new GeneratorConfig(path, new BasicModel.Templates.CS.TaskFactory(logger));

            //config.AddTypeTask(new OutputTask<TypeDescriptor>(new TypeTask(CreateTaskInput(@"D:\work\2012\ConsoleApplication1\Composite.stg"))));
            //config.AddTypeTask(new OutputTask<TypeDescriptor>(new TypeTask(CreateTaskInput(@"D:\work\2012\ConsoleApplication1\Primitive.stg"))));

            return config;

        }
    }
}
