using Codge.DataModel;
using Codge.DataModel.Descriptors;
using Codge.DataModel.Descriptors.Serialisation;
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

        [Option('m', "merge", Required = false, HelpText = "Processing - merge multiple models into one.")]
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
                //TODO check merge and convert

                if (Directory.Exists(options.Input))
                {//directory
                    var files = Directory.EnumerateFiles(options.Input).ToList();
                    if (options.Convert)
                    {
                        files.ForEach(_ => ConvertModel(_, options.ModelName, Path.Combine(options.Output, Path.GetFileName(_))));
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

        public class ModelMergeTypeSystemEventHandler
            : NamespaceTracingTypeSystemEventHandler<NamespaceDescriptor>
            , IAtomicNodeEnventHandler<PrimitiveTypeDescriptor>
            , IAtomicNodeEnventHandler<CompositeTypeDescriptor>
            , IAtomicNodeEnventHandler<EnumerationTypeDescriptor>
        {
            public ModelMergeTypeSystemEventHandler(NamespaceDescriptor ns)
                : base(ns, (n, descriptor)=> n.GetOrCreateNamespace(descriptor.Name))
            {
            }


            public void Handle(PrimitiveTypeDescriptor primitive)
            {
                var lhsType = Namespace.Types.FindByName(primitive.Name);
                if(lhsType == null)
                {
                    Namespace.CreatePrimitiveType(primitive.Name);
                }
            }

            public void Handle(CompositeTypeDescriptor composite)
            {
                var lhsType = Namespace.Types.FindByName(composite.Name)as CompositeTypeDescriptor;
                if (lhsType == null)
                {
                    lhsType = Namespace.CreateCompositeType(composite.Name);
                }


                //TODO preserve position of the field
                FieldDescriptor lhsPrevField = null;
                foreach(var field in composite.Fields)
                {
                    var lhsField = lhsType.Fields.FirstOrDefault(_ => _.Name == field.Name);
                    if(lhsField == null)
                    {
                        lhsField = lhsType.AddField(field.Name, field.TypeName, field.IsCollection);
                    }

                    if (lhsField.IsCollection != field.IsCollection)
                        Logger.WarnFormat("different field definitions {0} {1}", lhsField.IsCollection, field.IsCollection);
                    if(lhsField.TypeName != field.TypeName)
                        throw new Exception("different Type");
                        //TODO check type
                }
            }

            public void Handle(EnumerationTypeDescriptor enumeration)
            {
                var lhsType = Namespace.Types.FindByName(enumeration.Name) as EnumerationTypeDescriptor;
                if (lhsType == null)
                {
                    lhsType = Namespace.CreateEnumerationType(enumeration.Name);
                }

                //TODO preserve position?
                foreach(var item in enumeration.Items)
                {
                    var lhsItem = lhsType.Items.FirstOrDefault(_ => _.Name == item.Name);
                    if(lhsItem == null)
                    {
                        lhsType.AddItem(item.Name);//TODO value
                    }
                    else
                    {
                        //TODO check value
                    }
                }
            }
        }

        static ModelDescriptor Merge(ModelDescriptor lhs, ModelDescriptor rhs)
        {
            TypeSystemWalker.Walk(rhs.RootNamespace, new ModelMergeTypeSystemEventHandler(lhs.RootNamespace));
            return lhs;
        }

        static void ConvertModel(string path, string modelName, string outputPath)
        {
            var model = LoadModel(path, modelName);
            model.Save(outputPath);   
        }

        //TODO copy paste
        static ModelDescriptor LoadModel(string path, string modelName)
        {
            System.Console.WriteLine("Loading model [" + path + "]");

            ModelDescriptor model;
            if (path.ToLower().EndsWith(".xsd"))
            {//loader type selection
                var schema = Codge.Generator.Presentations.Xsd.SchemaLoader.Load(path);
                model = Codge.Generator.Presentations.Xsd.ModelLoader.Load(schema, modelName);
            }
            else
            {
                var reader = XmlReader.Create(path);
                model = Codge.Generator.Presentations.Xml.ModelLoader.Load(reader);
            }

            return model;
        }
    }
}
