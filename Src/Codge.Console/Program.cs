﻿using Codge.DataModel;
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
        [Option('m', "model", Required = true, Separator = ',', HelpText = "Paths to model files, comma separated (xml or xsd)")]
        public IReadOnlyCollection<string> Model { get; set; }

        [Option('n', "modelName", Required = false, HelpText = "Name of the model.", Default = "TODO")]
        public string ModelName { get; set; }

        [Option('o', "outputDir", Required = false, HelpText = "Path to a output directory.", Default = @"../../../Generated/CS")]
        public string OutputDir { get; set; }
    }

    public class Program
    {
        //-m "%scriptDir%\Codge.Generator.Test\TestStore\XsdLoader\LoadXsd\Test.xsd" -o "%scriptDir%/Generated/CS_xsd" -n XsdBasedModel
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                    .Enrich.FromLogContext()
                    .MinimumLevel.Debug()
                    .WriteTo.Console()
                    .CreateLogger();

            var loggerFactory = new LoggerFactory().AddSerilog(Log.Logger);
            var logger = loggerFactory.CreateLogger("");

            Parser.Default.ParseArguments<Options>(args)
                .WithParsed(options =>
                    {
                        var model = LoadModel(options.Model, options.ModelName, logger);
                        var config = new GeneratorConfig(options.OutputDir, new BasicModel.Templates.CS.TaskFactory(logger));

                        ProcessTemplates(config, model, logger);
                    });
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
            var model = new ModelLoader(logger).LoadModel(paths, modelName);

            var typeSystem = new TypeSystem();
            var compiler = new ModelProcessor(new LoggerFactory());
            return compiler.Compile(typeSystem, model);
        }
    }
}
