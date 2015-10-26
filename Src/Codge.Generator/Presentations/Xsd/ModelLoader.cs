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
        public static ModelDescriptor Load(XmlSchema schema, string modelName)
        {
            if(!schema.IsCompiled)
            {
                var set = new XmlSchemaSet();
                set.Add(schema);
                set.Compile();
            }

            var modelDescriptor = new ModelDescriptor(modelName, modelName);

            foreach (DictionaryEntry item in schema.SchemaTypes)
            {
                var simpleType = item.Value as XmlSchemaSimpleType;
                if (simpleType != null)
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

        private static void processSimpleType(NamespaceDescriptor namespaceDescriptor, XmlSchemaSimpleType simpleType)
        {
            var facets = simpleType.GetEnumerationFacets();
            if (facets.Any())
            {
                var descriptor = namespaceDescriptor.CreateEnumerationType(simpleType.Name);
                foreach (var facet in facets)
                {
                    if (!descriptor.Items.Any(_ => _.Name==facet.Value))
                        descriptor.AddItem(facet.Value);
                }
            }
            else
            {
                var descriptor = namespaceDescriptor.CreatePrimitiveType(simpleType.Name);
            }
        }

        private static void processCompositeType(NamespaceDescriptor namespaceDescriptor, XmlSchemaComplexType complexType, string typeHint)
        {
            var descriptor = namespaceDescriptor.CreateCompositeType(ConvertSchemaType(complexType, typeHint));

            foreach (DictionaryEntry entry in complexType.AttributeUses)
            {
                XmlSchemaAttribute attribute = (XmlSchemaAttribute)entry.Value;
                AddField(descriptor, attribute, false);
            }

            AddField(descriptor, complexType.ContentTypeParticle, false);

            if (complexType.ContentModel == null)
                return;

            var simpleContentModel = complexType.ContentModel as XmlSchemaSimpleContent;
            if (simpleContentModel != null)
            {
                var extension = simpleContentModel.Content as XmlSchemaSimpleContentExtension;
                if (extension != null)
                {
                    descriptor.AddField("Content", ConvertSchemaType(extension.BaseTypeName), false, new Dictionary<string, object> { { "isContent", true } });
                }
                else
                {
                    var restriction = simpleContentModel.Content as XmlSchemaSimpleContentRestriction;
                    if (restriction != null)
                    {
                        descriptor.AddField("Content", ConvertSchemaType(extension.BaseTypeName), false, new Dictionary<string, object> { { "isContent", true } });
                    }
                    else
                    {
                        throw new NotSupportedException("Not supported simple content model ");
                    }
                }
            }
            else
            {
                var complexContentModel = complexType.ContentModel as XmlSchemaComplexContent;
                if (complexContentModel != null)
                {
                    var extension = complexContentModel.Content as XmlSchemaComplexContentExtension;
                    if (extension != null)
                    {
                        /*foreach (var item in extension.Attributes)
                        {
                            XmlSchemaAttribute attribute = (XmlSchemaAttribute)item;
                            AddField(descriptor, attribute, false);
                        }*/
                    }

                    //TODO restriction
                }
            }
        }

        private static IDictionary<string, string> xsdTypeMappingFQN = new Dictionary<string, string> { 
                { "boolean", "bool" },
               
                { "string", "string" },

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
                { "IDREFS", "string" },
                { "language", "string" },
                { "Name", "string" },
                { "NCName", "string" },
                { "NMTOKEN", "string" },
                { "NMTOKENS", "string" },
                { "normalizedString", "string" },
                { "QName", "string" },
                { "token", "string" },
 
                { "anyURI", "string" },
               
                { "base64Binary", "string" },
                { "hexBinary", "string" },
 
                { "byte", "int" },
                { "decimal", "decimal" },
                { "float", "double" },
                { "double", "double" },

                { "int", "int" },
                { "integer", "int" },
                { "long", "int" },
                { "negativeInteger", "int" },
                { "nonNegativeInteger", "int" },
                { "positiveInteger", "int" },
                { "nonPositiveInteger", "int" },
                { "short", "int" },
                { "unsignedLong", "int" },
                { "unsignedInt", "int" },
                { "unsignedShort", "int" },
                { "unsignedByte", "int" }
        };

        private static string ConvertSchemaType(XmlQualifiedName name)
        {
            string mappedType;
            if (name.Namespace == "http://www.w3.org/2001/XMLSchema")
            {
                if (xsdTypeMappingFQN.TryGetValue(name.Name, out mappedType))
                    return mappedType;
                throw new NotSupportedException(string.Format("Type [{0}] [{1}] is not supported. Failed to find mapping.", name.Namespace, name.Name));
            }
            return name.Name;//TODO namespace
        }


        private static string ConvertSchemaType(XmlSchemaType schemaType, string hint)
        {
            var simpleType = schemaType as XmlSchemaSimpleType;
            if (simpleType != null)
                return ConvertSchemaType(simpleType.QualifiedName);

            var complexType = schemaType as XmlSchemaComplexType;
            if (complexType != null)
            {
                if (complexType.Name == null)
                {
                    return hint;
                }
            }

            return schemaType.Name;
        }


        private static string GetTypeForAnElement(XmlSchemaElement element)
        {
            if (element.Name != null)
            {
                XmlSchemaType type = element.ElementSchemaType;
                var simpleType = type as XmlSchemaSimpleType;
                if (simpleType != null && simpleType.IsEnumeration())
                {
                    return ConvertSchemaType(simpleType.QualifiedName);
                }
                else
                {
                    return ConvertSchemaType(type.QualifiedName);
                }
            }
            else
            {
                return ConvertSchemaType(element.ElementSchemaType, element.RefName.Name);
            }
        }

        private static void AddField(CompositeTypeDescriptor descriptor, XmlSchemaObject item, bool isOptional)
        {
            var att = item as XmlSchemaAttribute;
            if (att != null)
            {
                descriptor.AddField(att.Name, ConvertSchemaType(att.AttributeSchemaType.QualifiedName), false, new Dictionary<string, object> { { "isAttribute", true } });
            }
            else
            {
                var element = item as XmlSchemaElement;
                if (element != null)
                {
                    string type = GetTypeForAnElement(element);
                    if (string.IsNullOrEmpty(type))
                    {
                        var elementType = element.ElementSchemaType as XmlSchemaComplexType;
                        if (elementType != null)
                        {
                            if (elementType.IsEmptyType())
                            {
                                type = element.Name + "_EmptyComplex"; //TODO should there be the only empty complex?
                                if (!descriptor.Namespace.Types.Any(_ => _.Name == type))
                                    descriptor.Namespace.CreateCompositeType(type);
                            }
                            else
                            {
                                processCompositeType(descriptor.Namespace, elementType, element.Name);
                                type = element.Name;
                            }
                        }
                    }

                    string fieldName = element.Name != null ? element.Name : element.RefName.Name;
                    var field = descriptor.GetField(fieldName);
                    if (field == null)
                    {//TODO what if any of the properties are different?
                        field = descriptor.AddField(fieldName, type, element.MaxOccurs > 1);

                        if (isOptional || element.MinOccurs == 0)
                            field.AttachedData.Add("isOptional", true);
                    }
                }
                else
                {
                    var groupBase = item as XmlSchemaGroupBase;
                    if (groupBase != null)
                    {
                        AddFields(descriptor, groupBase.Items, isOptional || groupBase is XmlSchemaChoice);
                        return;
                    }

                    var groupRef = item as XmlSchemaGroupRef;
                    if (groupRef != null)
                    {
                        AddFields(descriptor, groupRef.Particle.Items, false);
                        return;
                    }

                    if (item.LineNumber == 0 && item.LinePosition == 0)
                    {//empty particle
                        return;
                    }
                    else
                    {
                        throw new NotSupportedException();
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
