using Codge.DataModel.Descriptors;
using Codge.Generator.Presentations;
using CommandLine;
using Microsoft.Extensions.Logging;
using Serilog;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace Codge.ModelProcessor.Console
{
    class Options
    {
        [Option('i', "input", Required = true, HelpText = "Path to file or directory that need to be processed.")]
        public string Input { get; set; }

        [Option('n', "modelName", Required = true, HelpText = "Name of the model.")]
        public string ModelName { get; set; }

        [Option('o', "output", Required = true, HelpText = "Path to a output file")]
        public string Output { get; set; }

        [Option('m', "merge", Required = false, HelpText = "Processing - merge multiple models into one. Files will be processed in alphabetical order.")]
        public bool Merge { get; set; }
    }

    public class Program
    {
        static ILogger Logger = new LoggerFactory().CreateLogger<Program>();

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
                        ModelDescriptor model;
                        if (Directory.Exists(options.Input))
                        {//directory
                            var files = Directory.EnumerateFiles(options.Input).OrderBy(_ => _).ToList();
                            if (options.Merge)
                            {
                                var processor = new DataModel.Framework.ModelProcessor(loggerFactory);
                                model = processor.MergeToLhs(files.Select(file => LoadModel(new[] { file }, options.ModelName)));
                            }
                            else
                            {
                                model = LoadModel(files, options.ModelName);
                            }
                        }
                        else
                        {//file
                            model = LoadModel(new[] { options.Input }, options.ModelName);
                        }

                        model.Save(options.Output);
                    }
                );
        }

        static ModelDescriptor LoadModel(IReadOnlyCollection<string> paths, string modelName)
        {
            return new ModelLoader(Logger).LoadModel(paths, modelName);
        }
    }
}
