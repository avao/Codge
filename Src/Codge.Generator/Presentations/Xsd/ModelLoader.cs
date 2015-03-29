using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
using Codge.DataModel;
using Codge.DataModel.Descriptors;

namespace Codge.Generator.Presentations.Xsd
{
    public class ModelLoader
    {
        public static DataModel.Model Load(TypeSystem typeSystem, string path, string modelName)
        {
            XmlSchema schema;
            using (var reader = new FileStream(path, FileMode.Open))
            {
                schema = XmlSchema.Read(reader, null);
            }

            var set = new XmlSchemaSet();
            set.Add(schema);
            set.Compile();

            return Load(typeSystem, schema, modelName);
        }

        public static DataModel.Model Load(TypeSystem typeSystem, XmlSchema schema, string modelName)
        {
            var modelDescriptor = new ModelDescriptor(modelName, modelName);

            foreach (DictionaryEntry item in schema.SchemaTypes)
            {
                var complexType = item.Value as XmlSchemaComplexType;
                if (complexType != null)
                {
                    processCompositeType(modelDescriptor.RootNamespace, complexType);
                }
            }

            var compiler = new ModelCompiler();
            return compiler.Compile(typeSystem, modelDescriptor);
        }

        static int id = 3000;
        private static int GetId(string typeName)
        {//|TODO
            return id++;
        }

        private static void processCompositeType(NamespaceDescriptor namespaceDescriptor, XmlSchemaComplexType compexType)
        {
            var descriptor = namespaceDescriptor.CreateCompositeType(compexType.Name);

            var sequence = compexType.Particle as XmlSchemaSequence;
            if (sequence != null)
            {
                foreach (var item in sequence.Items)
                {
                    var element = item as XmlSchemaElement;
                    if (element != null)
                    {
                        descriptor.AddField(element.Name, element.SchemaTypeName.Name);
                    }
                    else
                    {
                        var choice = item as XmlSchemaChoice;
                        if (choice != null)
                        {
                            //TODO find base type
                        }
                        else
                        {
                            throw new NotSupportedException();
                        }
                    }
                }
            }
        }
    }
}
