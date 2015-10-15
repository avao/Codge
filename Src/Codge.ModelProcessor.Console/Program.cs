using Codge.DataModel;
using Codge.DataModel.Descriptors;
using Codge.DataModel.Descriptors.Serialisation;
using Codge.DataModel.Framework;
using Codge.Generator.Presentations;
using CommandLine;
using Common.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

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

    class Program
    {
        static ILog Logger = LogManager.GetLogger("");

        //-m "%scriptDir%\Codge.Generator.Test\TestStore\XsdLoader\LoadXsd\Test.xsd" -o "%scriptDir%/Generated/CS_xsd" -n XsdBasedModel
        static void Main(string[] args)
        {

            var options = new Options();
            if (CommandLine.Parser.Default.ParseArgumentsStrict(args, options))
            {
                if (Directory.Exists(options.Input))
                {//directory
                    var files = Directory.EnumerateFiles(options.Input).OrderBy(_ => _).ToList();
                    if (options.Convert)
                    {
                        files.ForEach(_ => ConvertModel(_, options.ModelName, Path.Combine(options.Output, Path.GetFileNameWithoutExtension(_)+".xml")));
                    }
                    else if (options.Merge)
                    {
                        var model = LoadModel(files.First(), options.ModelName);
                        files.Skip(1).Select(_ => LoadModel(_, options.ModelName)).Aggregate(model, Merge);
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
        }

        
        static ModelDescriptor Merge(ModelDescriptor lhs, ModelDescriptor rhs)
        {
            TypeSystemWalker.Walk(rhs.RootNamespace, new ModelMergeTypeSystemEventHandler(lhs.RootNamespace, Logger));
            return lhs;
        }

        static void ConvertModel(string path, string modelName, string outputPath)
        {
            var model = LoadModel(path, modelName);
            model.Save(outputPath);   
        }

        static ModelDescriptor LoadModel(string path, string modelName)
        {
            System.Console.WriteLine("Loading model [" + path + "]");
            return new ModelLoader(Logger).LoadModel(path, modelName);
        }
    }
}
