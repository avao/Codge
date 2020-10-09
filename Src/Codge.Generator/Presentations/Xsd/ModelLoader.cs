using Codge.DataModel.Descriptors;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Schema;

namespace Codge.Generator.Presentations.Xsd
{
    public class ModelLoader
    {
        public static ModelDescriptor Load(XmlSchema schema, string modelName)
        {
            if (!schema.IsCompiled)
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
                    ProcessSimpleType(modelDescriptor.RootNamespace, simpleType);
                }
                else
                {
                    var complexType = item.Value as XmlSchemaComplexType;
                    if (complexType != null)
                    {
                        ProcessCompositeType(modelDescriptor.RootNamespace, complexType, string.Empty);
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
                        ProcessCompositeType(modelDescriptor.RootNamespace, complexType, element.Name);
                    }
                }
            }
            return modelDescriptor;
        }

        private static void ProcessSimpleType(NamespaceDescriptor namespaceDescriptor, XmlSchemaSimpleType simpleType)
        {
            var facets = simpleType.GetEnumerationFacets();
            if (facets.Any())
            {
                var descriptor = namespaceDescriptor.CreateEnumerationType(simpleType.Name);
                foreach (var facet in facets)
                {
                    if (!descriptor.Items.Any(_ => _.Name == facet.Value))
                        descriptor.AddItem(facet.Value);
                }
            }
            else
            {
                var descriptor = namespaceDescriptor.CreatePrimitiveType(simpleType.Name);
            }
        }

        private static void ProcessCompositeType(NamespaceDescriptor namespaceDescriptor, XmlSchemaComplexType complexType, string typeHint)
        {
            var descriptor = complexType.BaseXmlSchemaType != null
                ? namespaceDescriptor.CreateCompositeType(ConvertSchemaType(complexType, typeHint), complexType.BaseXmlSchemaType.Name) //TODO namespace
                : namespaceDescriptor.CreateCompositeType(ConvertSchemaType(complexType, typeHint));

            ProcessItems(descriptor, complexType.Attributes);
                        
            switch (complexType.ContentModel)
            {
                case XmlSchemaSimpleContent simpleContentModel:
                    switch (simpleContentModel.Content)
                    {
                        case XmlSchemaSimpleContentExtension extension:
                            ProcessItems(descriptor, extension.Attributes);
                            descriptor.AddField("Content", ConvertSchemaType(extension.BaseTypeName), false, new Dictionary<string, object> { { "isContent", true } });
                            break;
                        case XmlSchemaSimpleContentRestriction restriction:
                            descriptor.AddField("Content", ConvertSchemaType(restriction.BaseTypeName), false, new Dictionary<string, object> { { "isContent", true } });
                            break;
                        default:
                            throw new NotSupportedException("Not supported simple content model ");
                    }
                    break;
                case XmlSchemaComplexContent complexContentModel:
                    switch (complexContentModel.Content)
                    {
                        case XmlSchemaComplexContentExtension extension:
                            ProcessItems(descriptor, extension.Attributes);
                            if (extension.Particle is XmlSchemaSequence sequence)
                            {
                                ProcessItems(descriptor, sequence.Items);
                            }
                            break;
                        case XmlSchemaComplexContentRestriction restriction:
                            //TODO restriction
                            break;
                    }
                    break;
                case null:
                    AddField(descriptor, complexType.ContentTypeParticle, false);
                    break;
            }
        }

        private static void ProcessItems(CompositeTypeDescriptor descriptor, XmlSchemaObjectCollection attributes)
        {
            foreach (var attribute in attributes)
            {
                AddField(descriptor, attribute, false);
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
                { "anySimpleType", "string" },

                { "base64Binary", "string" },
                { "hexBinary", "string" },

                { "byte", "int" },
                { "decimal", "decimal" },
                { "float", "double" },
                { "double", "double" },

                { "int", "int" },
                { "integer", "long" },
                { "long", "long" },
                { "negativeInteger", "long" },
                { "nonNegativeInteger", "long" },
                { "positiveInteger", "long" },
                { "nonPositiveInteger", "long" },
                { "short", "int" },
                { "unsignedLong", "long" },
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
            return schemaType switch
            {
                XmlSchemaSimpleType simpleType
                    => ConvertSchemaType(simpleType.QualifiedName),

                XmlSchemaComplexType complexType when complexType.Name == null
                    => hint,

                _
                    => schemaType.Name
            };
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
            if (item is XmlSchemaAttribute att)
            {
                var qualifiedName = att.AttributeSchemaType.QualifiedName.IsEmpty
                    ? att.AttributeSchemaType.BaseXmlSchemaType.QualifiedName
                    : att.AttributeSchemaType.QualifiedName;

                descriptor.AddField(att.Name ?? att.RefName.Name, ConvertSchemaType(qualifiedName), false, new Dictionary<string, object> { { "isAttribute", true } });
            }
            else
            {
                var element = item as XmlSchemaElement;
                if (element != null)
                {
                    string type = GetTypeForAnElement(element);

                    var elementType = element.ElementSchemaType as XmlSchemaComplexType;
                    if (string.IsNullOrEmpty(type))
                    {
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
                                ProcessCompositeType(descriptor.Namespace, elementType, element.Name);
                                type = element.Name;
                            }
                        }
                    }

                    bool isCollection = false;
                    if (elementType != null)
                    {
                        if (elementType.ContentTypeParticle.MaxOccurs > 1)
                        {
                            isCollection = true;
                        }
                    }

                    string fieldName = element.Name != null ? element.Name : element.RefName.Name;
                    var field = descriptor.GetField(fieldName);
                    if (field == null)
                    {//TODO what if any of the properties are different?
                        field = descriptor.AddField(fieldName, type, element.MaxOccurs > 1 || isCollection);

                        if (isOptional || element.MinOccurs == 0)
                            field.AttachedData.Add("isOptional", true);
                    }
                }
                else
                {
                    var groupBase = item as XmlSchemaGroupBase;
                    if (groupBase != null)
                    {
                        AddFields(descriptor, groupBase.Items, isOptional || groupBase is XmlSchemaChoice || groupBase.MinOccurs == 0);
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
                    else if (item is XmlSchemaAny)
                    {//TODO skipped for now, should it be a field created?
                        return;
                    }
                    else
                    {
                        throw new NotSupportedException(string.Format("Not supported item line:{0}, position:{1}", item.LineNumber, item.LinePosition));
                    }
                }
            }
        }

        private static void AddFields(CompositeTypeDescriptor descriptor, XmlSchemaObjectCollection items, bool isOptional)
        {
            foreach (var item in items)
            {
                AddField(descriptor, item, isOptional);
            }
        }
    }
}
