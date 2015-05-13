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
        public static ModelDescriptor Load(TypeSystem typeSystem, Stream stream, string modelName)
        {
            XmlSchema schema = XmlSchema.Read(stream, null);

            var set = new XmlSchemaSet();
            set.Add(schema);
            set.Compile();

            return Load(typeSystem, schema, modelName);
        }

        public static ModelDescriptor Load(TypeSystem typeSystem, string path, string modelName)
        {
            using (var reader = new FileStream(path, FileMode.Open))
            {
                return Load(typeSystem, reader, modelName);
            }
        }

        public static ModelDescriptor Load(TypeSystem typeSystem, XmlSchema schema, string modelName)
        {
            var modelDescriptor = new ModelDescriptor(modelName, modelName);

            foreach (DictionaryEntry item in schema.SchemaTypes)
            {
                var simpleType = item.Value as XmlSchemaSimpleType;
                if(simpleType != null)
                {
                    processSimpleType(modelDescriptor.RootNamespace, simpleType);
                }
                else
                {
                    var complexType = item.Value as XmlSchemaComplexType;
                    if (complexType != null)
                    {
                        processCompositeType(modelDescriptor.RootNamespace, complexType, string.Empty);
                    }
                }
            }

            foreach (DictionaryEntry item in schema.Elements)
            {
                var element = item.Value as XmlSchemaElement;
                if (element != null)
                {
                    var complexType = element.ElementSchemaType as XmlSchemaComplexType;
                    if (complexType != null && complexType.Name == null)
                    {
                        processCompositeType(modelDescriptor.RootNamespace, complexType, element.Name);
                    }
                }
            }
            return modelDescriptor;
        }

        static int id = 3000;
        private static int GetId(string typeName)
        {//|TODO
            return id++;
        }

        private static void processSimpleType(NamespaceDescriptor namespaceDescriptor, XmlSchemaSimpleType simpleType)
        {
            var descriptor = namespaceDescriptor.CreatePrimitiveType(simpleType.Name);
        }

        private static void processCompositeType(NamespaceDescriptor namespaceDescriptor, XmlSchemaComplexType complexType, string typeHint)
        {
            var descriptor = namespaceDescriptor.CreateCompositeType(ConvertSchemaType(complexType, typeHint));
            
            foreach (DictionaryEntry entry in complexType.AttributeUses)
            {
                XmlSchemaAttribute attribute =(XmlSchemaAttribute)entry.Value;
                AddField(descriptor, attribute, false);
            }

            AddField(descriptor, complexType.ContentTypeParticle, false);

            var contentModel = complexType.ContentModel as XmlSchemaSimpleContent;
            if(contentModel != null)
            {
                var extension = contentModel.Content as XmlSchemaSimpleContentExtension;
                if(extension != null)
                {
                    descriptor.AddField("Content", ConvertSchemaType(extension.BaseTypeName.Name), new Dictionary<string, object>{{"isContent", true}});
                }

            }
        }

        private static IDictionary<string, string> xsdTypeMapping = new Dictionary<string, string> { 
                //TODO TypeCodes
                { "String", "string" },
                { "Int", "int" },
                { "Boolean", "bool" },
                { "Id", "string" },
                { "Idref", "string" },
                { "Date", "string" },
                { "Integer", "int" },
                { "Decimal", "int" },
 
                //XSD types
 
                { "boolean", "bool" },
               
                { "date", "string" },
                { "dateTime", "string" },
                { "duration", "string" },
                { "gDay", "string" },
                { "gMonth", "string" },
                { "gMonthDay", "string" },
                { "gYear", "string" },
                { "gYearMonth", "string" },
                { "time", "string" },
 
                { "ENTITIES", "string" },
                { "ENTITY", "string" },
                { "ID", "string" },
                { "IDREF", "string" },
                //{ "IDREFS", "string" },
                { "language", "string" },
                { "Name", "string" },
                { "NCName", "string" },
                { "NMTOKEN", "string" },
                //{ "NMTOKENS", "string" },
                { "normalizedString", "string" },
                { "QName", "string" },
                { "token", "string" },
 
                { "anyURI", "string" },
               
 
                { "byte", "int" },
                { "decimal", "int" },
                { "int", "int" },
                { "integer", "int" },
                { "long", "int" },
                { "negativeInteger", "int" },
                { "nonNegativeInteger", "int" },
                { "positiveInteger", "int" },
                { "short", "int" },
                { "unsignedLong", "int" },
                { "unsignedInt", "int" },
                { "unsignedShort", "int" },
                { "unsignedByte", "int" }
                };

        private static string ConvertSchemaType(string typeCode)
        {
            string mappedType;
            if (!xsdTypeMapping.TryGetValue(typeCode, out mappedType))
                return typeCode;
            return mappedType;
        }

        private static string ConvertSchemaType(XmlSchemaSimpleType simpleType)
        {
            //TODO proper conversion
            string typeCode = simpleType.TypeCode.ToString();
            return ConvertSchemaType(typeCode);
        }


        private static string ConvertSchemaType(XmlSchemaType schemaType, string hint)
        {
            var simpleType = schemaType as XmlSchemaSimpleType;
            if (simpleType != null)
                return ConvertSchemaType(simpleType);

            var complexType = schemaType as XmlSchemaComplexType;
            if (complexType != null)
            {
                if (complexType.Name == null)
                {
                    return hint;// +"_" + complexType.LineNumber + "_" + complexType.LinePosition;
                }
            }

            return schemaType.Name;
        }

        private static void AddField(CompositeTypeDescriptor descriptor, XmlSchemaObject item, bool isOptional)
        {
            var att = item as XmlSchemaAttribute;
            if (att != null)
            {
                descriptor.AddField(att.Name, ConvertSchemaType(att.AttributeSchemaType), new Dictionary<string, object> {{ "isAttribute", true}});
            }
            else
            {
                var element = item as XmlSchemaElement;
                if (element != null)
                {
                    string type = element.Name != null
                        ? ConvertSchemaType(element.SchemaTypeName.Name)//TODO use FQN
                        : ConvertSchemaType(element.ElementSchemaType, element.RefName.Name);

                    if(string.IsNullOrEmpty(type))
                    {
                        var elementType = element.ElementSchemaType as XmlSchemaComplexType;
                        if(elementType != null)
                        {
                            if(elementType.ContentModel == null && elementType.Attributes.Count==0)
                            {
                                //empty complex type
                                type = element.Name + "_EmptyComplex";
                                if(!descriptor.Namespace.Types.Any(_ => _.Name == type))
                                    descriptor.Namespace.CreateCompositeType(type);
                            }
                        }
                    }

                    FieldDescriptor field;
                    //TODO optimise
                    if(element.MaxOccurs == 1)
                    {
                        if (element.Name != null)
                        {
                            field = descriptor.AddField(element.Name, type);
                        }
                        else
                        {
                            field = descriptor.AddField(element.RefName.Name, type);
                        }
                    }
                    else
                    {
                        if (element.Name != null)
                        {
                            field = descriptor.AddCollectionField(element.Name, type);
                        }
                        else
                        {
                            field = descriptor.AddCollectionField(element.RefName.Name, type);
                        }
                    }

                    if (isOptional || element.MinOccurs == 0)
                        field.AttachedData.Add("isOptional", true);

                }
                else
                {
                    var choice = item as XmlSchemaGroupBase;
                    if (choice != null)
                    {
                        AddFields(descriptor, choice.Items, true);
                    }
                    else
                    {
                        var groupRef = item as XmlSchemaGroupRef;
                        if (groupRef != null)
                        {
                            AddFields(descriptor, groupRef.Particle.Items, false);
                        }
                        else
                        {
                            if(item.LineNumber==0 &&item.LinePosition==0)
                            {//empty particle
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

        private static void AddFields(CompositeTypeDescriptor descriptor, XmlSchemaObjectCollection items, bool isChoice)
        {
            foreach (var item in items)
            {
                AddField(descriptor, item, isChoice);
            }
        }
    }
}
