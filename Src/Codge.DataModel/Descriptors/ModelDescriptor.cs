using Codge.DataModel.Descriptors.Serialisation;
using Qart.Core.Validation;
using Qart.Core.Xml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Codge.DataModel.Descriptors
{
    public class ModelDescriptor
    {
        public string Name{get; private set;}
        public NamespaceDescriptor RootNamespace { get; private set; }

        public ModelDescriptor(string modelName, string rootNamespace)
            : this(modelName, new NamespaceDescriptor(rootNamespace))
        {            
        }

        public ModelDescriptor(string modelName, NamespaceDescriptor rootNamespace)
        {
            Require.DoesNotContain(modelName, ".");

            Name = modelName;
            RootNamespace = rootNamespace;
        }
    }


    public static class ModelDescriptorExtensions
    {
        public static void Save(this ModelDescriptor model, string path)
        {
            XmlWriterUtils.ToXmlFile(path, writer => model.ToXml(writer), true);
        }
    }
}
