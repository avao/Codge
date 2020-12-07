using Codge.DataModel.Descriptors;
using Codge.Generator.Presentations;
using CommandLine;
using Microsoft.Extensions.Logging;
using Serilog;
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

        [Option('o', "output", Required = true, HelpText = "Path to a output file or directory (depends on input).")]
        public string Output { get; set; }

        [Option('c', "convert", Required = false, HelpText = "Processing - convert to XML representation.")]
        public bool Convert { get; set; }

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
                        if (Directory.Exists(options.Input))
                        {//directory
                            var processor = new DataModel.Framework.ModelProcessor(loggerFactory);
                            var files = Directory.EnumerateFiles(options.Input).OrderBy(_ => _).ToList();
                            if (options.Convert)
                            {
                                files.ForEach(_ => ConvertModel(_, options.ModelName, Path.Combine(options.Output, Path.GetFileNameWithoutExtension(_) + ".xml")));
                            }
                            else if (options.Merge)
                            {
                                var model = processor.MergeToLhs(files.Select(_ => LoadModel(_, options.ModelName)));
                                model.Save(options.Output);
                            }
                        }
                        else
                        {//file
                            if (options.Convert)
                            {
                                ConvertModel(options.Input, options.ModelName, options.Output);
                            }
                        }
                    }
                );
        }

        static void ConvertModel(string path, string modelName, string outputPath)
        {
            var model = LoadModel(path, modelName);
            model.Save(outputPath);
        }

        static ModelDescriptor LoadModel(string path, string modelName)
        {
            System.Console.WriteLine("Loading model [" + path + "]");
            return new ModelLoader(Logger).LoadModel(new[] { path }, modelName);
        }
    }
}
