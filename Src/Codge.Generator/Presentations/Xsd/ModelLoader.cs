﻿using Codge.DataModel.Descriptors;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Schema;

using static Codge.Generator.Presentations.AttachedDataExtensions;

namespace Codge.Generator.Presentations.Xsd
{
    public class ModelLoader
    {
        private class ProcessingContext
        {
            public IReadOnlyCollection<XmlSchema> Schemas { get; }
            public string ModelName { get; }

            public ModelDescriptor ModelDescriptor { get; }

            public ProcessingContext(string modelName, IReadOnlyCollection<XmlSchema> schemas, ModelDescriptor modelDescriptor)
            {
                ModelName = modelName;
                Schemas = schemas;
                ModelDescriptor = modelDescriptor;
            }
        }

        public static ModelDescriptor Load(IReadOnlyCollection<XmlSchema> schemas, string modelName)
        {
            var set = new XmlSchemaSet();
            foreach (var schema in schemas)
            {
                set.Add(schema);
            }

            set.Compile();

            var modelDescriptor = new ModelDescriptor(modelName, modelName);

            var context = new ProcessingContext(modelName, schemas, modelDescriptor);

            foreach (XmlSchemaType schemaType in set.GlobalTypes.Values
                .Cast<XmlSchemaType>()
                .Where(schemaType => schemaType.Name != null))
            {
                switch (schemaType)
                {
                    case XmlSchemaSimpleType simpleType:
                        ProcessSimpleType(modelDescriptor.RootNamespace, simpleType);
                        break;
                    case XmlSchemaComplexType complexType:
                        ProcessCompositeType(modelDescriptor.RootNamespace, complexType, string.Empty, context);
                        break;
                }
            }

            foreach (DictionaryEntry item in set.GlobalElements)
            {
                var element = item.Value as XmlSchemaElement;
                if (element != null)
                {
                    var complexType = element.ElementSchemaType as XmlSchemaComplexType;
                    if (complexType != null && complexType.Name == null)
                    {
                        ProcessCompositeType(modelDescriptor.RootNamespace, complexType, element.Name, context);
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
                var descriptor = namespaceDescriptor.CreateEnumerationType(simpleType.Name ?? (simpleType.Parent as XmlSchemaElement)?.Name);
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

        private static void ProcessCompositeType(NamespaceDescriptor namespaceDescriptor, XmlSchemaComplexType complexType, string typeHint, ProcessingContext context)
        {
            var descriptor = complexType.BaseXmlSchemaType is XmlSchemaComplexType baseComplexType
                ? namespaceDescriptor.CreateCompositeType(ConvertSchemaType(complexType, typeHint), baseComplexType.Name) //TODO namespace
                : namespaceDescriptor.CreateCompositeType(ConvertSchemaType(complexType, typeHint));

            ProcessItems(descriptor, complexType.Attributes, context);

            switch (complexType.ContentModel)
            {
                case XmlSchemaSimpleContent simpleContentModel:
                    switch (simpleContentModel.Content)
                    {
                        case XmlSchemaSimpleContentExtension extension:
                            ProcessItems(descriptor, extension.Attributes, context);
                            descriptor.AddField("Content", ConvertSchemaType(extension.BaseTypeName), false, NewAttachedData().SetIsContent());
                            break;
                        case XmlSchemaSimpleContentRestriction restriction:
                            descriptor.AddField("Content", ConvertSchemaType(restriction.BaseTypeName), false, NewAttachedData().SetIsContent());
                            break;
                        default:
                            throw new NotSupportedException("Not supported simple content model ");
                    }
                    break;
                case XmlSchemaComplexContent complexContentModel:
                    switch (complexContentModel.Content)
                    {
                        case XmlSchemaComplexContentExtension extension:
                            ProcessItems(descriptor, extension.Attributes, context);
                            if (extension.Particle is XmlSchemaSequence sequence)
                            {
                                ProcessItems(descriptor, sequence.Items, context);
                            }
                            break;
                        case XmlSchemaComplexContentRestriction restriction:
                            //TODO restriction
                            break;
                    }
                    break;
                case null:
                    AddField(descriptor, complexType.ContentTypeParticle, false, context);
                    break;
            }
        }

        private static void ProcessItems(CompositeTypeDescriptor descriptor, XmlSchemaObjectCollection attributes, ProcessingContext context)
        {
            foreach (var attribute in attributes)
            {
                AddField(descriptor, attribute, false, context);
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
            return element.Name != null
                ? ConvertSchemaType(element.ElementSchemaType.QualifiedName)
                : ConvertSchemaType(element.ElementSchemaType, element.RefName.Name);
        }

        private static void AddField(CompositeTypeDescriptor descriptor, XmlSchemaObject item, bool isOptional, ProcessingContext context)
        {
            if (item is XmlSchemaAttribute att)
            {
                var qualifiedName = att.AttributeSchemaType.QualifiedName.IsEmpty
                    ? att.AttributeSchemaType.BaseXmlSchemaType.QualifiedName
                    : att.AttributeSchemaType.QualifiedName;

                var attachedData = NewAttachedData().SetIsAttribute();
                if(att.Use != XmlSchemaUse.Required)
                {
                    attachedData.SetIsOptional();
                }
                descriptor.AddField(att.Name ?? att.RefName.Name, ConvertSchemaType(qualifiedName), false, attachedData);
            }
            else
            {
                var element = item as XmlSchemaElement;
                if (element != null)
                {
                    string type = GetTypeForAnElement(element);

                    if (string.IsNullOrEmpty(type))
                    {
                        switch (element.ElementSchemaType)
                        {
                            case XmlSchemaComplexType complexType:
                                {
                                    if (complexType.IsEmptyType())
                                    {
                                        type = element.Name + "_EmptyComplex"; //TODO should there be the only empty complex?
                                        if (!descriptor.Namespace.Types.Any(_ => _.Name == type))
                                        {
                                            descriptor.Namespace.CreateCompositeType(type);
                                        }
                                    }
                                    else
                                    {
                                        var name = GetTypelessCompositeName(element);
                                        ProcessCompositeType(descriptor.Namespace, complexType, name, context);
                                        type = name;
                                    }

                                    break;
                                }
                            case XmlSchemaSimpleType simpleType:
                                {
                                    ProcessSimpleType(descriptor.Namespace, simpleType);
                                    type = element.Name;

                                    break;
                                }
                        }
                    }

                    bool isComplexCollection = element.ElementSchemaType is XmlSchemaComplexType schemaComplexType
                        && schemaComplexType.ContentTypeParticle.MaxOccurs > 1;

                    string fieldName = element.Name != null ? element.Name : element.RefName.Name;
                    var field = descriptor.GetField(fieldName);
                    if (field == null)
                    {//TODO what if any of the properties are different?
                        field = descriptor.AddField(fieldName, type, element.MaxOccurs > 1 || isComplexCollection);

                        if (isOptional || element.MinOccurs == 0)
                        {
                            field.AttachedData.SetIsOptional();
                        }
                    }
                }
                else
                {
                    if (item is XmlSchemaGroupBase groupBase)
                    {
                        AddFields(descriptor, groupBase.Items, isOptional || groupBase is XmlSchemaChoice || groupBase.MinOccurs == 0, context);
                        return;
                    }

                    if (item is XmlSchemaGroupRef groupRef)
                    {
                        AddFields(descriptor, groupRef.Particle.Items, isOptional || groupRef.Particle is XmlSchemaChoice || groupRef.Particle.MinOccurs == 0, context);
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
                    else if (item is XmlSchemaAttributeGroupRef attributeGroupRef)
                    {
                        ProcessItems(descriptor, ResolveRef(attributeGroupRef, context).Attributes, context);
                        return;
                    }
                    else
                    {
                        throw new NotSupportedException(string.Format("Not supported item line:{0}, position:{1}", item.LineNumber, item.LinePosition));
                    }
                }
            }
        }

        private static XmlSchemaAttributeGroup ResolveRef(XmlSchemaAttributeGroupRef attributeGroupRef, ProcessingContext context)
        {
            foreach (var schema in context.Schemas)
            {
                var attributeGroup = schema.AttributeGroups.Values.Cast<XmlSchemaAttributeGroup>().SingleOrDefault(g => g.QualifiedName == attributeGroupRef.RefName);
                if (attributeGroup != default)
                {
                    return attributeGroup;
                }
            }
            throw new Exception($"Could not resolve ref {attributeGroupRef.RefName}");
        }

        private static void AddFields(CompositeTypeDescriptor descriptor, XmlSchemaObjectCollection items, bool isOptional, ProcessingContext context)
        {
            foreach (var item in items)
            {
                AddField(descriptor, item, isOptional, context);
            }
        }

        private static string GetTypelessCompositeName(XmlSchemaElement element)
        {
            var parentName = element.GetFirstParentWithName();

            return parentName != null
                ? $"{parentName}_{element.Name}"
                : element.Name;
        }
    }
}
