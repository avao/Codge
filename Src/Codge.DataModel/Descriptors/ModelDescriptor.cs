using Codge.DataModel.Descriptors.Serialisation;
using Qart.Core.Validation;
using Qart.Core.Xml;

namespace Codge.DataModel.Descriptors
{
    public class ModelDescriptor
    {
        public string Name { get; }
        public NamespaceDescriptor RootNamespace { get; }

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
