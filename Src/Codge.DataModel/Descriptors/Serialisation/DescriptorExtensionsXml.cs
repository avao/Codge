using System;
using System.Xml;

namespace Codge.DataModel.Descriptors.Serialisation
{
    public static class DescriptorExtensionsXml
    {
        public static void ToXml(this ModelDescriptor descriptor, XmlWriter writer)
        {
            writer.WriteStartElement("Model", "http://codge/Model.xsd");
            writer.WriteAttributeString("name", descriptor.Name);
            descriptor.RootNamespace.ToXml(writer);
            writer.WriteEndElement();
        }

        public static void ToXml(this NamespaceDescriptor descriptor, XmlWriter writer)
        {
            writer.WriteStartElement("Namespace");
            writer.WriteAttributeString("name", descriptor.Name);
            foreach (var type in descriptor.Types)
            {
                switch (type)
                {
                    case CompositeTypeDescriptor composite:
                        composite.ToXml(writer);
                        break;
                    case EnumerationTypeDescriptor enumeration:
                        enumeration.ToXml(writer);
                        break;
                    case PrimitiveTypeDescriptor primitive:
                        primitive.ToXml(writer);
                        break;
                    default:
                        throw new NotSupportedException("Not supported type " + type.GetType().Name);
                }
            }

            foreach (var ns in descriptor.Namespaces)
            {
                ns.ToXml(writer);
            }
            writer.WriteEndElement();
        }

        public static void ToXml(this PrimitiveTypeDescriptor descriptor, XmlWriter writer)
        {
            writer.WriteStartElement("Primitive");
            writer.WriteAttributeString("name", descriptor.Name);
            writer.WriteEndElement();
        }

        public static void ToXml(this EnumerationTypeDescriptor descriptor, XmlWriter writer)
        {
            writer.WriteStartElement("Enumeration");
            writer.WriteAttributeString("name", descriptor.Name);
            foreach (var item in descriptor.Items)
            {
                item.ToXml(writer);
            }
            writer.WriteEndElement();
        }

        public static void ToXml(this ItemDescriptor descriptor, XmlWriter writer)
        {
            writer.WriteStartElement("Item");
            writer.WriteAttributeString("name", descriptor.Name);
            if (descriptor.Value.HasValue)
                writer.WriteAttributeString("value", descriptor.Value.Value.ToString());
            writer.WriteEndElement();
        }

        public static void ToXml(this CompositeTypeDescriptor descriptor, XmlWriter writer)
        {
            writer.WriteStartElement("Composite");
            writer.WriteAttributeString("name", descriptor.Name);
            if (descriptor.BaseTypeName != null)
            {
                writer.WriteAttributeString("baseType", descriptor.BaseTypeName);

            }
            foreach (var field in descriptor.Fields)
            {
                field.ToXml(writer);
            }
            writer.WriteEndElement();
        }

        public static void ToXml(this FieldDescriptor descriptor, XmlWriter writer)
        {
            writer.WriteStartElement("Field");
            writer.WriteAttributeString("name", descriptor.Name);
            writer.WriteAttributeString("type", descriptor.TypeName);
            if (descriptor.IsCollection)
                writer.WriteAttributeString("isCollection", "true");
            if (descriptor.AttachedData.Count > 0)
            {
                writer.WriteStartElement("AttachedData");
                foreach (var kvp in descriptor.AttachedData)
                {
                    writer.WriteStartElement("Item");
                    writer.WriteAttributeString("key", kvp.Key);
                    writer.WriteAttributeString("value", kvp.Value.ToString());
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
            }
            writer.WriteEndElement();
        }
    }
}
