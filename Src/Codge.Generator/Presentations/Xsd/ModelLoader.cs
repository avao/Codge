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
        public static DataModel.Model Load(TypeSystem typeSystem, Stream stream, string modelName)
        {
            XmlSchema schema = XmlSchema.Read(stream, null);

            var set = new XmlSchemaSet();
            set.Add(schema);
            set.Compile();

            return Load(typeSystem, schema, modelName);
        }

        public static DataModel.Model Load(TypeSystem typeSystem, string path, string modelName)
        {
            XmlSchema schema;
            using (var reader = new FileStream(path, FileMode.Open))
            {
                return Load(typeSystem, reader, modelName);
            }
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
            AddFields(descriptor, compexType.Attributes);
            if (compexType.Particle!=null)
                AddField(descriptor, compexType.Particle);
        }

        private static IDictionary<string, string> xsdTypeMapping = new Dictionary<string, string> { 
                { "boolean", "bool" }
                };

        private static string ConvertSchemaType(XmlSchemaSimpleType simpleType)
        {
            //TODO proper conversion
            string typeCode = simpleType.TypeCode.ToString().ToLower();
            string mappedType;
            if (!xsdTypeMapping.TryGetValue(typeCode, out mappedType))
                return typeCode;
            return mappedType;
        }

        private static void AddField(CompositeTypeDescriptor descriptor, XmlSchemaObject item)
        {
            var att = item as XmlSchemaAttribute;
            if (att != null)
            {
                descriptor.AddField(att.Name, ConvertSchemaType(att.AttributeSchemaType));
            }
            else
            {
                var element = item as XmlSchemaElement;
                if (element != null)
                {
                    if (element.Name != null)
                    {
                        descriptor.AddField(element.Name, element.SchemaTypeName.Name);
                    }
                    else
                    {
                        descriptor.AddField(element.RefName.Name, element.ElementSchemaType.Name);
                    }
                }
                else
                {
                    var choice = item as XmlSchemaGroupBase;
                    if (choice != null)
                    {
                        AddFields(descriptor, choice.Items);
                    }
                    else
                    {
                        var groupRef = item as XmlSchemaGroupRef;
                        if (groupRef != null)
                        {
                            AddFields(descriptor, groupRef.Particle.Items);
                        }
                        else
                        {
                            throw new NotSupportedException();
                        }
                    }
                }
            }
        }

        private static void AddFields(CompositeTypeDescriptor descriptor, XmlSchemaObjectCollection items)
        {
            foreach (var item in items)
            {
                AddField(descriptor, item);
            }
        }
    }
}
